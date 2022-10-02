using UnityEngine;

public class EnemyInitializer : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private GameObject _player;

    public void InitializeEnemy(GameObject ai)
    {
        if (!ai.TryGetComponent(out AIController controller) ||
            !ai.TryGetComponent(out Health health))
            return;

        controller.SetTarget(_player);
        health.Revive();
    }
}
