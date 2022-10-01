using UnityEngine;

public class TimeWarper : MonoBehaviour
{
    public void Warp(float timeScale)
    {
        TimeScaler.SetScale(timeScale);
    }
}
