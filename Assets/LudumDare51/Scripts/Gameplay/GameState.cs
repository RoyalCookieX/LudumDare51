using System.Collections;
using UnityEngine;

public class GameState : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private TimeWarper _timeWarper;
    [SerializeField] private CharacterSpawner _characterSpawner;
    [SerializeField] private UpgradeSpawner _upgradeSpawner;
    [SerializeField] private PlayerController _playerController;

    [Header("Properties")]
    [SerializeField] private bool _warped = false;
    [SerializeField] private float _timeInterval = 10.0f;

    private IEnumerator Start()
    {
        while(true)
        {
            switch (_warped)
            {
                case false:
                {
                    _characterSpawner.SpawnWave();
                    _characterSpawner.IncrementWave();
                    _upgradeSpawner.Spawn();
                    _playerController.EnableAction(false);
                } break;
                case true:
                {
                    _playerController.EnableAction(true);
                } break;
            }

            TimeFrame timeFrame = _warped ? TimeFrame.Warped : TimeFrame.Normal;
            _timeWarper.SetTimeFrame(timeFrame);
            _warped = !_warped;

            yield return new WaitForSecondsRealtime(_timeInterval);
        }
    }
}
