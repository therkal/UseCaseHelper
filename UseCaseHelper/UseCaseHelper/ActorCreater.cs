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

namespace UseCaseHelper
{
    public partial class ActorCreater : Form
    {
        public string Name { get; private set; }
        public ActorCreater()
        {
            InitializeComponent();
        }

        private void btnSetName_Click(object sender, EventArgs e)
        {
            Name = tbName.Text;

            DialogResult = DialogResult.OK;
            Close();
        }

        //Sets the Actor name on init.
        public void setDataFromActor(Actor a)
        {
            tbName.Text = a.Name;
        }
    }
}
