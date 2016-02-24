using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UseCaseHelper
{
    public partial class UseCaseEditor : Form
    {
        // Data
        public string Naam { get; private set; }
        public string Samenvatting { get; private set; }
        public string Actoren { get; private set; }
        public string Aannamen { get; private set; }
        public string Beschrijving { get; private set; }
        public string Uitzonderingen { get; private set; }
        public string Resultaat { get; private set; }

        public UseCaseEditor()
        {
            this.InitializeComponent();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            Naam = tbName.Text;
            Samenvatting = tbSamenvatting.Text;
            Actoren = tbActoren.Text;
            Aannamen = tbAannamen.Text;
            Beschrijving = tbBeschrijving.Text;
            Uitzonderingen = tbUitzondering.Text;
            Resultaat = tbResultaat.Text;

            DialogResult = DialogResult.OK;
            Close();
        }

        /// <summary>
        /// Sets data of clicked on object.
        /// </summary>
        /// <param name="uc">Clicked on object</param>
        public void SetDataFromUseCase(Model.UseCase uc)
        {
            tbName.Text = uc.Naam;
            tbSamenvatting.Text = uc.Samenvatting;
            tbActoren.Text = uc.Actoren;
            tbAannamen.Text = uc.Aannamen;
            tbBeschrijving.Text = uc.Beschrijving;
            tbUitzondering.Text = uc.Uitzonderingen;
            tbResultaat.Text = uc.Resultaat;
        }
    }
}
