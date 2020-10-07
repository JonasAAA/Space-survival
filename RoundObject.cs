// get rid of hardcoded constants

using Microsoft.Xna.Framework;

namespace Game1
{
    public class RoundObject : MyObject
    {
        public bool IsAlive { get; private set; }
        
        public readonly float radius;

        private readonly Image image, imageForMinimap;

        public RoundObject(float mass, Vector2 position, Vector2 velocity, float angle, float angularVel, Image image, Image imageForMinimap)
            : base(mass, angularMass: .125f * mass * image.width * image.width, position, velocity, angle, angularVel)
        {
            IsAlive = true;
            radius = image.width * .5f;
            this.image = image;
            this.imageForMinimap = imageForMinimap;
        }

        public bool Intersects(RoundObject other)
            => this != other && Vector2.Distance(Position, other.Position) < radius + other.radius;

        public void Collide(RoundObject other)
        {
            if (Intersects(other))
                Impact(other);
        }

        public virtual void Collide(Bullet other)
        {
            if (Intersects(other))
                Impact(other);
        }

        protected virtual void Impact(RoundObject other)
        { }

        protected virtual void Impact(Bullet other)
        { }

        public virtual void Die()
        {
            IsAlive = false;
        }

        public virtual void Draw()
        {
            image.Draw(Position, Angle);
        }

        public virtual void MinimapDraw()
        {
            imageForMinimap.Draw(Position, Angle);
        }

        public RoundObject CloneForPred()
            => new RoundObject(mass, Position, velocity, Angle, angularVel, image, imageForMinimap);
    }
}