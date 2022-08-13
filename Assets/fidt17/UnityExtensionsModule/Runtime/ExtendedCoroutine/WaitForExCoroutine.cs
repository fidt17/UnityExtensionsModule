using System;
using UnityEngine;

namespace fidt17.UnityExtensionsModule.Runtime.ExtendedCoroutine
{
    public class WaitForExCoroutine : CustomYieldInstruction
    {
        public override bool keepWaiting => _exCoroutine.HasFinished == false;

        private readonly ExCoroutine _exCoroutine;
        
        public WaitForExCoroutine(ExCoroutine exCoroutine)
        {
            if (exCoroutine == null) throw new ArgumentNullException();
            _exCoroutine = exCoroutine;
        }
    }
}