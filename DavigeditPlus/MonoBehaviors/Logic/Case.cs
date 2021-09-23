using System;
using UnityEngine;
using UnityEngine.Events;

namespace DavigeditPlus
{
    public class Case : MonoBehaviour
    {
        public float possibleCase;
        public UnityEvent onCase;
        [Tooltip("order = possibleCase OPERATION num. e.g. if set to Less than, the check would be (possibleCase < num).")]
        public LogicCompare compareType;

        public void CheckCase(float num)
        {
            if (possibleCase.CompareApproximate(num, 0.0001f))
            {
                onCase.Invoke();
            }
        }
    }

    public enum LogicCompare
    {
        [InspectorName("Equal to (==)")]
        EqualTo,
        [InspectorName("Not equal to (!=)")]
        NotEqualTo,
        [InspectorName("Greater than or equal to (>=)")]
        GreaterThanOrEqualTo,
        [InspectorName("Less than or equal to (<=)")]
        LessThanOrEqualTo,
        [InspectorName("Greater than (>)")]
        GreaterThan,
        [InspectorName("Less than (<)")]
        LessThan
    }
}
