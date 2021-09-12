using System;
using System.Collections.Generic;
using UnityEngine;

namespace DavigeditPlus.Filter
{
    class FilterCombine : Filter, IFilterBase
    {
        [SerializeField]
        [Tooltip("Subfilters to test.")]
        private Filter[] filters;
        [SerializeField]
        [Tooltip("AND: All subfilters must pass. OR: any subfilter must pass.")]
        private FilterOperation filterOperation = FilterOperation.AND;

        private void Start()
        {

        }

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