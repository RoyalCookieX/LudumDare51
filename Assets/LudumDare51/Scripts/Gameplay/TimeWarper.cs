using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public enum TimeFrame
{
    Normal,
    Warped,
    Paused
}

public class TimeWarper : MonoBehaviour
{
    [Header("Events")]
    [SerializeField] private UnityEvent<TimeFrame> _onWarped;

    [Header("Properties")]
    [SerializeField] private float _normalTimeScale = 1.0f;
    [SerializeField] private float _warpedTimeScale = 0.25f;
    [SerializeField] private float _pausedTimeScale = 0.0f;
    [SerializeField] private AnimationCurve _warpBlend;

    private Coroutine _current = null;

    public void SetTimeFrame(TimeFrame timeFrame)
    {
        if (_current != null)
            StopCoroutine(_current);

        float srcTimeScale = TimeScaler.GetScale();
        float dstTimeScale = timeFrame switch
        {
            TimeFrame.Normal => _normalTimeScale,
            TimeFrame.Warped => _warpedTimeScale,
            TimeFrame.Paused => _pausedTimeScale,
            _ => 1.0f
        };
        float t0 = _warpBlend[0].time;
        float t1 = _warpBlend[_warpBlend.length - 1].time;
        float duration = t1 - t0;
        _current = StartCoroutine(WarpRoutine(srcTimeScale, dstTimeScale, duration));
        _onWarped?.Invoke(timeFrame);
    }

    public void SetTimeFrameImmediate(TimeFrame timeFrame)
    {
        if (_current != null)
            StopCoroutine(_current);

        float dstTimeScale = timeFrame switch
        {
            TimeFrame.Normal => _normalTimeScale,
            TimeFrame.Warped => _warpedTimeScale,
            TimeFrame.Paused => _pausedTimeScale,
            _ => 1.0f
        };
        TimeScaler.SetScale(dstTimeScale);
    }

    private IEnumerator WarpRoutine(float srcTimeScale, float dstTimeScale, float duration)
    {
        if (_warpBlend.length < 2)
        {
            Debug.LogWarning("Invalid Warp Blend!");
        }
        else
        {
            float current = 0.0f;
            while (current < duration)
            {
                yield return null;
                current += Time.unscaledDeltaTime;
                float t = current / duration;
                float timeScale = Mathf.LerpUnclamped(srcTimeScale, dstTimeScale, _warpBlend.Evaluate(t));
                TimeScaler.SetScale(timeScale);
            }
        }
        TimeScaler.SetScale(dstTimeScale);
    }
}
