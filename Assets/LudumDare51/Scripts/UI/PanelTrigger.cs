using UnityEngine;
using UnityEngine.Events;

public class PanelTrigger : MonoBehaviour
{
    [Header("Events")]
    [SerializeField] private UnityEvent _onEnabled;

    private void OnEnable()
    {
        _onEnabled?.Invoke();
    }
}
