using System.CodeDom;
using Microsoft.Xna.Framework;

namespace Pong
{
    public class Ball:Sprite
    {
        /// <summary >
        /// Defines current ball speed in time .
        /// </summary >
        public float Speed { get; set; }
        public float BumpSpeedIncreaseFactor { get; set; }
        /// <summary >
        /// Defines ball direction .
        /// Valid values ( -1 , -1) , (1 ,1) , (1 , -1) , ( -1 ,1).
        /// Using Vector2 to simplify game calculation . Potentially
        /// dangerous because vector 2 can swallow other values as well .
        /// OPTIONAL TODO : create your own , more suitable type
        /// </summary >
        public Coordinates Direction { get; set; }

        public const float MaxSpeed = 0.7f;

        public Ball(int size, float speed, float
            defaultBallBumpSpeedIncreaseFactor) : base(size, size)
        {
            Speed = speed;
            BumpSpeedIncreaseFactor = defaultBallBumpSpeedIncreaseFactor;
            // Initial direction
            Direction = new Coordinates(Compass.SouthEast);
        }

    }
}