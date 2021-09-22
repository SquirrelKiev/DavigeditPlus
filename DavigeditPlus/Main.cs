using MelonLoader;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Reflection;
using System;

namespace DavigeditPlus
{
    public class Main : MelonMod
    {
        bool firstLoad = true;
        bool firstMegalophobia = true;

        public override void OnApplicationStart()
        {
            MelonLogger.Msg("yea im workin");
        }

        /*
         * requires alot of work
        // Funky hack to get flamethrower
        public override void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
            if(firstLoad && sceneName == "MenuClosedAlpha")
            {
                firstLoad = false;
                SceneManager.LoadScene("Megalophobia");
            }

            else if(firstMegalophobia && sceneName == "Megalophobia")
            {
                firstMegalophobia = false;
                GameObject flamethrowerController = GameObject.Find("FlamethrowerController");
                if(flamethrowerController != null)
                {
                    flamethrowerController = GameObject.Instantiate(flamethrowerController, Vector3.zero, new Quaternion());

                    IdentifiableObject idObject = flamethrowerController.AddComponent<IdentifiableObject>();
                    FieldInfo guidPropertyInfo = idObject.GetType().GetField("guid", BindingFlags.NonPublic | BindingFlags.Instance);
                    guidPropertyInfo.SetValue(idObject, 1597936874);

                    flamethrowerController.transform.GetChild(0).position = Vector3.zero;
                    flamethrowerController.transform.GetChild(0).rotation = new Quaternion();
                    flamethrowerController.transform.GetChild(1).position = Vector3.zero;
                    flamethrowerController.transform.GetChild(1).rotation = new Quaternion();

                    GameObject.DontDestroyOnLoad(flamethrowerController);

                    FieldInfo identifiableObjectsGameResourcesField = SingletonBehaviour<GameResources>.Instance.GetType().GetField("identifiableObjects", BindingFlags.NonPublic | BindingFlags.Instance);
                    IdentifiableObject[] identifiableObjects = (IdentifiableObject[])identifiableObjectsGameResourcesField.GetValue(SingletonBehaviour<GameResources>.Instance);
                    
                    Array.Resize(ref identifiableObjects, identifiableObjects.Length + 1);
                    identifiableObjects[identifiableObjects.Length - 1] = idObject;
                    identifiableObjectsGameResourcesField.SetValue(SingletonBehaviour<GameResources>.Instance, identifiableObjects);
                }

                SceneManager.LoadScene("MenuClosedAlpha");
            }
        }
        */
    }
}
