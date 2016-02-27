using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UseCaseHelper.Properties;
using System.Windows.Media.Imaging;
using UseCaseHelper.BaseClasses;
using UseCaseHelper.Interface;

namespace UseCaseHelper.Model
{
    public class Actor : ShapeObject, ISelectable
    {
        public const int HeadSize = 30,
            Width = HeadSize,
            Height = HeadSize * 3;

        public string Name { get; private set; }

        public bool IsSelected = false;

        /// <summary>
        /// Create
        /// </summary>
        /// <param name="start">Starting point of the Actor.</param>
        public Actor(Point start) : base (DrawableType.Actor , start , new Point(Width, Height))
        {

        }

        public override void Draw(Graphics g, Color c)
        {
            Pen p = new Pen(c);

            //Head
            g.FillEllipse(Brushes.Beige, Start.X, Start.Y, HeadSize, HeadSize);             //Fils the base
            g.DrawEllipse(p, Start.X, Start.Y, HeadSize, HeadSize);

            //Body
            g.DrawLine(p, Start.X + HeadSize / 2, Start.Y + HeadSize, Start.X + HeadSize / 2, Start.Y + HeadSize * 2);

            //Left arm
            g.DrawLine(p, Start.X + HeadSize / 2, Start.Y + HeadSize, Start.X, Start.Y + HeadSize * 2);

            //Right arm
            g.DrawLine(p, Start.X + HeadSize / 2, Start.Y + HeadSize, Start.X + HeadSize, Start.Y + HeadSize * 2);

            //Left leg
            g.DrawLine(p, Start.X + HeadSize / 2, Start.Y + HeadSize * 2, Start.X, Start.Y + HeadSize * 3);

            //Right leg
            g.DrawLine(p, Start.X + HeadSize / 2, Start.Y + HeadSize * 2, Start.X + HeadSize, Start.Y + HeadSize * 3);

            //Name 
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;

            g.DrawString(Name, new Font(FontFamily.GenericSansSerif, 10), Brushes.Black, new RectangleF(Start.X - Width, Start.Y + Height, Width * 3, 20), sf);
        }


        public void GetDataFromDialog(ActorCreater form)
        {
            Name = form.Name;
        }

        protected override Point CalculateClip()
        {
            return new Point(Start.X + HeadSize /2 , Start.Y + HeadSize /2 );
        }

        public void SetSelected(bool selected)
        {
            IsSelected = selected;
        }
    }
}
