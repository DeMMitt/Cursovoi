using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace KursovoiProjectNomer1
{
    public partial class Register : Form
    {
        LoadingScreen loadingScreen = new LoadingScreen(); //Экземпляр загруз. класса
        public Register()
        {
            this.Controls.Add(loadingScreen);
            InitializeComponent();
            Global.lockTextBox(loginTextBox);
            Global.lockTextBox(passwordAgainTextBox);
            Global.lockTextBox(passwordTextBox);
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (passwordTextBox.Text == passwordAgainTextBox.Text)
            {
                if (passwordTextBox.Text.Length >= 5)
                {
                    if (loginTextBox.Text.Length >= 5)
                    {
                        if (MessageBox.Show("Вы уверены что хотите зарегистрировать аккаунт?", "Info", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            loadingScreen.Show();

                            Async.CallAsync(() =>
                            {
                                loadingScreen.SetText("Регистрация в базе...");
                                if (WebWorking.RegIn(loginTextBox.Text,passwordTextBox.Text.ToLower(), realNameTextBox.Text))
                                {
                                    loadingScreen.SetText("Регистрация завершена!");
                                    Thread.Sleep(3000);
                                    Async.AsyncControlModif<Form>(() => { this.Close(); }, this);
                                }
                                else
                                {
                                    MessageBox.Show("Ошибка регистрации аккаунта! \nВозможно такой аккаунт уже существует!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    loadingScreen.Show(false);
                                }
                            });
                        }
                    }
                    else
                    {
                        MessageBox.Show("Логин должен быть больше или равен 5 символам!");
                    }
                }
                else
                {
                    MessageBox.Show("Пароль должен быть больше или равен 5 символам!");
                }
            }
            else
            {
                MessageBox.Show("Пароли не совпадают!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
