using Microsoft.Xna.Framework;

namespace Game1
{
    public abstract class RoundObject
    {
        public class Parameters
        {
            public string imageName;
            public float radius;
            public float mass;
            public Color color;

            public Parameters(string imageName, float radius, float mass, Color color)
            {
                this.imageName = imageName;
                this.radius = radius;
                this.mass = mass;
                this.color = color;
            }
        }

        public struct InitValues
        {
            public Vector2 position, velocity;
            public float rotation, rotVel;

            public InitValues(Vector2 position, float rotation, Vector2 velocity, float rotVel)
            {
                this.position = position;
                this.rotation = rotation;
                this.velocity = velocity;
                this.rotVel = rotVel;
            }
        }

        public bool IsAlive { get; private set; }
        public Vector2 Position { get; protected set; }
        public float Rotation { get; protected set; }
        public Vector2 Velocity { get; protected set; }
        public float RotVel { get; protected set; }
        public readonly float radius, mass;

        private readonly RoundImage image;

        public RoundObject(Parameters parameters, InitValues initValues)
        {
            IsAlive = true;
            Position = initValues.position;
            Rotation = initValues.rotation;
            Velocity = initValues.velocity;
            RotVel = initValues.rotVel;
            radius = parameters.radius;
            mass = parameters.mass;
            image = new RoundImage(parameters.imageName, radius, parameters.color);
        }

        public void GravityPull(RoundObject other)
        {
            if (this == other)
                return;

            Vector2 direction = other.Position - Position;
            direction.Normalize();
            float distanceSquared = Vector2.DistanceSquared(Position, other.Position);
            Velocity += direction * other.mass / distanceSquared;
        }

        public virtual void Update(float elapsed)
        {
            Position += Velocity * elapsed;
            Rotation += RotVel * elapsed;
        }

        protected bool IfIntersects(RoundObject other)
            => this != other && Vector2.Distance(Position, other.Position) < radius + other.radius;

        public virtual void Collide(RoundObject other)
        {
        }

        public virtual void Die()
        {
            IsAlive = false;
        }

        public virtual void Draw()
        {
            image.Draw(Position, Rotation);
        }
    }
}
