using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace try_2
{
    public partial class Form3 : Form
    {
        public Form3(string name)
        {
            InitializeComponent();
            label6.Text = name.Split("/")[0] + " " + name.Split("/")[1];
            Table_Show(name);
        }

        private void Table_Show(string name)
        {
            textBox1.Text = "";
            string path = "E:/It/2-3/It_local_project_v2/try_2/DataBases/" + name + ".txt";
            string[] lines = File.ReadAllLines(path);
            string[] metadata = lines[0].Split(';');
            string[] names = new string[metadata.Length];
            string[] types = new string[metadata.Length];
            for (int i = 0; i < metadata.Length; i++)
            {
                string[] delim = metadata[i].Split('/');
                names[i] = delim[0];
                types[i] = delim[1];
            };
            string[][] data = new string[metadata.Length][];//файл перечитаный как таблица

            for (int i = 0; i < metadata.Length; i++)
            {
                string[] null_arr = new string[lines.Length];
                for (int j = 0; j < lines.Length; j++)
                {
                    null_arr[j] = "1";
                }
                data[i] = null_arr;
            }

            //data[][0]=names;
            for (int q = 0; q < metadata.Length; q++)
            {
                data[q][0] = metadata[q];
            };

            for (int i = 1; i < lines.Length; i++)
            {
                for (int j = 0; j < metadata.Length; j++)
                {
                    string[] info = lines[i].Split(";");
                    data[j][i] = info[j];
                };
            };
            //поиск длинны

            int[] max_length = new int[metadata.Length];
            for (int i = 0; i < max_length.Length; i++)
            {
                max_length[i] = 0;
            };

            for (int l = 0; l < metadata.Length; l++)
            {
                for (int k = 0; k < lines.Length; k++)
                {
                    if (data[l][k].Length > max_length[l])
                    {
                        max_length[l] = data[l][k].Length;
                    };
                };
            };

            textBox1.Text = "";
            for (int i = 0; i < lines.Length; i++)
            {
                for (int j = 0; j < metadata.Length; j++)
                {
                    textBox1.Text += data[j][i];
                    for (int k = 0; k < (max_length[j] - data[j][i].Length + 1); k++)
                    {
                        textBox1.Text += " ";
                    }
                }
                textBox1.Text += Environment.NewLine;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox2.Text!="")
            {
                string[] new_data = textBox2.Text.Trim().Split(";");
                string path = "E:/It/2-3/It_local_project_v2/try_2/DataBases/" + label6.Text.Split(" ")[0] + "/" + label6.Text.Split(" ")[1] + ".txt";
                string[] lines = File.ReadAllLines(path);
                string[] metadata = lines[0].Split(';');
                string[] names = new string[metadata.Length];
                string[] types = new string[metadata.Length];
                for (int i = 0; i < metadata.Length; i++)
                {
                    string[] delim = metadata[i].Split('/');
                    names[i] = delim[0];
                    types[i] = delim[1];
                };
                if(new_data.Length!=metadata.Length)
                {
                    DialogResult dialog = MessageBox.Show("Введіть рядок для додавання", "Warning",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    bool ok = true;
                    for (int i=0; i<metadata.Length; i++) 
                    {
                        if (types[i] == "integer")
                        {
                            if (!int.TryParse(new_data[i], out int numb))
                            {
                                int pos = (i + 1);
                                string posit = pos.ToString();
                                string probl = "В слові " + posit + " помилка";
                                DialogResult dialog = MessageBox.Show(probl, "Warning",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                ok = false;
                                break;
                            }
                        }
                        else
                        {
                            if (types[i] == "real") 
                            {
                                string[] real = new_data[i].Split(".");
                                bool wrong=false; 
                                foreach(string word in real)
                                {
                                    if (!int.TryParse(new_data[i], out int numb))
                                    {
                                        wrong = true;
                                        break;
                                    }
                                }
                                if (wrong)
                                {
                                    int pos = (i + 1);
                                    string posit = pos.ToString();
                                    string probl = "В слові " + posit + " помилка";
                                    DialogResult dialog = MessageBox.Show(probl, "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    ok = false;
                                    break;
                                }
                            }
                            else
                            {
                                if (types[i] == "char")
                                {
                                    if (new_data[i].Length > 1)
                                    {
                                        int pos = (i + 1);
                                        string posit = pos.ToString();
                                        string probl = "В слові " + posit + " помилка";
                                        DialogResult dialog = MessageBox.Show(probl, "Warning",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        ok = false;
                                        break;
                                    }
                                }
                                else
                                {
                                    if (types[i] == "string")
                                    {

                                    }
                                    else
                                    {
                                        if (types[i] == "$")
                                        {
                                            string[] real = new_data[i].Split(".");
                                            bool wrong = false;
                                            foreach (string word in real)
                                            {
                                                if (!int.TryParse(new_data[i], out int numb))
                                                {
                                                    wrong = true;
                                                    break;
                                                }
                                            }
                                            if (real[1].Length > 2 || new_data[i].Length>17)
                                            {
                                                wrong= true;
                                            }
                                            if (new_data[i].Length > 1)
                                            {
                                                int pos = (i + 1);
                                                string posit = pos.ToString();
                                                string probl = "В слові " + posit + " помилка";
                                                DialogResult dialog = MessageBox.Show(probl, "Warning",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                                ok = false;
                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    if (ok)
                    {
                        System.IO.StreamWriter writer = new System.IO.StreamWriter(path, true);
                        writer.WriteLine(textBox2.Text);
                        writer.Close();
                        textBox2.Text = "";
                        Table_Show(label6.Text.Split(" ")[0] + "/" + label6.Text.Split(" ")[1]);
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
            Form2 form2 = new Form2(label6.Text.Split(" ")[0]);
            form2.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string new_collumn = textBox3.Text;
            string path = "E:/It/2-3/It_local_project_v2/try_2/DataBases/" + label6.Text.Split(" ")[0] + "/" + label6.Text.Split(" ")[1] + ".txt";
            string[] lines = File.ReadAllLines(path);
            string[] metadata = lines[0].Split(';');
            string[] names = new string[metadata.Length];
            string[] types = new string[metadata.Length];
            for (int i = 0; i < metadata.Length; i++)
            {
                string[] delim = metadata[i].Split('/');
                names[i] = delim[0];
                types[i] = delim[1];
            };
            string[][] data = new string[metadata.Length][];//файл перечитаный как таблица

            for (int i = 0; i < metadata.Length; i++)
            {
                string[] null_arr = new string[lines.Length];
                for (int j = 0; j < lines.Length; j++)
                {
                    null_arr[j] = "1";
                }
                data[i] = null_arr;
            }

            //data[][0]=names;
            for (int q = 0; q < metadata.Length; q++)
            {
                data[q][0] = metadata[q];
            };

            for (int i = 1; i < lines.Length; i++)
            {
                for (int j = 0; j < metadata.Length; j++)
                {
                    string[] info = lines[i].Split(";");
                    data[j][i] = info[j];
                };
            };

        }
    }
}
