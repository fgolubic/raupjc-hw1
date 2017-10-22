using System;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Xna.Framework;

namespace Pong
{
    public class CollisionDetector
    {
       
        /// <summary>
        /// Checks if boundbox of object a overlaps with object b.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool Overlaps(IPhysicalObject2D a, IPhysicalObject2D b)
        {
            // return true if overlaps , false otherwise ...
            
           
            Vector2[] firstObj =
            {
                new Vector2(a.X,a.Y),
                new Vector2(a.X, a.Y + a.Height),
                new Vector2(a.X + a.Width , a.Y + a.Height),
                new Vector2(a.X + a.Width, a.Y)

            };

           

            foreach (Vector2 temp in firstObj)
            {
                if (temp.X >= b.X && temp.X <= b.X + b.Width && temp.Y >= b.Y && temp.Y <= b.Y + b.Height)
                {

                    return true;
                }
            }

           
            return false;
        }
    }
}