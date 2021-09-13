using System;
using UnityEngine;
using UnityEngine.Events;

namespace DavigeditPlus.Logic
{
    class LogicCounter : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField]
        private float initialValue = 0;
        [SerializeField]
        private float min = 0;
        [SerializeField]
        private float max = 10;

        [Header("Events")]
        [SerializeField]
        private UnityEvent onValueChanged = new UnityEvent();
        [SerializeField]
        private UnityEvent onReachedMin = new UnityEvent();
        [SerializeField]
        private UnityEvent onReachedMax = new UnityEvent();
        [SerializeField]
        private Case[] cases;

        private void Start()
        {

        }

        private void OnValidate()
        {
            if(initialValue < min)
            {
                Debug.LogWarning("initalValue is smaller than min! Clamping!");
                initialValue = min;
            }
            else if(initialValue > max)
            {
                Debug.LogWarning("initalValue is larger than max! Clamping!");
                initialValue = max;
            }
        }
    }

    [Serializable]
    class Case
    {
        [SerializeField]
        public float possibleCase = 0;
        [SerializeField]
        public UnityEvent onCase = new UnityEvent();
    }
}
