using UnityEngine;
using UnityEngine.Events;

namespace DavigeditPlus
{
    public class LogicAuto : MonoBehaviour
    {
        [Header("Events")]
        [SerializeField]
        private UnityEvent onEnable = new UnityEvent();

        private void OnEnable()
        {
            onEnable.Invoke();
        }

        void OnDrawGizmos()
        {
            Gizmos.DrawIcon(transform.position, "DavigeditPlus/Logic/Logic_auto.png", true);
        }
    }
}
