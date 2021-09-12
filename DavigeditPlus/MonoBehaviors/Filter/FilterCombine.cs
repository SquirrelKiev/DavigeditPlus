using System;
using System.Collections.Generic;
using UnityEngine;

namespace DavigeditPlus.Filter
{
    class FilterCombine : Filter, IFilterBase
    {
        [SerializeField]
        [Tooltip("AND: All subfilters must pass. OR: any subfilter must pass.")]
        private FilterOperation filterOperation = FilterOperation.AND;

        [Header("Subfilters to test")]
        [SerializeField]
        private Filter filter1;
        [SerializeField]
        private Filter filter2;

        private IFilterBase iFilter1;
        private IFilterBase iFilter2;

        private void Start()
        {
            iFilter1 = filter1.GetComponent<IFilterBase>();
            iFilter2 = filter2.GetComponent<IFilterBase>();
        }

        public bool CheckFilter(GameObject filterObject)
        {
            if (filterOperation == FilterOperation.AND)
            {
                if (iFilter1.CheckFilter(filterObject) && iFilter2.CheckFilter(filterObject))
                {
                    onPass.Invoke();
                    return !reverseOutcome;
                }
            }
            else if (filterOperation == FilterOperation.OR)
            {
                if (iFilter1.CheckFilter(filterObject) || iFilter2.CheckFilter(filterObject))
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