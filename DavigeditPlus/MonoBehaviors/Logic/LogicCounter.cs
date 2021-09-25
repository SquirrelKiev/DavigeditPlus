﻿using System;
using System.Reflection;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using System.Collections.Generic;

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


        private List<Case> cases;

        private float currentValue;

        private void Start()
        {
            foreach(Case _case in GetComponentsInChildren<Case>())
            {
                cases.Add(_case);
            }
            currentValue = initialValue;

            onValueChanged.AddListener(OnValueChanged);
        }

        private void OnValueChanged()
        {
            foreach (Case _case in cases)
            {
                _case.CheckCase(currentValue);
            }
        }

        public void SetCounter(float value)
        {
            currentValue = value;
            ValidateValue();
            onValueChanged.Invoke();
        }

        public void Add(float value)
        {
            currentValue += value;
            ValidateValue();
            onValueChanged.Invoke();
        }

        public void Subtract(float value)
        {
            currentValue -= value;
            ValidateValue();
            onValueChanged.Invoke();
        }

        public void Divide(float value)
        {
            currentValue /= value;
            ValidateValue();
            onValueChanged.Invoke();
        }

        public void Multiply(float value)
        {
            currentValue *= value;
            ValidateValue();
            onValueChanged.Invoke();
        }

        public void resetToInitialValue()
        {
            currentValue = initialValue;
            ValidateValue();
            onValueChanged.Invoke();
        }

        public void setToGiantHP()
        {
            GiantHealth giantHealth = Utility.GetHierarchy(SingletonBehaviour<GameController>.Instance.Giant.gameObject)[0].GetComponentInChildren<GiantHealth>();
            FieldInfo field = typeof(GiantHealth).GetField("health", BindingFlags.Instance | BindingFlags.NonPublic);
            Health health = (Health)field.GetValue(giantHealth);
            currentValue = health.Current * (SingletonBehaviour<GameController>.Instance.GameOptions.GiantHealth);
            ValidateValue();
            onValueChanged.Invoke();
        }

        public void SetTextToCounter(TextMeshPro text)
        {
            text.text = currentValue.ToString();
        }

        private void ValidateValue()
        {
            if(currentValue < min)
            {
                currentValue = min;
                onReachedMin.Invoke();
            }

            if(currentValue > max)
            {
                currentValue = max;
                onReachedMax.Invoke();
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


        void OnDrawGizmos()
        {
            Gizmos.DrawIcon(transform.position, "DavigeditPlus/Logic/Logic_counter.png", true);
        }
    }
}
