using UnityEngine;

namespace DavigeditPlus.Filter
{
    class FilterByGiant : Filter, IFilterBase
    {
        [SerializeField]
        private bool leftHand = true;
        [SerializeField]
        private bool rightHand = true;
        [SerializeField]
        private bool head = true;

        public bool CheckFilter(GameObject filterObject)
        {
            GameObject[] hierarchy = Utility.GetHierarchy(filterObject);

            if (hierarchy.Length <= 2)
            {
                onFail.Invoke();
                return reverseOutcome;
            }

            // could do this whole thing with && and or but this is more readable
            if (
                (hierarchy[1].name == "HandLeft" && leftHand)   ||
                (hierarchy[1].name == "HandRight" && rightHand) ||
                (hierarchy[1].name == "Head" && head)           
               )
            {
                onPass.Invoke();
                return !reverseOutcome;
            }

            return false;
        }
    }
}
