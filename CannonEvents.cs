using UnityEngine;
using MelonLoader;
using UnityEngine.Events;

namespace DavigeditPlus
{
    class CannonEvents : MonoBehaviour
    {
        [SerializeField]
        private UnityEvent onReady = new UnityEvent();
        [SerializeField]
        private UnityEvent onReloading = new UnityEvent();
        [SerializeField]
        private UnityEvent onFiring = new UnityEvent();

        [HideInInspector]
        public GameObject cannonObject;
        [HideInInspector]
        public CannonController cannonController;

        private void Start()
        {
            cannonController = cannonObject.GetComponent<CannonController>();

            MelonLogger.Msg("not null!");
            cannonController.OnReady += CannonController_OnReady;
            cannonController.OnReloading += CannonController_OnReloading;
            cannonController.OnRising += CannonController_OnRising;
        }

        private void CannonController_OnRising()
        {
            MelonLogger.Msg("CannonController_OnRising");
        }

        private void CannonController_OnReloading()
        {
            MelonLogger.Msg("CannonController_OnReloading");
        }

        private void CannonController_OnReady()
        {
            MelonLogger.Msg("CannonController_OnReady");
        }
    }
}
