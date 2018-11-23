using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KursovoiProjectNomer1
{
    public class StoreClass
    {
        public StoreClass() { }
        public StoreClass(string uslogin, string usreal, string itemname, int am, int pr, string prictype, string contactt, string not, string data, string co = "") //Конструктор
        {
            userlogin = uslogin;
            userrealname = usreal;
            item_name = itemname;
            amount = am;
            price = pr;
            condition = co;
            price_type = prictype;
            contact_info = contactt;
            note = not;
            datacreated = data;
        }
        public StoreClass(StoreClass input)
        {
            userlogin = input.userlogin;
            userrealname = input.userrealname;
            item_name = input.item_name;
            amount = input.amount;
            price = input.price;
            condition = input.condition;
            price_type = input.price_type;
            contact_info = input.contact_info;
            note = input.note;
            datacreated = input.datacreated;
        }
        //Логин
        public string userlogin { get; set; }
        //Имя пользователя
        public string userrealname { get; set; }
        //Наименование лота
        public string item_name { get; set; } 
        //Количество
        public int amount { get; set; } 
        //Цена
        public int price { get; set; } 
        //Условие
        public string condition { get; set; } 
        //Вид оплаты
        public string price_type { get; set; } 
        //Контакт. информация
        public string contact_info { get; set; } 
        //Примечание
        public string note { get; set; } 
        //Дата создания
        public string datacreated { get; set; } 
    }

    public class SearchClass
    {
        public SearchClass() { setted = false; } //Дефолтный конструктор
        public SearchClass(int f, int t, string pt, string rn, string im) //Конструктор
        {
            from = f;
            to = t;
            price_type = pt;
            realname = rn;
            itemname = im;
        }
        //Булевая переменная для определения применились настройки или нет
        public bool setted { get; set; }
        //Знчение цены от
        public int from { get; set; }
        //Знчение цены lj
        public int to { get; set; }
        //Вариант оплаты
        public string price_type { get; set; }
        //Имя продавца/покупателя
        public string realname { get; set; }
        //Имя продукции
        public string itemname { get; set; }

    }
}
