using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Drawing;
using Color = Microsoft.Xna.Framework.Color;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace GptPong2
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
      

        private Texture2D _paddleTexture;
        private Texture2D _ballTexture;

        private Vector2 _paddlePosition;
        private const int PADDLE_SPEED = 8;

        private Vector2 _ballPosition;
        private Vector2 _ballVelocity;
        private float BALL_SPEED = 5.0f;
        private bool _ballInPlay;

        private int _playerScore;
        private SpriteFont _font;
        private const int WINDOW_WIDTH = 800;
        private const int WINDOW_HEIGHT = 600;
        public static Texture2D Pixel;
        private List<Brick> _bricks;
        private const int BRICK_WIDTH = 40;
        private const int BRICK_HEIGHT = 15;
        private const int BRICK_MARGIN = 10;
        private const int NUM_ROWS = 10;
        private const int NUM_COLS = 8;
        private Color _color;
        //Sound Effects
        private SoundEffect hitBrickSound;
        private SoundEffect hitPaddleSound;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _paddlePosition = new Vector2(WINDOW_WIDTH / 2-64, WINDOW_HEIGHT - 200);
            _ballPosition = new Vector2(WINDOW_WIDTH / 2, _paddlePosition.Y-10);
            _ballVelocity = new Vector2(-BALL_SPEED, -BALL_SPEED);
            _ballInPlay = false;
            _playerScore = 0;
            _bricks = new List<Brick>();

            int startX = (WINDOW_WIDTH - (NUM_COLS * (BRICK_WIDTH + BRICK_MARGIN))) / 2;
            int startY = 50;

            for (int row = 0; row < NUM_ROWS; row++)
            {
                for (int col = 0; col < NUM_COLS; col++)
                {
                    int x = startX + col * (BRICK_WIDTH + BRICK_MARGIN);
                    int y = startY + row * (BRICK_HEIGHT + BRICK_MARGIN);
                    var brickBounds = new Rectangle(x, y, BRICK_WIDTH, BRICK_HEIGHT);
                    // Generate a random color for the brick
                    Random rand = new Random();
                    _color = new Color(rand.Next(256), rand.Next(256), rand.Next(256));
                    var brick = new Brick(brickBounds, _color);
                    _bricks.Add(brick);
                }
            }
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _font = Content.Load<SpriteFont>("scorefont");
            _paddleTexture = Content.Load<Texture2D>("paddle");
            _ballTexture = Content.Load<Texture2D>("ball");
            Pixel = new Texture2D(GraphicsDevice, 1, 1);
            Pixel.SetData(new[] { Color.White });
            //Sound Effects
            hitBrickSound = Content.Load<SoundEffect>("Sounds/ping_pong_8bit_beeep");
            hitPaddleSound = Content.Load<SoundEffect>("Sounds/ping_pong_8bit_plop");

        }

        protected override void Update(GameTime gameTime)
        {
            // Exit the game if the Escape key is pressed
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // Move the paddle left and right with the arrow keys
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
                _paddlePosition.X -= PADDLE_SPEED;
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
                _paddlePosition.X += PADDLE_SPEED;

            // Keep the paddle within the screen bounds
            _paddlePosition.X = MathHelper.Clamp(_paddlePosition.X, 0, WINDOW_WIDTH - _paddleTexture.Width);

            if (_ballInPlay)
            {
               
                // Update the ball's position based on its velocity
                _ballPosition += _ballVelocity;

                // Check for collisions with the left and right sides of the screen
                if (_ballPosition.X < 0 || _ballPosition.X > WINDOW_WIDTH - _ballTexture.Width)
                {
                    _ballVelocity.X = -_ballVelocity.X;
                }

                // Check for collisions with the paddle
                if (_ballPosition.Y + _ballTexture.Height >= _paddlePosition.Y && _ballPosition.Y + _ballTexture.Height <= _paddlePosition.Y + _paddleTexture.Height
                    && _ballPosition.X + _ballTexture.Width >= _paddlePosition.X && _ballPosition.X <= _paddlePosition.X + _paddleTexture.Width)
                {
                    _ballVelocity.Y = -_ballVelocity.Y;
                    _playerScore += 1;
                    hitPaddleSound.Play();
                   // BALL_SPEED += .1f;
                }
                // Bounce the ball off the top of the screen
                if (_ballPosition.Y < _ballTexture.Height/2)
                    _ballVelocity.Y *= -1;
                // Check for collisions with the bottom of the screen
                if (_ballPosition.Y > WINDOW_HEIGHT)
                {
                    _ballInPlay = false;
                    _playerScore--;
                    _ballPosition = new Vector2(WINDOW_WIDTH / 2, _paddlePosition.Y - 10);
                    _ballVelocity = new Vector2(-BALL_SPEED, -BALL_SPEED);
                    _paddlePosition = new Vector2(WINDOW_WIDTH / 2 - 64, WINDOW_HEIGHT - 200);
                }
                // Check for collision with bricks
                var ballBounds = new Rectangle((int)_ballPosition.X - _ballTexture.Height / 2, (int)_ballPosition.Y - _ballTexture.Height / 2, _ballTexture.Height / 2 * 2, _ballTexture.Height / 2 * 2);
                for (int i = _bricks.Count - 1; i >= 0; i--)
                {
                    var brick = _bricks[i];
                    if (!brick.Hit && ballBounds.Intersects(brick.Bounds))
                    {
                        brick.Hit = true;
                        _ballVelocity.Y *= -1;
                        _playerScore++;
                        hitBrickSound.Play();
                    }
                }

                // Remove hit bricks from list
                _bricks.RemoveAll(brick => brick.Hit);
            }
            else
            {
                // Launch the ball when the spacebar is pressed
                if (Keyboard.GetState().IsKeyDown(Keys.Space))
                {
                    _ballInPlay = true;
                }
            }
         
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            // Draw the paddle and ball
            _spriteBatch.Draw(_paddleTexture, _paddlePosition, Color.White);
            _spriteBatch.Draw(_ballTexture, _ballPosition, Color.White);
            // Draw bricks
            foreach (var brick in _bricks)
            {
                brick.Draw(_spriteBatch);
            }
            // Draw the score at the top of the screen
            string scoreText = $"Score: {_playerScore}";
            Vector2 scorePosition = new Vector2((WINDOW_WIDTH - _font.MeasureString(scoreText).X) / 2, 10);
            _spriteBatch.DrawString(_font, scoreText, scorePosition, Color.White);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}