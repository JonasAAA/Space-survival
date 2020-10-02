using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Game1
{
    public abstract class Spaceship : RoundObject
    {
        protected bool forward, turnLeft, moveLeft, turnRight, moveRight, shoot;
        
        private readonly HealthBar healthBar;
        private readonly List<Gun> guns;
        private readonly List<Engine> engines, RCSThrusters;

        public Spaceship(float mass, float radius, Vector2 position, Vector2 velocity, float angle, string imageName, Color color, HealthBar healthBar, float thrust, float RCSThrust)
            : base(mass, radius, position, velocity, angle, angularVel: 0, imageName, color)
        {
            forward = false;
            turnLeft = false;
            moveLeft = false;
            turnRight = false;
            moveRight = false;
            shoot = false;
            this.healthBar = healthBar;
            guns = new List<Gun>();
            engines = new List<Engine>()
            {
                new Engine(relPos: new Vector2(0, radius), relDir: new Vector2(0, -1), thrust),
            };
            RCSThrusters = new List<Engine>()
            {
                new Engine(relPos: new Vector2(0, -radius), relDir: new Vector2(1, 0), RCSThrust),
                new Engine(relPos: new Vector2(0, -radius), relDir: new Vector2(-1, 0), RCSThrust),
                new Engine(relPos: new Vector2(0, radius), relDir: new Vector2(-1, 0), RCSThrust),
                new Engine(relPos: new Vector2(0, radius), relDir: new Vector2(1, 0), RCSThrust),
            };
            foreach (Engine engine in engines.Concat(RCSThrusters))
                engine.SetParent(this);
        }

        public void AddGun(Gun gun)
        {
            gun.SetParent(this);
            guns.Add(gun);
        }

        public override void Update(float elapsed)
        {
            /*
              
               0><1
                /\
               /  \
              /    \
               3><2
            
            */

            foreach (Engine engine in engines)
                engine.Update(on: forward);
            RCSThrusters[0].Update(on: turnRight | moveRight);
            RCSThrusters[1].Update(on: turnLeft | moveLeft);
            RCSThrusters[2].Update(on: turnRight | moveLeft);
            RCSThrusters[3].Update(on: turnLeft | moveRight);

            base.Update(elapsed);

            foreach (Gun gun in guns)
                gun.Update(elapsed, shoot);
        }

        protected override void Impact(RoundObject other)
        {
            Die();
        }

        protected override void Impact(Bullet other)
        {
            healthBar.TakeDamage(other.damage);
            if (!healthBar.IsAlive)
                Die();
        }

        public override void Draw()
        {
            healthBar.Draw(Position);
            base.Draw();
        }
    }
}
