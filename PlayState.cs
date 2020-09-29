using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Game1
{
    public class PlayState
    {
        private readonly Camera camera;
        private readonly List<RoundObject> roundObjects;

        public PlayState()
        {
            camera = new Camera();

            roundObjects = new List<RoundObject>()
            {
                new CelestialBody(new RoundObject.Parameters(imageName: "disk", radius: 100, mass: 100000, Color.Yellow), new RoundObject.InitValues(position: new Vector2(-50, 0), rotation: 0, velocity: new Vector2(0, -10), rotVel: 0)),
                new CelestialBody(new RoundObject.Parameters(imageName: "disk", radius: 10, mass: 10000, Color.Brown), new RoundObject.InitValues(position: new Vector2(500, 0), rotation: 0, velocity: new Vector2(0, 100), rotVel: 0)),
            };
            
            RoundObject.Parameters playerParams = new RoundObject.Parameters(imageName: "unit", radius: 10, mass: 0, Color.White);
            RoundObject.InitValues playerInitValues = new RoundObject.InitValues(position: new Vector2(-500, 0), rotation: 0, velocity: Vector2.Zero, rotVel: 0);
            HealthBar healthBar = new HealthBar(health: 200, width: 100, height: 20, color: Color.White, healthColor: Color.Red, distAboveParent: 20);
            Player player = new Player(playerParams, healthBar, thrustPower: 100, RCSPower: 10, playerInitValues, forwardKey: Keys.W, turnLeftKey: Keys.A, moveLeftKey: Keys.Q, turnRightKey: Keys.D, moveRightKey: Keys.E, shootKey: Keys.Space);
            Bullet.Parameters bulletParams = new Bullet.Parameters(imageName: "bullet", radius: 5, mass: 0, damage: 50, Color.Black);
            Gun gun = new Gun(bulletParams, bulletSpeed: 500, shootPause: 0.05f);
            player.AddGun(gun);
            roundObjects.Add(player);
        }

        public void Update(float elapsed)
        {
            // calculate gravity, but objects with 0 mass have no gravitational pull
            foreach (RoundObject otherRoundObject in roundObjects)
                if (otherRoundObject.mass != 0)
                    foreach (RoundObject roundObject in roundObjects)
                        roundObject.GravityPull(otherRoundObject);

            foreach (RoundObject roundObject in roundObjects)
                roundObject.Update(elapsed);

            foreach (RoundObject roundObject in roundObjects)
                foreach (RoundObject otherRoundObject in roundObjects)
                    roundObject.Collide(otherRoundObject);

            roundObjects.RemoveAll(roundObject => !roundObject.IsAlive);
            roundObjects.AddRange(C.newRoundObjects);
            C.newRoundObjects.Clear();

            camera.Update(center: Vector2.Zero, rotation: 0);
        }

        public void Draw()
        {
            camera.BeginDraw();

            foreach (RoundObject roundObject in roundObjects)
                roundObject.Draw();

            camera.EndDraw();
        }
    }
}
