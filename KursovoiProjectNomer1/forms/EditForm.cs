using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KursovoiProjectNomer1
{
    public partial class EditForm : Form
    {
        StoreClass inputted;
        public bool isSetted = false;
        public EditForm(ref StoreClass input)
        {
            inputted = input;
            InitializeComponent();
        }

        private void EditForm_Load(object sender, EventArgs e)
        {
            textBox2.Text = inputted.item_name;
            numericUpDown1.Value = (decimal)inputted.amount;
            numericUpDown2.Value = (decimal)inputted.price;
            comboBox1.SelectedIndex = comboBox1.Items.IndexOf(inputted.price_type);
            textBox3.Text = inputted.condition;
            textBox1.Text = inputted.contact_info;
            textBox4.Text = inputted.note;
            if (inputted.condition == "")
            {
                textBox3.Enabled = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            isSetted = true;
            inputted.item_name = textBox2.Text;
            inputted.amount = (int)numericUpDown1.Value;
            inputted.price = (int)numericUpDown2.Value;
            inputted.price_type = comboBox1.Items[comboBox1.SelectedIndex].ToString();
            inputted.condition = (textBox3.Enabled) ? textBox3.Text : "";
            inputted.contact_info = textBox1.Text;
            inputted.note = textBox4.Text;
            Close();
        }
    }
}
