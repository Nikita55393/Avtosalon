using ClassLibrary1;
using System;
using System.Collections.Generic;
using System.Data;
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
    /// Логика взаимодействия для Prodav.xaml
    /// </summary>
    public partial class Prodav : Window
    {
        // Создание экземпляра базы данных
        public AvtosalonEntities13 db = new AvtosalonEntities13();
        // Переменная для хранения текущего сотрудника
        public Сотрудники user;
        // Переменные для хранения данных клиента
        string fam, tel, serpas, num;

        // Конструктор формы Prodav
        public Prodav(Сотрудники user)
        {
            InitializeComponent(); // Инициализация компонентов формы
            this.user = user; // Установка текущего сотрудника
        }

        // Обработчик события для кнопки "автомобили"
        private void butauto_Click(object sender, RoutedEventArgs e)
        {
            SpisokAuto sp = new SpisokAuto(user); // Создание новой формы SpisokAuto
            sp.Show(); this.Hide(); // Отображение новой формы и скрытие текущей
        }

        // Обработчик события для кнопки "добавить клиента"
        private void But5_Click(object sender, RoutedEventArgs e)
        {
            bor.Visibility = Visibility.Visible; // Показываем область для ввода данных о клиенте
        }

        int n = 1; // Счетчик для кода клиента

        // Обработчик события для кнопки "сохранить"
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            fam = tx1.Text; // Получаем ФИО клиента
            tel = tx2.Text; // Получаем телефон клиента
            serpas = tx3.Text; // Получаем серию паспорта
            num = tx4.Text; // Получаем номер паспорта

            if (tx1.Text != "" && tx2.Text != "" && tx3.Text != "" && tx4.Text != "")
            {
                // Проверяем длину телефонного номера
                if (tel.Length < 11)
                {
                    MessageBox.Show("Введите 11 цифр, для номера телефона!");
                }
                else
                {
                    // Проверяем длину серии паспорта
                    if (serpas.Length < 4)
                    {
                        MessageBox.Show("Введите 4 цифр, для серии паспорта!");
                    }
                    else
                    {
                        // Проверяем длину номера паспорта
                        if (num.Length < 6)
                        {
                            MessageBox.Show("Введите 6 цифр, для номера паспорта!");
                        }
                        else
                        {
                            // Создаем новый объект паспорта
                            Паспорт pas = new Паспорт
                            {
                                Серия = serpas,
                                Номер = num,
                                Дата_выдачи = pik.SelectedDate
                            };
                            db.Паспорт.Add(pas); // Добавляем паспорт в базу данных
                            db.SaveChanges(); // Сохраняем изменения

                            // Создаем новый объект клиента
                            Клиенты restr = new Клиенты
                            {
                                Код_клиента = n,
                                ФИО = fam,
                                Телефон = tel,
                                Код_паспорта = pas.Код_паспорта
                            };
                            n++; // Увеличиваем счетчик кода клиента
                            db.Клиенты.Add(restr); // Добавляем клиента в базу данных
                            db.SaveChanges(); // Сохраняем изменения

                            MessageBox.Show("Клиент добавлен!"); // Сообщение о успешном добавлении
                            bor.Visibility = Visibility.Hidden; // Скрываем область ввода данных
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Не все данные введены!"); // Сообщение, если данные не введены
            }
        }
        private void tx1_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            foreach (char c in e.Text)
            {
                if (char.IsDigit(c))
                {
                    e.Handled = true;
                    return;
                }
            }
            int maxLength = 60;
            if (tx1 != null && tx1.Text.Length >= maxLength)
            {
                e.Handled = true; // Отменить ввод, если достигнуто максимальное количество символов
            }
        }

        private void tx2_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            int maxLength = 11; // Максимальное количество цифр, которое можно ввести

            if (tx2 != null && tx2.Text.Length >= maxLength)
            {
                e.Handled = true; // Отменить ввод, если достигнуто максимальное количество символов
            }
            else if (!char.IsDigit(e.Text, e.Text.Length - 1))
            {
                e.Handled = true; // Отменить ввод, если введенный символ не является цифрой
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txt1.Text = user.ФИО;
            txt2.Text = user.Логин;
            txt3.Text = user.Тип_пользователя.Наименование;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            zaiv za = new zaiv(user);
            za.Show(); this.Hide();
        }

        private void b11_Click(object sender, RoutedEventArgs e)
        {
            bor.Visibility = Visibility.Hidden;
        }

        private void tx3_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            int maxLength = 4; // Максимальное количество цифр, которое можно ввести

            if (tx3 != null && tx3.Text.Length >= maxLength)
            {
                e.Handled = true; // Отменить ввод, если достигнуто максимальное количество символов
            }
            else if (!char.IsDigit(e.Text, e.Text.Length - 1))
            {
                e.Handled = true; // Отменить ввод, если введенный символ не является цифрой
            }
        }

        private void tx4_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            int maxLength = 6; // Максимальное количество цифр, которое можно ввести

            if (tx4 != null && tx4.Text.Length >= maxLength)
            {
                e.Handled = true; // Отменить ввод, если достигнуто максимальное количество символов
            }
            else if (!char.IsDigit(e.Text, e.Text.Length - 1))
            {
                e.Handled = true; // Отменить ввод, если введенный символ не является цифрой
            }
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }
    }
}
