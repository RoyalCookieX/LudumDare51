using UnityEngine;

enum FollowMode
{ 
    None,
    WorldPosition,
    Transform
}

enum UpdateMode
{ 
    Explicit,
    Update,
}

public class Rotator : MonoBehaviour
{
    [Header("Follow")]
    [SerializeField] private FollowMode _followMode = FollowMode.None;
    [SerializeField] private UpdateMode _updateMode = UpdateMode.Update;
    [SerializeField] private Vector2 _followPosition;
    [SerializeField] private Transform _followTransform;

    [Header("Components")]
    [SerializeField] private Transform _target;

    public void SetFollowPosition(Vector2 target)
    {
        _followPosition = target;
    }

    public void SetFollowTransform(Transform target)
    {
        _followTransform = target;
    }

    public void FollowTarget()
    {
        if (!_target)
        {
            Debug.LogWarning("Invalid Target Transform!");
            return;
        }

        Vector3 targetPosition = Vector2.zero;
        switch (_followMode)
        {
            case FollowMode.None:
                break;
            case FollowMode.WorldPosition:
            {
                targetPosition = _followPosition;
                break;
            }
            case FollowMode.Transform:
            {
                if (!_followTransform)
                {
                    Debug.LogWarning("Invalid Follow Transform!");
                    return;
                }
                targetPosition = _followTransform.position;
            }
            break;
        }
        Vector3 targetDirection = (targetPosition - _target.position).normalized;
        float angle = Vector2.SignedAngle(Vector2.up, targetDirection);
        _target.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
    private void Update()
    {
        if (_updateMode != UpdateMode.Update)
            return;
        FollowTarget();
    }
}