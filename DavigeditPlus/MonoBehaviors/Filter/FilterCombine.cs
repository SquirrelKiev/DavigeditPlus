using System;
using System.Collections.Generic;
using UnityEngine;

namespace DavigeditPlus.Filter
{
    public class FilterCombine : Filter
    {
        [SerializeField]
        [Tooltip("AND: All subfilters must pass. OR: any subfilter must pass.")]
        private FilterOperation filterOperation = FilterOperation.AND;

        [Header("Subfilters to test")]
        [SerializeField]
        private Filter[] filters;


        public override bool CheckFilter(GameObject filterObject)
        {
            if (filterOperation == FilterOperation.AND)
            {
                foreach (Filter filter in filters)
                {
                    if (filter.CheckFilter(filterObject) == false)
                    {
                        onFail.Invoke();
                        return reverseOutcome;
                    }
                }
                onPass.Invoke();
                return !reverseOutcome;
            }
            else if (filterOperation == FilterOperation.OR)
            {
                foreach (Filter filter in filters)
                {
                    if (filter.CheckFilter(filterObject))
                    {
                        onPass.Invoke();
                        return !reverseOutcome;
                    }
                }
                onFail.Invoke();
                return reverseOutcome;
            }
            else
            {
                return reverseOutcome;
            }
        }
    }

    enum FilterOperation
    {
        AND,
        OR
    }
}