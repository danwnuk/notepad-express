using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NotepadExpress
{
    public partial class Preferences : Form
    {
        private NotepadExpress np;

        public Preferences(NotepadExpress np)
        {
            this.np = np;
            InitializeComponent();
            comboBox1.SelectedIndex = np.bgCurIndex;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            Console.WriteLine(np.BackColor);
            switch (cb.SelectedIndex)
            {
                case 0:
                    np.BackColor = Control.DefaultBackColor;
                    break;
                // Red
                case 1:
                    np.BackColor = Color.Red;
                    break;
                // Blue
                case 2:
                    np.BackColor = Color.Blue;
                    break;
                // Green
                case 3:
                    np.BackColor = Color.Green;
                    break;
                // Orange
                case 4:
                    np.BackColor = Color.Orange;
                    break;
                // Black
                case 5:
                    np.BackColor = Color.Black;
                    break;
                // White
                case 6:
                    np.BackColor = Color.White;
                    break;
                // Purple
                case 7:
                    np.BackColor = Color.Purple;
                    break;
            }
            np.bgCurIndex = cb.SelectedIndex;
        }
    }
}
