using Microsoft.Xna.Framework;

namespace Game1
{
    public class HealthBar
    {
        public bool IsAlive { get; private set; }
        private readonly int maxHealth;
        private int health;
        private readonly PartialRectangle healthBar;
        private readonly float distAboveParent;

        public HealthBar(int health, float width, float height, Color color, Color healthColor, float distAboveParent)
        {
            maxHealth = health;
            this.health = health;
            healthBar = new PartialRectangle(width, height, color, healthColor);
            this.distAboveParent = distAboveParent;
        }

        public void TakeDamage(int damage)
        {
            health -= damage;
            IsAlive = health > 0;
        }

        public void Draw(Vector2 parentPos)
        {
            healthBar.Draw(parentPos - new Vector2(0, distAboveParent), (float)health / maxHealth);
        }
    }
}
