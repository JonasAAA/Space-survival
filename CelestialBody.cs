using Microsoft.Xna.Framework;

namespace Game1
{
    public class CelestialBody : RoundObject
    {
        public CelestialBody(float mass, Vector2 position, Vector2 velocity, float angle, float angularVel, Image image, Image imageForMinimap)
            : base(mass, position, velocity, angle, angularVel, image, imageForMinimap)
        { }
    }
}
