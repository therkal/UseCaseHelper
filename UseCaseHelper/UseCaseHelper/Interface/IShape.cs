using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UseCaseHelper.Interface
{
    interface IShape
    {
        void Draw(Graphics g, Color c);
    }
}
