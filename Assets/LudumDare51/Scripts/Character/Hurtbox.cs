public interface IHurtbox
{
    int HealthID { get; }
    int Damage { get; }

    void SetAsset(HealthAsset asset);
}
