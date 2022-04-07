using System;
using JetBrains.Annotations;
using TimeForChange.Cinematic;
using TMPro;
using UnityEngine;

namespace TimeForChange.Timer
{
    [RequireComponent(typeof(TextMeshPro))]
    public class Timer : MonoBehaviour
    {
        [SerializeField] private int _minutes = 15;
        [SerializeField] private bool _callCinematic;
        [SerializeField, CanBeNull] private Meteor _meteor;

        private TextMeshPro _timerText;
        private bool _timeIsRunning;
        private float _startTime;
        private bool _isMeteorNotNull;

        private void Awake() => _timerText = GetComponent<TextMeshPro>();

        private void Start()
        {
            _isMeteorNotNull = _meteor != null;
            _timeIsRunning = true;
            _startTime = _minutes * 60;
        }

        private void Update()
        {
            if (_timeIsRunning)
            {
                if (_startTime > 0)
                {
                    CalculateTime(_startTime);
                    _startTime -= Time.deltaTime;
                }
                else
                {
                    ResetTime();
                    TimeRunOut();
                }
            }
        }

        private void ResetTime() => _timerText.text = "00:00";

        private void CalculateTime(float startTime)
        {
            startTime += 1;

            TimeSpan time = TimeSpan.FromSeconds(startTime);
            _timerText.text = time.ToString(@"mm\:ss");
        }

        private void TimeRunOut()
        {
            _startTime = 0;
            _timeIsRunning = false;

            if (_callCinematic && _isMeteorNotNull)
            {
                 _meteor.gameObject.SetActive(true);
            }
        }
    }
}
