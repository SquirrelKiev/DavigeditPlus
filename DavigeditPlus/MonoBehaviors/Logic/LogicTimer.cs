using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace DavigeditPlus.Logic
{
    class LogicTimer : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField, Tooltip("Max time before repeating, in seconds. ")]
        private float timerInterval = 10f;
        [SerializeField]
        private bool autoRepeat = true;
        
        [Header("Events")]
        [SerializeField]
        private UnityEvent onTimerEnded = new UnityEvent();

        private Coroutine timer;
        private float currentTimer = 0;
        private bool paused = false;

        private void Start()
        {
            timer = StartCoroutine(IETimer());
        }

        public void SetNewTimerInterval(float seconds)
        {
            timerInterval = seconds;
        }

        public void PauseTimer()
        {
            paused = !paused;
        }

        public void ResetTimer()
        {
            StopCoroutine(timer);
            currentTimer = 0;
            timer = StartCoroutine(IETimer());
        }

        private IEnumerator IETimer()
        {
            while (currentTimer <= timerInterval)
            {
                if(!paused)
                    currentTimer += Time.deltaTime;
                yield return null;
            }
            onTimerEnded.Invoke();
            if (autoRepeat)
            {
                ResetTimer();
            }
            yield break;
        }
    }
}
