//TODO
//{
//    G needs to be tuned so that player ship mass = 1 would work
//}

using Microsoft.Xna.Framework;

namespace Game1
{
    public class MyObject
    {
        public readonly float mass, angularMass;
        public Vector2 Position { get; private set; }
        public Vector2 Velocity { get; private set; }
        private Vector2 force;
        public float Angle { get; private set; }
        public float AngularVel { get; private set; }
        private float torque;
        // gravitational constant
        private const float G = 6.67408e-11f;
        private Matrix rotByAngle, rotByRevAngle;

        public MyObject(float mass, float angularMass, Vector2 position, Vector2 velocity, float angle, float angularVel)
        {
            this.mass = mass;
            this.angularMass = angularMass;
            Position = position;
            Velocity = velocity;
            Angle = angle;
            AngularVel = angularVel;
            rotByAngle = Matrix.CreateRotationZ(Angle);
            rotByRevAngle = Matrix.CreateRotationZ(-Angle);
        }

        public virtual void Update(float elapsed)
        {
            Velocity += force / mass * elapsed;
            Position += Velocity * elapsed;
            force = Vector2.Zero;

            AngularVel += torque / angularMass * elapsed;
            Angle += AngularVel * elapsed;
            Angle %= MathHelper.TwoPi;
            torque = 0;

            rotByAngle = Matrix.CreateRotationZ(Angle);
            rotByRevAngle = Matrix.CreateRotationZ(-Angle);
        }

        public void GravityPull(RoundObject other)
        {
            if (this == other)
                return;

            Vector2 direction = other.Position - Position;
            direction.Normalize();
            float distanceSquared = Vector2.DistanceSquared(Position, other.Position);
            ActOn(actForce: direction * G * other.mass / distanceSquared, actPos: Position);
        }

        public void ActOn(Vector2 actForce, Vector2 actPos)
        {
            // name relActPos is bad since this name means something else in ActFromInside
            Vector2 relActPos = actPos - Position;
            force += actForce;
            torque += actForce.Y * relActPos.X - actForce.X * relActPos.Y;
        }

        public Vector2 PosToGlobFrame(Vector2 relPos)
            => Position + Vector2.Transform(relPos, rotByAngle);

        public Vector2 VelToGlobFrame(Vector2 relVel)
            => Velocity + Vector2.Transform(relVel, rotByAngle);

        public Vector2 ForceToGlobFrame(Vector2 relForce)
            => Vector2.Transform(relForce, rotByAngle);

        public Vector2 PosToMyFrame(Vector2 position)
            => Vector2.Transform(position - Position, rotByRevAngle);

        public Vector2 VelToMyFrame(Vector2 velocity)
            => Vector2.Transform(velocity - Velocity, rotByRevAngle);

        public Vector2 ForceToMyFrame(Vector2 force)
            => Vector2.Transform(force, rotByRevAngle);
    }
}
