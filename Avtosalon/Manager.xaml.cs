using System;
using System.Collections;
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
    /// Логика взаимодействия для Manager.xaml
    /// </summary>
    public partial class Manager : Window
    {
        public AvtosalonEntities13 db = new AvtosalonEntities13();
        List<Заявка> zai;
        public Сотрудники user;
        public Manager(Сотрудники user)
        {
            this.user = user;
            InitializeComponent();
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            txt1.Text = user.ФИО;
            txt2.Text = user.Логин;
            txt3.Text = user.Тип_пользователя.Наименование;
        }

        private void btn12_Click(object sender, RoutedEventArgs e)
        {
            Document docu = new Document(user);
            docu.Show(); this.Hide();
        }

    }
}
