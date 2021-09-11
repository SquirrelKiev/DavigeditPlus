using System;
using UnityEngine;

namespace DavigeditPlus.Filter
{
    class FilterCombine : Filter, IFilterBase
    {
        [SerializeField]
        [Tooltip("Subfilters to test.")]
        private IFilterBase[] filters;
        [SerializeField]
        [Tooltip("AND: All subfilters must pass. OR: any subfilter must pass.")]
        private FilterOperation filterOperation;

        public bool CheckFilter(GameObject filterObject)
        {
            return true;
        }
    }

    enum FilterOperation
    {
        AND,
        OR
    }
}
