using Microsoft.Xna.Framework;

namespace Game1
{
    public class Enemy : Spaceship
    {
        public Enemy(float mass, Vector2 position, Vector2 velocity, float angle, float maxAngSpeed, HealthBar healthBar, float thrust, float RCSThrust, Image image, Image imageForMinimap)
            : base(mass, position, velocity, angle, maxAngSpeed, healthBar, thrust, RCSThrust, image, imageForMinimap)
        { }

        public override void Update(float elapsed)
        {
            shoot = true;
            base.Update(elapsed);
        }
    }
}
