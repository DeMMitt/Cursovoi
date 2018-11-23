using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace KursovoiProjectNomer1
{
    public partial class AddForm : Form
    {
        private LoadingScreen loadingScreen = new LoadingScreen();
        public AddForm()
        {
            this.Controls.Add(loadingScreen);
            InitializeComponent();
        }

        private void AddForm_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                textBox3.Enabled = false;
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                textBox3.Enabled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox2.Text.Length < 3 || (textBox3.Text.Length < 3 && radioButton2.Checked) || textBox1.Text.Length < 3)
            {
                MessageBox.Show("Ошибка", "Некоторые важные поля не заполнены!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                loadingScreen.Show();
                loadingScreen.SetText("Добавление...");
                bool checked1 = radioButton1.Checked;
                string tb2 = textBox2.Text;
                int nud1 = (int)numericUpDown1.Value;
                int nud2 = (int)numericUpDown2.Value;
                string comb1;
                if (comboBox1.SelectedIndex > 0)
                {
                    comb1 = comboBox1.Items[comboBox1.SelectedIndex].ToString();
                }
                else
                {
                    comb1 = comboBox1.Items[0].ToString();
                }
                string tb1 = textBox1.Text;
                string rich1 = textBox4.Text;
                File.WriteAllText("test.txt", rich1);
                string cond = textBox3.Text;

                Async.CallAsync(() =>
                {
                    try
                    {
                        bool Added = radioButton1.Checked ? WebWorking.AddInBuyBase(tb2, nud1, nud2, comb1, tb1, rich1) : WebWorking.AddInSellBase(tb2, nud1, nud2, comb1, tb1, rich1, cond);
                        if (Added)
                        {
                            loadingScreen.SetText("Добавлено!");
                            Thread.Sleep(500);
                            Async.AsyncControlModif<Form>(() =>
                            {
                                this.Close();
                            }, this);
                        }
                        else
                        {
                            loadingScreen.Show(false);
                            MessageBox.Show("Ошибка", "Ошибка на стороне сервера \r\nПопробуйте позже", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex) { MessageBox.Show(ex.Message); }

                });
            }
        }

    }
}
