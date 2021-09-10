using System.Reflection;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace DavigeditPlus
{
    class RocketSpawnerSettings : MonoBehaviour
    {
        [SerializeField]
        private UnityEvent onPickedUp = new UnityEvent();
        [SerializeField]
        private UnityEvent onRespawn = new UnityEvent();


        [SerializeField]
        [Min(0)]
        private float respawnTime = 10f;

        [HideInInspector]
        public RocketSpawner rocketSpawner;

        private void Start()
        {
            rocketSpawner.OnPickedUp += onPickedUp_Invoke;
            FieldInfo field = typeof(RocketSpawner).GetField("respawnTime", BindingFlags.NonPublic | BindingFlags.Instance);
            field.SetValue(rocketSpawner, respawnTime);
        }

        private void onPickedUp_Invoke(GameObject gameObject)
        {
            onPickedUp.Invoke();
            StartCoroutine(FixedLogic.InvokeFixed(respawnTime, new Action(onRespawn.Invoke)));
        }

        /*public void ForceSpawnRocket() // Hiding as respawn still runs
        {
            MethodInfo method = typeof(RocketSpawner).GetMethod("Reload");
            method.Invoke(rocketSpawner, new object[] { null });
            StopCoroutine(respawnTimer);
        }*/
    }
}