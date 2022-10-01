using UnityEngine;
using UnityEngine.Events;

public enum UpgradeType
{
    Launcher
}

public class Upgrade : MonoBehaviour
{
    public UpgradeType Type => _type;
    public LauncherAsset LauncherAsset => _launcherAsset;

    [Header("Events")]
    [SerializeField] private UnityEvent _onConsumed;

    [Header("Properties")]
    [SerializeField] UpgradeType _type;
    [SerializeField] private LauncherAsset _launcherAsset;

    public void Consume()
    {
        _onConsumed?.Invoke();
    }
}
