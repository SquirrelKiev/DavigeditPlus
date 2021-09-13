using System;
using UnityEngine;

namespace DavigeditPlus.Filter
{
    class FilterByComponent : Filter
    {
        [SerializeField]
        private string componentClassName;
        [SerializeField]
        private bool searchParents = false, searchChildren = false;

        private Type componentClass;

        private void Start()
        {
            componentClass = Type.GetType(componentClassName);

            // wouldnt detect davigo components otherwise
            if(componentClass == null)
                componentClass = Type.GetType($"{componentClassName}, Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null");
            MelonLoader.MelonLogger.Msg(componentClass);
        }

        public override bool CheckFilter(GameObject filterObject)
        {
            if (filterObject.GetComponent(componentClass) != null)
            {
                onPass.Invoke();
                return !reverseOutcome;
            }

            bool passed = false;
            if(searchChildren)
                passed = SearchChildren(filterObject);

            if (passed)
            {
                onPass.Invoke();
                return !reverseOutcome;
            }

            if (searchParents)
                passed = SearchParents(filterObject);

            if (passed)
            {
                onPass.Invoke();
                return !reverseOutcome;
            }

            onFail.Invoke();
            return reverseOutcome;
        }

        private bool SearchChildren(GameObject gameObject)
        {
            foreach (Transform child in gameObject.transform)
            {
                if (child.GetComponent(componentClass) != null)
                    return true;
            }
            return false;
        }

        private bool SearchParents(GameObject gameObject)
        {
            Transform currentParent = gameObject.transform.parent;
            while (currentParent != null)
            {
                if (currentParent.GetComponent(componentClass) != null)
                    return true;
                currentParent = currentParent.parent;
            }
            return false;
        }
    }
}
