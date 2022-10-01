using UnityEngine;

[CreateAssetMenu(menuName = "LudumDare51/Assets/Launcher")]
public class LauncherAsset : ScriptableObject
{
    public float Cooldown => _cooldown;

    [Header("Properties")]
    [SerializeField, Min(0.001f)] private float _cooldown = 0.1f;
}
