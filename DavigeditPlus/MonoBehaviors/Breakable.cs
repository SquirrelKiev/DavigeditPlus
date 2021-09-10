using System;
using UnityEngine;

namespace DavigeditPlus
{
    class Breakable : MonoBehaviour, IDamageable
    {
        [SerializeField]
        private float health = 10f;

        public void TakeDamage(float amount, DamageType damageType, Vector3 point, Vector3 direction, Player owner, float giantDamage)
        {
            MelonLoader.MelonLogger.Msg(amount.ToString() + damageType.ToString());
            MelonLoader.MelonLogger.Msg(giantDamage.ToString());
        }
    }
}
