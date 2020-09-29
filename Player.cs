using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Game1
{
    public class Player : Spaceship
    {
        private readonly Keys forwardKey, turnLeftKey, moveLeftKey, turnRightKey, moveRightKey, shootKey;

        public Player(Parameters parameters, HealthBar healthBar, float thrustPower, float RCSPower, InitValues initValues, Keys forwardKey, Keys turnLeftKey, Keys moveLeftKey, Keys turnRightKey, Keys moveRightKey, Keys shootKey)
            : base(parameters, healthBar, thrustPower, RCSPower, initValues)
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
            KeyboardState keyState = Keyboard.GetState();
            forward = keyState.IsKeyDown(forwardKey);
            turnLeft = keyState.IsKeyDown(turnLeftKey);
            moveLeft = keyState.IsKeyDown(moveLeftKey);
            turnRight = keyState.IsKeyDown(turnRightKey);
            moveRight = keyState.IsKeyDown(moveRightKey);
            shoot = keyState.IsKeyDown(shootKey);
            base.Update(elapsed);
        }
    }
}
