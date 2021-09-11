using System;
using UnityEngine;

namespace DavigeditPlus.Filter
{
    class FilterByObjectName : FilterBase, IFilterBase
    {
        [SerializeField]
        [Tooltip("Filter will only pass if object entering is exactly this name.")]
        private string objectName;

        public bool CheckFilter(GameObject filterObject)
        {
            return true;
        }
    }
}
