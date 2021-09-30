using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace DavigeditPlus.Logic
{
    public class LogicTimer : MonoBehaviour
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
            if(timer != null)
                StopCoroutine(timer);
            currentTimer = 0;
            timer = StartCoroutine(IETimer());
        }

        public void SetTextToTimer_CountUp(GameObject objectWithText)
        {
            TimeSpan timeSpan = TimeSpan.FromSeconds(currentTimer);
            TextMeshPro worldSpaceText = objectWithText.GetComponent<TextMeshPro>();
            if (worldSpaceText != null)
                worldSpaceText.text = timeSpan.ToString(@"mm\:ss\:ff");
            else
            {
                TextMeshProUGUI uiSpaceText = objectWithText.GetComponent<TextMeshProUGUI>();
                if (uiSpaceText != null)
                    uiSpaceText.text = timeSpan.ToString(@"mm\:ss\:ff");
                else
                {
                    MelonLoader.MelonLogger.Warning($"hey buddy you called a method that sets textmeshpro text on a game object and your game object doesnt have textmeshpro text on it. called from {gameObject.name}");
                }
            }
        }

        public void SetTextToTimer_CountDown(GameObject objectWithText)
        {
            TimeSpan timeSpan = TimeSpan.FromSeconds(timerInterval - currentTimer);
            TextMeshPro worldSpaceText = objectWithText.GetComponent<TextMeshPro>();
            if (worldSpaceText != null)
                worldSpaceText.text = timeSpan.ToString(@"mm\:ss\:ff");
            else
            {
                TextMeshProUGUI uiSpaceText = objectWithText.GetComponent<TextMeshProUGUI>();
                if (uiSpaceText != null)
                    uiSpaceText.text = timeSpan.ToString(@"mm\:ss\:ff");
                else
                {
                    MelonLoader.MelonLogger.Warning($"hey buddy you called a method that sets textmeshpro text on a game object and your game object doesnt have textmeshpro text on it. called from {gameObject.name}");
                }
            }
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


        void OnDrawGizmos()
        {
            Gizmos.DrawIcon(transform.position, "DavigeditPlus/Logic/Logic_timer.png", true);
        }
    }
}
