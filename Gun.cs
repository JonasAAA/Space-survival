using Microsoft.Xna.Framework;

namespace Game1
{
    public class Gun
    {
        private RoundObject parent;
        private readonly Bullet.Parameters bulletParams;
        private readonly float bulletSpeed, shootPause;
        private float timeSinceLastShot;

        public Gun(Bullet.Parameters bulletParams, float bulletSpeed, float shootPause)
        {
            parent = null;
            this.bulletParams = bulletParams;
            this.bulletSpeed = bulletSpeed;
            this.shootPause = shootPause;
            timeSinceLastShot = shootPause;
        }

        public void SetParent(RoundObject parent)
        {
            this.parent = parent;
        }

        public void Update(float elapsed, bool shoot)
        {
            timeSinceLastShot += elapsed;
            if (shoot && timeSinceLastShot >= shootPause)
            {
                Vector2 direction = C.Direction(parent.Rotation),
                    bulletPosition = parent.Position + (parent.radius + bulletParams.radius * .5f) * direction,
                    bulletVelocity = parent.Velocity + bulletSpeed * direction;
                RoundObject.InitValues bulletInitValues = new RoundObject.InitValues(bulletPosition, parent.Rotation, bulletVelocity, parent.RotVel);
                C.newRoundObjects.Add(new Bullet(bulletParams, bulletInitValues));
                timeSinceLastShot = 0;
            }
        }
    }
}
