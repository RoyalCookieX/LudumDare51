using System.Collections.Generic;
using UnityEngine;

public enum BoundsType {
    Circle,
    Box
}

public class UpgradeSpawner : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Upgrade _upgrade;

    [Header("Properties")]
    [SerializeField] private BoundsType _boundsType;
    [SerializeField] private Vector2 _bounds;
    [SerializeField] private List<LauncherAsset> _launcherAssets;

    [ContextMenu("Spawn Upgrade")]
    public Upgrade Spawn()
    {
        Vector2 targetPosition = Vector2.zero;
        float boundsHalfX = _bounds.x / 2.0f;
        float boundsHalfY = _bounds.y / 2.0f;
        switch (_boundsType)
        {
            case BoundsType.Circle: targetPosition = Random.insideUnitCircle * boundsHalfX; break;
            case BoundsType.Box: targetPosition = new Vector2(Random.Range(-boundsHalfX, boundsHalfX), Random.Range(-boundsHalfY, boundsHalfY)); break;
        }
        _upgrade.transform.position = targetPosition;
        _upgrade.gameObject.SetActive(true);
        switch (Random.Range(0, 1)) {
            case 0: _upgrade.SetAsLauncher(_launcherAssets[Random.Range(0, _launcherAssets.Count)]); break;
        }
        return _upgrade;
    }

    private void Start()
    {
        Spawn();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        switch (_boundsType) {
            case BoundsType.Circle: Gizmos.DrawWireSphere(transform.position, _bounds.x / 2.0f); break;
            case BoundsType.Box: Gizmos.DrawWireCube(transform.position, _bounds); break;
        }
    }
}
