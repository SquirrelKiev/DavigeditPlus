using System;
using System.Collections.Generic;
using UnityEngine;

namespace DavigeditPlus
{
    class Utility
    {
        /// <summary>
        /// returns an array containing the entire hierarchy of gameObject.
        /// </summary>
        /// <param name="gameObject">the GameObject to get hierarchy of. </param>
        /// <returns>gameObject's hierarchy.</returns>
        public static GameObject[] GetHierarchy(GameObject gameObject)
        {
            List<GameObject> hierarchy = new List<GameObject>();

            hierarchy.Add(gameObject);
            Transform currentParent = gameObject.transform.parent;
            while(currentParent != null)
            {
                hierarchy.Add(currentParent.gameObject);

                currentParent = currentParent.parent;
            }
            GameObject[] newHierarchy = hierarchy.ToArray();
            Array.Reverse(newHierarchy);

            return newHierarchy;
        }
    }
}
