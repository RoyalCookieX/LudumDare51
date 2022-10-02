using UnityEngine;

public class AIController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private ProjectileLauncher _launcher;
    [SerializeField] private Rotator _rotator;

    public void SetTarget(GameObject target)
    {
        _rotator.SetFollowTransform(target.transform);
    }

    private void Update()
    {
        _launcher.Launch();
    }
}
