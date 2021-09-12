using System;
using UnityEngine;

namespace DavigeditPlus.Filter
{
    // delegate my beloved
    delegate bool ReturnTheThing(GameObject filterObject, string name);

    class FilterByObjectName : Filter, IFilterBase
    {
        [SerializeField]
        private string objectName;
        [SerializeField]
        [Tooltip("Pass whether object contains or is exactly.")]
        private SearchType searchType;
        [SerializeField]
        private bool searchParents, searchChildren = false;

        private ReturnTheThing thing;

        public bool CheckFilter(GameObject filterObject)
        {
            if (searchType == SearchType.containsMatch)
                thing = IfContains;
            else if (searchType == SearchType.exactMatch)
                thing = IfExact;


            if (thing(filterObject, objectName))
            {
                onPass.Invoke();
                return !reverseOutcome;
            }

            if (searchChildren)
                if (SearchChildren(filterObject, objectName))
                {
                    onPass.Invoke();
                    return !reverseOutcome;
                }

            if (searchParents)
                if (SearchParents(filterObject, objectName))
                {
                    onPass.Invoke();
                    return !reverseOutcome;
                }

            return reverseOutcome;
        }



        private bool SearchChildren(GameObject gameObject, string name)
        {
            foreach (Transform child in gameObject.transform)
            {
                if (thing(gameObject, name))
                    return true;
            }
            return false;
        }

        private bool SearchParents(GameObject gameObject, string name)
        {
            Transform currentParent = gameObject.transform.parent;
            while (currentParent != null)
            {
                if (thing(gameObject, name))
                    return true;
                currentParent = currentParent.parent;
            }
            return false;
        }

        private bool IfExact(GameObject filterObject, string objectName)
        { return filterObject.name == objectName; }

        private bool IfContains(GameObject filterObject, string objectName)
        { return filterObject.name.Contains(objectName); }
    }
    enum SearchType
    {
        exactMatch,
        containsMatch
    }
}