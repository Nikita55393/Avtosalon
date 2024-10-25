using System;
using System.Collections.Generic;
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

namespace Avtosalon
{
    /// <summary>
    /// Логика взаимодействия для Otchet.xaml
    /// </summary>
    public partial class Otchet : Window
    {
        public AvtosalonEntities13 db = new AvtosalonEntities13();
        public Сотрудники user;
        List<Договор> dog = new List<Договор>();
        public Автомобили auto;
        public Список_моделей spisok;
        public Клиенты cli;
        public Сотрудники sot;
        public Паспорт pasport;
        public Заявка selectedZai;
        public Договор selectDog;
        public Otchet(Сотрудники user)
        {
            InitializeComponent();
            this.user = user;
            dog = db.Договор.ToList();
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            Admin a = new Admin(user);
            a.Show(); this.Close();
        }

        private void list1_Loaded(object sender, RoutedEventArgs e)
        {
            dog = db.Договор.ToList();
            for (int i = 0; i < dog.Count; i++)
            {
                WrapPanel wpi = new WrapPanel();
                System.Windows.Controls.Label LabName = new System.Windows.Controls.Label();

                wpi.Height = 100;
                wpi.Width = 200;
                LabName.Content = "Договор номер " + dog[i].Код_договора;
                LabName.Height = 50;
                LabName.Width = 200;

                wpi.Uid = dog[i].Код_договора.ToString();

                wpi.Children.Add(LabName);

                list1.Items.Add(wpi);
            }
        }

        private void list1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            WrapPanel selectedItem = (WrapPanel)list1.SelectedItem;

            int selectedId = Convert.ToInt32(selectedItem.Uid);
            // значение code для поиска информации об описании в базе данных
            // Получение информации о выбранной заявке из базы данных
            selectDog = db.Договор.FirstOrDefault(x => x.Код_договора ==  selectedId);
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
                tx8.Text = Convert.ToString(selectDog.Дата);
                tx9.Text = selectedZai.Итоговая_цена;
            }
        }
    }
}
