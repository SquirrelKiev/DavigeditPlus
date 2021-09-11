using System;
using UnityEngine;
using UnityEngine.Events;

namespace DavigeditPlus.Filter
{
    interface IFilterBase
    {
        bool CheckFilter(GameObject filterObject);
    }
}
