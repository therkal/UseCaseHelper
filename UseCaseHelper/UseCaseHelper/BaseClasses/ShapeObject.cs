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

        public static int SafeShapeId
        {
            get { safeShapeId++; return safeShapeId; } set { safeShapeId = value; }
        }

        public ShapeObject(DrawableType type , Point startPoint , Point endPoint) : base (type, startPoint, endPoint)
        {
            Clip = CalculateClip();
            Id = SafeShapeId;
        }

        public void SetStartLocation(Point loc)
        {
            Clip = CalculateClip();
            Start = loc;
        }

        protected abstract Point CalculateClip();
    }
}
