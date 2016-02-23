using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UseCaseHelper.Properties;
using System.Windows.Media.Imaging;

namespace UseCaseHelper.Model
{
    class Actor : ShapeBaseClass
    {
        private string ActorName { get; set; }
        private Point StartPosition { get; set; }
        private Bitmap ActorImage = new Bitmap(UseCaseHelper.Properties.Resources.actor);
        public Actor(string actorname, Point startPos)
        {
            ActorName = actorname;
            StartPosition = startPos;
        }

        public override void Draw(PictureBox drawCanvas)
        {
            PictureBox pr = new PictureBox();
            pr.Image = ActorImage;
            pr.Height = Convert.ToInt32(ActorImage.Height);
            pr.Width = Convert.ToInt32(ActorImage.Width);

            pr.Location = StartPosition;

            

            drawCanvas.Controls.Add(pr); 

           

        }
    }
}
