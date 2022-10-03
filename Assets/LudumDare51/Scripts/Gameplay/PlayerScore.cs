using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class PlayerScore : MonoBehaviour
{
    public int BaseScore => _baseScore;
    public int Kills => _kills;
    public int TotalScore => _baseScore + (_kills * _killBonus);

    [Header("Events")]
    [SerializeField] private UnityEvent<int> _onScoreChanged;
    [SerializeField] private UnityEvent<int> _onKillsChanged;
    [SerializeField] private UnityEvent<int> _onScoreEnded;

    [Header("Properties")]
    [SerializeField] private bool _paused = false;
    [SerializeField] private int _killBonus = 500;
    [SerializeField] private float _passiveDelay = 0.02f;

    private bool _endScore = false;
    private int _baseScore = 0;
    private int _kills = 0;

    public void SetPaused(bool paused)
    {
        _paused = paused;
    }

    public void EndScore()
    {
        _endScore = true;
        _onScoreEnded?.Invoke(TotalScore);
    }

    public void AddKill()
    {
        if (_endScore)
            return;

        _kills++;
        _onKillsChanged?.Invoke(_kills);
    }

    private void SetBaseScore(int score)
    {
        if (score < 1)
            return;

        _baseScore = score;
        _onScoreChanged?.Invoke(_baseScore);
    }

    private IEnumerator Start()
    {
        _onScoreChanged?.Invoke(0);
        _onKillsChanged?.Invoke(0);
        _onScoreChanged?.Invoke(0);

        float current = 0.0f;
        while(!_endScore)
        {
            current = _passiveDelay;
            while(current > 0.0f)
            {
                yield return new WaitUntil(() => !_paused);
                yield return null;
                current -= Time.unscaledDeltaTime;
            }

            SetBaseScore(_baseScore + 1);
        }
    }
}
