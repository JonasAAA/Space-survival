// get rid of hardcoded constants

using Microsoft.Xna.Framework;
using System;

namespace Game1
{
    public class Engine
    {
        private MyObject parent;
        private readonly Vector2 relPos, relDir;
        private readonly float thrust;
        private readonly Image exhaust;
        private bool on;

        public Engine(Vector2 relPos, Vector2 relDir, float thrust)
        {
            this.relPos = relPos;
            this.relDir = relDir;
            this.thrust = thrust;
            parent = null;
            exhaust = new Image(imageName: "exhaust", width: (float)Math.Sqrt(thrust) * .5f, Color.LightSkyBlue);
            exhaust.SetOrigin(centerX: 0, centerY: -1);
        }

        public void SetParent(MyObject parent)
        {
            this.parent = parent;
        }

        public void Update(bool on)
        {
            this.on = on;
            if (!on)
                return;
            Vector2 actForce = parent.ForceToGlobFrame(relForce: -relDir * thrust),
                actPos = parent.PosToGlobFrame(relPos);
            parent.ActOn(actForce, actPos);
        }

        public void Draw()
        {
            if (!on)
                return;
            Vector2 exhaustPosition = parent.PosToGlobFrame(relPos);
            float exhaustAngle = parent.AngleToGlobalFrame(C.Angle(relDir));

            exhaust.Draw(position: exhaustPosition, angle: exhaustAngle);
        }
    }
}
