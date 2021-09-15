using System;
using UnityEngine;
using UnityEngine.Events;

namespace DavigeditPlus.Logic
{
    // why wont you serialize???
    [Serializable]
    public struct Case
    {
        public float possibleCase;
        public UnityEvent onCase;

        /*
        public void CheckOnCase(float value)
        {
            if (Mathf.Approximately(possibleCase, value))
            {
                onCase.Invoke();
            }
        }*/
    }
}
