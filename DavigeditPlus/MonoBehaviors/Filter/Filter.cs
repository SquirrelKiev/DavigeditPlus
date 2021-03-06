using System;
using UnityEngine;
using UnityEngine.Events;

namespace DavigeditPlus.Filter
{
    public abstract class Filter : MonoBehaviour
    {
        [Header("Filter")]
        [SerializeField]
        [Tooltip("Reverses the outcome. If filter passed, it fails, if it fails, it passes. Note that onPass will still fire if the filter was supposed to pass, but failed due to this being true. Same with onFail. Hope that makes sense :)")]
        public bool reverseOutcome = false;

        [SerializeField]
        public UnityEvent onPass = new UnityEvent();

        [SerializeField]
        public UnityEvent onFail = new UnityEvent();

        public abstract bool CheckFilter(GameObject filterObject);
    }
}
