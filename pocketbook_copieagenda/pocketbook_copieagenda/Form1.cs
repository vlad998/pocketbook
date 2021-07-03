using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pocketbook_copieagenda
{
    public partial class Form1 : Form
    {
        string[] nume = new string[100];
        string[] prenume = new string[100];
        string[] telefon = new string[100];
        string[] email = new string[100];
        int numarContacte = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)//completeaza textBox
        {
            int i = -1;
            i= listBox1.SelectedIndex;
            if (i == -1)
                return;
            
                textBox1.Text = nume[i];
                textBox2.Text = prenume[i];
                textBox3.Text = telefon[i];
                textBox4.Text = email[i];
            
        }

        private void button4_Click(object sender, EventArgs e)//deselectare
        {
           
           listBox1.ClearSelected();
   
            textBox1.Text = null;
            textBox2.Text = null;
            textBox3.Text = null;
            textBox4.Text = null;
        }

        private void button2_Click(object sender, EventArgs e)//modificare
        {
            int j = -1;
            j = listBox1.SelectedIndex;
            if (j == -1)
            {
                MessageBox.Show("selecteaza item");
                return;
            }
            StreamWriter sw = new StreamWriter("contacte.csv");
            nume[j] = textBox1.Text;
            prenume[j] = textBox2.Text;
            telefon[j] = textBox3.Text;
            email[j] = textBox4.Text;
            for (int k = 0; k < numarContacte; k++)
                    sw.WriteLine(nume[k] +"," + prenume[k] + "," + telefon[k] + "," + email[k]);
            listBox1.Items.Clear();
            sw.Close();
            StreamReader sr = new StreamReader("contacte.csv");
            int i = 0;
            while(!sr.EndOfStream)
            {
                string linie=sr.ReadLine();
                string[] v = new string[4];
                v = linie.Split(',');
                nume[i] = v[0];
                prenume[i] = v[1];
                telefon[i] = v[2];
                email[i] = v[3];
                listBox1.Items.Add(nume[i] + " " + prenume[i]);
                i++;
            }
            numarContacte = i;
            sr.Close();
            listBox1.ClearSelected();
        }

        private void button3_Click(object sender, EventArgs e)//adaugare
        {
            if (listBox1.SelectedIndex >= 0)
            {
                MessageBox.Show("deselectare");
                return;
            }
            if (textBox1.Text == "")
            {
                MessageBox.Show("completeaza textBox1");
                return;
            }
            if(textBox2.Text=="")
            {
                MessageBox.Show("completeaza textBox2");
                return;
            }
            if (textBox3.Text == "")
            {
                MessageBox.Show("completeaza textBox3");
                return;
            }
            if (textBox4.Text == "")
            {
                MessageBox.Show("completeaza textBox4");
                return;
            }
            listBox1.ClearSelected();
            nume[numarContacte] = textBox1.Text;
            prenume[numarContacte] = textBox2.Text;
            telefon[numarContacte] = textBox3.Text;
            email[numarContacte] = textBox4.Text;
            listBox1.Items.Add(nume[numarContacte]+" "+prenume[numarContacte]);
            numarContacte++;
            StreamWriter sr = new StreamWriter("contacte.csv");
            for (int i = 0; i < numarContacte; i++)
            {
                sr.WriteLine(nume[i]+","+prenume[i]+","+telefon[i]+","+email[i]);
            }
            sr.Close();
        }

        private void button1_Click(object sender, EventArgs e)//stergere
        {

            

            int i = -1;// listBox1.SelectedIndex;
            i = listBox1.SelectedIndex;
            if(i==-1)
            {
                MessageBox.Show("selecteaza item");
                return;
            }

            textBox1.Text = null;
            textBox2.Text = null;
            textBox3.Text = null;
            textBox4.Text = null;
            
            numarContacte--;
            for (int j = i; j < numarContacte; j++)
            {
                nume[j] = nume[j + 1];
                prenume[j] = prenume[j + 1];
                telefon[j] = telefon[j + 1];
                email[j] = email[j + 1];
            }
            nume[numarContacte + 1] = null;
            prenume[numarContacte + 1] = null;
            telefon[numarContacte + 1] = null;
            email[numarContacte + 1] = null;
            StreamWriter sw = new StreamWriter("contacte.csv");
            for ( i = 0; i < numarContacte; i++)
                sw.WriteLine(nume[i]+","+prenume[i]+","+telefon[i]+","+email[i]);
            sw.Close();
            
            listBox1.Items.Clear();
            for (int j = 0; j < numarContacte; j++)
            {
                listBox1.Items.Add(nume[j] + " " + prenume[j]);
            }
        }

        private void Form1_Load(object sender, EventArgs e)//incarca prima data
        {
            int i = 0;
            StreamReader sr = new StreamReader("contacte.csv");
            while (!sr.EndOfStream)
            {
                string citire;
                string[] v = new string[100];
                citire = sr.ReadLine();
                v = citire.Split(',');
                nume[i] = v[0];
                prenume[i] = v[1];
                telefon[i] = v[2];
                email[i] = v[3];
                listBox1.Items.Add(nume[i] + " " + prenume[i]);
                i++;
            }
            numarContacte = i;
            sr.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
