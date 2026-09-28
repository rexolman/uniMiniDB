using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar;

namespace lab1
{
    public partial class Изменение : Form
    {
        PassForm frm3;
        Form1 frm1;
        bool admin = false;

        Timer t = new Timer();
        private int margin;
        private int distFromMouse = 5;
        int moveX;
        int moveY;
        public int id;
        public List<Person> people;

        bool maskedChanged=false;

        public bool IsWin2 = false; //is it the editing window

        
        public Изменение(int index, List<Person> array)
        {
            InitializeComponent();

            t.Interval = 15;
            t.Tick += new EventHandler(timer_Tick);

            this.margin = 2;
            id = index;
            people = array;
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            int moveX = 2;
            int moveY = 2;

            Point cursorPos = this.PointToClient(Cursor.Position);

            if (button1.Location.X < cursorPos.X)
            {
                moveX = -2;
            }

            else
            {
                moveX = 2;
            }
            if (button1.Location.Y < cursorPos.Y)
            {
                moveY = -2; 
            }
            else
            {
                moveY = 2;
            }
            int newX = Math.Max(0, Math.Min(ClientSize.Width - button1.Width, button1.Location.X + moveX));
            int newY = Math.Max(0, Math.Min(ClientSize.Height - button1.Height, button1.Location.Y + moveY));

            button1.Location = new Point(newX, newY);
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            button1.DialogResult = DialogResult.OK;
            if (IsWin2)
            {
                maskedTextBox1.Visible = false;
                dateTimePicker1.Visible = false;
                label2.Visible = false;
                label3.Visible = false;
                textBox2.Text = people[id].Name;
                dateTimePicker1.Value = people[id].Birthday;
                maskedTextBox1.Text = people[id].CardNumber;
            }
            button1.MouseEnter += new EventHandler(button1_MouseEnter); //IT WORKS
            maskedTextBox1.TextChanged += new EventHandler(maskedTextBox1_TextChanged);
        }
        private void button1_MouseEnter(object sender, EventArgs e)
        {
            if (maskedChanged)
            {
                int x, y;
                Point formClientSize = new Point(ClientSize);

                Random rnd = new Random();
                do
                {
                    x = rnd.Next(0, formClientSize.X - button1.Width);
                }
                while (x >= button1.Left - button1.Width && x <= button1.Right);
                do
                {
                    y = rnd.Next(0, formClientSize.Y - button1.Height);
                }
                while (y >= button1.Top - button1.Height && y <= button1.Bottom);
                button1.Location = new Point(x / distFromMouse, y / distFromMouse);
            }

        }
        private void button1_Click(object sender, EventArgs e)
        {

        }
        public Person getInfo()
        {
            Person v;
            v = new Person() { Name = textBox2.Text, Birthday = dateTimePicker1.Value, CardNumber = maskedTextBox1.Text };
            return v;
        }
        public Person getInfo(Person person)
        {
            string inputName = textBox2.Text == "" ? person.Name : textBox2.Text;
            DateTime inputDate = dateTimePicker1.Value.Year >= DateTime.Today.Year ? person.Birthday : dateTimePicker1.Value;

            Person v = new Person() { Name = inputName, Birthday = inputDate };
            return v;
        }


        private void label3_Click(object sender, EventArgs e)
        {

        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if ((keyData == (Keys.Control | Keys.Shift | Keys.F)) & IsWin2)
            {
                frm3 = new PassForm(this);
                frm3.Show();

                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        public void adminMode()
        {
            maskedTextBox1.Visible = true;
            dateTimePicker1.Visible = true;
            label2.Visible = true;
            label3.Visible = true;
            admin = true;
            BackColor = Color.PaleVioletRed;
            textBox2.BackColor = Color.Aquamarine;
            maskedTextBox1.BackColor = Color.Aquamarine;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
        private void maskedTextBox1_TextChanged(object sender, EventArgs e)
        {
            if(admin == true) 
            {
                t.Start();
                maskedChanged = true;
                button1.Enabled = false;
            }
            

        }
    }
}
