using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UseCaseHelper.Model
{
    class Actor : ShapeBaseClass
    {
        public override void Draw(PictureBox drawCanvas)
        {
            Graphics draw = drawCanvas.CreateGraphics();
        }
    }
}
