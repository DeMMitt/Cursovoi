//using DataGridViewRichTextBox;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KursovoiProjectNomer1
{
    public partial class Main : Form
    {
        private LoadingScreen ls_mine = new LoadingScreen(); //Экземпляр загрузочного сркина для главной таблицы
        private LoadingScreen ls_buy = new LoadingScreen(); //Экземпляр загрузочного скрина для покупочной таблицы
        private LoadingScreen ls_sell = new LoadingScreen(); //Экземпляр загрузочного скрина для продажной таблицы
        private List<StoreClass> buysDataBaseGetted = new List<StoreClass>(); //Коллекция StoreClass для хранения данных покупочной таблицы
        private List<StoreClass> sellsDataBaseGetted = new List<StoreClass>(); //Коллекция StoreClass для хранения данных покупной таблицы
        Font fnt = new Font("Times New Roman", 50); //Шрифт для таблиц
        public Main()
        {


            InitializeComponent();
            dataGridView1.Controls.Add(ls_buy);
            dataGridView2.Controls.Add(ls_sell);
            dataGridView3.Controls.Add(ls_mine);
            dataGridView1.CellPainting += new DataGridViewCellPaintingEventHandler(dataGridView_CellPainting);
            dataGridView3.CellPainting += new DataGridViewCellPaintingEventHandler(dataGridView_CellPainting);
            dataGridView2.CellPainting += new DataGridViewCellPaintingEventHandler(dataGridView_CellPainting);
            dataGridView1.SelectionChanged += new EventHandler(datagridview_SelectionChanged);
            dataGridView2.SelectionChanged += new EventHandler(datagridview_SelectionChanged);


        }
        private void UpdateMineData() //Обновляет все данные, обновляет главную таблицу.
        {
            Async.CallAsync(() =>
                {
                    ls_mine.Show();
                    ls_mine.SetText("Загрузка...");
                    UpdateBuysData();
                    UpdateSellsData();
                    ls_mine.Show(false);
                    UpdateMineView();
                });
        }
        private void UpdateMineView() //Обновляет главную таблицу
        {
            Async.CallAsync(() =>
                {
                    
                    Async.AsyncControlModif<DataGridView>(() =>
                    {
                        dataGridView3.Rows.Clear();
                        if (buysDataBaseGetted.Count > 0)
                        {
                            for (int i = 0; i < buysDataBaseGetted.Count; i++)
                            {
                                try
                                {
                                    if (buysDataBaseGetted[i].userlogin == Global.Login)
                                    {
                                        dataGridView3.Rows.Add(buysDataBaseGetted[i].item_name, "Покупка", buysDataBaseGetted[i].amount, buysDataBaseGetted[i].price, buysDataBaseGetted[i].price_type, "", buysDataBaseGetted[i].contact_info, buysDataBaseGetted[i].note, buysDataBaseGetted[i].datacreated);
                                    }
                                }
                                catch { }
                            }
                        }
                        if (sellsDataBaseGetted.Count > 0)
                        {

                            for (int i = 0; i < sellsDataBaseGetted.Count; i++)
                            {
                                try
                                {
                                    if (sellsDataBaseGetted[i].userlogin == Global.Login)
                                    {
                                        dataGridView3.Rows.Add(sellsDataBaseGetted[i].item_name, "Продажа", sellsDataBaseGetted[i].amount, sellsDataBaseGetted[i].price, sellsDataBaseGetted[i].price_type, sellsDataBaseGetted[i].condition, sellsDataBaseGetted[i].contact_info, sellsDataBaseGetted[i].note, sellsDataBaseGetted[i].datacreated);
                                    }
                                }
                                catch { }
                            }
                        }
                    }, dataGridView3);
                    
                });
        }
        private void UpdateSellsData() //Обновляет данные для покупной таблицы
        {
            ls_sell.Show();
            ls_sell.SetText("Загрузка...");
            Async.CallAsyncW(() =>
           {
               sellsDataBaseGetted.Clear();
               int count;
               dynamic values = WebWorking.getBaseData("sell", out count);
               if (values != null)
               {
                   for (int i = 0; i < count; i++)
                   {

                       string login = values[i].login;
                       string ownername = values[i].owner_name;
                       string itemname = values[i].item_name;
                       int amo = Convert.ToInt32(values[i].amount);
                       int pri = Convert.ToInt32(values[i].price);
                       string cond = values[i].condition;
                       string ptype = values[i].price_type;
                       string ci = values[i].contact_info;
                       string not = values[i].note;
                       string timd = values[i].time_created;
                       sellsDataBaseGetted.Add(new StoreClass(login, ownername, itemname, amo, pri, ptype, ci, not, timd, cond));

                   }
               }
           });
            ls_sell.Show(false);
            UpdateSellsDataView();
        }
        private void UpdateBuysData() //Обновляет данные для продажной таблицы
        {
            ls_buy.Show();
            ls_buy.SetText("Загрузка...");
            Async.CallAsyncW(() =>
            {
                buysDataBaseGetted.Clear();
                int count;
                dynamic values = WebWorking.getBaseData("buy", out count);
                if (values != null)
                {
                    for (int i = 0; i < count; i++)
                    {
                        string login = values[i].login;
                        string ownername = values[i].owner_name;
                        string itemname = values[i].item_name;
                        int amo = Convert.ToInt32(values[i].amount);
                        int pri = Convert.ToInt32(values[i].price);
                        string ptype = values[i].price_type;
                        string ci = values[i].contact_info;
                        string not = values[i].note;
                        string timd = values[i].time_created;
                        buysDataBaseGetted.Add(new StoreClass(login, ownername, itemname, amo, pri, ptype, ci, not, timd));

                    }
                }
            });
            ls_buy.Show(false);
            UpdateBuysDataView();
        }
        private bool CheckStringsContains(string where, string what) //Функция проверки текста для фильтра
        {
            if (what == "") { return true; }
            string[] wherePatterns = where.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            string[] whatPatterns = what.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            try
            {
                for (int i = 0; i < wherePatterns.Length; i++)
                {
                    for (int x = 0; x < whatPatterns.Length; x++)
                    {
                        if (wherePatterns[i].ToLower().Contains(whatPatterns[x].ToLower()))
                        {
                            return true;
                        }
                    }
                }
            }
            catch { }
            return false;
        }
        private bool CheckForFilter(SearchClass sInput, StoreClass scInput) //Функция для фильтрации StoreClass с помощью SearchClass
        {
            if (scInput.price >= sInput.from && scInput.price <= sInput.to && scInput.price_type == sInput.price_type)
            {
                return (CheckStringsContains(scInput.userrealname, sInput.realname) && CheckStringsContains(scInput.item_name, sInput.itemname));
            }
            return false;
        }
        private void UpdateBuysDataView(SearchClass input = null) //Обновляет покупную таблицу
        {
                Async.AsyncControlModif<DataGridView>(() => {
                    dataGridView1.Rows.Clear();
                    if (buysDataBaseGetted.Count > 0)
                    {
                        try
                        {
                            for (int i = 0; i < buysDataBaseGetted.Count; i++)
                            {
                                if (buysDataBaseGetted[i].userlogin != Global.Login)
                                {
                                    if (input != null)
                                    {
                                        if (!CheckForFilter(input, buysDataBaseGetted[i]))
                                        {
                                            continue;
                                        }
                                    }
                                    dataGridView1.Rows.Add(buysDataBaseGetted[i].userrealname, buysDataBaseGetted[i].item_name, buysDataBaseGetted[i].amount, buysDataBaseGetted[i].price, buysDataBaseGetted[i].price_type, buysDataBaseGetted[i].contact_info, buysDataBaseGetted[i].note, buysDataBaseGetted[i].datacreated);
                                }
                            }
                        }
                        catch { }
                    }
                }, dataGridView1);
        }

        private void UpdateSellsDataView(SearchClass input = null) //Обновляет продажную таблицу
        {
            Async.AsyncControlModif<DataGridView>(() =>
            {
                dataGridView2.Rows.Clear();
                if (sellsDataBaseGetted.Count > 0)
                {
                    try
                    {
                        for (int i = 0; i < sellsDataBaseGetted.Count; i++)
                        {
                            if (sellsDataBaseGetted[i].userlogin != Global.Login)
                            {
                                if (input != null)
                                {
                                    if (!CheckForFilter(input, sellsDataBaseGetted[i]))
                                    {
                                        continue;
                                    }
                                }

                                dataGridView2.Rows.Add(sellsDataBaseGetted[i].userrealname, sellsDataBaseGetted[i].item_name, sellsDataBaseGetted[i].amount, sellsDataBaseGetted[i].price, sellsDataBaseGetted[i].price_type, sellsDataBaseGetted[i].condition, sellsDataBaseGetted[i].contact_info, sellsDataBaseGetted[i].note, sellsDataBaseGetted[i].datacreated);
                            }
                        }
                    }
                    catch { }
                }
            }, dataGridView2);
        }

        private void dataGridView_CellPainting(object sender, DataGridViewCellPaintingEventArgs e) //Эвент красивой отрисовки клеток таблицы
        {
            DataGridView dgv = ((DataGridView)sender);
            if (e.Value == null)
                return;
            dgv.Font = new Font("Arial", 11);
            var s = e.Graphics.MeasureString(e.Value.ToString(), dgv.Font);
            bool selected = (DataGridViewElementStates.Selected & e.State) == DataGridViewElementStates.Selected;
            using (Brush gridBrush = new SolidBrush(dgv.GridColor), backColorBrush = new SolidBrush(selected ? e.CellStyle.SelectionBackColor : e.CellStyle.BackColor))
            {
                using (Pen gridLinePen = new Pen(selected ? Color.Black : dgv.GridColor))
                {
                    e.Graphics.FillRectangle(backColorBrush, e.CellBounds);
                    e.Graphics.DrawString(e.Value.ToString(), dgv.Font, selected ? Brushes.White : Brushes.Black, e.CellBounds, StringFormat.GenericDefault);
                    if (!selected)
                    {
                        e.Graphics.DrawLine(gridLinePen, e.CellBounds.Left, e.CellBounds.Bottom - 1, e.CellBounds.Right - 1, e.CellBounds.Bottom - 1);
                        e.Graphics.DrawLine(gridLinePen, e.CellBounds.Right - 1, e.CellBounds.Top, e.CellBounds.Right - 1, e.CellBounds.Bottom);
                    }
                    e.Handled = true;
                }
            }

        }
        private void datagridview_SelectionChanged(object sender, EventArgs e) //Запрет на выбирание элемента
        {
            DataGridView dgv = ((DataGridView)sender);
            dgv.ClearSelection();
            
        }
        private void Main_Load(object sender, EventArgs e)
        {
            UpdateMineData();
        }

  

        private void button1_Click(object sender, EventArgs e)
        {
            AddForm af = new AddForm();
            af.ShowDialog();
            UpdateMineData();

        }

        private void button7_Click(object sender, EventArgs e)
        {
            UpdateBuysData();
            UpdateBuysDataView();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            UpdateSellsData();
            UpdateSellsDataView();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (dataGridView3.SelectedRows.Count > 0)
            {
                Async.CallAsync(() =>
                    {
                        string itemname = dataGridView3.SelectedRows[0].Cells[0].Value.ToString();
                        string database = dataGridView3.SelectedRows[0].Cells[1].Value.ToString() == "Покупка" ? "buy" : "sell";
                        string date = dataGridView3.SelectedRows[0].Cells[8].Value.ToString();
                        WebWorking.DeleteFromBase(itemname, database, date);
                        UpdateMineData();
                    });
               
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (dataGridView3.SelectedRows.Count > 0)
            {
                Async.CallAsync(() =>
                    {
                        string filename = new Random().Next(0, int.MaxValue).ToString() + ".txt";
                        foreach (StoreClass each in (dataGridView3.SelectedRows[0].Cells[1].Value.ToString() == "Покупка") ? buysDataBaseGetted : sellsDataBaseGetted)
                        {

                            if (each.price == Convert.ToInt32(dataGridView3.SelectedRows[0].Cells[3].Value) && each.userlogin == Global.Login && each.item_name == dataGridView3.SelectedRows[0].Cells[0].Value.ToString() && each.datacreated == dataGridView3.SelectedRows[0].Cells[8].Value.ToString())
                            {
                                StreamWriter SW = new StreamWriter(Path.GetTempPath() + "\\" + filename);
                                SW.WriteLine("Имя: " + each.userrealname);
                                SW.WriteLine("Тип: " + dataGridView3.SelectedRows[0].Cells[1].Value.ToString());
                                SW.WriteLine("Название товара: " + each.item_name);
                                SW.WriteLine("Размер партии:" + each.amount.ToString());
                                SW.WriteLine("Цена: " + each.price.ToString() + " грн");
                                SW.WriteLine("Форма оплаты:" + each.price_type);
                                if (each.condition != "")
                                {
                                    SW.WriteLine("Условия: " + each.condition);
                                }
                                //SW.WriteLine((each.condition != "") ? "\nУсловия: " + each.condition : "");
                                SW.WriteLine("Контакт. информация: " + each.contact_info);
                                SW.WriteLine("Примечание: " + each.note);
                                SW.WriteLine("Дата публикации: " + each.datacreated);
                                SW.Close();
                                break;
                            }
                        }
                        Process.Start(Path.GetTempPath() + "\\" + filename);
                    });
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            SearchClass output = new SearchClass();
            Search searchForm = new Search(ref output);
            searchForm.ShowDialog();
            if (output.setted)
            {
                UpdateBuysDataView(output);
            }
   
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SearchClass output = new SearchClass();
            Search searchForm = new Search(ref output);
            searchForm.ShowDialog();
            if (output.setted)
            {
                UpdateSellsDataView(output);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView3.SelectedRows.Count > 0)
            {
                foreach (StoreClass each in (dataGridView3.SelectedRows[0].Cells[1].Value.ToString() == "Покупка") ? buysDataBaseGetted : sellsDataBaseGetted)
                {
                      if (each.price == Convert.ToInt32(dataGridView3.SelectedRows[0].Cells[3].Value) && each.userlogin == Global.Login && each.item_name == dataGridView3.SelectedRows[0].Cells[0].Value.ToString() && each.datacreated == dataGridView3.SelectedRows[0].Cells[8].Value.ToString())
                      {
                          StoreClass eached = new StoreClass(each);
                          EditForm efm = new EditForm(ref eached);
                          efm.ShowDialog();
                          if (efm.isSetted)
                          {
                              if (WebWorking.EditInBase(each.item_name, eached.item_name, eached.amount, eached.price, eached.price_type, eached.contact_info, eached.note, eached.condition, each.datacreated))
                              {
                                  UpdateMineData();
                              }
                              
                          }
                      }
                }
            }
        }
    }
}
