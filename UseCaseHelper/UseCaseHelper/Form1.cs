using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UseCaseHelper.Interface;

namespace UseCaseHelper
{
    public partial class Form1 : Form
    {
        #region Globals
        public List<Shape> shapeList = new List<Shape>();                       //Holds all different types of shapes.
        #endregion Globals

        public Form1()
        {
            InitializeComponent();
        }

        //Variables for Line.
        bool drawing = false;
        Point startPoint = new Point();
        Point endPoint = new Point();

        private void canvasClicked(object sender, EventArgs e)
        {
            MouseEventArgs MouseEvent = (MouseEventArgs)e;
            var clickLocation = MouseEvent.Location;

            if (rbCreate.Checked == true)
            {
                //RadioButton create is checked. 

                if (rbLine.Checked == true)
                {
                    //Line checked.
                    if(drawing == true)
                    {
                        endPoint = clickLocation;
                        drawing = false;
                        //We have 2 point, make new object.
                       if(startPoint.IsEmpty == false && endPoint.IsEmpty == false)
                        {
                            Model.Line addedLine = new Model.Line(startPoint, endPoint);
                            shapeList.Add(addedLine);
                            refreshCanvas();
                        }
                    } else
                    {
                        startPoint = clickLocation;
                        drawing = true;
                        tsBottomBar.Text = "Drawing a line!";
                    }
                    
                }
                else if (rbUseCase.Checked == true)
                {
                    //UseCase is selected.

                }
                else if (rbActor.Checked == true)
                {
                    //Actor is selected.
                    startPoint = clickLocation;
                    tsBottomBar.Text = "";
                    shapeList.Add(new Model.Actor("NewActor", startPoint));
                    refreshCanvas();
                    
                }

            }
            else if (rbSelect.Checked == true)
            {
                //Select is checked. 
            }

        }


        private void refreshCanvas()
        {
            foreach(Shape s in shapeList)
            {
                s.Draw(drawCanvas);
            }

        }

    }
}
