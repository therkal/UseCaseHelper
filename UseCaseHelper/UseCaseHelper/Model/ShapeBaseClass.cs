using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UseCaseHelper.Model
{
    abstract class ShapeBaseClass : Interface.Shape
    {
        Graphics draw;

        public abstract void Draw(PictureBox drawCanvas);
     
    }
}
