using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Events;

namespace DavigeditPlus
{
    public class TriggerHurt : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField]
        private Filter.Filter filter;
        [SerializeField]
        private float damageToDeal = 1.0f;
        [SerializeField, Tooltip("In seconds. ")]
        private float timeBetweenDamage = 1.0f;

        [Header("Events")]
        [SerializeField]
        private UnityEvent onHurt = new UnityEvent();

        private List<GameObject> notAllowedObjects = new List<GameObject>();

        private void OnTriggerStay(Collider other)
        {
            if (!notAllowedObjects.Contains(other.gameObject))
            {
                if (filter != null && !filter.CheckFilter(other.gameObject))
                    return;

                GameObject[] hierarchy = Utility.GetHierarchy(other.gameObject);
                DamageableType damageableType = GetDamageableType(other.gameObject);

                // custom list add requirements
                switch (damageableType)
                {
                    case DamageableType.Warrior:
                        // so we get all the dummy rigidbodies
                        Collider[] colliders = other.gameObject.transform.root.GetComponentsInChildren<Collider>();
                        foreach (Collider item in colliders)
                        {
                            notAllowedObjects.Add(item.gameObject);
                            StartCoroutine(FixedLogic.InvokeFixed(timeBetweenDamage, new Action(() => { if (item != null) notAllowedObjects.Remove(item.gameObject); })));
                        }

                        break;

                    case DamageableType.GiantHands:
                        // theres alotta hand parts
                        foreach (Transform item in hierarchy[1].GetComponentsInChildren<Transform>())
                        {
                            notAllowedObjects.Add(item.gameObject);
                            StartCoroutine(FixedLogic.InvokeFixed(timeBetweenDamage, new Action(() => { if (item != null) notAllowedObjects.Remove(item.gameObject); })));
                        }

                        break;
                }

                // deal damage
                switch (damageableType)
                {
                    case DamageableType.GiantHead:
                        hierarchy[1].GetComponent<Damageable>().TakeDamage(damageToDeal, DamageType.Explosive, Vector3.forward, Vector3.forward, null, damageToDeal);
                        onHurt.Invoke();
                        break;
                    case DamageableType.GiantHands:
                        GiantHand giantHand = hierarchy[1].GetComponent<GiantHand>();

                        MethodInfo method = giantHand.GetType().GetMethod("Death", BindingFlags.NonPublic | BindingFlags.Instance);
                        method.Invoke(giantHand, null);
                        onHurt.Invoke();

                        break;
                    case DamageableType.Warrior:
                        other.gameObject.transform.root.GetComponentInChildren<PlayerHealth>().TakeDamage(damageToDeal * GameController.Instance.GameOptions.WarriorOutOfBoundsDamage, true);
                        MelonLoader.MelonLogger.Msg("Damaged!");
                        onHurt.Invoke();

                        break;
                    case DamageableType.IDamageable:
                        other.gameObject.GetComponent<IDamageable>().TakeDamage(damageToDeal, DamageType.Explosive, Vector3.forward, Vector3.forward, null);
                        onHurt.Invoke();

                        break;
                }

                if (damageableType != DamageableType.Warrior || damageableType != DamageableType.GiantHands)
                {
                    notAllowedObjects.Add(other.gameObject);
                    StartCoroutine(FixedLogic.InvokeFixed(timeBetweenDamage, new Action(() => { if (other != null) notAllowedObjects.Remove(other.gameObject); })));
                }
            }
        }

        private DamageableType GetDamageableType(GameObject gameObject)
        {
            GameObject[] hierarchy = Utility.GetHierarchy(gameObject);

            if (gameObject.GetComponentInParent<Player>() != null)
            {
                return DamageableType.Warrior;
            }

            else if (gameObject.GetComponent<IDamageable>() != null)
            {
                return DamageableType.IDamageable;
            }
            else if (hierarchy.Length >= 2)
            {
                if (hierarchy[1].name == "HandLeft" || hierarchy[1].name == "HandRight")
                {
                    return DamageableType.GiantHands;
                }

                else if (hierarchy[1].name == "Head")
                {
                    return DamageableType.GiantHead;
                }
            }
            return DamageableType.None;
        }

        private enum DamageableType
        {
            GiantHead,
            GiantHands,
            Warrior,
            IDamageable,
            None
        }
    }
}
