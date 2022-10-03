using UnityEngine;

public class GameplayDebug : MonoBehaviour
{
    private int _wave;
    private int _waveSize;
    private float _health;
    private float _cooldown;
    private Camera _mainCamera;

    public void SetWave(int wave, int waveSize)
    {
        _wave = wave;
        _waveSize = waveSize;
    }

    public void SetHealth(float health)
    {
        _health = health;
    }

    public void SetCooldown(float cooldown)
    {
        _cooldown = cooldown;
    }

    private void Start()
    {
        _mainCamera = Camera.main;
    }

#if UNITY_EDITOR
    private void OnGUI()
    {
        GUI.Label(new Rect(Vector2.up *  0.0f, new Vector2(200, 20)), $"Wave: {_wave}");
        GUI.Label(new Rect(Vector2.up * 20.0f, new Vector2(200, 20)), $"Wave Size: {_waveSize}");
        GUI.Label(new Rect(Vector2.up * 40.0f, new Vector2(200, 20)), $"Health: {_health:0.##%}");
        GUI.Label(new Rect(Vector2.up * 60.0f, new Vector2(200, 20)), $"Cooldown: {_cooldown:0.##%}");
    }
#endif
}
