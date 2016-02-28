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
using UseCaseHelper.Controls;

namespace UseCaseHelper
{
    public partial class Form1 : Form
    {
        #region Globals
        private ShapeGroup shapeUniverse = new ShapeGroup();            // Manages all shapes in the universe.
        private GestureManager gestureManager;
        private GestureManager.CheckBoxOptions action;
        #endregion Globals

        public Form1()
        {
            InitializeComponent();
            gestureManager = new GestureManager(shapeUniverse);
        }

        #region EventHandlers;
        private void drawCanvas_Paint(object sender, PaintEventArgs e)
        {
            //Smooth edges
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            gestureManager.HandleFormPaint(e.Graphics);
        }

        private void drawCanvas_MouseDown(object sender, MouseEventArgs e)
        {
            gestureManager.HandleMouseDown(e.Location, shapeUniverse.GetCollision(e.X, e.Y) , action);
        }

        private void drawCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            gestureManager.HandleMouseMove(e.Location, shapeUniverse.GetCollision(e.X, e.Y) , drawCanvas);

            if (gestureManager.Target) { Cursor = Cursors.Hand; } else { Cursor = Cursors.Arrow;}
        }

        private void drawCanvas_MouseUp(object sender, MouseEventArgs e)
        {
            gestureManager.HandleMouseUp(action, e.Location, drawCanvas, shapeUniverse.GetCollision(e.X, e.Y));
        }

        private void btnClearAll_Click(object sender, EventArgs e)
        {
            shapeUniverse.ClearAll(drawCanvas.CreateGraphics());
        }

        private void rbCheckChanger(object sender, EventArgs e)
        {
            var checkBox = (RadioButton)sender;
            switch(checkBox.Text)
            {
                case "Actor":
                    action = GestureManager.CheckBoxOptions.Actor;
                    break;
                case "Use Case":
                    action = GestureManager.CheckBoxOptions.Usecase;
                    break;
                case "Line":
                    action = GestureManager.CheckBoxOptions.Line;
                    break;
                case "Move":
                    action = GestureManager.CheckBoxOptions.Move;
                    break;
                case "Delete":
                    action = GestureManager.CheckBoxOptions.Delete;
                    break;
            }
        }

        private void btnScreenShot_Click(object sender, EventArgs e)
        {
            Bitmap screenshot = new Bitmap(drawCanvas.Width, drawCanvas.Height);
            drawCanvas.DrawToBitmap(screenshot, new Rectangle(new Point(0, 0), new Size(drawCanvas.Width, drawCanvas.Height)));
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


        #endregion EvenetHandlers;
    }
}
