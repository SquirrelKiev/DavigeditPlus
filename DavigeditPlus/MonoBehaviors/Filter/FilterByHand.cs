using UnityEngine;

namespace DavigeditPlus.Filter
{
    class FilterByHand : Filter, IFilterBase
    {
        public bool CheckFilter(GameObject filterObject)
        {
            return true;
        }
    }
}
