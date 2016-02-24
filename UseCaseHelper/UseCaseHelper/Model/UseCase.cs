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
        public const int Width = 160, Height = 80;

        public string Naam { get; private set; }
        public string Samenvatting { get; private set; }
        public string Actoren { get; private set; }
        public string Aannamen { get; private set; }
        public string Beschrijving { get; private set; }
        public string Uitzonderingen { get; private set; }
        public string Resultaat { get; private set; }


        public UseCase(int id, Point start, string naam, string samenvatting, string actoren, string aannamen, string beschrijving, string uitzonderingen, string resultaat)
            : base(DrawableType.UseCase, start, new Point(Width, Height))
        {
            Id = id;
            Naam = naam;
            Samenvatting = samenvatting;
            Actoren = actoren;
            Aannamen = aannamen;
            Beschrijving = beschrijving;
            Uitzonderingen = uitzonderingen;
            Resultaat = resultaat;
        }

        public override void Draw(Graphics g)
        {
            throw new NotImplementedException();
        }

        public override void DrawColor(Graphics g, Color c)
        {
            throw new NotImplementedException();
        }

        protected override Point CalculateIntersectionPoint()
        {
            throw new NotImplementedException();
        }
    }
}