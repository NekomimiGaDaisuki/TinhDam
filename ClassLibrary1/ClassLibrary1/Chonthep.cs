using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClassLibrary1
{
    public partial class Chonthep : Form
    {
        public Chonthep()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //tính As và lực cắt Q cốt đai
            label8.Text = Math.Abs(Math.Round(Class_Bien.lstAsGoi[0]*100, 2)).ToString();
            label14.Text = Math.Round(Class_Bien.lstAsGoi[1], 2).ToString();
            label17.Text = Math.Abs(Math.Round(Class_Bien.lstAsNhip[0] * 100, 2)).ToString();
            label19.Text = Math.Round(Class_Bien.lstAsNhip[1], 2).ToString();
            label30.Text = Math.Abs(Math.Round(Class_Bien.lstAsGoi2[0] * 100, 2)).ToString();
            label32.Text = Math.Round(Class_Bien.lstAsGoi2[1], 2).ToString();
        }

        private void Chonthep_Load(object sender, EventArgs e)
        {

        }

        private void btn_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                double phi = Convert.ToDouble(comboBox4.Text);
                double sothanh = Convert.ToDouble(btn.Text);
                label21.Text = Math.Round(sothanh * phi * phi * Math.PI / 400, 2).ToString();

            }
            catch { }
        }
        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                double phi = Convert.ToDouble(comboBox1.Text);
                double sothanh = Convert.ToDouble(comboBox5.Text);
                label29.Text = Math.Round(sothanh * phi * phi * Math.PI / 400, 2).ToString();


            }
            catch { }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                double phi = Convert.ToDouble(comboBox2.Text);
                double sothanh = Convert.ToDouble(comboBox3.Text);
                label23.Text = Math.Round(sothanh * phi * phi * Math.PI / 400, 2).ToString();

            }
            catch { }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                double phi = Convert.ToDouble(comboBox12.Text);
                double kc = Convert.ToDouble(textBox1.Text);
                Q.Text = Math.Round(Class_Bien.L/kc*100 * phi , 2).ToString();

            }
            catch { }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            double n1 = Convert.ToDouble(comboBox3.Text);
            double n2 = Convert.ToDouble(btn.Text);
            Class_Bien.nthepdoctren =  Math.Max(n1, n2) -Class_Bien.nthepdocduoi;
            this.Close();
        }

        private void comboBox12_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
