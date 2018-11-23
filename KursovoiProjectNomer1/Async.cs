using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KursovoiProjectNomer1
{
    //Асинхронность
    //Nickitee (C) Jeka :3

    public class Async //Класс для вызова асинхронных функций
    {
        //Вызов асинхронной анонимной функции
        public static void CallAsync(Action action) 
        {
            Task.Factory.StartNew(action);
        }
        //Вызов асинхронной анонимной функции с lock Wait
        public static void CallAsyncW(Action action)
        {
            Task.Factory.StartNew(action).Wait();
        }
        //Вызов асихронной анонимной обобщённой функции для редактирования элемента
        public static void AsyncControlModif<T>(Action action, T control) where T : Control
        {
            control.Invoke(new MethodInvoker(delegate() { action(); }));
        }
    }
}
