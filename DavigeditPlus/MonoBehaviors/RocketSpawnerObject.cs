using Davigo.Davigedit;
using UnityEngine;

namespace DavigeditPlus
{
    public class RocketSpawnerObject : DavigeditObject
    {
        public override void Process()
        {
            RocketSpawnerSettings rocketSpawnerSettings = gameObject.GetComponentInParent<RocketSpawnerSettings>();
            if (rocketSpawnerSettings != null)
            {
                rocketSpawnerSettings.rocketSpawner = Replacement.gameObject.GetComponent<RocketSpawner>();
            }
        }
    }
}
