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
    /// Логика взаимодействия для Mexanik.xaml
    /// </summary>
    public partial class Mexanik : Window
    {
        public Сотрудники user;
        private int kodsp;

        public Mexanik(Сотрудники user)
        {
            this.user = user;
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txt1.Text = user.ФИО;
            txt2.Text = user.Логин;
            txt3.Text = user.Тип_пользователя.Наименование;
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Remont rem = new Remont(user, kodsp);
            rem.Show();
            this.Hide();
        }
    }
}
