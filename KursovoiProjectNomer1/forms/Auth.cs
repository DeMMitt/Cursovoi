using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KursovoiProjectNomer1
{
    public partial class Auth : Form
    {
        private LoadingScreen loadingScreen = new LoadingScreen(); //Экземпляр загрузочного экрана
        public Auth() 
        {
            this.Controls.Add(loadingScreen);
            InitializeComponent();
            Global.lockTextBox(loginTextBox);
            Global.lockTextBox(passwordTextBox);
         
        }
        private void StartLogin() //Функция авторизации
        {
            loadingScreen.Show();
            loadingScreen.SetText("Авторизация...");
            Async.CallAsync(() =>
            {
            //label22:
                if (WebWorking.LogIn(loginTextBox.Text, passwordTextBox.Text.ToLower()))
                {
                    loadingScreen.SetText("Успешно!");
                    Thread.Sleep(500);
                    Async.AsyncControlModif<Form>(() =>
                    {
                        this.Close();
                    }, this);

                }
                else
                {
                    loadingScreen.Show(false);
                    MessageBox.Show("Ошибка авторизации! \nПроверьте правильность ввода логина и пароля!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            });

        }
        private void button1_Click(object sender, EventArgs e)
        {
            StartLogin();
        }

        private void registerButton_Click(object sender, EventArgs e)
        {
            Register regForm = new Register();
            regForm.ShowDialog();
        }

        private void passwordTextBox_KeyDown(object sender, KeyEventArgs e)
        {
             if(e.KeyCode==Keys.Enter)
             {
                 StartLogin();
             }
        }
  
    }
}
