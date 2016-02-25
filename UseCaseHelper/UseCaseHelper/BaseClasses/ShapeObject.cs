using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UseCaseHelper.BaseClasses
{
    public abstract class ShapeObject : Shape
    {
        private static int safeShapeId = 0;

        public Point Clip { get; private set; }
        public int Id { get; protected set; }

        /// <summary>
        /// Generates ID for new shape. 
        /// </summary>
        public static int SafeShapeId
        {
            get { safeShapeId++; return safeShapeId; } set { safeShapeId = value; }
        }

        /// <summary>
        /// Creates a new ShapeObject
        /// </summary>
        /// <param name="type">Type of the shape</param>
        /// <param name="startPoint">Startpoint where to draw shape</param>
        /// <param name="endPoint">Endpoint, where should the shape end.</param>
        public ShapeObject(DrawableType type , Point startPoint , Point endPoint) : base (type, startPoint, endPoint)
        {
            //Calculate the coordinates where to clip on to object.
            Clip = CalculateClip();
            //Set safe ID.
            Id = SafeShapeId;
        }

        public void SetStartLocation(Point loc)
        {
            Clip = CalculateClip();
            Start = loc;
        }

        /// <summary>
        /// Calculates where to snap a line to an object.
        /// </summary>
        /// <returns></returns>
        protected abstract Point CalculateClip();
    }
}
