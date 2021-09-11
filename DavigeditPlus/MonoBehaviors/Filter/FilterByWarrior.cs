using System;
using UnityEngine;

namespace DavigeditPlus.Filter
{
    class FilterByWarrior : FilterBase, IFilterBase
    {

        public bool CheckFilter(GameObject filterObject)
        {
            return true;
        }
    }
}
