using System;
using UnityEngine;

namespace DavigeditPlus.Filter
{
    class FilterByWarrior : Filter, IFilterBase
    {

        public bool CheckFilter(GameObject filterObject)
        {
            return true;
        }
    }
}
