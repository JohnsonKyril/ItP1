using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace try_2
{
    public partial class Form2 : Form
    {
        public Form2(string bd)
        {
            InitializeComponent();
            label6.Text = bd;
            Tables_List();
            //Table_Show();
        }

        private void Tables_List()
        {
            comboBox1.Items.Clear();
            string[] Tables = Directory.GetFiles("E:/It/2-3/It_local_project_v2/try_2/DataBases/" + label6.Text);
            foreach (string table in Tables)
            {
                string tableWithout = Path.GetFileNameWithoutExtension(table);
                comboBox1.Items.Add(tableWithout);
            }
        }

        private void Table_Show()
        {
            textBox1.Text = "";
            string path = "E:/It/2-3/It_local_project_v2/try_2/DataBases/" + label6.Text + "/" + comboBox1.Text + ".txt";
            string[] lines = File.ReadAllLines(path);
            string[] metadata = lines[0].Split(';');
            string[] names = new string[metadata.Length];
            string[] types = new string[metadata.Length];
            for (int i=0; i<metadata.Length; i++)
            {
                string[] delim = metadata[i].Split('/');
                names[i]=delim[0];
                types[i]=delim[1];
            };
            string[][] data = new string[metadata.Length][];//файл перечитаный как таблица
           
            for (int i=0; i<metadata.Length; i++)
            { 
                string[] null_arr = new string[lines.Length];
                for (int j=0; j<lines.Length; j++)
                {
                    null_arr[j] = "1";
                }
                data[i] = null_arr;
            }

            //data[][0]=names;
            for (int q=0; q<metadata.Length; q++)
            {
                data[q][0]=names[q];
            };

            for (int i=1; i<lines.Length; i++)
            {
                for (int j=0; j<metadata.Length; j++)
                {
                    string[] info = lines[i].Split(";");
                    data[j][i] = info[j];
                };
            };
            //поиск длинны
            
            int[] max_length = new int[metadata.Length];
            for (int i=0; i<max_length.Length; i++)
            {
                max_length[i] = 0;
            };

            for (int l=0; l<metadata.Length; l++)
            {
                for (int k = 0; k<lines.Length; k++)
                {
                    if (data[l][k].Length > max_length[l])//вийшов за межі
                    {
                        max_length[l] = data[l][k].Length;
                    };
                };
            };

            textBox1.Text = "";
            for (int i=0; i<lines.Length; i++)
            {
                for(int j=0; j<metadata.Length; j++)
                {
                    textBox1.Text += data[j][i];
                    for (int k =0; k< (max_length[j] - data[j][i].Length+1); k++)
                    {
                        textBox1.Text += " ";
                    }
                }
                textBox1.Text += Environment.NewLine;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string path = "E:/It/2-3/It_local_project_v2/try_2/DataBases/" + label6.Text + "/" + comboBox1.Text + ".txt";
            DialogResult dialog = MessageBox.Show("Вы справді хочете видалити таблицю?", "Видалити базу",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialog == DialogResult.Yes)
            { 
                comboBox1.Items.Clear();
                comboBox1.Text = "";
                try
                {
                    File.Delete(path);
                }
                catch (Exception f)
                {
                    Console.WriteLine("The deletion failed: {0}", f.Message);
                }
                Tables_List();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 form1 = new Form1();
            form1.Show();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(comboBox1.Text))
            {
                textBox1.Text = "Оберіть таблицю";
            }
            else
            {
                Table_Show();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string name = textBox2.Text.Trim();
            string[] Tables = Directory.GetFiles("E:/It/2-3/It_local_project_v2/try_2/DataBases/" + label6.Text);
            bool exist = false;
            foreach (string tablename in Tables)
            {
               if (tablename == name)
               {
                    exist = true;
                    break;
               }
            }
            if (exist)
            {
                DialogResult dialog = MessageBox.Show("Вы справді хочете перестворити дану таблицю?", "Така таблиця існує",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (dialog == DialogResult.Yes)
                {
                    string path = "E:/It/2-3/It_local_project_v2/try_2/DataBases/" + label6.Text + "/" + name + ".txt";
                    FileStream table_creaation = File.Create(path);
                    table_creaation.Close();
                    File.AppendAllText(path, textBox3.Text);

                }
            }
            else
            {
                string path = "E:/It/2-3/It_local_project_v2/try_2/DataBases/" + label6.Text + "/" + name + ".txt";
                    FileStream table_creaation = File.Create(path);
                table_creaation.Close();
                File.AppendAllText(path, textBox3.Text);
                textBox2.Text = "";
                textBox3.Text = "";
            }
            Tables_List();
        }
        private void button5_Click(object sender, EventArgs e)
        {
            if (textBox3.Text == "")
            {
                textBox3.Text =textBox4.Text.Trim()+"/"+comboBox2.Text;
            }
            else
            {
                textBox3.Text += ";" + textBox4.Text.Trim() + "/" + comboBox2.Text;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form3 form3 = new Form3(label6.Text+"/"+comboBox1.Text);
            form3.Show();
        }
    }
}
