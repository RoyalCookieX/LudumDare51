public interface IHurtbox
{
    int TeamID { get; }
    int Damage { get; }

    void SetAsset(TeamAsset asset);
}
