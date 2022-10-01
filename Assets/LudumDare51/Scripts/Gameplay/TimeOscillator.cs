using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class TimeOscillator : MonoBehaviour
{
    public float TimeRemaining { get; private set; }
    public float TimeInterval => _timeInterval;
    public float Percentage => TimeRemaining / _timeInterval;
    private float SrcScale => _slowed ? _normalMultiplier : _slowMultiplier;
    private float DstScale => _slowed ? _slowMultiplier : _normalMultiplier;

    [Header("Events")]
    [SerializeField] private UnityEvent<float> _onWarp;
    [SerializeField] private UnityEvent<float> _onTimeRemainingChanged;

    [Header("Properties")]
    [SerializeField] private bool _slowed = false;
    [SerializeField] private float _timeInterval = 10.0f;
    [SerializeField] private float _slowMultiplier = 0.25f;
    [SerializeField] private float _normalMultiplier = 1.0f;
    [SerializeField] private AnimationCurve _timeIntervalBlend;

    private Coroutine _current;

    private void Warp()
    {
        if (_current != null)
            StopCoroutine(_current);
        _current = StartCoroutine(WarpRoutine());
        _onWarp?.Invoke(DstScale);
    }

    private void WarpImmediate()
    {
        if (_current != null)
            StopCoroutine(_current);
        TimeScaler.SetScale(DstScale);
        _onWarp?.Invoke(DstScale);
    }

    private IEnumerator WarpRoutine()
    {
        if (_timeIntervalBlend.length < 2)
        {
            TimeScaler.SetScale(DstScale);
            Debug.LogWarning("Invalid Interval Blend!");
        }
        float current = 0.0f;
        float t0 = _timeIntervalBlend[0].time;
        float t1 = _timeIntervalBlend[_timeIntervalBlend.length - 1].time;
        float duration = Mathf.Abs(t1 - t0);
        while (current < duration)
        {
            yield return null;
            current += Time.unscaledDeltaTime;
            float t = current / duration;
            float timeScale = Mathf.LerpUnclamped(SrcScale, DstScale, _timeIntervalBlend.Evaluate(t));
            TimeScaler.SetScale(timeScale);
        }
        TimeScaler.SetScale(DstScale);
    }

    private IEnumerator Start()
    {
        TimeRemaining = _timeInterval;
        _onTimeRemainingChanged?.Invoke(Percentage);
        WarpImmediate();

        while(true)
        {
            yield return null;
            TimeRemaining -= Time.unscaledDeltaTime;
            _onTimeRemainingChanged?.Invoke(Percentage);
            if (TimeRemaining > 0.0f)
                continue;

            _slowed = !_slowed;
            TimeRemaining = _timeInterval;
            Warp();
        }
    }
}
