using System;
using System.Reflection;
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

        // i cant get unity to like my class no matter how hard i try, so im just gonna do this. sorry UX, but this is getting tiring
        [Header("Cases")]

        #region
        [SerializeField]
        private float possibleCase1;
        [SerializeField]
        private UnityEvent onCase1 = new UnityEvent();
        [SerializeField]
        private float possibleCase2;
        [SerializeField]
        private UnityEvent onCase2 = new UnityEvent();
        [SerializeField]
        private float possibleCase3;
        [SerializeField]
        private UnityEvent onCase3 = new UnityEvent();
        [SerializeField]
        private float possibleCase4;
        [SerializeField]
        private UnityEvent onCase4 = new UnityEvent();
        [SerializeField]
        private float possibleCase5;
        [SerializeField]
        private UnityEvent onCase5 = new UnityEvent();
        [SerializeField]
        private float possibleCase6;
        [SerializeField]
        private UnityEvent onCase6 = new UnityEvent();
        [SerializeField]
        private float possibleCase7;
        [SerializeField]
        private UnityEvent onCase7 = new UnityEvent();
        [SerializeField]
        private float possibleCase8;
        [SerializeField]
        private UnityEvent onCase8 = new UnityEvent();
        [SerializeField]
        private float possibleCase9;
        [SerializeField]
        private UnityEvent onCase9 = new UnityEvent();
        [SerializeField]
        private float possibleCase10;
        [SerializeField]
        private UnityEvent onCase10 = new UnityEvent();
        [SerializeField]
        private float possibleCase11;
        [SerializeField]
        private UnityEvent onCase11 = new UnityEvent();
        [SerializeField]
        private float possibleCase12;
        [SerializeField]
        private UnityEvent onCase12 = new UnityEvent();
        #endregion

        private float currentValue;

        private Action AC_onValueChanged;
        private Action AC_onReachedMin;
        private Action AC_onReachedMax;

        private void Start()
        {
            currentValue = initialValue;

            AC_onValueChanged += OnValueChanged;
            OnValueChanged();

            AC_onValueChanged += onValueChanged.Invoke;
            AC_onReachedMin += onReachedMin.Invoke;
            AC_onReachedMax += onReachedMax.Invoke;
        }

        // garbage code that i dont have an alternative for, im going to vomit
        private void OnValueChanged()
        {
            Garbage(ref possibleCase1, ref onCase1);
            Garbage(ref possibleCase2, ref onCase2);
            Garbage(ref possibleCase3, ref onCase3);
            Garbage(ref possibleCase4, ref onCase4);
            Garbage(ref possibleCase5, ref onCase5);
            Garbage(ref possibleCase6, ref onCase6);
            Garbage(ref possibleCase7, ref onCase7);
            Garbage(ref possibleCase8, ref onCase8);
            Garbage(ref possibleCase9, ref onCase9);
            Garbage(ref possibleCase10, ref onCase10);
            Garbage(ref possibleCase11, ref onCase11);
            Garbage(ref possibleCase12, ref onCase12);
        }

        private void Garbage(ref float possibleCase, ref UnityEvent onCase)
        {
            if(possibleCase == currentValue)
            {
                onCase.Invoke();
            }
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

        public void setToGiantHP()
        {
            GiantHealth giantHealth = Utility.GetHierarchy(SingletonBehaviour<GameController>.Instance.Giant.gameObject)[0].GetComponentInChildren<GiantHealth>();
            FieldInfo field = typeof(GiantHealth).GetField("health", BindingFlags.Instance | BindingFlags.NonPublic);
            Health health = (Health)field.GetValue(giantHealth);
            currentValue = health.Current * (SingletonBehaviour<GameController>.Instance.GameOptions.GiantHealth);
            MelonLoader.MelonLogger.Msg(currentValue);
            AC_onValueChanged.Invoke();
            ValidateValue();
        }

        private void ValidateValue()
        {
            if(currentValue < min)
            {
                currentValue = min;
                AC_onReachedMin.Invoke();
            }

            if(currentValue > max)
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
