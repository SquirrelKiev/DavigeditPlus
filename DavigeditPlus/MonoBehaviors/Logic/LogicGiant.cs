using UnityEngine;
using DavigeditPlus.Filter;

namespace DavigeditPlus.Logic
{
    class LogicGiant : Filter.Filter
    {
        [SerializeField]
        private bool leftHand = true;
        [SerializeField]
        private bool rightHand = true;
        [SerializeField]
        private bool head = true;

        public override bool CheckFilter(GameObject filterObject)
        {
            GameObject[] hierarchy = Utility.GetHierarchy(filterObject);

            if (hierarchy.Length <= 2)
            {
                onFail.Invoke();
                return reverseOutcome;
            }

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
