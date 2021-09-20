using UnityEngine;
using UnityEngine.Events;

namespace DavigeditPlus
{
    public class Trigger : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField]
        [Tooltip("Amount of time, in seconds, after OnTrigger has triggered before it can be triggered again. If set to -1, it will never trigger again.")]
        private float delayBeforeReset = 1;
        [SerializeField]
        private Filter.Filter filter;

        [Header("Events")]
        [SerializeField]
        private UnityEvent onTriggerEnter = new UnityEvent();
        [SerializeField]
        private UnityEvent onTriggerExit = new UnityEvent();

        private bool canTriggerEnter = true;
        private bool canTriggerExit = true;

        private void OnTriggerEnter(Collider other)
        {
            if (canTriggerEnter == true)
            {
                if (filter != null && !filter.CheckFilter(other.gameObject))
                    return;
                onTriggerEnter.Invoke();
                canTriggerEnter = false;
                if (delayBeforeReset > 0)
                {
                    canTriggerEnter = false;
                    StartCoroutine(FixedLogic.InvokeFixed(delayBeforeReset, new System.Action(SetCanTriggerEnter)));
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (canTriggerExit == true)
            {
                if (filter != null && !filter.CheckFilter(other.gameObject))
                    return;
                onTriggerExit.Invoke();
                canTriggerExit = false;
                StartCoroutine(FixedLogic.InvokeFixed(delayBeforeReset, new System.Action(SetCanTriggerExit)));
            }
        }

        private void SetCanTriggerEnter()
        { canTriggerEnter = true; }
        private void SetCanTriggerExit()
        { canTriggerExit = true; }
    }
}
