using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace Game1
{
    public abstract class Spaceship : RoundObject
    {
        protected bool forward, turnLeft, moveLeft, turnRight, moveRight, shoot;
        
        private readonly float rotInertia, thrustPower, RCSPower;
        private readonly HealthBar healthBar;
        private readonly List<Gun> guns;

        public Spaceship(Parameters parameters, HealthBar healthBar, float thrustPower, float RCSPower, InitValues initValues)
            : base(parameters, initValues)
        {
            forward = false;
            turnLeft = false;
            moveLeft = false;
            turnRight = false;
            moveRight = false;
            shoot = false;
            RotVel = 0;
            rotInertia = .5f * radius * radius;
            this.thrustPower = thrustPower;
            this.RCSPower = RCSPower;
            this.healthBar = healthBar;
            guns = new List<Gun>();
        }

        public void AddGun(Gun gun)
        {
            gun.SetParent(this);
            guns.Add(gun);
        }

        public override void Update(float elapsed)
        {
            /*
              
               1><2
                /\
               /  \
              /    \
               4><3
            
            */

            bool use1 = turnRight | moveRight,
                use2 = turnLeft | moveLeft,
                use3 = turnRight | moveLeft,
                use4 = turnLeft | moveRight;

            float frontAccel = (Convert.ToInt32(use1) - Convert.ToInt32(use2)) * RCSPower,
                backAccel = (Convert.ToInt32(use4) - Convert.ToInt32(use3)) * RCSPower;

            Vector2 direction = C.Direction(Rotation),
                orthDir = new Vector2(-direction.Y, direction.X);
            
            Velocity += orthDir * (frontAccel + backAccel) * elapsed;
            if (forward)
                Velocity += direction * thrustPower * elapsed;

            RotVel += (frontAccel - backAccel) * radius / rotInertia * elapsed;

            base.Update(elapsed);

            foreach (Gun gun in guns)
                gun.Update(elapsed, shoot);
        }

        public override void Collide(RoundObject other)
        {
            if (IfIntersects(other))
                Die();
            base.Collide(other);
        }

        public void Collide(Bullet other)
        {
            healthBar.TakeDamage(other.damage);
            if (!healthBar.IsAlive)
                Die();
            base.Collide(other);
        }
    }
}
