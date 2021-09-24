using System;
using UnityEngine;
using UnityEngine.Events;

namespace DavigeditPlus
{
    public class Case : MonoBehaviour
    {
        public float possibleCase;
        public UnityEvent onCase;
        [Tooltip("order = possibleCase OPERATION num. e.g. if set to Less than, the check would be (possibleCase < input).")]
        public LogicCompare compareType;

        public void CheckCase(float num)
        {
            switch (compareType)
            {
                case LogicCompare.EqualTo:
                    if (possibleCase.CompareApproximate(num, 0.0001f))
                    {
                        onCase.Invoke();
                    }
                    break;
                case LogicCompare.NotEqualTo:
                    if (!possibleCase.CompareApproximate(num, 0.0001f))
                    {
                        onCase.Invoke();
                    }
                    break;
                case LogicCompare.GreaterThanOrEqualTo:
                    if (possibleCase >= num)
                    {
                        onCase.Invoke();
                    }
                    break;
                case LogicCompare.LessThanOrEqualTo:
                    if (possibleCase <= num)
                    {
                        onCase.Invoke();
                    }
                    break;
                case LogicCompare.GreaterThan:
                    if (possibleCase > num)
                    {
                        onCase.Invoke();
                    }
                    break;
                case LogicCompare.LessThan:
                    if (possibleCase < num)
                    {
                        onCase.Invoke();
                    }
                    break;
                default:
                    MelonLoader.MelonLogger.Msg("Case not in logiccompare or is null. how??");
                    break;
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
