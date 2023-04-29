using BreakOutGame;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Reflection.Emit;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using Vector2 = Microsoft.Xna.Framework.Vector2;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace BreakoutGame
{
    //    Power-ups [Arkanoid/Gameplay]

    //Power-ups appear after you remove a random number of non-silver bricks.Only one power-up capsule will appear at a time,
    //and they slowly fall to the bottom of the screen. In order to activate a power-up, you must collect it with the Vaus.Power-up effects last until another power-up is collected or the current ball is lost.

    //Power-up Name            Description
    //Laser                    Collect the red capsule to transform the Vaus into its Laser-firing configuration. In this form, you can fire lasers at the top of the screen by pushing the fire button.Lasers can be used against every brick except Gold bricks, and against enemies.Silver bricks can only be destroyed by lasers when they are hit the required number of times.
    //Enlarge                  Collect the blue capsule to extend the width of the Vaus.
    //Catch                    Collect the green capsule to gain the catch ability.When the ball hits the Vaus, it will stick to the surface. Press the Fire button to release the ball. The ball will automatically release after a certain period of time has passed.
    //Slow                     Collect the orange capsule to slow the velocity at which the ball moves.Collecting multiple orange capsules will have a cumulative effect and the ball velocity can become extremely slow.However, the ball velocity will gradually increase as it bounces and destroys bricks.The velocity may sometimes suddenly increase with little warning.
    //Break                    Collect the violet capsule to create a "break out" exit on the right side of the stage. Passing through this exit will cause you to advance to the next stage immediately, as well as earn a 10,000 point bonus.
    //Disruption               Collect the cyan capsule to cause the ball to split into three instances of itself.All three balls can be kept aloft.There is no penalty for losing the first two balls.No colored capsules will fall as long as there is more than one ball in play.This is the only power up that, while in effect, prevents other power ups from falling.
    //Player                   Collect the gray capsule to earn an extra Vaus.
    internal class PowerUp
    {
        public enum PowerUpType{ L_Laser,E_Enlarge,C_Catch,S_Slow,B_BreakoutSide,D_Disruption,P_PlayerExtra};
        private PowerUpType powerUpType;
        private Vector2 powerUpPosition;
        private Rectangle powerUpRectangle;
        private int powerUpWidth;
        private int powerUpHeight;

        public Vector2 PowerUpPosition
        {
            get { return powerUpPosition; }
            set { powerUpPosition = value; }
        }
        public Rectangle PowerUpRectangle
        {
            get { return powerUpRectangle; }
            set { powerUpRectangle = value; }
        }


        public PowerUp(Vector2 powerUpStartPosition,PowerUpType powerUpType)
        {
            //Create a new PowerUp
            this.powerUpPosition = powerUpStartPosition;
            this.powerUpType= powerUpType;
            this.powerUpRectangle = new Rectangle((int)powerUpPosition.X, (int)powerUpPosition.Y, powerUpWidth, powerUpHeight);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            
        }
        public void Update(GameTime gameTime)
        {
            


        }
    }
}
