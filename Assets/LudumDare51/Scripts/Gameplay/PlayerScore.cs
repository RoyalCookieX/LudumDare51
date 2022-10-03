using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class PlayerScore : MonoBehaviour
{
    public int Score => _score;

    [Header("Events")]
    [SerializeField] private UnityEvent<int> _onScoreChanged;
    [SerializeField] private UnityEvent<int> _onScoreEnded;

    [Header("Properties")]
    [SerializeField] private float _passiveDelay = 0.02f;
    [SerializeField] private bool _paused = false;

    private bool _endScore = false;
    private int _score = 0;

    public void SetPaused(bool paused)
    {
        _paused = paused;
    }

    public void SetScore(int score)
    {
        if (score < 1)
            return;

        _score = score;
        _onScoreChanged?.Invoke(_score);
    }

    public void EndScore()
    {
        _endScore = true;
        _onScoreEnded?.Invoke(_score);
    }

    public void AddBonus(int bonus)
    {
        if (_endScore)
            return;

        SetScore(_score + bonus);
    }

    private IEnumerator Start()
    {
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

            SetScore(_score + 1);
        }
    }
}
