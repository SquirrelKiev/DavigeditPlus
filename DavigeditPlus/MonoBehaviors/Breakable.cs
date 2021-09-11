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
        [Tooltip("If set to false, the object will not destroy itself on death. ")]
        private bool destroyOnDeath = true;

        [Header("Events")]
        [SerializeField]
        private UnityEvent onTakeDamage = new UnityEvent();
        [SerializeField]
        private UnityEvent onBreak = new UnityEvent();

        private float currentHealth;

        private void Start()
        {
            ResetHealth();
        }

        [HideInInspector]
        public void TakeDamage(float amount, DamageType damageType, Vector3 point, Vector3 direction, Player owner, float giantDamage)
        {
            DealDamage(giantDamage);
        }

        public void ResetHealth()
        {
            currentHealth = health;
        }
        public void DealDamage(float damageAmount)
        {
            onTakeDamage.Invoke();

            if (health >= 0)
            {
                currentHealth -= damageAmount;
                if (currentHealth <= 0)
                {
                    Break();
                }
            }
        }
        public void Break()
        {
            onBreak.Invoke();
            if (destroyOnDeath)
            {
                Destroy(gameObject);
            }
        }

        private enum DamageFilter
        {
            Concussive,
            Explosive,
            All
        }
    }
}
