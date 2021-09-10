using System;
using System.Reflection;
using UnityEngine;
using UnityEngine.Events;

namespace DavigeditPlus
{
    class CannonSettings : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField]
        [Min(0)]
        public float reloadDuration = 15f;

        [Header("Events")]
        [SerializeField]
        private UnityEvent onFiring = new UnityEvent();
        [SerializeField]
        private UnityEvent onFinishedReloading = new UnityEvent();
        [SerializeField]
        private UnityEvent onReloading = new UnityEvent();

        [HideInInspector]
        public GameObject cannonObject;
        [HideInInspector]
        public CannonController cannonController;
        private Cannon[] cannonChildren;

        private void Start()
        {
            if (cannonObject != null)
            {
                cannonController = cannonObject.GetComponent<CannonController>();

                cannonController.OnReady += onFinishedReloading.Invoke;
                cannonController.OnReloading += onReloading.Invoke;
                cannonController.OnRising += onFiring.Invoke;

                cannonChildren = cannonObject.GetComponentsInChildren<Cannon>();

                foreach (Cannon cannon in cannonChildren)
                {
                    FieldInfo field = cannon.GetType().GetField("reloadDuration", BindingFlags.NonPublic | BindingFlags.Instance);
                    field.SetValue(cannon, reloadDuration);
                }
            }
        }

        public void FireCannon()
        {
            if ((CannonStates)cannonController.stateMachine.currentState == CannonStates.Idle)
            {
                cannonController.Trigger();
            }
        }

        // just gonna
        private enum CannonStates
        {
            Idle,
            Rising,
            Tracking,
            Firing,
            Cooldown,
            Lowering,
            Reloading,
            Grabbed,
            Broken
        }
    }
}
