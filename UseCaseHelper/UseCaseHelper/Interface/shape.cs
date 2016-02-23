using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UseCaseHelper.Interface
{
    public interface Shape
    {
        /// <summary>
        /// Interface handles the draw function for shapes.
        /// </summary>
        /// 

        /// <summary>
        /// Draw is derived from all shapes. 
        /// </summary>
        void Draw(PictureBox drawCanvas);
    }
}
