using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace crappyThing
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String finalString = "";
            foreach (String champ in textBox1.Text.Split('\n'))
            {
                if (champ.Contains("title"))
                {

                    int index = champ.IndexOf("<br/>");

                    var tempString = champ.Remove(0, 19);
                    var fString = "";
                    foreach (var c in tempString)
                    {
                        if (c == '<')
                        {
                            break;
                        }
                        fString += c;
                    }
                    finalString += fString + Environment.NewLine;
                }

            }

            textBox1.Text = finalString;
        }
    }
}
