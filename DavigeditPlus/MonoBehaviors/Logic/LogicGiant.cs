using UnityEngine;
using System.Reflection;
using UnityEngine.Events;

namespace DavigeditPlus.Logic
{
    public class LogicGiant : Filter.Filter
    {
        [Header("Settings (for filter)")]
        [SerializeField]
        private bool leftHand = true;
        [SerializeField]
        private bool rightHand = true;
        [SerializeField]
        private bool head = true;

        [Header("Events")]
        [SerializeField]
        private UnityEvent onHeadDamaged = new UnityEvent();
        [SerializeField]
        private UnityEvent onDeath = new UnityEvent();
        [SerializeField]
        private UnityEvent onRespawn = new UnityEvent();

        private GameController gameController;
        private Giant giant;
        private Damageable headDamageable;

        private void Start()
        {
            gameController = SingletonBehaviour<GameController>.Instance;
            giant = gameController.Giant;

            FieldInfo giant_headDamageable = giant.GetType().GetField("headDamageable", BindingFlags.NonPublic | BindingFlags.Instance);
            headDamageable = (Damageable)giant_headDamageable.GetValue(giant);

            giant.OnTakeHeadDamage += Giant_OnTakeHeadDamage;
            giant.OnDeath += onDeath.Invoke;
            giant.OnRespawn += onRespawn.Invoke;
        }

        public void TeleportGiant(GameObject location)
        {
            FieldInfo startPos = typeof(SetStartPosition).GetField("startPosition", BindingFlags.NonPublic | BindingFlags.Instance);
            Transform originalStartPos = (Transform)startPos.GetValue(FindObjectOfType<SetStartPosition>());
            startPos.SetValue(FindObjectOfType<SetStartPosition>(), location.transform);

            Quaternion newGiantRot = Quaternion.Euler(Vector3.Scale(Vector3.up, giant.transform.GetChild(0).transform.rotation.eulerAngles));
            FindObjectOfType<SetStartPosition>().Set(location.transform.position, newGiantRot * Quaternion.Inverse(originalStartPos.rotation) * location.transform.rotation);
        }

        public void RespawnGiant()
        {
            giant.Respawn();
        }

        public void DealDamageToGiant(float damage)
        {
            headDamageable.TakeDamage(damage, DamageType.Explosive, Vector3.forward, Vector3.forward, null, damage);
        }

        public void KillGiant()
        {
            giant.Die();
        }

        public override bool CheckFilter(GameObject filterObject)
        {
            GameObject[] hierarchy = Utility.GetHierarchy(filterObject);

            if (hierarchy.Length <= 2)
            {
                onFail.Invoke();
                return reverseOutcome;
            }

            if (
                (hierarchy[1].name == "HandLeft" && leftHand) ||
                (hierarchy[1].name == "HandRight" && rightHand) ||
                (hierarchy[1].name == "Head" && head)
               )
            {
                onPass.Invoke();
                return !reverseOutcome;
            }

            return reverseOutcome;

        }

        private void Giant_OnTakeHeadDamage(Damage obj)
        {
            onHeadDamaged.Invoke();
        }
    }
}
