using System;
using System.Collections;
using TMPro;
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
        private bool autoStart = true;
        [SerializeField]
        private bool autoRepeat = true;
        
        [Header("Events")]
        [SerializeField]
        private UnityEvent onTimerEnded = new UnityEvent();
        [SerializeField, Tooltip("Called every frame the timer is running. ")]
        private UnityEvent onTimer = new UnityEvent();

        private Coroutine timer;
        private float currentTimer = 0;
        private bool paused = false;

        private void Start()
        {
            if (autoStart)
            {
                timer = StartCoroutine(IETimer());
            }
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

        public void SetTextToTimer_CountUp(TextMeshPro text)
        {
            TimeSpan timeSpan = TimeSpan.FromSeconds(currentTimer);
            text.text = timeSpan.ToString(@"mm\:ss\:ff");
        }

        public void SetTextToTimer_CountDown(TextMeshPro text)
        {
            TimeSpan timeSpan = TimeSpan.FromSeconds(timerInterval - currentTimer);
            text.text = timeSpan.ToString(@"mm\:ss\:ff");
        }

        private IEnumerator IETimer()
        {
            while (currentTimer <= timerInterval)
            {
                if (!paused)
                {
                    currentTimer += Time.deltaTime;
                    onTimer.Invoke();
                }
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
