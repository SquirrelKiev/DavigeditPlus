using System;
using UnityEngine;
using UnityEngine.Events;

namespace DavigeditPlus.Filter
{
    class Filter : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("Reverses the outcome. If filter passed, it fails, if it fails, it passes. Hope that makes sense :)")]
        public bool reverseOutcome = false;

        [SerializeField]
        public UnityEvent onPass = new UnityEvent();

        [SerializeField]
        public UnityEvent onFail = new UnityEvent();
    }
}
