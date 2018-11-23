using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Net;
using System.IO;


namespace KursovoiProjectNomer1
{

    class Global
    {
        //Url к главному обработчику на сервере
        public static string Url = "http://duckhacks.tk/kurs/main.php";
        //Допустимые символы для ввода логина и пароля
        public static string allowedChars = "qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM1234567890";
        //Полученный пароль в формате hash md5 для подтверждения авторизации
        public static string LogginedData { get; set; }
        //Полученный логин после авторизации
        public static string Login { get; set; }
        //Функция для применения фильтра к текст боксу для ввода только допустимых символов
        public static void lockTextBox(TextBox input)
        {
            input.TextChanged += (object sender, EventArgs args) =>
            {
                input.Text = string.Concat(input.Text.Where(c => Global.allowedChars.Contains(c)));
                input.SelectionStart = input.Text.Length + 1;
            };
        }
        //Функция для создания запроса в сети 
        public static string WRequest(string url)
        {
            HttpWebResponse response = null;
            //StreamReader reader = null;
            HttpWebRequest request = null;
            request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            response = (HttpWebResponse)request.GetResponse();
            StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
            string returned = sr.ReadToEnd();
            //reader.Close();
            response.Close();
            return returned;

        }
    }

}
