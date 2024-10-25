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
    /// Логика взаимодействия для Admin.xaml
    /// </summary>
    public partial class Admin : Window
    {
        public Сотрудники user;
        public Admin(Сотрудники user)
        {
            InitializeComponent();
            this.user = user;
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow m = new MainWindow();
            m.Show(); this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Automob automob = new Automob(user);
            automob.Show(); this.Hide();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txt1.Text = user.ФИО;
            txt2.Text = user.Логин;
            txt3.Text = user.Тип_пользователя.Наименование;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            History history = new History(user);
            history.Show(); this.Hide();
        }

        private void But5_Click(object sender, RoutedEventArgs e)
        {
            Otchet ot = new Otchet(user);
            ot.Show(); this.Hide();
        }
    }
}
