using System;
using UnityEngine;
using UnityEngine.Events;

namespace DavigeditPlus.Logic
{
    public class LogicCounter : MonoBehaviour
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

        [SerializeField]
        private Case testCase = new Case();

        private float currentValue;

        private Action AC_onValueChanged;
        private Action AC_onReachedMin;
        private Action AC_onReachedMax;

        private void Start()
        {
            currentValue = initialValue;

            AC_onValueChanged += OnValueChanged;

            AC_onValueChanged += onValueChanged.Invoke;
            AC_onReachedMin += onReachedMin.Invoke;
            AC_onReachedMax += onReachedMax.Invoke;
        }

        private void OnValueChanged()
        {
            MelonLoader.MelonLogger.Msg(testCase.possibleCase.ToString());

            // Array.ForEach(cases, (Case theCase) => MelonLoader.MelonLogger.Msg(theCase.possibleCase.ToString()));
        }

        public void SetCounter(float value)
        {
            currentValue = value;
            AC_onValueChanged.Invoke();
            ValidateValue();
        }

        public void Add(float value)
        {
            currentValue += value;
            AC_onValueChanged.Invoke();
            ValidateValue();
        }

        public void Subtract(float value)
        {
            currentValue -= value;
            AC_onValueChanged.Invoke();
            ValidateValue();
        }

        public void Divide(float value)
        {
            currentValue /= value;
            AC_onValueChanged.Invoke();
            ValidateValue();
        }

        public void Multiply(float value)
        {
            currentValue *= value;
            AC_onValueChanged.Invoke();
            ValidateValue();
        }

        public void resetToInitialValue()
        {
            currentValue = initialValue;
            AC_onValueChanged.Invoke();
            ValidateValue();
        }

        private void ValidateValue()
        {
            if (currentValue < min)
            {
                currentValue = min;
                AC_onReachedMin.Invoke();
            }

            if (currentValue > max)
            {
                currentValue = max;
                AC_onReachedMax.Invoke();
            }
        }

        private void OnValidate()
        {
            if (initialValue < min)
            {
                Debug.LogWarning("initalValue is smaller than min! Clamping!");
                initialValue = min;
            }
            else if (initialValue > max)
            {
                Debug.LogWarning("initalValue is larger than max! Clamping!");
                initialValue = max;
            }
        }
    }
}
