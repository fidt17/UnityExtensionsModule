using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace fidt17.UnityExtensionsModule.Runtime.ExtendedCoroutine
{
    public class ExCoroutine
    {
        public bool IsRunning { get; private set; }
        public bool HasFinished { get; private set; }

        public enum ExecutionOrder
        {
            /// <summary>
            /// Coroutines will be executed one after one
            /// </summary>
            Sequential,
            
            /// <summary>
            /// Coroutines will be started simultaneously 
            /// </summary>
            Parallel
        }
        
        private readonly MonoBehaviour _owner;
        private readonly List<IEnumerator> _enumerators;

        private Coroutine _mainCoroutine;
        private readonly List<ExCoroutine> _subExCoroutines = new List<ExCoroutine>();
        private Action _onStopAction;
        
        public ExCoroutine(MonoBehaviour owner)
        {
            if (owner == null) throw new ArgumentNullException();
            _owner = owner;
            _enumerators = new List<IEnumerator>();
        }

        /// <summary>
        /// Set an <paramref name="action"/> to execute when ExCoroutine stop execution.
        /// </summary>
        public void SetOnStop(Action action)
        {
            if (action == null) throw new ArgumentNullException();
            _onStopAction = action;
        }

        /// <summary>
        /// Start single coroutine
        /// </summary>
        public void Start(IEnumerator enumerator)
        {
            if (IsRunning) Stop();
            _enumerators.Add(enumerator);
            StartExCoroutine(ExecutionOrder.Sequential);
        }

        /// <summary>
        /// Start multiple coroutines with <paramref name="executionOrder"/>
        /// </summary>
        public void Start(IEnumerable<IEnumerator> enumerators, ExecutionOrder executionOrder)
        {
            if (_owner == null || !_owner.gameObject.activeInHierarchy) return;
            
            if (IsRunning) Stop();
            _enumerators.AddRange(enumerators);
            StartExCoroutine(executionOrder);
        }

        /// <summary>
        /// Stops execution of ExCoroutine
        /// </summary>
        public void Stop()
        {
            if (IsRunning == false) return;
            if (_mainCoroutine == null) return; // can happen when ExCoroutine gets started and canceled in the same frame
            
            _onStopAction?.Invoke();
            
            if (_owner != null)
            {
                _owner.StopCoroutine(_mainCoroutine);
                _mainCoroutine = null;

                foreach (var subExCoroutines in _subExCoroutines)
                {
                    subExCoroutines.Stop();
                }
                _subExCoroutines.Clear();
            }

            HasFinished = true;
            IsRunning = false;
            _onStopAction = null;
            _enumerators.Clear();
        }
        
        private void StartExCoroutine(ExecutionOrder executionOrder)
        {
            if (_owner == null)
            {
                Debug.LogWarning("[ExCoroutine:Warning] Cannot start ExCoroutine because owner is missing.");
                return;
            }

            _mainCoroutine = _owner.StartCoroutine(executionOrder == ExecutionOrder.Sequential ? RunSequentially() : RunInParallel());
        }

        private IEnumerator RunSequentially()
        {
            IsRunning = true;
            for (var i = 0; i < _enumerators.Count; i++)
            {
                yield return _enumerators[i];
            }
            Stop();
        }

        private IEnumerator RunInParallel()
        {
            IsRunning = true;
            for (var i = 0; i < _enumerators.Count; i++)
            {
                var subExCoroutine = new ExCoroutine(_owner);
                subExCoroutine.Start(_enumerators[i]);
                _subExCoroutines.Add(subExCoroutine);
            }
            
            for (var i = 0; i < _subExCoroutines.Count; i++)
            {
                yield return _subExCoroutines[i].WaitFor();
            }
            
            Stop();
        }
    }
}