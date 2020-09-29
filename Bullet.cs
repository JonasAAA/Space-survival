using Microsoft.Xna.Framework;

namespace Game1
{
    public class Bullet : RoundObject
    {
        public new class Parameters : RoundObject.Parameters
        {
            public int damage;

            public Parameters(string imageName, float radius, float mass, int damage, Color color)
                : base(imageName, radius, mass, color)
            {
                this.damage = damage;
            }
        }

        public readonly int damage;

        public Bullet(Parameters parameters, InitValues initValues)
            : base(parameters, initValues)
        {
            damage = parameters.damage;
        }

        public override void Collide(RoundObject other)
        {
            if (IfIntersects(other))
                Die();
            base.Collide(other);
        }
    }
}
