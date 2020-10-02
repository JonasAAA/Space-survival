using Microsoft.Xna.Framework;

namespace Game1
{
    public class Gun
    {
        private readonly Vector2 relPos, relDir;
        private MyObject parent;
        private readonly Bullet.Parameters bulletParams;
        private readonly float bulletSpeed, shootPause;
        private float timeSinceLastShot;

        public Gun(Vector2 relPos, Vector2 relDir, Bullet.Parameters bulletParams, float bulletSpeed, float shootPause)
        {
            this.relPos = relPos;
            this.relDir = relDir;
            parent = null;
            this.bulletParams = bulletParams;
            this.bulletSpeed = bulletSpeed;
            this.shootPause = shootPause;
            timeSinceLastShot = shootPause;
        }

        public void SetParent(MyObject parent)
        {
            this.parent = parent;
        }

        public void Update(float elapsed, bool shoot)
        {
            timeSinceLastShot += elapsed;
            if (!shoot || timeSinceLastShot < shootPause)
                return;

            Vector2 bulletPos = parent.PosToGlobFrame(relPos),
                bulletVel = parent.VelToGlobFrame(relDir * bulletSpeed);

            Bullet bullet = new Bullet(bulletParams, bulletPos, bulletVel, angle: parent.Angle, angularVel: parent.AngularVel);
            // so that it does not collide with the ship which fired it
            bullet.Update(elapsed * .5f);
            C.newBullets.Add(bullet);
            timeSinceLastShot = 0;
        }
    }
}
