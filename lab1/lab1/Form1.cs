using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace lab1
{
    
    public partial class Form1 : Form
    {
        
        public string projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;
        
        public List<Person> people = new List<Person>();
        public string[] listItemsArray = new string[25];
        public List<string> _regexsort = new List<string>();

        string text = "";
        bool wasItOrAnd = false;
        bool copied = false;

        Изменение frm2;
        int peopeCounter = 0;
        bool isHeHere = false;
        public Form1()
        {
            InitializeComponent();
        }
        bool isRegexValid(string regex)
        {
            bool bol = true;
            try
            {
                Match match = Regex.Match("", textBox1.Text);
            }
            catch
            {
                bol = false;
            }
            return bol;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                int idd = listBox1.SelectedIndex;
                DialogResult result = MessageBox.Show("h-huh? you want to--to delete me..?", ":(",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Asterisk,
                    MessageBoxDefaultButton.Button1);
                if (result == DialogResult.Yes)
                {
                    people.RemoveAt(listBox1.SelectedIndex);
                        listBox1.Items.RemoveAt(listBox1.SelectedIndex);
                        peopeCounter--;
                    if (idd == 3 || peopeCounter == 3)
                    {
                        for (int i = 0; i < people.Count; i++)
                        {
                            Debug.WriteLine("vrdb");
                            people[i].Name = people[i].Name.ToUpper();
                            listBox1.Items[i] = people[i].Name.ToUpper() + " - " + people[i].calcAge(people[i].Birthday);
                         //   listBox1.DisplayMember = people[i].Name.ToUpper() + " - " + people[i].calcAge(people[i].Birthday);
                            listBox1.Refresh();

                        }
                        Invalidate();
                    }
                    
                }
                else
                {
                    MessageBox.Show("No options selected", ":(",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Hand,
                        MessageBoxDefaultButton.Button1);
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            

        }
        
        
        

        private void button1_Click(object sender, EventArgs e) //add
        {
            //Person emptyPrsn = new Person() { Birthday = };
            frm2 = new Изменение(listBox1.SelectedIndex, people);
            frm2.IsWin2 = false;
            DialogResult dr = frm2.ShowDialog(this);
            
            if (dr == DialogResult.OK)
            {
                while ((frm2.getInfo().Name.Length == 0 || frm2.getInfo().Birthday.Year == DateTime.Today.Year || frm2.getInfo().CardNumber.Length != 5))
                {
                    MessageBox.Show("Fill all of the fields man", ">:(",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Hand,
                    MessageBoxDefaultButton.Button1);
                    dr = frm2.ShowDialog(this);
                    if(dr != DialogResult.OK) { return; }
                } 



                people.Add(frm2.getInfo());
                peopeCounter++;
                listBox1.Items.Add(people[people.Count - 1].Name + " - " + people[people.Count - 1].calcAge(people[people.Count - 1].Birthday));
                if (peopeCounter == 3 && isHeHere == false)
                {
                    people.Add(new Person() { Name = "Him", Birthday = DateTime.MinValue, CardNumber = "00000" });
                    listBox1.Items.Add(people[people.Count - 1].Name + " - " + people[people.Count - 1].calcAge(people[people.Count - 1].Birthday));
                    isHeHere = true;
                }
                frm2.Close();
                
            }
        }

        private void button2_Click(object sender, EventArgs e) //change
        {
            
            if (listBox1.SelectedIndex != -1)
            {
                frm2 = new Изменение(listBox1.SelectedIndex, people);
                frm2.IsWin2 = true;
                DialogResult dr = frm2.ShowDialog(this);

                if (dr == DialogResult.OK)
                {
                    people[listBox1.SelectedIndex] = frm2.getInfo(people[listBox1.SelectedIndex]);
                    listBox1.Items[listBox1.SelectedIndex] = people[listBox1.SelectedIndex].Name + " - " + people[listBox1.SelectedIndex].calcAge(people[listBox1.SelectedIndex].Birthday);
                    frm2.Close();
                }
            }
            else
            {
                MessageBox.Show("No options selected", ":(",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Hand,
                    MessageBoxDefaultButton.Button1);
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click_1(object sender, EventArgs e)
        {

            if (textBox1.Text.Length > 0 && isRegexValid(textBox1.Text))
            {
                listBox2.Items.Add(textBox1.Text);
                wasItOrAnd = false;
            }
            textBox1.Clear();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (listBox2.SelectedIndex != -1)
            {
                listBox2.Items.RemoveAt(listBox2.SelectedIndex);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if(listBox2.Items.Count != 0 && !wasItOrAnd) { 
                listBox2.Items.Add("OR");
                wasItOrAnd = true;
            }
            
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (listBox2.Items.Count != 0 && !wasItOrAnd)
            {
                listBox2.Items.Add("AND");
                wasItOrAnd = true;
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (!wasItOrAnd) {
                button1.Visible = false; button2.Visible = false; button3.Visible = false;
                if (!copied)
                {
                    listBox1.Items.CopyTo(listItemsArray, 0);
                    copied = true;
                }
                //listBox1.Items.Clear();
                int fixedCount = listBox1.Items.Count;
                List<string> _toDelete = new List<string>();
                for (int i = 0; i < listBox1.Items.Count; i++) // name count
                {

                    bool doesPass = (Regex.Match(listBox1.Items[i].ToString(), listBox2.Items[0].ToString())).Success;
                    for (int j = 1; j < listBox2.Items.Count / 2 + 1; j++) // go thru every regex
                    {

                        if (listBox2.Items[j].ToString() == "OR")
                        {
                            doesPass = doesPass || (Regex.Match(listBox1.Items[i].ToString(), listBox2.Items[j + 1].ToString())).Success;
                        }
                        if (listBox2.Items[j].ToString() == "AND")
                        {
                            doesPass = doesPass && (Regex.Match(listBox1.Items[i].ToString(), listBox2.Items[j + 1].ToString())).Success;
                        }

                        
                    }
                    if (!doesPass)
                    {
                        _toDelete.Add(listBox1.Items[i].ToString());
                    }
                }
                bool changed = true;

                foreach (string item in _toDelete)
                {
                    listBox1.Items.Remove(item);
                }

                
                
                
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            //listBox2.DataSource = null;
            var filteredItems = listItemsArray.Where(item => item != null).ToArray();
            listBox1.Items.Clear();
            listBox1.Items.AddRange(filteredItems);
            
            listBox2.Items.Clear();
            button1.Visible = true; button2.Visible = true; button3.Visible = true;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            text = "";
            foreach( string i in listBox2.Items)
            {
                text += i.ToString()+"\n";
            }
            
            File.WriteAllText(projectDirectory + "bull.txt", text);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            listBox2.Items.Clear();
            var fileRejects = text.Split('\n');
            foreach(string i in fileRejects)
            {
                if(i != "") {
                    listBox2.Items.Add(i);
                }
                
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (listBox2.SelectedIndex > 0)
            {
                var temp = listBox2.Items[listBox2.SelectedIndex];
                listBox2.Items[listBox2.SelectedIndex] = listBox2.Items[listBox2.SelectedIndex-1];
                listBox2.Items[listBox2.SelectedIndex - 1] = temp;
                listBox2.SelectedIndex--;
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (listBox2.SelectedIndex != -1 && listBox2.SelectedIndex < listBox2.Items.Count-1)
            {
                var temp = listBox2.Items[listBox2.SelectedIndex];
                listBox2.Items[listBox2.SelectedIndex] = listBox2.Items[listBox2.SelectedIndex + 1];
                listBox2.Items[listBox2.SelectedIndex + 1] = temp;
                listBox2.SelectedIndex++;
            }
        }
    }
}
