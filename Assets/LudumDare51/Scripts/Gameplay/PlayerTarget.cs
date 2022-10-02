using UnityEngine;

public class PlayerTarget : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private GameObject _player;

    public void TargetPlayer(GameObject ai)
    {
        if (!ai.TryGetComponent(out AIController controller))
            return;
        controller.SetTarget(_player);
    }
}
