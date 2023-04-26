using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GptPong2
{
    internal class Brick
    {
        public bool Hit { get; set; }
        public Rectangle Bounds { get; set; }
        public Color Color { get; set; }
        public Brick(Rectangle bounds, Color color)
        {
            Bounds = bounds;
            Color = color;
            Hit = false;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (!Hit)
            {
                spriteBatch.Draw(Game1.Pixel, Bounds, Color);
            }
        }
    }
}
