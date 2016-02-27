using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCaseHelper.Model;
using UseCaseHelper.BaseClasses;

namespace UseCaseHelper.Controls
{
    class GestureManager
    {
        public ShapeGroup ShapeUniverse { get; private set; }
        public GestureManager(ShapeGroup shapeUniverse)
        {
            ShapeUniverse = shapeUniverse;
        }

        public bool HandleMouseDown(Point location , Shape firstTarget )
        {
            if (firstTarget != null)
            {
                
            }

            return false;
        }

    }
}
