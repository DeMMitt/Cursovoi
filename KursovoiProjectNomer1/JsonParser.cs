using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using fastJSON;

namespace KursovoiProjectNomer1
{
    class JsonParser
    {
        public static dynamic GetValues(string input)
        {
            return JSON.ToDynamic(input);
           //JSON
            //dynamic data = null;
            //try
            //{
            //    data = JSON.ToObject<dynamic>(input);
            //}
            //catch { }
            //return data;
        }
    }
}
