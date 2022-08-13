using System;
using UnityEngine.Events;

namespace fidt17.UnityExtensionsModule.Runtime.ExtendedEventHandling
{
    public class OneTimeEventCallback
    {
        private readonly Action _callback;
        private readonly WaitForEvent _waitForEvent;

        public OneTimeEventCallback(UnityEvent unityEvent, Action callback) : this(new WaitForEvent(unityEvent), callback)
        {
            
        }
        
        public OneTimeEventCallback(WaitForEvent waitForEvent, Action callback)
        {
            if (waitForEvent == null) throw new ArgumentNullException();
            if (callback == null) throw new ArgumentNullException();

            _waitForEvent = waitForEvent;
            _callback = callback;
            
            _waitForEvent.OnTrigger += HandleTrigger;
        }

        private void HandleTrigger()
        {
            _waitForEvent.OnTrigger -= HandleTrigger;
            _callback?.Invoke();
        }
    }
}