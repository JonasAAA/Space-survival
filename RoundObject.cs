using Microsoft.Xna.Framework;

namespace Game1
{
    public abstract class RoundObject : MyObject
    {
        public bool IsAlive { get; private set; }
        
        public readonly float radius;

        private readonly RoundImage image;

        public RoundObject(float mass, float radius, Vector2 position, Vector2 velocity, float angle, float angularVel, string imageName, Color color)
            : base(mass, angularMass: .5f * mass * radius * radius, position, velocity, angle, angularVel)
        {
            IsAlive = true;
            this.radius = radius;
            image = new RoundImage(imageName, radius, color);
        }

        private bool IfIntersects(RoundObject other)
            => this != other && Vector2.Distance(Position, other.Position) < radius + other.radius;

        public void Collide(RoundObject other)
        {
            if (IfIntersects(other))
                Impact(other);
        }

        public virtual void Collide(Bullet other)
        {
            if (IfIntersects(other))
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
    }
}


//public class Parameters
//{
//    public string imageName;
//    public float radius;
//    public float mass;
//    public Color color;

//    public Parameters(string imageName, float radius, float mass, Color color)
//    {
//        this.imageName = imageName;
//        this.radius = radius;
//        this.mass = mass;
//        this.color = color;
//    }
//}

//public struct InitValues
//{
//    public Vector2 position, velocity;
//    public float rotation, rotVel;

//    public InitValues(Vector2 position, float rotation, Vector2 velocity, float rotVel)
//    {
//        this.position = position;
//        this.rotation = rotation;
//        this.velocity = velocity;
//        this.rotVel = rotVel;
//    }
//}
