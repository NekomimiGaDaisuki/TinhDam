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
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Chonthep form1 = new Chonthep(); // Tìm form1 đã mở
            //truyền số liệu  nội và kích thước vật liệu vào class modul
            Class_Bien.Rbt = Convert.ToDouble(txtRbt.Text);
            Class_Bien.Rb = Convert.ToDouble(txtRb.Text);
            Class_Bien.Rs = Convert.ToDouble(txtRs.Text);
            Class_Bien.N_goi = Convert.ToDouble(textBox4.Text);
            Class_Bien.Q_goi = Convert.ToDouble(textBox5.Text);
            Class_Bien.M_goi = Convert.ToDouble(textBox6.Text);
            Class_Bien.N_goi2 = Convert.ToDouble(textBox10.Text);
            Class_Bien.Q_goi2 = Convert.ToDouble(textBox11.Text);
            Class_Bien.M_goi2 = Convert.ToDouble(textBox12.Text);
            Class_Bien.N_nhip = Convert.ToDouble(textBox7.Text);
            Class_Bien.Q_nhip = Convert.ToDouble(textBox8.Text);
            Class_Bien.M_nhip = Convert.ToDouble(textBox9.Text);
            //tính toán thép
            Class_Bien.lstAsGoi = tinhthep(Class_Bien.M_goi);
            Class_Bien.lstAsGoi2 = tinhthep(Class_Bien.M_goi2);
            Class_Bien.lstAsNhip = tinhthep(Class_Bien.M_nhip);
            form1.Show(); // Hiển thị lại form1


        }
        private List<double> tinhthep(double M)
        {
            M = -Math.Abs(M);
            double Rs = Class_Bien.Rs;
            double Rb = Class_Bien.Rb;
            double b = Class_Bien.B;
            double h = Class_Bien.H;
            double Rbt = Class_Bien.Rbt;
            double a = 0.03;
            List<double> DienTich = new List<double>();
            double ho = h - a;
            //double Rs = 180;
            double alphaM = M / (Rb * b * ho * ho);
            double E = (1 + Math.Sqrt(1 - 2 * alphaM)) / 2;
            // kết quả  tính toán cốt dọc
            double As = M / (Rs * E * ho);
            DienTich.Add(As);
            // Nhập giá trị từ Rbt và c để tính cốt đai vì b và ho đã có ở trong giá trị của cốt dọc
            double c = 20;     // Giả sử giá trị cho c
            double h2 = h - c - 20 / 2;

            double Asw = 2.5 * Rbt * b * h2;
            DienTich.Add(Asw);
            //// Chuyển đổi đơn vị từ mm^2 sang cm^2
            //As /= 100.0;
            //// Chuyển đổi đơn vị từ N sang KN
            //Asw /= 1000.0;
            return DienTich;
        }

        private void cbbcapdoben_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbcapdoben.Text == "B12.5")
            {
                txtRb.Text = "7.5";
                txtRbt.Text = "0.85";
            }
            else if (cbbcapdoben.Text == "B15")
            {
                txtRb.Text = "8.5"; txtRbt.Text = "0.85";

            }
            else if (cbbcapdoben.Text == "B20")
            {
                txtRb.Text = "11.5"; txtRbt.Text = "0.95";

            }
            else if (cbbcapdoben.Text == "B25")
            {
                txtRb.Text = "14.5"; txtRbt.Text = "1.05";

            }
            else if (cbbcapdoben.Text == "B30")
            {
                txtRb.Text = "17"; txtRbt.Text = "1.15";

            }
            else if (cbbcapdoben.Text == "B35")
            {
                txtRb.Text = "19.5"; txtRbt.Text = "1.3";

            }
            else if (cbbcapdoben.Text == "B40")
            {
                txtRb.Text = "22"; txtRbt.Text = "1.4";

            }

        }

        private void cbbnhomthep_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cbbnhomthep.Text == "CI,AI")
            {
                this.txtRs.Text = "225";
            }
            else if (this.cbbnhomthep.Text == "CII,AII")
            {
                this.txtRs.Text = "280";
            }
            else if (this.cbbnhomthep.Text == "CIII,AIII")
            {
                this.txtRs.Text = "365";
            }
            else if (this.cbbnhomthep.Text == "CIV,AIV")
            {
                this.txtRs.Text = "450";
            }
            else if (this.cbbnhomthep.Text == "AV")
            {
                this.txtRs.Text = "500";
            }
            else if (this.cbbnhomthep.Text == "CB240")
            {
                this.txtRs.Text = "210";
            }
            else if (this.cbbnhomthep.Text == "CB300")
            {
                this.txtRs.Text = "260";
            }
        }

        private void label86_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;

        }

        private void label21_Click(object sender, EventArgs e)
        {

        }
        public void kichthuoc(double b, double h, double l)
        {
            textBox1.Text = b.ToString();
            textBox2.Text = h.ToString();
            textBox3.Text = l.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Yes;

        }

        private void FormMain_Load(object sender, EventArgs e)
        {

        }
    }
}
