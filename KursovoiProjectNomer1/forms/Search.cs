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
    public partial class Search : Form
    {
        public SearchClass input; //Экземпляр типа SearchClass для поиска
        public Search(ref SearchClass _input) //Конструктор с 1 указательным аргументом
        {
            input = _input; 
            InitializeComponent();
        }
       
        private void button1_Click(object sender, EventArgs e)
        {
            input.from = (int)numericUpDown2.Value;
            input.to = (int)numericUpDown1.Value;
            input.price_type = comboBox2.Items[(comboBox2.SelectedIndex >= 0) ? comboBox2.SelectedIndex : 0].ToString();
            input.realname = textBox1.Text;
            input.itemname = textBox2.Text;
            input.setted = true;
            Close();

        }

        private void Search_Load(object sender, EventArgs e)
        {
            comboBox2.SelectedIndex = 0;
        }

    }
}

