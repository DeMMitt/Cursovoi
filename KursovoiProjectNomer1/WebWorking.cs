using fastJSON;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KursovoiProjectNomer1
{
    class WebWorking
    {
        //Метод авторизации
        public static bool LogIn(string login, string password)
        {
            string page = Global.WRequest(Global.Url + "?" + "method=loginning&login=" + Uri.EscapeDataString(login) + "&password=" + Uri.EscapeDataString(password));
            dynamic value = JSON.ToDynamic(page);
           
            if (value.response == "error")
            {
                return false;
            }
            else
            {
                Global.LogginedData = value.data;
                Global.Login = value.login;
                Global.Login = Global.Login.ToLower();
                return true;
            }
        }
        //Метод регистрации
        public static bool RegIn(string login, string password, string realName) 
        {
            string page = Global.WRequest(Global.Url + "?" + "method=registering&login=" + 
            Uri.EscapeDataString(login) + 
            "&password=" + Uri.EscapeDataString(password) + 
            "&realname=" + Uri.EscapeDataString(realName));
            dynamic data = JSON.ToDynamic(page);
            if (data.response == "error")
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        //Метод получения данных
        public static dynamic getBaseData(string name, out int count, string filters = "")
        {
            string page = Global.WRequest(Global.Url + "?" + "method=getdata&name=" + name);
            dynamic temp = JSON.ToDynamic(page, out count);
            try
            {
                if (temp.response == "none")
                {
                    return null;
                }
            }
            catch { }
            return temp;

        }
        //Метод удаления из базы
        public static bool DeleteFromBase(string itemname, string database, string date)
        {
            string page = Global.WRequest(Global.Url + "?" + 
                "method=dell&login=" + Global.Login + 
                "&data=" + Global.LogginedData + 
                "&itemname=" + Uri.EscapeDataString(itemname) + 
                "&base=" + database + 
                "&date=" + Uri.EscapeDataString(date));
            dynamic parsed = JSON.ToDynamic(page);
            if (parsed.response == "success")
            {
                return true;
            }

            else
            {
                return false;
            }
        }
        //Метод редактирования в базе
        public static bool EditInBase(string olditemname, string newitemname, int amount, int price, string pricetype, string contactinfo, string note, string cond, string time)
        {
            string page = Global.WRequest(Global.Url + "?" + 
                "method=edit&login=" + Global.Login + 
                "&data=" + Global.LogginedData + 
                "&olditemname=" + Uri.EscapeDataString(olditemname) + 
                "&newitemname=" + Uri.EscapeDataString(newitemname) + 
                "&amount=" + amount.ToString() + 
                "&price=" + price.ToString() + 
                "&pricetype=" + Uri.EscapeDataString(pricetype) + 
                "&contactinfo=" + Uri.EscapeDataString(contactinfo) + 
                "&note=" + Uri.EscapeDataString(note) + 
                "&cond=" + Uri.EscapeDataString(cond) + 
                "&oldtime=" + Uri.EscapeDataString(time));
            dynamic parsed = JSON.ToDynamic(page);
            if (parsed.response == "success")
            {

                return true;
            }
            else
            {
                return false;
            }
        }
        //Метод добавления в базу продавцов
        public static bool AddInSellBase(string itemname, int amount, int price, string pricetype, string contactinfo,string note, string cond)
        {
            string page = Global.WRequest(Global.Url + "?" + 
                "method=addinsell&login=" + Global.Login + 
                "&data=" + Global.LogginedData + 
                "&itemname=" + Uri.EscapeDataString(itemname) + 
                "&amount=" + amount.ToString() + 
                "&price=" + price.ToString() + 
                "&pricetype=" + Uri.EscapeDataString(pricetype) + 
                "&contactinfo=" + Uri.EscapeDataString(contactinfo) + 
                "&note=" + Uri.EscapeDataString(note) + 
                "&cond=" + Uri.EscapeDataString(cond));
            dynamic parsed = JSON.ToDynamic(page);
            if (parsed.response == "success")
            {

                return true;
            }
            else
            {
                return false;
            }
        }
        //Метод добавления в базу покупателей
        public static bool AddInBuyBase(string itemname, int amount, int price, string pricetype, string contactinfo,string note)
        {
            string page = Global.WRequest(Global.Url + "?" + 
                "method=addinbuy&login=" + Global.Login + 
                "&data=" + Global.LogginedData + 
                "&itemname=" + Uri.EscapeDataString(itemname) + 
                "&amount=" + amount.ToString() + 
                "&price=" + price.ToString() + 
                "&pricetype=" + Uri.EscapeDataString(pricetype) + 
                "&contactinfo=" + Uri.EscapeDataString(contactinfo) + 
                "&note=" + Uri.EscapeDataString(note));
            dynamic parsed = JSON.ToDynamic(page);
            if (parsed.response == "success")
            {
                
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
