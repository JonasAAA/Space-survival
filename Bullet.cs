using Microsoft.Xna.Framework;

namespace Game1
{
    public class Bullet : RoundObject
    {
        public struct Parameters
        {
            public float mass;
            public int damage;
            public Image image, imageForMinimap;

            public Parameters(float mass, int damage, Image image, Image imageForMinimap)
            {
                this.mass = mass;
                this.damage = damage;
                this.image = image;
                this.imageForMinimap = imageForMinimap;
            }
        }

        public readonly int damage;

        public Bullet(Parameters parameters, Vector2 position, Vector2 velocity, float angle, float angularVel)
            : base(parameters.mass, position, velocity, angle, angularVel, parameters.image, parameters.imageForMinimap)
        {
            damage = parameters.damage;
        }

        protected override void Impact(RoundObject other)
        {
            Die();
        }
    }
}
