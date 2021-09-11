using System;
using UnityEngine;
using UnityEngine.Events;

namespace DavigeditPlus
{
    [RequireComponent(typeof(Rigidbody))]
    class Breakable : MonoBehaviour, IDamageable
    {
        [Header("Settings")]
        [SerializeField]
        [Tooltip("Health of the object. When the health of the object goes below this, it will call OnBreak and destroy itself. Based off the damage values for the giant.")]
        private int health = 10;
        [SerializeField]
        [Tooltip("If set to false, the object will not destroy itself. ")]
        private bool destroyOnDeath = true;
        [SerializeField]
        private DamageFilter damageFilter = DamageFilter.All;

        [Header("Events")]
        [SerializeField]
        private UnityEvent OnBreak;
        [SerializeField]
        private UnityEvent onTakeDamage;

        [HideInInspector]
        public void TakeDamage(float amount, DamageType damageType, Vector3 point, Vector3 direction, Player owner, float giantDamage)
        {
            MelonLoader.MelonLogger.Msg(amount.ToString() + damageType.ToString());
            MelonLoader.MelonLogger.Msg(giantDamage.ToString());
        }

        public void ResetHealth()
        {

        }
        public void SetHealth(int health)
        {

        }
        public void Break()
        {

        }

        private enum DamageFilter
        {
            Concussive,
            Explosive,
            All
        }
    }
}
