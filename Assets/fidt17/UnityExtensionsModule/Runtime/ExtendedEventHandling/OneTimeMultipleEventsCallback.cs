using System;
using System.Collections.Generic;
using System.Linq;

namespace fidt17.UnityExtensionsModule.Runtime.ExtendedEventHandling
{
    public class OneTimeMultipleEventsCallback
    {
        public enum ListenMethod
        {
            /// <summary>
            /// Wait for invocation of every event
            /// </summary>
            All = 0,
            
            /// <summary>
            /// Wait for invocation of any event
            /// </summary>
            Any = 1
        }

        private readonly ListenMethod _listenMethod;
        private readonly IReadOnlyList<WaitForEvent> _waitForEvents;
        private readonly Action _callback;
        
        public OneTimeMultipleEventsCallback(IReadOnlyList<WaitForEvent> waitForEvents, Action callback, ListenMethod listenMethod = ListenMethod.All)
        {
            if (waitForEvents == null) throw new ArgumentNullException();
            if (waitForEvents.Count == 0) throw new ArgumentException();
            if (waitForEvents.Any(x => x == null)) throw new ArgumentNullException();
            if (callback == null) throw new ArgumentNullException();
            
            _listenMethod = listenMethod;
            _waitForEvents = waitForEvents;
            foreach (var waitForEvent in _waitForEvents)
            {
                waitForEvent.OnTrigger += HandleTrigger;
            }
            _callback = callback;
        }

        private void HandleTrigger()
        {
            bool executeCallback = _listenMethod == ListenMethod.All
                ? _waitForEvents.All(x => x.HasBeenTriggered)
                : _waitForEvents.Any(x => x.HasBeenTriggered);

            if (!executeCallback) return;
            
            _callback?.Invoke();
            foreach (var waitForEvent in _waitForEvents)
            {
                waitForEvent.OnTrigger -= HandleTrigger;
            }
        }
    }
}