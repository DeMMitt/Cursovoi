using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KursovoiProjectNomer1
{
    //Загрузочный экран
    //Nickitee (C) Evgeniy


    public class LoadingScreen : PictureBox 
    {
        //Свойства форматирования внешнего вида
        Label text = new Label()
        {
            Dock = DockStyle.Fill,
            AutoSize = false,
            TextAlign = ContentAlignment.MiddleCenter,
            Font = new Font(FontFamily.GenericSerif, 20.0f, FontStyle.Regular)
        };
        //Дефолтный конструктор
       public LoadingScreen()
       {
           this.Visible = false;
           this.Dock = DockStyle.Fill;
           this.Controls.Add(text);
       }
       //Функция для отображения либо скрытия экрана
       public void Show(bool onoff = true, int delay = 0)
       {
           Async.AsyncControlModif<PictureBox>(() => { this.Visible = onoff; }, this);
       }
       //Функция для задания текста загрузочного экрана
       public void SetText(string _text)
       {
           Async.AsyncControlModif<Label>(() => { text.Text = _text; }, text);
       }
    }
}
