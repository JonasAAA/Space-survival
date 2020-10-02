using Microsoft.Xna.Framework;

namespace Game1
{
    public class CelestialBody : RoundObject
    {
        public CelestialBody(float mass, float radius, Vector2 position, Vector2 velocity, float angle, float angularVel, string imageName, Color color)
            : base(mass, radius, position, velocity, angle, angularVel, imageName, color)
        { }
    }
}
