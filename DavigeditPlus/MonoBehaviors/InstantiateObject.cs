using UnityEngine;
using UnityEngine.Events;

namespace DavigeditPlus
{
    class InstantiateObject : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField]
        private GameObject prefab;
        [SerializeField]
        private GameObject[] locationsToSpawn;

        [Header("Events")]
        [SerializeField]
        private UnityEvent onInstantiate = new UnityEvent();

        public void Instantiate()
        {
            foreach(GameObject gameObject in locationsToSpawn)
                Instantiate(prefab, gameObject.transform.position, gameObject.transform.rotation);

            onInstantiate.Invoke();
        }
    }
}
