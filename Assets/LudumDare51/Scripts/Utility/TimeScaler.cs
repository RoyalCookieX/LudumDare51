using UnityEngine;

public static class TimeScaler
{
    const float FIXED_TIMESTEP = 0.02f;

    public static float GetScale()
    {
        return Time.timeScale;
    }

    public static void SetScale(float timeScale)
    {
        Time.timeScale = timeScale;
        Time.fixedDeltaTime = Time.timeScale * FIXED_TIMESTEP;
    }
}
