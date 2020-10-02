using Microsoft.Xna.Framework;

namespace Game1
{
    public class Bullet : RoundObject
    {
        public struct Parameters
        {
            public string imageName;
            public float radius, mass;
            public int damage;
            public Color color;

            public Parameters(string imageName, float radius, float mass, int damage, Color color)
            {
                this.imageName = imageName;
                this.radius = radius;
                this.mass = mass;
                this.damage = damage;
                this.color = color;
            }
        }

        public readonly int damage;

        public Bullet(Parameters parameters, Vector2 position, Vector2 velocity, float angle, float angularVel)
            : base(parameters.mass, parameters.radius, position, velocity, angle, angularVel, parameters.imageName, parameters.color)
        {
            damage = parameters.damage;
        }

        protected override void Impact(RoundObject other)
        {
            Die();
        }
    }
}
