using UnityEngine;
using UnityEngine.Events;

public interface ITeamReference
{
    public UnityEvent OnHealthDamaged { get; set; }
    public UnityEvent OnHealthKilled { get; set; }
    public TeamAsset Team { get; }
    public void SetTeam(TeamAsset team);
}

[CreateAssetMenu(menuName = "LudumDare51/Assets/Team")]
public class TeamAsset : ScriptableObject
{
    public int ID => _id;
    public Color Color => _color;

    [Header("Properties")]
    [SerializeField] private int _id = 0;

    [Header("Display")]
    [SerializeField] private Color _color;
}
