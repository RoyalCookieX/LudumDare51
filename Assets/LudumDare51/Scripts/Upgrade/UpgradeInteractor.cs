using UnityEngine;
using UnityEngine.Events;

public class UpgradeInteractor : MonoBehaviour
{
    [Header("Events")]
    [SerializeField] private UnityEvent<LauncherAsset> _onLauncherUpgrade;

    public void ConsumeUpgrade(Upgrade upgrade)
    {
        switch (upgrade.Type)
        {
            case UpgradeType.Launcher: _onLauncherUpgrade?.Invoke(upgrade.LauncherAsset); break;
        }
        upgrade.Consume();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.TryGetComponent(out Upgrade upgrade))
            return;

        ConsumeUpgrade(upgrade);
    }
}
