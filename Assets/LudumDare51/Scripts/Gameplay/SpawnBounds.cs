using UnityEngine;

public enum BoundsType
{
    Circle,
    Box
}

[System.Serializable]
public class SpawnBounds
{
    public BoundsType Type => _type;
    public Vector2 Bounds => _bounds;
    public Vector2 TargetPosition => _target ? (Vector2)_target.position : Vector2.zero;

    [SerializeField] private BoundsType _type;
    [SerializeField] private Vector2 _bounds;
    [SerializeField] private Transform _target;

    public Vector2 Evaluate()
    {
        Vector2 offset = Vector2.zero;
        float boundsHalfX = _bounds.x / 2.0f;
        float boundsHalfY = _bounds.y / 2.0f;
        switch (_type)
        {
            case BoundsType.Circle: offset = Random.insideUnitCircle * boundsHalfX; break;
            case BoundsType.Box: offset = new Vector2(Random.Range(-boundsHalfX, boundsHalfX), Random.Range(-boundsHalfY, boundsHalfY)); break;
        }
        return TargetPosition + offset;
    }

#if UNITY_EDITOR
    public void DrawGizmos(Color color)
    {
        Gizmos.color = color;
        switch (_type)
        {
            case BoundsType.Circle: Gizmos.DrawWireSphere(TargetPosition, _bounds.x / 2.0f); break;
            case BoundsType.Box: Gizmos.DrawWireCube(TargetPosition, _bounds); break;
        }
    }
#endif
}
