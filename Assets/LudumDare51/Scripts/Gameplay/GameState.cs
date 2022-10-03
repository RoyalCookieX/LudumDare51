using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class GameState : MonoBehaviour
{
    public float Percentage => _timeRemaining / _timeInterval;
    private TimeFrame CurTimeFrame => _warped ? TimeFrame.Warped : TimeFrame.Normal;

    [Header("Events")]
    [SerializeField] private UnityEvent<float> _onTimeRemainingChanged;

    [Header("Components")]
    [SerializeField] private TimeWarper _timeWarper;
    [SerializeField] private CharacterSpawner _characterSpawner;
    [SerializeField] private UpgradeSpawner _upgradeSpawner;
    [SerializeField] private PlayerScore _playerScore;
    [SerializeField] private PlayerController _playerController;

    [Header("Properties")]
    [SerializeField] private bool _warped = false;
    [SerializeField] private bool _paused = false;
    [SerializeField, Min(0.01f)] private float _timeInterval = 10.0f;

    private float _timeRemaining = 0.0f;

    public void SetPaused(bool paused)
    {
        if (paused == _paused)
            return;

        _paused = paused;
        _timeWarper.SetTimeFrameImmediate(_paused ? TimeFrame.Paused : CurTimeFrame);
        _playerScore.SetPaused(_paused);
    }

    public void EndGame()
    {
        _timeWarper.SetTimeFrame(TimeFrame.Paused);
        _playerScore.EndScore();
    }

    private void SetTimeRemaining(float timeRemaining)
    {
        _timeRemaining = timeRemaining;
        _onTimeRemainingChanged?.Invoke(Percentage);
    }

    private IEnumerator Start()
    {
        SetTimeRemaining(0.0f);
        while (true)
        {
            SetTimeRemaining(_timeInterval);
            
            switch (_warped)
            {
                case false:
                {
                    _characterSpawner.SpawnWave();
                    _characterSpawner.IncrementWave();
                    _upgradeSpawner.Spawn();
                    _playerController.EnableAction(false);
                    _playerController.ResetLauncher();
                } break;
                case true:
                {
                    _playerController.EnableAction(true);
                } break;
            }

            _timeWarper.SetTimeFrame(CurTimeFrame);

            while(_timeRemaining > 0.0f)
            {
                yield return new WaitUntil(() => !_paused);
                yield return null;
                SetTimeRemaining(_timeRemaining - Time.unscaledDeltaTime);
            }
            
            _warped = !_warped;
        }
    }
}
