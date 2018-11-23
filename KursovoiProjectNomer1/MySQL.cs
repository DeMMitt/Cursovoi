using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace KursovoiProjectNomer1
{
    //Работаем с MySQL
    //Nickitee (C) Evgeniy


    public class MySQL
    {
        private MySqlConnection our_mysql;
        public MySQL()
        {
            our_mysql = new MySqlConnection("Data Source=178.63.47.123;User Id=account1;Password=4es7843hywejhf;Database=kurs");
        }
        
        public bool Auth(string login, string md5password)
        {
            try
            {
                if (Connect())
                {
                    using (MySqlCommand command = new MySqlCommand("SELECT COUNT(*) FROM `users` WHERE `login` = '" + login + "' AND `password` = '" + md5password + "';", our_mysql))
                    {
                        if (Convert.ToInt32(command.ExecuteScalar()) == 1)
                        {
                            our_mysql.Close();
                            return true;
                           
                        }
                    }
                }
                our_mysql.Close();
                return false;
            }
            catch { return false; }
        }
        public bool RegInBase(string login, string md5password)
        {
            try
            {
                if (Connect())
                {
                    if (!ExistsInBase(login))
                    {
                        using (MySqlCommand command = new MySqlCommand("INSERT INTO `users` (`login`, `password`) VALUES ('" + login + "', '" + md5password + "');", our_mysql))
                        {
                            command.ExecuteNonQuery();
                            our_mysql.Close();
                            return true;
                        }
                    }
                }
                our_mysql.Close();
                return false;
            }
            catch { return false; }
        }
        private bool ExistsInBase(string login)
        {
            using(MySqlCommand command = new MySqlCommand("SELECT COUNT(*) FROM `users` WHERE `login` = '" + login + "';", our_mysql))
            {
                if (Convert.ToInt32(command.ExecuteScalar()) == 0)
                {
                        return false;
                }
                else
                { return true; }
            }
        }

        private bool Connect()
        {
            try
            {
                our_mysql.Open();
                if (our_mysql.Ping())
                {
                    return true;
                }
                return false;
            
            }
            catch  {  return false; }
        }
        //public bool 
        
    }
}
