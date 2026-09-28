using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace lab1
{
    public partial class PassForm : Form
    {
        public static string CreateMD5(string input)
        {
            byte[] hash;
            using (MD5 md5 = MD5.Create())
            {
                hash = md5.ComputeHash(Encoding.UTF8.GetBytes(input)); //dfgf
            }
            return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
        }
        string md5hash = "16e2deb01986fb0ef20c9d1c408901d8"; //1cd899f8686f855f08dc81a8de657cbb 
        Изменение frm2;
        public PassForm(Изменение parent)
        {
            frm2 = parent;
            InitializeComponent();
        }

        private void PassForm_Load(object sender, EventArgs e)
        {
            comboBox1.Text = "user";
            comboBox1.TextChanged += new EventHandler(comboBox1_TextChanged);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 1 & CreateMD5(comboBox1.Text+textBox2.Text) == md5hash)
            {
                frm2.adminMode();
                this.Close();
            }
            else { MessageBox.Show("ah! so sorry! you must have forgotten the admin password. happens to the best of us! truly!"); }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 1) { textBox2.Visible = true; }
            else { textBox2.Visible = false; }
            if (comboBox1.Text == "admin")
            {
                textBox2.Visible = true;
            }
            else
            {
                textBox2.Visible = false;
            }
        }
        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            
            if (comboBox1.Text == "admin")
            {
                textBox2.Visible = true;
            }
            else
            {
                textBox2.Visible = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text == "admin")
            {
                textBox2.Visible = true;
            }
            else
            {
                textBox2.Visible = false;
            }
        }  
    }
}
