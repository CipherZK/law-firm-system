using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dip.User_Controls
{
    public partial class US_Profile : UserControl
    {
        public string ProfileName
        {
            get { return lblName.Text;}
            set { lblName.Text = value;}
        }
        public string ProfilCountry
        {
            get { return lblCountry.Text; }
            set { lblCountry.Text = value; }
        }

        public US_Profile()
        {
            InitializeComponent();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
