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
    public class UseCase : ShapeObject
    {
        public const int Width = 160, Height = 80;              //Standard with & Height for the actor.

        //Form data.
        public string Naam { get; private set; }
        public string Samenvatting { get; private set; }
        public string Actoren { get; private set; }
        public string Aannamen { get; private set; }
        public string Beschrijving { get; private set; }
        public string Uitzonderingen { get; private set; }
        public string Resultaat { get; private set; }
        
        public UseCase(Point start) : base (DrawableType.UseCase, start, new Point(Width,Height))
        {

        }

        public override void Draw(Graphics g , Color c)
        {
            //Rectangle with Object dimensions
            Rectangle dimensions = new Rectangle(Start.X, Start.Y, Width, Height);

            //Draw & fill the Ellipse
            g.FillEllipse(Brushes.White, dimensions);
            g.DrawEllipse(new Pen(c), dimensions);

            //Create the string 
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;

            g.DrawString(Naam, new Font(FontFamily.GenericSansSerif, 10), Brushes.Black, new RectangleF(Start.X + 20, Start.Y + 10, 120, 60), sf);
        }


        protected override Point CalculateClip()
        {
            return new Point(Start.X + Width / 2, Start.Y + Height / 2);
        }

        public void GetDataFromDialog(UseCaseEditor form)
        {
            Naam = form.Naam;
            Samenvatting = form.Samenvatting;
            Actoren = form.Actoren;
            Aannamen = form.Aannamen;
            Beschrijving = form.Beschrijving;
            Uitzonderingen = form.Uitzonderingen;
            Resultaat = form.Resultaat;
        }
    }
}