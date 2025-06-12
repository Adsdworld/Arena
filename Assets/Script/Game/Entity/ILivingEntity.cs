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
        bool HasArrived { get; set; }
        float PosX { get; set; }
        float PosZ { get; set; }
        float PosY { get; set; }
        float PosSkinX { get; set; }
        float PosSkinZ { get; set; }
        float PosSkinY { get; set; }
        float SkinScale { get; set; }
        string SkinAnimation { get; set; }
        float SkinAnimationSpeed { get; set; }
        float PosXDesired { get; set; }
        float PosZDesired { get; set; }
        float PosYDesired { get; set; }
        float RotationY { get; set; }
        int Team { get; set; }
        long CooldownQStart { get; set; }
        long CooldownWStart { get; set; }
        long CooldownEStart { get; set; }
        long CooldownRStart { get; set; }
        long CooldownQEnd { get; set; }
        long CooldownWEnd { get; set; }
        long CooldownEEnd { get; set; }
        long CooldownREnd { get; set; }
        long CooldownQMs { get; set; }
        long CooldownWMs { get; set; }
        long CooldownEMs { get; set; }
        long CooldownRMs { get; set; }
    }
}