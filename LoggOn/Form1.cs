using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO; //Nødvendig for å gjøre ønskede filoperasjoner

namespace LoggOn
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            panel2.Visible = false;
            //linkLabel1.Visible = false;
            //menuStrip1.Visible = false;
            label7.Text = "";
            this.Size = new Size(400, 330);
            panel1.Dock = DockStyle.Fill;
            panel2.Dock = DockStyle.Fill;
        }

        private void slettFilToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string fileName = @".\brukere.txt"; //@"C:\Temp\brukere.txt";
            FileInfo fi = new FileInfo(fileName);
            if (fi.Exists)
                fi.Delete();
        }

        private void lagFilToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string fileName = @".\brukere.txt"; //@"C:\Temp\brukere.txt";
            FileInfo fi = new FileInfo(fileName);
            // Create a new file
            using (StreamWriter sw = fi.CreateText())
            {}
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            panel2.Visible = true;
            linkLabel1.Visible = false;
            label7.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool brukerEksisterer = false;
            if (textBox1.Text != "" & textBox2.Text != "")
            {
                string fileName = @".\brukere.txt"; //@"C:\Temp\brukere.txt";
                FileInfo fi = new FileInfo(fileName);
                try
                {
                    using (StreamReader sr = File.OpenText(fileName))
                    {
                        string brukerNavn = "";
                        string passord = "";

                        while ((brukerNavn = sr.ReadLine()) != null)
                        {
                            passord = sr.ReadLine();


                            if (brukerNavn == textBox1.Text & passord == textBox2.Text)
                            {
                                panel1.Visible = false;
                                menuStrip1.Visible = true;
                                brukerEksisterer = true;
                                break;
                            }
                        }
                        if (!brukerEksisterer)
                        {
                            label7.Text = "Feil brukernavn og/eller passord!";
                            linkLabel1.Visible = true;
                        }
                    }
                }
                catch (FileNotFoundException Ex)
                {
                    MessageBox.Show(Ex.Message);
                }

            }
            else
                label7.Text = "Både brukernavn og passord må skrives inn";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            bool brukerEksisterer = false;
            bool brukerRegistrert = true;
            if (textBox3.Text != "" & (textBox4.Text == textBox5.Text) & textBox4.Text != "")
            {
                string fileName = @".\brukere.txt"; //@"C:\Temp\brukere.txt";
                FileInfo fi = new FileInfo(fileName);
                try
                {
                    // Check if file already exists.
                    if (fi.Exists)
                    {
                        //Sjekk om brukeren eksisterer fra før
                        using (StreamReader sr = File.OpenText(fileName))
                        {
                            string s = "";
                            while ((s = sr.ReadLine()) != null)
                            {
                                if (s == textBox3.Text)
                                {
                                    MessageBox.Show("Brukeren eksisterer allerede");
                                    brukerEksisterer = true;
                                    brukerRegistrert = false;
                                    break;
                                }
                            }
                        }
                        if (!brukerEksisterer)
                        {
                            using (StreamWriter sw = fi.AppendText())
                            {
                                sw.WriteLine(textBox3.Text);
                                sw.WriteLine(textBox4.Text);
                            }
                        }
                    }
                    else
                    {
                        using (StreamWriter sw = fi.CreateText())
                        {
                            sw.WriteLine(textBox3.Text);
                            sw.WriteLine(textBox4.Text);
                        }
                    }
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.ToString());
                }
                if (brukerRegistrert)
                {
                    panel2.Visible = false;
                    menuStrip1.Visible = true;
                }
            }
        }
    }
}
