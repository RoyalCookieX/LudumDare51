using UnityEngine;

[CreateAssetMenu(menuName = "LudumDare51/Assets/Health")]
public class HealthAsset : ScriptableObject
{
    public int ID => _id;
    public Color Color => _color;

    [Header("Properties")]
    [SerializeField] private int _id = 0;

    [Header("Display")]
    [SerializeField] private Color _color;
}
