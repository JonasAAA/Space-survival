using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;

namespace Game1
{
    public class PlayState
    {
        private readonly Camera camera;
        private readonly Player player;
        private readonly List<RoundObject> nonBulletObjects;
        private readonly List<Bullet> bullets;

        public PlayState()
        {
            camera = new Camera();

            nonBulletObjects = new List<RoundObject>()
            {
                new CelestialBody(mass: 1e17f, radius: 100, position: Vector2.Zero, velocity: Vector2.Zero, angle: 0, angularVel: 0, imageName: "planet", Color.Yellow),
                //new CelestialBody(new RoundObject.Parameters(imageName: "disk", radius: 100, mass: 100000, Color.Yellow), new RoundObject.InitValues(position: new Vector2(-50, 0), rotation: 0, velocity: new Vector2(0, -10), rotVel: 0)),
                //new CelestialBody(new RoundObject.Parameters(imageName: "disk", radius: 10, mass: 10000, Color.Brown), new RoundObject.InitValues(position: new Vector2(500, 0), rotation: 0, velocity: new Vector2(0, 100), rotVel: 0)),
            };
            bullets = new List<Bullet>();
            
            HealthBar playerHealthBar = new HealthBar(health: 200, width: 20, height: 5, color: Color.White, healthColor: Color.Red, distAboveParent: 20);
            player = new Player(mass: 1, radius: 10, position: new Vector2(-500, 0), velocity: Vector2.Zero, angle: 0, imageName: "unit", Color.White, playerHealthBar, thrust: 100, RCSThrust: 10, forwardKey: Keys.W, turnLeftKey: Keys.A, moveLeftKey: Keys.Q, turnRightKey: Keys.D, moveRightKey: Keys.E, shootKey: Keys.Space);
            Bullet.Parameters bulletParams = new Bullet.Parameters(imageName: "bullet", radius: 5, mass: .1f, damage: 50, Color.Black);
            Gun playerGun = new Gun(relPos: new Vector2(0, -10), relDir: new Vector2(0, -1), bulletParams, bulletSpeed: 500, shootPause: .5f);
            player.AddGun(playerGun);
            nonBulletObjects.Add(player);
            
            HealthBar enemyHealthBar = new HealthBar(health: 100, width: 20, height: 5, color: Color.White, healthColor: Color.Red, distAboveParent: 20);
            Enemy enemy = new Enemy(mass: 1, radius: 10, position: new Vector2(200, 0), velocity: new Vector2(0, 200), angle: 0, imageName: "unit", Color.Red, enemyHealthBar, thrust: 0, RCSThrust: 0);
            Gun enemyGun = new Gun(relPos: new Vector2(0, -10), relDir: new Vector2(0, -1), bulletParams, bulletSpeed: 500, shootPause: .005f);
            enemy.AddGun(enemyGun);
            nonBulletObjects.Add(enemy);
        }

        public void Update(float elapsed)
        {
            // calculate gravity ignoring gravitational pull of bullets
            foreach (RoundObject roundObject in nonBulletObjects.Concat(bullets))
                foreach (RoundObject otherRoundObject in nonBulletObjects)
                    roundObject.GravityPull(otherRoundObject);

            foreach (RoundObject roundObject in nonBulletObjects.Concat(bullets))
                roundObject.Update(elapsed);

            foreach (RoundObject roundObject in nonBulletObjects)
                foreach (RoundObject otherRoundObject in nonBulletObjects)
                    roundObject.Collide(otherRoundObject);

            foreach (RoundObject roundObject in nonBulletObjects)
                foreach (Bullet bullet in bullets)
                {
                    roundObject.Collide(bullet);
                    bullet.Collide(roundObject);
                }

            nonBulletObjects.RemoveAll(roundObject => !roundObject.IsAlive);
            bullets.RemoveAll(bullet => !bullet.IsAlive);
            bullets.AddRange(C.newBullets);
            C.newBullets.Clear();

            camera.Update(center: Vector2.Zero, rotation: 0);
            //camera.Update(center: player.Position, rotation: 0);
            //camera.Update(center: player.Position, rotation: player.Rotation + MathHelper.PiOver2);
        }

        public void Draw()
        {
            camera.BeginDraw();

            foreach (RoundObject roundObject in nonBulletObjects.Concat(bullets))
                roundObject.Draw();

            camera.EndDraw();
        }
    }
}
