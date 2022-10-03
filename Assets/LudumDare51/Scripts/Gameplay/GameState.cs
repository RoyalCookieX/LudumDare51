using System.Collections;
using UnityEngine;

public class GameState : MonoBehaviour
{
    private TimeFrame CurTimeFrame => _warped ? TimeFrame.Warped : TimeFrame.Normal;

    [Header("Components")]
    [SerializeField] private TimeWarper _timeWarper;
    [SerializeField] private CharacterSpawner _characterSpawner;
    [SerializeField] private UpgradeSpawner _upgradeSpawner;
    [SerializeField] private PlayerController _playerController;

    [Header("Properties")]
    [SerializeField] private bool _warped = false;
    [SerializeField] private bool _paused = false;
    [SerializeField] private float _timeInterval = 10.0f;

    public void SetPaused(bool paused)
    {
        if (paused == _paused)
            return;

        _paused = paused;
        _timeWarper.SetTimeFrameImmediate(_paused ? TimeFrame.Paused : CurTimeFrame);
    }

    private IEnumerator Start()
    {
        float current = 0.0f;
        while(true)
        {
            current = _timeInterval;
            
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

            while(current > 0.0f)
            {
                yield return new WaitUntil(() => !_paused);
                yield return null;
                current -= Time.unscaledDeltaTime;
            }
            
            _warped = !_warped;
        }
    }
}
