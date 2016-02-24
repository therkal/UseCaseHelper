using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UseCaseHelper.BaseClasses;

namespace UseCaseHelper
{
    public partial class Form1 : Form
    {
        #region Globals
        private List<Shape> myShapes = new List<Shape>();
        private bool isDrawingLine = false, isMovingObject = false;
        private Point lineStart, lineEnd;
        private ShapeObject firstTarget, secondTarget;
        private readonly Random random = new Random();
        #endregion Globals


        public Form1()
        {
            InitializeComponent();
        }

        #region EventHandlers;
        private void drawCanvas_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            //Order list by types.
            myShapes = myShapes.OrderBy(t => t.Type).ToList();

            //Draw all shapes.
            foreach(Shape s in myShapes)
            {
                if(s !=null) { s.Draw(g); }
            }
        }

        private void drawCanvas_MouseDown(object sender, MouseEventArgs e)
        {

        }


        private void drawCanvas_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void drawCanvas_MouseUp(object sender, MouseEventArgs e)
        {
            if (rbActor.Checked)
            {
                ShapeObject so;
                if((so = getObjectCollision(e.X, e.Y)) != null && so.Type == DrawableType.Actor) {
                    //We have clicked on an exisiting object of type Actor. Edit name
                    using(ActorCreater ActorForm = new ActorCreater())
                    {
                        Model.Actor a = (Model.Actor)so;
                        ActorForm.setDataFromActor(a);
                        DialogResult r = ActorForm.ShowDialog();
                        if(r == DialogResult.OK)
                        {
                            a.GetDataFromDialog(ActorForm);
                        }
                    }
                } else
                {
                    // There is no existing Actor. Make a new one.
                    using (ActorCreater ActorForm = new ActorCreater())
                    {
                        DialogResult r = ActorForm.ShowDialog();
                        if (r == DialogResult.OK)
                        {
                            //Create new Actor.
                            Model.Actor newActor = new Model.Actor(new Point(e.X - Model.Actor.HeadSize / 2, e.Y - Model.Actor.HeadSize / 2));
                            //Get data from actor form
                            newActor.GetDataFromDialog(ActorForm);
                            //Add to our shapelist
                            myShapes.Add(newActor);
                        }
                    }
                }
            }
            else if (rbDelete.Checked)
            {
                ShapeObject so;
                if ((so = getObjectCollision(e.X, e.Y)) != null)
                {
                    // We have clicked on an exising object + we have checked delete. 
                    myShapes.Remove(so);
                } else
                {
                    // We might have clicked on a line?
                }

                drawCanvas.Refresh();
            }

            

            

            //Refresh our canvas
            drawCanvas.Refresh();
        }

        #endregion EvenetHandlers;





        #region HELPERS;
        /// <summary>
        /// Checks for collisions with near objects. 
        /// </summary>
        /// <param name="x">Mouse X -> Where clicked</param>
        /// <param name="y">Mouse y position -> Where clicked</param>
        /// <returns></returns>
        public ShapeObject getObjectCollision(int x, int y)
        {
            foreach(Shape s in myShapes.Where(s => s.Type != DrawableType.Line).Reverse())
            {
                if(x >= s.Start.X && x < s.Start.X + s.End.X && y >= s.Start.Y && y < s.Start.Y + s.End.Y)
                {
                    return (ShapeObject)s;
                }
            }

            return null; 
        }
        #endregion HELPERS;

    }
}
