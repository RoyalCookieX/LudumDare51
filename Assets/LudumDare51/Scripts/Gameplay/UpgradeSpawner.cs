using System.Collections.Generic;
using UnityEngine;

public class UpgradeSpawner : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Upgrade _upgrade;

    [Header("Properties")]
    [SerializeField] private SpawnBounds _spawnBounds;
    [SerializeField] private List<LauncherAsset> _launcherAssets;

    [ContextMenu("Spawn Upgrade")]
    public Upgrade Spawn()
    {
        Vector2 targetPosition = _spawnBounds.Evaluate(transform.position);
        _upgrade.transform.position = targetPosition;
        _upgrade.gameObject.SetActive(true);
        switch (Random.Range(0, 1)) {
            case 0: _upgrade.SetAsLauncher(_launcherAssets[Random.Range(0, _launcherAssets.Count)]); break;
        }
        return _upgrade;
    }

    private void OnDrawGizmosSelected()
    {
        _spawnBounds.DrawGizmos(transform.position, Color.green);
    }
}
