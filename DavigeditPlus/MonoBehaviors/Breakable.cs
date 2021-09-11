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
        [Tooltip("Based off the damage values for the giant. 0 means don't break. ")]
        private float health = 10;
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

        private float currentHealth;

        private void Start()
        {
            currentHealth = health;
        }

        [HideInInspector]
        public void TakeDamage(float amount, DamageType damageType, Vector3 point, Vector3 direction, Player owner, float giantDamage)
        {
        }

        public void ResetHealth()
        {
            currentHealth = health;
        }
        public void DealDamage(int damageAmount)
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
