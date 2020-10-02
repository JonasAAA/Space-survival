using Microsoft.Xna.Framework;

namespace Game1
{
    public class Enemy : Spaceship
    {
        public Enemy(float mass, float radius, Vector2 position, Vector2 velocity, float angle, string imageName, Color color, HealthBar healthBar, float thrust, float RCSThrust)
            : base(mass, radius, position, velocity, angle, imageName, color, healthBar, thrust, RCSThrust)
        { }

        public override void Update(float elapsed)
        {
            shoot = true;
            base.Update(elapsed);
        }
    }
}
