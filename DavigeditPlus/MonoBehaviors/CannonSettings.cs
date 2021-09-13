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

        private Cannon cannon;
        private PropertyInfo HasReloaded_PropInfo;
        private TriggerButton triggerButton;
        private MethodInfo triggerButton_Press;

        private void Start()
        {
            if (cannonObject != null)
            {
                cannonController = cannonObject.GetComponent<CannonController>();

                cannonController.OnReady += onFinishedReloading.Invoke;
                cannonController.OnReloading += onReloading.Invoke;
                cannonController.OnRising += onFiring.Invoke;

                cannon = cannonController.Cannon;

                // reflection yay
                HasReloaded_PropInfo = cannon.GetType().GetProperty("HasReloaded");

                triggerButton = (TriggerButton)cannonController.GetType().GetField("triggerButton", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(cannonController);
                triggerButton_Press = triggerButton.GetType().GetMethod("Press", BindingFlags.NonPublic | BindingFlags.Instance);

                FieldInfo cannon_reloadDuration = cannon.GetType().GetField("reloadDuration", BindingFlags.NonPublic | BindingFlags.Instance);
                cannon_reloadDuration.SetValue(cannon, reloadDuration);
            }
        }

        public void FireCannon()
        {
            if ((CannonStates)cannonController.stateMachine.currentState == CannonStates.Idle)
            {
                ForceFireCannon();
            }
        }
        public void ForceFireCannon()
        {
            HasReloaded_PropInfo.SetValue(cannon, true);
            // was firing the cannon directly but that caused problems when the warrior pressed the button
            triggerButton_Press.Invoke(triggerButton, null);
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
