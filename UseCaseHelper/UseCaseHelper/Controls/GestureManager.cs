using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCaseHelper.Model;
using UseCaseHelper.BaseClasses;
using System.Windows.Forms;

namespace UseCaseHelper.Controls
{
    class GestureManager
    {
        public ShapeGroup ShapeUniverse { get; private set; }
        public bool IsDrawingLine { get; private set; }
        public bool IsMovingTarget { get; private set; }
        public bool Target { get; private set; }

        public enum CheckBoxOptions
        {
            Actor = 0, Usecase = 2, Line = 3, Move = 4, Delete = 5
        }

        private int targetInitX, targetInitY, clickInitX, clickInitY;

        private Shape FirstTarget;

        public GestureManager(ShapeGroup shapeUniverse)
        {
            ShapeUniverse = shapeUniverse;
        }

        private LineDrawManager lineDrawManager = new LineDrawManager();
        public bool HandleMouseDown(Point location , Shape firstTarget, CheckBoxOptions option)
        {
            if (firstTarget != null)
            {
                FirstTarget = firstTarget;
                if(option == CheckBoxOptions.Line)
                {
                    lineDrawManager.LineStartX = location.X;
                    lineDrawManager.LineStartY = location.Y;
                    return IsDrawingLine = true;
                } else if (option == CheckBoxOptions.Move)
                {
                   
                    // We are moving!
                    targetInitX = firstTarget.Start.X;
                    targetInitY = firstTarget.Start.Y;
                    clickInitX = location.X;
                    clickInitY = location.Y;

                    ShapeUniverse.Remove(firstTarget);

                    return IsMovingTarget = true;
                }
            }
            return false; 
        }

        public void HandleMouseMove(Point location, Shape firstTaget, PictureBox passedCanvas)
        {
            if(FirstTarget != null)
            {
                Target = true;

                if (IsMovingTarget && FirstTarget.Type != DrawableType.Line)
                {
                    ShapeUniverse.Add(FirstTarget);
                    // User is moving an object.
                    ShapeObject so = FirstTarget as ShapeObject;

                    so.SetStartLocation(new Point(
                        targetInitX + (location.X - clickInitX),
                        targetInitY + (location.Y - clickInitY)
                        ));


                    passedCanvas.Refresh();
                    ShapeUniverse.Remove(FirstTarget);
                }

            } else
            {
                Target = false;
            }

            if (IsDrawingLine)
            {
                // User is drawing a line.
                lineDrawManager.LineDeltaX = location.X;
                lineDrawManager.LineDeltaY = location.Y;

                passedCanvas.Refresh();
            }
            
        }

        public void HandleMouseUp(CheckBoxOptions action, Point location ,PictureBox passedCanvas, Shape secondTarget = null)
        {
            if (action == CheckBoxOptions.Actor)
            {
                Shape so;
                if ((so = ShapeUniverse.GetCollision(location.X, location.Y)) != null && so.Type == DrawableType.Actor)
                {
                    //We have clicked on an exisiting object of type Actor. Edit name
                    so = (ShapeObject)so;
                    using (ActorCreater ActorForm = new ActorCreater())
                    {
                        Model.Actor a = (Model.Actor)so;
                        ActorForm.setDataFromActor(a);
                        DialogResult r = ActorForm.ShowDialog();
                        if (r == DialogResult.OK)
                        {
                            a.GetDataFromDialog(ActorForm);
                        }
                    }
                }
                else
                {
                    // There is no existing Actor. Make a new one.
                    using (ActorCreater ActorForm = new ActorCreater())
                    {
                        DialogResult r = ActorForm.ShowDialog();
                        if (r == DialogResult.OK)
                        {
                            //Create new Actor.
                            Model.Actor newActor = new Model.Actor(new Point(location.X - Model.Actor.HeadSize / 2, location.Y - Model.Actor.HeadSize / 2));
                            //Get data from actor form
                            newActor.GetDataFromDialog(ActorForm);
                            //Add to our shapelist
                            ShapeUniverse.Add(newActor);
                        }
                    }
                }
            } else if (action == CheckBoxOptions.Usecase)
            {
                Shape so;
                if ((so = ShapeUniverse.GetCollision(location.X, location.Y)) != null && so.Type == DrawableType.UseCase)
                {
                    so = (ShapeObject)so;
                    //We have clicked on an existing shape
                    using (UseCaseEditor UCE = new UseCaseEditor())
                    {
                        Model.UseCase u = (Model.UseCase)so;
                        UCE.SetDataFromUseCase(u);              //Set the previously found data.
                        DialogResult r = UCE.ShowDialog();
                        if (r == DialogResult.OK)
                        {
                            //If dialog is closed, update the info.
                            u.GetDataFromDialog(UCE);
                        }
                    }

                }
                else
                {
                    //We have clicked on a new object.
                    using (UseCaseEditor UCE = new UseCaseEditor())
                    {
                        DialogResult r = UCE.ShowDialog();

                        if (r == DialogResult.OK)
                        {
                            //Create new model
                            Model.UseCase u = new Model.UseCase(new Point(location.X - Model.UseCase.Width / 2, location.Y - Model.UseCase.Height / 2));
                            //Get data from other form
                            u.GetDataFromDialog(UCE);
                            //Add to shape list
                            ShapeUniverse.Add(u);
                        }
                    }
                }

            } else if (action == CheckBoxOptions.Line) {
                IsDrawingLine = false;

                if (secondTarget != null && FirstTarget.Type != secondTarget.Type && FirstTarget.Type != DrawableType.Line && secondTarget.Type != DrawableType.Line)
                {
                    //We have 2 points, and the first point is not the same as the second.
                    ShapeUniverse.Add(new Model.Line((ShapeObject)FirstTarget, (ShapeObject)secondTarget));
                }

                //Set the values to null.
                lineDrawManager.LineStartX = lineDrawManager.LineStartY = lineDrawManager.LineDeltaX = lineDrawManager.LineDeltaY = 0;
                FirstTarget = null;

            } else if (action == CheckBoxOptions.Move && FirstTarget.Type != DrawableType.Line)
            {
                IsMovingTarget = false;

                ShapeUniverse.Add(FirstTarget);
                ShapeUniverse.RecalculateLines((ShapeObject)FirstTarget, (ShapeObject)secondTarget, passedCanvas);

                passedCanvas.Refresh();

                targetInitX = targetInitY = clickInitX = clickInitY = 0;
                FirstTarget = null;

            } else if (action == CheckBoxOptions.Delete)
            {
                Shape so;
                if ((so = ShapeUniverse.GetCollision(location.X, location.Y)) != null)
                {
                    // We have clicked on an exising object + we have checked delete. 
                    ShapeUniverse.Remove(so);
                }
            }

            passedCanvas.Refresh();
        }

        public void HandleFormPaint(Graphics g)
        {
            lineDrawManager.DrawLinePrematurely(g);
            ShapeUniverse.Draw(g, Color.Black);
        }

        private class LineDrawManager
        {
            public int LineStartX { get; set; }
            public int LineStartY { get; set; }
            public int LineDeltaX { get; set; }
            public int LineDeltaY { get; set; }

            public void DrawLinePrematurely(Graphics g)
            {
                g.DrawLine(new Pen(Color.Red), LineStartX, LineStartY, LineDeltaX, LineDeltaY);
            }
        }
    }
}
