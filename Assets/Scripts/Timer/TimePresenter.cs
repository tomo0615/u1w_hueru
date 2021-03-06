﻿using System;
using UniRx;
using UnityEngine;

namespace Timer
{
    public class TimePresenter : MonoBehaviour
    {
        [SerializeField] private int startTimeValue = 60;
        
        [SerializeField] private TimeView timeView;

        private TimeModel _timeModel;

        private IDisposable _timerEndObservable;
        
        private void Awake()
        {
            _timeModel = new TimeModel();
        }

        public void OnStartTimer(Action action)
        {
            var observable = _timeModel.CreateTimerObservable(startTimeValue);

            _timeModel.GameTimer
                .Subscribe(timeView.SetTimeValue)
                .AddTo(gameObject);


            //終了後
            _timerEndObservable
                = observable
                    .Subscribe(value => { _timeModel.SetTimerValue(value); },
                    action);
        }

        public void OnStopTimer()
        {
            _timerEndObservable.Dispose();
        }
    }
}
