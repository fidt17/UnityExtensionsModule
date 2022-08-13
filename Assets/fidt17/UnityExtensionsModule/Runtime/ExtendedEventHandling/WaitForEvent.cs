using System;
using UnityEngine;
using UnityEngine.Events;

namespace fidt17.UnityExtensionsModule.Runtime.ExtendedEventHandling
{
    /// <summary>
    /// Event handler that listens for an event invocation only once.
    /// Can be used as yield instruction in Coroutines
    /// </summary>
    public class WaitForEvent : CustomYieldInstruction
    {
        public event Action OnTrigger;
        public bool HasBeenTriggered { get; private set; }

        public override bool keepWaiting => HasBeenTriggered == false;
            
        private readonly Action<WaitForEvent> _unsubscribeAction;

        public WaitForEvent(UnityEvent unityEvent)
        {
            if (unityEvent == null) throw new ArgumentNullException();
            unityEvent.AddListener(OnEvent);
            _unsubscribeAction = _ => unityEvent.RemoveListener(OnEvent);
        }

        public WaitForEvent(Action<WaitForEvent> subscribeAction, Action<WaitForEvent> unsubscribeAction)
        {
            if (subscribeAction == null) throw new ArgumentNullException();
            if (unsubscribeAction == null) throw new ArgumentNullException();
            
            subscribeAction.Invoke(this);
            _unsubscribeAction = unsubscribeAction;
        }
        
        /// <summary>
        /// Use this method to subscribe to an Action event.
        /// e.g. new WaitForEvent(w => w => [action] += w.OnEvent, w => [action] -= w.OnEvent)
        /// </summary>
        public void OnEvent()
        {
            HasBeenTriggered = true;
            Unsubscribe();
            OnTrigger?.Invoke();
        }

        /// <summary>
        /// Unsubscribe from event
        /// </summary>
        public void Unsubscribe()
        {
            _unsubscribeAction?.Invoke(this);
        }
    }
}