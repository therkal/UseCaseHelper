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
        private int lineStartX, lineStartY, lineEndX, lineEndY;
        private ShapeObject firstTarget, secondTarget;
        private readonly Random random = new Random();

        private const int LineCollisionRadius = 5;          //Sets buffer zone for line click. (User is likely to miss the line.)
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
                if(s !=null) { s.Draw(g , Color.Black); }
            }

            //If we are drawing a line, draw whilst moving with our mouse.
            if (rbLine.Checked && isDrawingLine)
            {
                g.DrawLine(new Pen(Color.Red), lineStartX, lineStartY, lineEndX, lineEndY);
            }
        }

        private void drawCanvas_MouseDown(object sender, MouseEventArgs e)
        {
            firstTarget = getObjectCollision(e.X, e.Y);
            if( firstTarget != null)
            {
                if(rbLine.Checked)
                {
                    //WE are crearing a line
                    isDrawingLine = true;
                    lineStartX = e.X;
                    lineStartY = e.Y;
                } else if (rbMove.Checked)
                {
                    //We are moving a line.
                }
            }
        }


        private void drawCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if(rbLine.Checked && isDrawingLine)
            {
                // We are drawing a line! 
                lineEndX = e.X;
                lineEndY = e.Y;

                drawCanvas.Refresh();
            }
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
            else if (rbUseCase.Checked)
            {
                ShapeObject so;
                if((so = getObjectCollision(e.X , e.Y)) != null && so.Type == DrawableType.UseCase) 
                {
                    //We have clicked on an existing shape
                    using(UseCaseEditor UCE = new UseCaseEditor())
                    {
                        Model.UseCase u = (Model.UseCase)so;
                        UCE.SetDataFromUseCase(u);              //Set the previously found data.
                        DialogResult r = UCE.ShowDialog();
                        if(r == DialogResult.OK)
                        {
                            //If dialog is closed, update the info.
                            u.GetDataFromDialog(UCE);
                        }
                    }
                    
                    
                } else
                {
                    //We have clicked on a new object.
                    using (UseCaseEditor UCE = new UseCaseEditor())
                    {
                        DialogResult r = UCE.ShowDialog();

                        if(r == DialogResult.OK)
                        {
                            //Create new model
                            Model.UseCase u = new Model.UseCase(new Point(e.X - Model.UseCase.Width / 2, e.Y - Model.UseCase.Height / 2));
                            //Get data from other form
                            u.GetDataFromDialog(UCE);
                            //Add to shape list
                            myShapes.Add(u);
                        }
                    }
                }
            }
            else if (rbLine.Checked && isDrawingLine)
            {
                //Person has already clicked once.
                isDrawingLine = false;

                secondTarget = getObjectCollision(e.X, e.Y);            //Get second target
                if(secondTarget != null && firstTarget.Type != secondTarget.Type)
                {
                    //We have 2 points, and the first point is not the same as the second.
                    myShapes.Add(new Model.Line(firstTarget, secondTarget));
                }

                lineEndX = lineEndY = lineStartX = lineStartY = 0;
                firstTarget = secondTarget = null;

                drawCanvas.Refresh();

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

        /// <summary>
        /// Checks for collisions with a line.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private Model.Line getLineCollision(int x, int y)
        {
            foreach(Model.Line l in myShapes.Where(l => l.Type == DrawableType.Line).Reverse())
            {
                double lineIncline = (double)(l.End.Y - l.Start.Y) / (double)(l.End.X - l.Start.X);
                Point linePoint = new Point(x, (int)((x - l.Start.X) * lineIncline) + l.Start.Y);
                if (x < linePoint.X + LineCollisionRadius && x >= linePoint.X - LineCollisionRadius && y < linePoint.Y + LineCollisionRadius && y >= linePoint.Y - LineCollisionRadius)
                {
                    return l;
                }
            }

            return null; 
        } 
        #endregion HELPERS;

    }
}
