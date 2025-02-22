﻿using System;
using System.Collections;
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
    /// Логика взаимодействия для History.xaml
    /// </summary>
    public partial class History : Window
    {
        public AvtosalonEntities13 db = new AvtosalonEntities13();
        public Сотрудники user;
        List<Сотрудники> zak = new List<Сотрудники>();
        public History(Сотрудники user)
        {
            InitializeComponent();
            this.user = user;
            zak = db.Сотрудники.ToList();
        }

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            list1.ItemsSource = zak;
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            Admin ad = new Admin(user);
            ad.Show(); this.Close();
        }
    }
}
