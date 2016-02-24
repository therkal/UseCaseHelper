using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UseCaseHelper.BaseClasses;

namespace UseCaseHelper.Model
{
    class Line : Shape
    {
        public ShapeObject FirstTarget, SecondTarget;

        public Line(ShapeObject first, ShapeObject second) :base(DrawableType.Line, first.Clip, second.Clip)
        {
            FirstTarget = first;
            SecondTarget = second;
        }

        public override void Draw(Graphics g, Color c)
        {
            g.DrawLine(new Pen(c), Start.X, Start.Y, End.X, End.Y);
        }

    }
}
