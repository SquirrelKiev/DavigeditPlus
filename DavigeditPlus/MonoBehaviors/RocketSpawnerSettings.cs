using System.Reflection;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace DavigeditPlus
{
    public class RocketSpawnerSettings : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField]
        [Min(0)]
        private float respawnTime = 10f;

        [Header("Events")]
        [SerializeField]
        private UnityEvent onPickedUp = new UnityEvent();
        [SerializeField]
        private UnityEvent onRespawn = new UnityEvent();


        [HideInInspector]
        public RocketSpawner rocketSpawner;
        private Coroutine respawnTimer;

        private void Start()
        {
            rocketSpawner.OnPickedUp += onPickedUp_Invoke;
            FieldInfo field = typeof(RocketSpawner).GetField("respawnTime", BindingFlags.NonPublic | BindingFlags.Instance);
            field.SetValue(rocketSpawner, respawnTime);
        }

        private void onPickedUp_Invoke(GameObject gameObject)
        {
            onPickedUp.Invoke();
            respawnTimer = StartCoroutine(FixedLogic.InvokeFixed(respawnTime, new Action(onRespawn.Invoke)));
        }

        public void ForceSpawnRocket()
        {
            FieldInfo loaded = typeof(RocketSpawner).GetField("loaded", BindingFlags.NonPublic | BindingFlags.Instance);
            if (!(bool)loaded.GetValue(rocketSpawner))
            {
                MethodInfo method = typeof(RocketSpawner).GetMethod("Reload", BindingFlags.NonPublic | BindingFlags.Instance);
                method.Invoke(rocketSpawner, null);
                StopCoroutine(respawnTimer);
                onRespawn.Invoke();
                rocketSpawner.StopAllCoroutines();
            }
        }
    }
}