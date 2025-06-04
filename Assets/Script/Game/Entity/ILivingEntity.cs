namespace Script.Game.Entity
{
    public interface ILivingEntity
    {
        string Id { get; set; }
        string Name { get; set; }
        int Health { get; set; }
        int MaxHealth { get; set; }
        int Armor { get; set; }
        int MagicResist { get; set; }
        int AttackDamage { get; set; }
        int AbilityPower { get; set; }
        float MoveSpeed { get; set; }
        bool Moving { get; set; }
        float PosX { get; set; }
        float PosZ { get; set; }
        float PosXDesired { get; set; }
        float PosZDesired { get; set; }
        int Team { get; set; }
    }
}