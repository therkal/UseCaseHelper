using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UseCaseHelper.BaseClasses
{
    /// <summary>
    /// Enum for possible encountered types.
    /// </summary>
    public enum DrawableType
    {
        Line = 0, Actor = 1, UseCase = 2
    }
    /// <summary>
    /// Bsaeclass for shapes. 
    /// </summary>
    public abstract class Shape
    {
        public DrawableType Type { get; private set; }
        public Point Start { get; protected set; }
        public Point End { get; protected set; }

        public Shape(DrawableType type, Point start, Point end)
        {
            Type = type;
            Start = start;
            End = end;
        }

        public abstract void Draw(Graphics g , Color c);
    }



}
