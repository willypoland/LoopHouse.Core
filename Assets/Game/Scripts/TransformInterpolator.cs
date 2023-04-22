using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;


namespace Game.Scripts
{
    public class TransformInterpolator : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private Transform _start;
        [SerializeField] private Transform _end;
        [SerializeField] private float _time = 1f;
        [SerializeField] private UnityEvent _whenStarting;
        [SerializeField] private UnityEvent _whenEnd;

        private Coroutine _coroutine;
        private float _delta = 1f;
        private float _currentTime = 0f;

        private void Reset()
        {
            _target = transform;
        }

        private void OnDisable()
        {
            StopInterpolation();
        }

        public void PlayFromStart(bool breakActive)
        {
            _delta = 1f;
            _currentTime = 0f;
            ReRunCoroutine();
        }

        public void PlayFromEnd(bool breakActive)
        {
            _delta = -1f;
            _currentTime = _time;
            ReRunCoroutine();
        }

        public void Toggle(bool breakActive)
        {
            if (_currentTime <= 0)
                _delta = 1f;
            else if (_currentTime >= _time)
                _delta = -1f;
            else
                _delta = -_delta;
            
            if (breakActive || _coroutine == null)
                ReRunCoroutine();
        }

        private void ReRunCoroutine()
        {
            StopInterpolation();
            _coroutine = StartCoroutine(RunInterpolation());
        }

        private void StopInterpolation()
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
                _coroutine = null;
            }
        }

        private IEnumerator RunInterpolation()
        {
            _whenStarting?.Invoke();
            
            while (true)
            {
                yield return null;

                _currentTime = Mathf.Clamp(_currentTime + Time.deltaTime * _delta, 0f, _time);
                float t = Mathf.SmoothStep(0f, 1f, _currentTime / _time);
                var newPosition = Vector3.Lerp(_start.position, _end.position, t);
                var newRotation = Quaternion.Lerp(_start.rotation, _end.rotation, t);
                _target.SetPositionAndRotation(newPosition, newRotation);

                if (_currentTime >= _time || _currentTime <= 0f)
                    break;
            }

            _whenEnd?.Invoke();
            _coroutine = null;
        }

    }
}
