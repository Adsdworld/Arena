

namespace Script.Game.Entity.Champion
{
    public class Garen : Entity
    {
        private void Awake()
        {
            maxHealth = 620f;
            currentHealth = maxHealth;

            armor = 36f;
            magicResist = 32f;

            attackDamage = 66f;
            abilityPower = 0f; // Garen est AD

            moveSpeed = 340f;
        }
    }
}