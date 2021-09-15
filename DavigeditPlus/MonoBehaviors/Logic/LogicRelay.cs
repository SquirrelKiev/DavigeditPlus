using UnityEngine;
using UnityEngine.Events;

namespace DavigeditPlus.Logic
{
    class LogicRelay : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField]
        private bool onlyTriggerOnce = false;

        private bool triggered = true;

        [Header("Events")]
        [SerializeField]
        private UnityEvent onTrigger = new UnityEvent();

        public void Trigger()
        {
            if (triggered)
            {
                if (onlyTriggerOnce) triggered = false;
                onTrigger.Invoke();
            }
        }
    }
}
