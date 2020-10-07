using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace Game1
{
    public class Player : Spaceship
    {
        private readonly Keys forwardKey, turnLeftKey, moveLeftKey, turnRightKey, moveRightKey, shootKey;

        public Player(float mass, Vector2 position, Vector2 velocity, float angle, float maxAngSpeed, HealthBar healthBar, float thrust, float RCSThrust, Image image, Image imageForMinimap, Keys forwardKey, Keys turnLeftKey, Keys moveLeftKey, Keys turnRightKey, Keys moveRightKey, Keys shootKey)
            : base(mass, position, velocity, angle, maxAngSpeed, healthBar, thrust, RCSThrust, image, imageForMinimap)
        {
            this.forwardKey = forwardKey;
            this.turnLeftKey = turnLeftKey;
            this.moveLeftKey = moveLeftKey;
            this.turnRightKey = turnRightKey;
            this.moveRightKey = moveRightKey;
            this.shootKey = shootKey;
        }

        public override void Update(float elapsed)
        {
            Func<Keys, bool> IsKeyDown = Keyboard.GetState().IsKeyDown;
            forward = IsKeyDown(forwardKey);
            turnLeft = IsKeyDown(turnLeftKey);
            moveLeft = IsKeyDown(moveLeftKey);
            turnRight = IsKeyDown(turnRightKey);
            moveRight = IsKeyDown(moveRightKey);
            shoot = IsKeyDown(shootKey);
            base.Update(elapsed);
        }
    }
}
