using System;
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

        public bool CheckFilter(GameObject filterObject)
        {
            bool passed = false;

            if (filterOperation == FilterOperation.AND)
            {
                passed = true;
                foreach (Filter filter in filters)
                {
                    if (!filter.GetComponent<IFilterBase>().CheckFilter(filterObject))
                    {
                        passed = false;
                    }
                }
            }

            else if (filterOperation == FilterOperation.OR)
            {
                passed = false;
                foreach (Filter filter in filters)
                {
                    if (filter.GetComponent<IFilterBase>().CheckFilter(filterObject))
                    {
                        passed = true;
                    }
                }
            }

            if (reverseOutcome == true)
                return !passed;
            else
                return passed;
        }
    }

    enum FilterOperation
    {
        AND,
        OR
    }
}