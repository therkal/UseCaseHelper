using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UseCaseHelper.Model
{
    class Line : ShapeBaseClass
    {
        private Point LineStartingPoint { get; set; }
        private Point LineEndingPoint { get; set; }

        public Line(Point start , Point end)
        {
            LineStartingPoint = start;
            LineEndingPoint = end;
        }
        public override void Draw(PictureBox drawCanvas)
        {
            Graphics draw = drawCanvas.CreateGraphics();
            Pen pen = new Pen(Color.Black);

            draw.DrawLine(pen, LineStartingPoint, LineEndingPoint);
        }
    }
}
