using UnityEngine;
using UnityEngine.Events;

namespace DavigeditPlus.Logic
{
    public class LogicRelay : MonoBehaviour
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


        void OnDrawGizmos()
        {
            Gizmos.DrawIcon(transform.position, "DavigeditPlus/Logic/Logic_relay.png", true);
        }
    }
}
