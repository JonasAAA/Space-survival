using Microsoft.Xna.Framework;
using System;

namespace Game1
{
    public class MyObject
    {
        public readonly float mass, angularMass;
        public Vector2 Position { get; protected set; }
        protected Vector2 velocity;
        private Vector2 force;
        public float Angle { get; protected set; }
        protected float angularVel;
        private float torque;
        // gravitational constant
        private const float G = 6.67408e-11f;
        private Matrix rotByAngle, rotByRevAngle;

        public MyObject(float mass, float angularMass, Vector2 position, Vector2 velocity, float angle, float angularVel)
        {
            this.mass = mass;
            this.angularMass = angularMass;
            Position = position;
            this.velocity = velocity;
            force = Vector2.Zero;
            Angle = angle;
            this.angularVel = angularVel;
            torque = 0;
            rotByAngle = Matrix.CreateRotationZ(Angle);
            rotByRevAngle = Matrix.CreateRotationZ(-Angle);
        }

        public virtual void Update(float elapsed)
        {
            velocity += force / mass * elapsed;
            Position += velocity * elapsed;
            force = Vector2.Zero;

            angularVel += torque / angularMass * elapsed;
            Angle += angularVel * elapsed;
            Angle %= MathHelper.TwoPi;
            torque = 0;

            rotByAngle = Matrix.CreateRotationZ(Angle);
            rotByRevAngle = Matrix.CreateRotationZ(-Angle);
        }

        public void GravityPull(MyObject other)
        {
            if (this == other)
                return;

            Vector2 direction = other.Position - Position;
            direction.Normalize();
            float distanceSquared = Vector2.DistanceSquared(Position, other.Position);
            ActOn(actForce: direction * G * mass * other.mass / distanceSquared, actPos: Position);
        }

        public void ActOn(Vector2 actForce, Vector2 actPos)
        {
            // name relActPos is bad since this name means something else in ActFromInside
            Vector2 relActPos = actPos - Position;
            force += actForce;
            float newTorque = actForce.Y * relActPos.X - actForce.X * relActPos.Y;
            // so that not all forces give some torque from floating point errors
            if (Math.Abs(newTorque) < .0001f)
                newTorque = 0;
            torque += newTorque;
        }

        public void Reposition(Vector2 center)
            => Position -= center;

        public Vector2 PosToGlobFrame(Vector2 relPos)
            => Position + Vector2.Transform(relPos, rotByAngle);

        public Vector2 VelToGlobFrame(Vector2 relVel)
            => velocity + Vector2.Transform(relVel, rotByAngle);

        public Vector2 ForceToGlobFrame(Vector2 relForce)
            => Vector2.Transform(relForce, rotByAngle);

        public float AngleToGlobalFrame(float angle)
            => Angle + angle;

        public float AngVelToGlobalFrame(float angularVel)
            => this.angularVel + angularVel;

        public Vector2 PosToMyFrame(Vector2 position)
            => Vector2.Transform(position - Position, rotByRevAngle);

        public Vector2 VelToMyFrame(Vector2 velocity)
            => Vector2.Transform(velocity - this.velocity, rotByRevAngle);

        public Vector2 ForceToMyFrame(Vector2 force)
            => Vector2.Transform(force, rotByRevAngle);
    }
}
