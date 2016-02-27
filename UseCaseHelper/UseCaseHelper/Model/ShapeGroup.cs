using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCaseHelper.BaseClasses;
using UseCaseHelper.Interface;

namespace UseCaseHelper.Model
{
    class ShapeGroup : Shape, ISelectable
    {
        private List<Shape> myShapes = new List<Shape>();
        public int Count { get { return myShapes.Count; } }
        public List<Shape> Shapes { get { return myShapes; } }

        public void Add (Shape s) { myShapes.Add(s); }
        public bool Remove (Shape s) { return myShapes.Remove(s); }

        public override void Draw(Graphics g, Color c)
        {
            //Reverse list. Ensures that lines are painted afterwards.
            myShapes = myShapes.OrderBy(t => t.Type).ToList();
            foreach (Shape s in myShapes )
            {
                s.Draw(g, c);
            } 
            
        }

        public void SetSelected(bool selected)
        {
            myShapes.OfType<ISelectable>().ToList().ForEach(s => s.SetSelected(selected));
        }

        public Shape GetCollision(int x , int y)
        {
            
            foreach (Shape s in myShapes)
            {
                if (s.Type != DrawableType.Line)
                {
                    //A ShapeObject => Actor, UseCase.
                    if (x >= s.Start.X && x < s.Start.X + s.End.X && y >= s.Start.Y && y < s.Start.Y + s.End.Y)
                    {
                        return (ShapeObject)s;
                    }
                } else
                {
                    //A shape => Line
                    const int LineCollisionRadius = 5;
                    double lineIncline = (double)(s.End.Y - s.Start.Y) / (double)(s.End.X - s.Start.X);         //Calculates the angle of the line
                    Point linePoint = new Point(x, (int)((x - s.Start.X) * lineIncline) + s.Start.Y);           //Gets the click offset (if user doesn't click the exact pixel)
                    if (x < linePoint.X + LineCollisionRadius && x >= linePoint.X - LineCollisionRadius && y < linePoint.Y + LineCollisionRadius && y >= linePoint.Y - LineCollisionRadius)
                    {
                        return s;
                    }
                }

            }

            return null;
        }

        public void ClearAll(Graphics g)
        {
            g.Clear(Color.White);
        }

    }
}
