using Microsoft.Xna.Framework;

namespace Game1
{
    public class Engine
    {
        private MyObject parent;
        private readonly Vector2 relPos, relDir;
        private readonly float thrust;

        // crurrently relDir means relative direction of thrust
        // although it may be better if it meant relative diretion of engine
        public Engine(Vector2 relPos, Vector2 relDir, float thrust)
        {
            this.relPos = relPos;
            this.relDir = relDir;
            this.thrust = thrust;
            parent = null;
        }

        public void SetParent(MyObject parent)
        {
            this.parent = parent;
        }

        public void Update(bool on)
        {
            if (!on)
                return;
            Vector2 actForce = parent.ForceToGlobFrame(relForce: relDir * thrust),
                actPos = parent.PosToGlobFrame(relPos);
            parent.ActOn(actForce, actPos);
        }
    }
}
