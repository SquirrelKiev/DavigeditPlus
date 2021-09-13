using System;
using System.Collections.Generic;
using UnityEngine;

namespace DavigeditPlus.Filter
{
    class FilterCombine : Filter
    {
        [SerializeField]
        [Tooltip("AND: All subfilters must pass. OR: any subfilter must pass.")]
        private FilterOperation filterOperation = FilterOperation.AND;

        [Header("Subfilters to test")]
        [SerializeField]
        private Filter filter1;
        [SerializeField]
        private Filter filter2;


        public override bool CheckFilter(GameObject filterObject)
        {
            // was originally using arrays of filters but that was less readable and didnt work iirc
            if (filterOperation == FilterOperation.AND)
            {
                if (filter1.CheckFilter(filterObject) && filter2.CheckFilter(filterObject))
                {
                    onPass.Invoke();
                    return !reverseOutcome;
                }
            }
            else if (filterOperation == FilterOperation.OR)
            {
                if (filter1.CheckFilter(filterObject) || filter2.CheckFilter(filterObject))
                {
                    onPass.Invoke();
                    return !reverseOutcome;
                }
            }
            onFail.Invoke();
            return reverseOutcome;
        }
    }

    enum FilterOperation
    {
        AND,
        OR
    }
}