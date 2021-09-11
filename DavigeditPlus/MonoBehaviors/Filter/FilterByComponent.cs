using System;
using UnityEngine;

namespace DavigeditPlus.Filter
{
    class FilterByComponent : Filter, IFilterBase
    {
        [SerializeField]
        private string componentClassName;

        public bool CheckFilter(GameObject filterObject)
        {
            return true;
        }
    }
}
