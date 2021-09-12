using System;
using UnityEngine;

namespace DavigeditPlus.Filter
{
    class FilterByObjectName : Filter, IFilterBase
    {
        [SerializeField]
        private string objectName;
        [SerializeField]
        [Tooltip("Pass whether object contains or is exactly.")]
        private SearchType searchType;
        [SerializeField]
        private bool searchParents, searchChildren = false;

        // could prob use delegates here, but im lazy
        public bool CheckFilter(GameObject filterObject)
        {
            if (searchType == SearchType.containsMatch)
            {
                if (filterObject.name.Contains(objectName))
                {
                    return true;
                }

                if (searchChildren)
                    if(SearchChildren_Contains(filterObject, objectName))
                        return true;

                if (searchParents)
                    if(SearchParents_Contains(filterObject, objectName))
                        return true;
            }
            else if(searchType == SearchType.exactMatch)
            {
                if (filterObject.name == objectName)
                    return true;

                if(searchChildren)
                    if(SearchChildren_Exactly(filterObject, objectName))
                        return true;

                if (searchParents)
                    if (SearchParents_Exactly(filterObject, objectName))
                        return true;
            }
            return false;
        }

        private bool SearchChildren_Exactly(GameObject filterObject, string objectName)
        {
            foreach (Transform child in gameObject.transform)
            {
                if (child.name == name)
                    return true;
            }
            return false;
        }

        private bool SearchParents_Exactly(GameObject filterObject, string objectName)
        {
            Transform currentParent = gameObject.transform.parent;
            while (currentParent != null)
            {
                if (currentParent.gameObject.name == objectName)
                    return true;
                currentParent = currentParent.parent;
            }
            return false;
        }

        private bool SearchChildren_Contains(GameObject gameObject, string name)
        {
            foreach(Transform child in gameObject.transform)
            {
                if (child.name.Contains(name))
                    return true;
            }
            return false;
        }

        private bool SearchParents_Contains(GameObject gameObject, string name)
        {
            Transform currentParent = gameObject.transform.parent;
            while (currentParent != null)
            {
                if(currentParent.gameObject.name.Contains(name))
                    return true;
                currentParent = currentParent.parent;
            }
            return false;
        }
    }
    enum SearchType
    {
        exactMatch,
        containsMatch
    }
}
