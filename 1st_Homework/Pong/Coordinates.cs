using System;
using Microsoft.Xna.Framework;

namespace Pong
{
    public class Coordinates
    {
        public Compass VecDir { get; set; }

        public Vector2 ValueVector { get; }


        /// <summary>
        /// Creates a coordinates object. Gets a Compass enum and sets ValueVectorAccordingly.
        /// (1,1) SouthEast, (-1,1) SouthWest, (-1,-1) NorthWest, (1,-1) NorthEast. 
        /// </summary>
        /// <param name="dir"></param>
        public Coordinates(Compass dir)
        {
            VecDir = dir;

            if (dir.Equals(Compass.SouthEast))
            {
               ValueVector=new Vector2(1,1); 
            }
            else if (dir.Equals(Compass.SouthWest))
            {
                ValueVector = new Vector2(-1, 1);
            }
            else if (dir.Equals(Compass.NorthWest))
            {
                ValueVector = new Vector2(-1, -1);
            }
            else if (dir.Equals(Compass.NorthEast))
            {
                ValueVector = new Vector2(1, -1);
            }
        }

        /// <summary>
        /// Returns new Coordinates direction.
        /// </summary>
        /// <param name="direction"></param>
        /// <param name="vector"></param>
        /// <returns></returns>
        public static Coordinates operator *(Coordinates direction, Vector2 vector)
        {
            Vector2 temp= direction.ValueVector * vector;

            if (temp.Equals(new Vector2(1, 1)))
            {
                return new Coordinates(Compass.SouthEast);
            }

            else if (temp.Equals(new Vector2(-1, 1)))
            {
                return new Coordinates(Compass.SouthWest);
            }

            else if (temp.Equals(new Vector2(-1, -1)))
            {
                return new Coordinates(Compass.NorthWest);
            }

            else if (temp.Equals(new Vector2(1, -1)))
            {
                return new Coordinates(Compass.NorthEast);
            }

            else throw new Exception("Invalid Direction");

           
        }


        /// <summary>
        /// Return a vector2 ass ValueVector * num.
        /// </summary>
        /// <param name="direction"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        public static Vector2 operator *(Coordinates direction, float num)
        {
            return direction.ValueVector * num;
        }
    }
}