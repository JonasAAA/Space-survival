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
        private readonly Predictor predictor;
        private readonly Minimap minimap;

        public PlayState()
        {
            camera = new Camera(width: C.screenWidth, height: C.screenHeight, scale: 1);
            minimap = new Minimap(width: 500, height: 500, left: false, top: true, scale: .01f);

            nonBulletObjects = new List<RoundObject>()
            {
                new CelestialBody(mass: 1e18f, position: new Vector2(-500, 0), velocity: new Vector2(0, -10), angle: 0, angularVel: 0, image: new Image(imageName: "planet", width: 200, Color.Yellow), imageForMinimap: new Image(imageName: "disk", width: 2000, Color.Yellow)),
                new CelestialBody(mass: 1e17f, position: new Vector2(5000, 0), velocity: new Vector2(0, 100), angle: 0, angularVel: 0, image: new Image(imageName: "planet", width: 20, Color.Yellow), imageForMinimap: new Image(imageName: "disk", width: 2000, Color.Yellow)),
            };
            bullets = new List<Bullet>();

            HealthBar playerHealthBar = new HealthBar(health: 200, width: 20, height: 5, color: Color.White, healthColor: Color.Red, distAboveParent: 20);
            Image playerImage = new Image(imageName: "unit", width: 20, Color.White),
                playerImageForMinimap = new Image(imageName: "triangle", width: 1000, Color.Green);
            player = new Player(mass: 1, position: new Vector2(500, 500), velocity: Vector2.Zero, angle: 0, maxAngSpeed: 3, playerHealthBar, thrust: 100, RCSThrust: 10, image: playerImage, imageForMinimap: playerImageForMinimap, forwardKey: Keys.W, turnLeftKey: Keys.A, moveLeftKey: Keys.Q, turnRightKey: Keys.D, moveRightKey: Keys.E, shootKey: Keys.Space);
            Image playerBulletImage = new Image(imageName: "bullet", width: 10, Color.White),
                playerBulletImageForMinimap = new Image(imageName: "disk", width: 100, Color.Red);
            Bullet.Parameters bulletParams = new Bullet.Parameters(mass: .1f, damage: 50, playerBulletImage, playerBulletImageForMinimap);
            Gun playerGun = new Gun(relPos: new Vector2(0, -10), relDir: new Vector2(0, -1), bulletParams, bulletSpeed: 1000, shootPause: .5f);
            player.AddGun(playerGun);
            nonBulletObjects.Add(player);
            int playerInd = nonBulletObjects.Count - 1;

            //HealthBar enemyHealthBar = new HealthBar(health: 100, width: 20, height: 5, color: Color.White, healthColor: Color.Red, distAboveParent: 20);
            //Enemy enemy = new Enemy(mass: 1, position: new Vector2(100, 0), velocity: new Vector2(0, 200), angle: 0, maxAngSpeed: 3, imageName: "unit", Color.Red, enemyHealthBar, thrust: 0, RCSThrust: 0);
            //Gun enemyGun = new Gun(relPos: new Vector2(0, -10), relDir: new Vector2(0, -1), bulletParams, bulletSpeed: 1000, shootPause: .005f);
            //enemy.AddGun(enemyGun);
            //nonBulletObjects.Add(enemy);
            predictor = new Predictor(predictableObjs: nonBulletObjects, playerInd);
        }

        //public PlayState()
        //{
        //    camera = new Camera(width: C.screenWidth, height: C.screenHeight, scale: 1);
        //    minimap = new Minimap(width: 500, height: 500, left: false, top: true, scale: .01f);

        //    nonBulletObjects = new List<RoundObject>()
        //    {
        //        new CelestialBody(mass: 1e17f, position: new Vector2(-50, 0), velocity: new Vector2(0, -10), angle: 0, angularVel: 0, image: new Image(imageName: "planet", width: 200, Color.Yellow), imageForMinimap: new Image(imageName: "disk", width: 2000, Color.Yellow)),
        //        new CelestialBody(mass: 1e16f, position: new Vector2(500, 0), velocity: new Vector2(0, 100), angle: 0, angularVel: 0, image: new Image(imageName: "planet", width: 20, Color.Yellow), imageForMinimap: new Image(imageName: "disk", width: 2000, Color.Yellow)),
        //        //new CelestialBody(mass: 1e17f, radius: 10, position: new Vector2(500, 0), velocity: new Vector2(0, 100), angle: 0, angularVel: 0, imageName: "planet", Color.Yellow),
        //        //new CelestialBody(mass: 1e17f, radius: 10, position: new Vector2(-500, 0), velocity: new Vector2(0, -100), angle: 0, angularVel: 0, imageName: "planet", Color.Yellow),
        //        //new CelestialBody(mass: 1e17f, radius: 10, position: new Vector2(1000, 0), velocity: new Vector2(0, 50), angle: 0, angularVel: 0, imageName: "planet", Color.Yellow),
        //        //new CelestialBody(mass: 1e17f, radius: 10, position: new Vector2(-1000, 0), velocity: new Vector2(0, -50), angle: 0, angularVel: 0, imageName: "planet", Color.Yellow),
        //        //new CelestialBody(mass: 1e17f, radius: 10, position: new Vector2(0, 500), velocity: new Vector2(-100, 0), angle: 0, angularVel: 0, imageName: "planet", Color.Yellow),
        //        //new CelestialBody(mass: 1e17f, radius: 10, position: new Vector2(0, -500), velocity: new Vector2(100, 0), angle: 0, angularVel: 0, imageName: "planet", Color.Yellow),
        //    };
        //    bullets = new List<Bullet>();

        //    HealthBar playerHealthBar = new HealthBar(health: 200, width: 20, height: 5, color: Color.White, healthColor: Color.Red, distAboveParent: 20);
        //    Image playerImage = new Image(imageName: "unit", width: 20, Color.White),
        //        playerImageForMinimap = new Image(imageName: "disk", width: 200, Color.Green);
        //    player = new Player(mass: 1, position: new Vector2(500, 500), velocity: Vector2.Zero, angle: 0, maxAngSpeed: 3, playerHealthBar, thrust: 100, RCSThrust: 10, image: playerImage, imageForMinimap: playerImageForMinimap, forwardKey: Keys.W, turnLeftKey: Keys.A, moveLeftKey: Keys.Q, turnRightKey: Keys.D, moveRightKey: Keys.E, shootKey: Keys.Space);
        //    Image playerBulletImage = new Image(imageName: "bullet", width: 10, Color.White),
        //        playerBulletImageForMinimap = new Image(imageName: "disk", width: 100, Color.Red);
        //    Bullet.Parameters bulletParams = new Bullet.Parameters(mass: .1f, damage: 50, playerBulletImage, playerBulletImageForMinimap);
        //    Gun playerGun = new Gun(relPos: new Vector2(0, -10), relDir: new Vector2(0, -1), bulletParams, bulletSpeed: 1000, shootPause: .5f);
        //    player.AddGun(playerGun);
        //    nonBulletObjects.Add(player);
        //    int playerInd = nonBulletObjects.Count - 1;

        //    //HealthBar enemyHealthBar = new HealthBar(health: 100, width: 20, height: 5, color: Color.White, healthColor: Color.Red, distAboveParent: 20);
        //    //Enemy enemy = new Enemy(mass: 1, position: new Vector2(100, 0), velocity: new Vector2(0, 200), angle: 0, maxAngSpeed: 3, imageName: "unit", Color.Red, enemyHealthBar, thrust: 0, RCSThrust: 0);
        //    //Gun enemyGun = new Gun(relPos: new Vector2(0, -10), relDir: new Vector2(0, -1), bulletParams, bulletSpeed: 1000, shootPause: .005f);
        //    //enemy.AddGun(enemyGun);
        //    //nonBulletObjects.Add(enemy);
        //    //predictor = new Predictor(player);
        //    predictor = new Predictor(predictableObjs: nonBulletObjects, playerInd);
        //}

        public void Update(float elapsed)
        {
            elapsed = 1f / 60;
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

            foreach (RoundObject roundObject in nonBulletObjects.Concat(bullets))
                roundObject.Reposition(center: player.Position);

            nonBulletObjects.RemoveAll(roundObject => !roundObject.IsAlive);
            bullets.RemoveAll(bullet => !bullet.IsAlive);
            bullets.AddRange(C.newBullets);
            C.newBullets.Clear();

            if (player.IsAlive)
                predictor.Update(frames: 1000, elapsed);

            

            //camera.Update(center: Vector2.Zero, rotation: 0);
            camera.Update(center: player.Position, rotation: 0);
            //camera.Update(center: player.Position, rotation: player.Angle);
            minimap.Update(center: player.Position, rotation: 0);
        }

        public void Draw()
        {
            minimap.BeginDrawToMinimap(background: Color.White);
            foreach (RoundObject roundObject in nonBulletObjects.Concat(bullets))
                roundObject.MinimapDraw();
            minimap.EndDrawToMinimap();

            camera.BeginDraw();
            predictor.Draw();
            foreach (RoundObject roundObject in nonBulletObjects.Concat(bullets))
                roundObject.Draw();
            camera.EndDraw();

            C.SpriteBatch.Begin();
            minimap.Draw();
            C.SpriteBatch.End();
        }
    }
}
