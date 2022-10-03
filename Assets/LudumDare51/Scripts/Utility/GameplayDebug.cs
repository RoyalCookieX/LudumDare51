using UnityEngine;

public class GameplayDebug : MonoBehaviour
{
    private int _wave;
    private int _waveSize;
    private float _health;
    private float _cooldown;
    private float _timeRemaining;

    public void SetWave(int wave, int waveSize)
    {
        _wave = wave;
        _waveSize = waveSize;
    }

    public void SetTimeRemaining(float timeRemaining)
    {
        _timeRemaining = timeRemaining;
    }

    public void SetHealth(float health)
    {
        _health = health;
    }

    public void SetCooldown(float cooldown)
    {
        _cooldown = cooldown;
    }

#if UNITY_EDITOR
    private void OnGUI()
    {
        GUI.Label(new Rect(Vector2.up *  0.0f, new Vector2(200, 20)), $"Wave: {_wave}");
        GUI.Label(new Rect(Vector2.up * 20.0f, new Vector2(200, 20)), $"Wave Size: {_waveSize}");
        GUI.Label(new Rect(Vector2.up * 40.0f, new Vector2(200, 20)), $"Time Remaining: {_timeRemaining:00.##%}");
        GUI.Label(new Rect(Vector2.up * 60.0f, new Vector2(200, 20)), $"Health: {_health:00.##%}");
        GUI.Label(new Rect(Vector2.up * 80.0f, new Vector2(200, 20)), $"Cooldown: {_cooldown:00.##%}");
    }
#endif
}
