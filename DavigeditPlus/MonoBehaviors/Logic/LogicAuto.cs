using UnityEngine;
using UnityEngine.Events;

namespace DavigeditPlus
{
    class LogicAuto : MonoBehaviour
    {
        [Header("Events")]
        [SerializeField]
        private UnityEvent onEnable = new UnityEvent();

        private void OnEnable()
        {
            onEnable.Invoke();
        }
    }
}
