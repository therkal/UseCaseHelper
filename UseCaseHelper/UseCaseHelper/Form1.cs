using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UseCaseHelper.Model;
using UseCaseHelper.BaseClasses;

namespace UseCaseHelper
{
    public partial class Form1 : Form
    {
        #region Globals
        private bool isDrawingLine = false;                             // Checks if we are drawing
        private bool isMovingObject = false;                            // (Bool) Checks if we are moving.
        private bool skipMouse = false;                                 // If we are deleting objects, skip mouse up.
        private int lineStartX, lineStartY, lineEndX, lineEndY;         // (int) Gets the coördinates of LineStart and End. Used for drawing line in MoveMouse function.
        private Shape firstTarget, secondTarget;                        //  Predefined class used for the boundaries 
        private readonly Random random = new Random();
        #endregion Globals

        private ShapeGroup shapeUniverse = new ShapeGroup();

        public Form1()
        {
            InitializeComponent();

        }

        #region EventHandlers;
        private void drawCanvas_Paint(object sender, PaintEventArgs e)
        {
            //Smooth edges
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            //Handles draw of every element.
            shapeUniverse.Draw(e.Graphics, Color.Black);

            //If we are drawing a line, draw whilst moving with our mouse.
            if (rbLine.Checked && isDrawingLine)
            {
               e.Graphics.DrawLine(new Pen(Color.Red), lineStartX, lineStartY, lineEndX, lineEndY);
            }
        }

        private void drawCanvas_MouseDown(object sender, MouseEventArgs e)
        {
            firstTarget =  shapeUniverse.GetCollision(e.X, e.Y);
            if( firstTarget != null && firstTarget.Type != DrawableType.Line)
            {
                // We moused down on a target.
                if(rbLine.Checked)
                {
                    //WE are crearing a line
                    isDrawingLine = true;
                    lineStartX = e.X;
                    lineStartY = e.Y;
                } else if (rbMove.Checked)
                {
                    //We are moving an object
                }
            }
        }

        private void btnClearAll_Click(object sender, EventArgs e)
        {
            shapeUniverse.ClearAll(drawCanvas.CreateGraphics());
        }

        private void btnScreenShot_Click(object sender, EventArgs e)
        {
            Bitmap screenshot = new Bitmap(drawCanvas.Width, drawCanvas.Height);
            drawCanvas.DrawToBitmap(screenshot, new Rectangle(new Point(0,0), new Size(drawCanvas.Width , drawCanvas.Height)));
            using (SaveFileDialog sf = new SaveFileDialog())
            {
                sf.Filter = "Bitmap Image | *.bmp";
                DialogResult r = sf.ShowDialog();

                if (r == DialogResult.OK)
                {
                    if (sf.FileName != "")
                    {
                        // user supplied a file name
                        System.IO.FileStream fs = (System.IO.FileStream)sf.OpenFile();
                        screenshot.Save(fs, System.Drawing.Imaging.ImageFormat.Bmp);
                        fs.Close();
                    }
                }
            }
        }

        private void drawCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            Shape shape;
            if((shape = shapeUniverse.GetCollision(e.X , e.Y)) != null)
            {
                tsBottomBar.Text = "Edit " + shape.Type;

                Cursor = Cursors.Hand;
            } else if ((shape = shapeUniverse.GetCollision(e.X, e.Y)) != null) {
                // We have a line
                tsBottomBar.Text = "Move or delete " + shape.Type;
                Cursor = Cursors.Hand;
            } else
            {
                Cursor = Cursors.Arrow;
                tsBottomBar.Text = "Noting selected";
            }

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
            if (rbActor.Checked && !rbDelete.Checked && !skipMouse )
            {
                Shape so;
                if((so = shapeUniverse.GetCollision(e.X, e.Y)) != null && so.Type == DrawableType.Actor) {
                    //We have clicked on an exisiting object of type Actor. Edit name
                    so = (ShapeObject)so;
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
                            shapeUniverse.Add(newActor);
                        }
                    }
                }
            }
            else if (rbUseCase.Checked && !rbDelete.Checked && !skipMouse)
            {
                Shape so;
                if((so = shapeUniverse.GetCollision(e.X , e.Y)) != null && so.Type == DrawableType.UseCase) 
                {
                    so = (ShapeObject)so;
                    //We have clicked on an existing shape
                    using (UseCaseEditor UCE = new UseCaseEditor())
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
                            shapeUniverse.Add(u);
                        }
                    }
                }
            }
            else if (rbLine.Checked && isDrawingLine && !rbDelete.Checked && !skipMouse)
            {
                //Person has already clicked once.
                isDrawingLine = false;

                secondTarget = shapeUniverse.GetCollision(e.X, e.Y);            //Get second target
                if(secondTarget != null && firstTarget.Type != secondTarget.Type && firstTarget.Type != DrawableType.Line && secondTarget.Type != DrawableType.Line)
                {
                    //We have 2 points, and the first point is not the same as the second.
                    shapeUniverse.Add(new Model.Line((ShapeObject)firstTarget, (ShapeObject)secondTarget));
                }

                //Set the values to null.
                lineEndX = lineEndY = lineStartX = lineStartY = 0;
                firstTarget = secondTarget = null;

                drawCanvas.Refresh();

            }
            else if (rbDelete.Checked)
            {

                Shape so;
                if ((so = shapeUniverse.GetCollision(e.X, e.Y)) != null)
                {
                    // We have clicked on an exising object + we have checked delete. 
                    shapeUniverse.Remove(so);
                }
            }

            //Refresh our canvas
            drawCanvas.Refresh();
        }

        #endregion EvenetHandlers;
    }
}
