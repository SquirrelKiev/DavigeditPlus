using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace DavigeditPlus
{
    public class Trigger : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("Amount of time, in seconds, after OnTrigger has triggered before it can be triggered again. If set to -1, it will never trigger again.")]
        private float delayBeforeReset = 1;
        [SerializeField]
        private Filter.Filter filter;
        [SerializeField]
        private UnityEvent onTrigger = new UnityEvent();
        [SerializeField]
        private UnityEvent onTriggerExit = new UnityEvent();

        private bool canTriggerEnter = true;
        private bool canTriggerExit = true;

        private void OnTriggerEnter(Collider other)
        {
            if (canTriggerEnter == true)
            {
                MelonLoader.MelonLogger.Msg(other.gameObject.name);
                onTrigger.Invoke();
                if (delayBeforeReset < 0)
                {
                    canTriggerEnter = false;
                }
                else
                {
                    canTriggerEnter = false;
                    StartCoroutine(FixedLogic.InvokeFixed(delayBeforeReset, new System.Action(setCanTriggerEnter)));
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if(canTriggerExit == true)
            {
                onTriggerExit.Invoke();
                canTriggerExit = false;
                StartCoroutine(FixedLogic.InvokeFixed(delayBeforeReset, new System.Action(setCanTriggerExit)));
            }
        }

        private void setCanTriggerEnter()
        { canTriggerEnter = true; }
        private void setCanTriggerExit()
        { canTriggerExit = true; }
    }
}
