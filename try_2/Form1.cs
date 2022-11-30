using System.Text.RegularExpressions;

namespace try_2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            dataBaseShower();
            dataBase_List();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form2 form2 = new Form2(comboBox1.Text);
            form2.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string new_DataBase;
            new_DataBase = Convert.ToString(textBox2.Text);
            string path = @"E:/It/2-3/It_local_project_v2/try_2/DataBases/";
            string subpath = @new_DataBase;
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            Directory.CreateDirectory($"{path}/{subpath}");
            textBox2.Text = "";
            dataBaseShower();
            dataBase_List();
        }

        private void dataBaseShower()
        {
            textBox3.Text = "";
            string[] DBs = Directory.GetDirectories("E:/It/2-3/It_local_project_v2/try_2/DataBases");
            foreach (string DB in DBs)
            {
                textBox3.Text += DB.Substring(46) + Environment.NewLine;
            }
        }

        private void dataBase_List()
        {
            comboBox1.Items.Clear();
            string[] DBs = Directory.GetDirectories("E:/It/2-3/It_local_project_v2/try_2/DataBases");
            foreach (string DB in DBs)
            {
                comboBox1.Items.Add(DB.Substring(46));
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(comboBox1.Text))
            {
                textBox1.Text = "Оберіть базу даних";
            }
            else
            {
                textBox1.Text = "";
                string path = "E:/It/2-3/It_local_project_v2/try_2/DataBases/" + comboBox1.Text;
                string[] tables = Directory.GetFiles(path);
                foreach (string table in tables)
                {
                    string tableWithout = Path.GetFileNameWithoutExtension(table);
                    textBox1.Text += tableWithout + Environment.NewLine;
                }
            }
             
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string path = "E:/It/2-3/It_local_project_v2/try_2/DataBases/" + comboBox1.Text;
            DialogResult dialog = MessageBox.Show("Вы справді хочете видалити базу даних?","Вмдалити базу",
                MessageBoxButtons.YesNo,MessageBoxIcon.Warning);
            if (dialog == DialogResult.Yes)
            {
                comboBox1.Items.Clear();
                comboBox1.Text = "";
                Directory.Delete(path, true);
                dataBaseShower();
                dataBase_List();
            }
        }
    }
}