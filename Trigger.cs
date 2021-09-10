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
        private UnityEvent onTrigger = new UnityEvent();
        [SerializeField]
        private UnityEvent onTriggerExit = new UnityEvent();

        private bool canTrigger = true;

        private void OnTriggerEnter(Collider other)
        {
            if (canTrigger == true)
            {
                onTrigger.Invoke();
                if (delayBeforeReset <= -1)
                {
                    canTrigger = false;
                }
                else
                {
                    StartCoroutine(Delay(delayBeforeReset));
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            onTriggerExit.Invoke();
        }

        private IEnumerator Delay(float delay)
        {
            canTrigger = false;
            yield return new WaitForSeconds(delay);
            canTrigger = true;
        }
    }
}
