using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;
using Word = Microsoft.Office.Interop.Word;

namespace Avtosalon
{
    /// <summary>
    /// Логика взаимодействия для Document.xaml
    /// </summary>
    public partial class Document : System.Windows.Window
    {
        // Объявление экземпляра контекста базы данных
        public AvtosalonEntities13 db = new AvtosalonEntities13();
        // Список заявок
        List<Заявка> zai;
        // Текущий пользователь
        public Сотрудники user;

        // Конструктор класса Document
        public Document(Сотрудники user)
        {
            InitializeComponent();
            this.user = user;
        }

        // Метод, вызываемый при загрузке списка заявок
        private void list1_Loaded(object sender, RoutedEventArgs e)
        {
            zai = db.Заявка.ToList();
            for (int i = 0; i < zai.Count; i++)
            {
                WrapPanel wpi = new WrapPanel();
                System.Windows.Controls.Label LabName = new System.Windows.Controls.Label();

                wpi.Height = 100;
                wpi.Width = 200;
                LabName.Content = "Заявка номер " + zai[i].Код_заявки;
                LabName.Height = 50;
                LabName.Width = 200;

                wpi.Uid = zai[i].Код_заявки.ToString();

                wpi.Children.Add(LabName);

                list1.Items.Add(wpi);
            }
        }

        private void tx8_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;

            if (textBox.Text.Length > 10)
            {
                textBox.Text = textBox.Text.Substring(0, 10);
                textBox.CaretIndex = textBox.Text.Length;
            }
        }

        private void tx5_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;

            if (textBox.Text.Length > 10)
            {
                textBox.Text = textBox.Text.Substring(0, 10);
                textBox.CaretIndex = textBox.Text.Length;
            }
        }

        private void nazad_Click(object sender, RoutedEventArgs e)
        {
            Manager mana = new Manager(user);
            mana.Show(); this.Close();
        }
        // Объявление переменных для связанных таблиц
        public Автомобили auto;
        public Список_моделей spisok;
        public Клиенты cli;
        public Сотрудники sot;
        public Паспорт pasport;
        public Заявка selectedZai;
        // Обработчик выбора заявки из списка
        private void list1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            WrapPanel selectedItem = (WrapPanel)list1.SelectedItem;

            int selectedId = Convert.ToInt32(selectedItem.Uid);
            // значение code для поиска информации об описании в базе данных
            // Получение информации о выбранной заявке из базы данных
            selectedZai = db.Заявка.FirstOrDefault(c => c.Код_заявки == selectedId);
            auto = db.Автомобили.FirstOrDefault(x => x.Код_авто == selectedZai.Код_авто);
            spisok = db.Список_моделей.Where(x => x.Код_модели == auto.Код_модели).FirstOrDefault();
            cli = db.Клиенты.Where(x => x.Код_клиента == selectedZai.Код_клиента).FirstOrDefault();
            sot = db.Сотрудники.Where(x => x.Код_сотрудника == selectedZai.Код_сотрудника).FirstOrDefault();
            pasport = db.Паспорт.Where(x => x.Код_паспорта == cli.Код_паспорта).FirstOrDefault();
            if (selectedZai != null)
            {
                // Заполнение текстовых полей данными о выбранной заявке
                tx1.Text = cli.ФИО;
                tx2.Text = cli.Телефон;
                tx3.Text = pasport.Серия;
                tx4.Text = pasport.Номер;
                tx5.Text = Convert.ToString(pasport.Дата_выдачи);
                tx6.Text = sot.ФИО;
                tx7.Text = "Nissan " + spisok.Наименование;
                tx8.Text = Convert.ToString(selectedZai.Дата);
                tx9.Text = selectedZai.Итоговая_цена;
            }
        }

        private void btn15_Click(object sender, RoutedEventArgs e)
        {
            if (selectedZai != null)
            {
                string monthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(DateTime.Now.Month);

                var WordApp = new Word.Application();
                WordApp.Visible = false;
                var Worddoc = WordApp.Documents.Open(Environment.CurrentDirectory + @"\template.docx");
                Repwo("{Код_Заказа}", selectedZai.Код_заявки.ToString(), Worddoc);
                Repwo("{День}", DateTime.Now.Day.ToString(), Worddoc);
                Repwo("{Месяц}", monthName, Worddoc);
                Repwo("{ФИО}", cli.ФИО, Worddoc);
                Repwo("{Сумма}", auto.Цена, Worddoc);
                Repwo("{Дата_Заключения}", DateTime.Today.ToString(), Worddoc);
                Repwo("{ФИО}", cli.ФИО, Worddoc);
                Repwo("{Телефон}", cli.Телефон, Worddoc);
                Repwo("{Серия}", pasport.Серия, Worddoc);
                Repwo("{Номер}", pasport.Номер, Worddoc);
                Repwo("{Дата_выдачи}", pasport.Дата_выдачи.ToString(), Worddoc);
                Worddoc.SaveAs2(Environment.CurrentDirectory + $@"\Договор №{selectedZai.Код_заявки}.docx");
                Договор dogo = new Договор
                {
                    Код_заявки = selectedZai.Код_заявки,
                    Дата = DateTime.Today
                };
                db.Договор.Add(dogo);
                db.SaveChanges();
                MessageBox.Show("Договор оформлен!");
            }
            else
            {
                MessageBox.Show("Необходимо выбрать заявку!");
            }
        }

        private void Repwo(string subToReplace, string text, Word.Document worddoc)
        {
            var range = worddoc.Content;
            range.Find.ClearFormatting();
            range.Find.Execute(FindText: subToReplace, ReplaceWith: text);
        }
    }
}
