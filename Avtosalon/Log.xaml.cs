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
using ClassLibrary1;

namespace Avtosalon
{
    /// <summary>
    /// Логика взаимодействия для Log.xaml
    /// </summary>
    public partial class Log : Window
    {
        // Инициализация данных
        public AvtosalonEntities13 db = new AvtosalonEntities13();
        List<Тип_пользователя> typ = new List<Тип_пользователя>();
        string fam, tel, potch, log, pass, pass2;

        public Log()
        {
            InitializeComponent();
            // Получаем список типов пользователей из базы данных и добавляем их в комбо бокс
            typ = db.Тип_пользователя.ToList();
            for (int i = 0; i < typ.Count; i++)
            {
                Combo.Items.Add(typ[i].Наименование);
            }
        }

        // Очистка полей формы
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            textBox1.Text = "";
            textBox1.Clear();
            textBox3.Text = "";
            textBox3.Clear();
            textBox4.Text = "";
            textBox4.Clear();
            textBox5.Text = "";
            textBox5.Clear();
            textBox6.Text = "";
            textBox6.Clear();
        }

        // Обработка нажатия на кнопку "Регистрация пользователя"
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            // Получение данных из полей формы
            fam = textBox1.Text;
            tel = textBox2.Text;
            potch = textBox3.Text;
            log = textBox4.Text;
            pass = textBox5.Text;
            pass2 = textBox6.Text;
            // Проверка заполнения всех полей
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "" && Combo != null)
            {
                // Проверка формата почты
                if (Class1.CheckMail(potch))
                {
                    // Проверка логина
                    if (Class1.Checklogin(log))
                    {
                        // Проверка пароля
                        if (Class1.CheckPassword(pass))
                        {
                            // Проверка совпадения паролей
                            if (pass == pass2)
                            {
                                // Проверка длины номера телефона
                                if (tel.Length < 11)
                                {
                                    MessageBox.Show("Введите 11 цифр, для номера телефона!");
                                }
                                else
                                {
                                    // Получение выбранного типа пользователя и создание нового пользователя
                                    Тип_пользователя na = db.Тип_пользователя.Where(x => x.Наименование == Combo.Text).FirstOrDefault();
                                    Сотрудники registr = new Сотрудники
                                    {
                                        ФИО = fam,
                                        Логин = log,
                                        email = potch,
                                        Пароль = pass,
                                        Код_типа_пользователя = na.Код_типа_пользователя
                                    };
                                    n++;
                                    // Добавление пользователя в базу данных
                                    db.Сотрудники.Add(registr);
                                    db.SaveChanges();
                                    MessageBox.Show("Пользователь зарегистрирован!");
                                    MainWindow yu = new MainWindow();
                                    yu.Show();
                                    this.Close();
                                }

                            }
                            else
                            {
                                MessageBox.Show("пароли не совпадают!");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Пароль не соответствует условиям сложности.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Логин не соответствует условиям сложности.");
                    }
                }
                else
                {
                    MessageBox.Show("Некорректный формат почты.");
                }
            }
            else
            {
                MessageBox.Show("Не все данные введены!");
            }
        }
        int n = 1;

        private void textBox2_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            foreach (char c in e.Text)
            {
                if (char.IsLetter(c))
                {
                    e.Handled = true;
                    return;
                }
            }
            int maxLength = 11; // Максимальное количество цифр, которое можно ввести

            if (textBox2 != null && textBox2.Text.Length >= maxLength)
            {
                e.Handled = true; // Отменить ввод, если достигнуто максимальное количество символов
            }
            else if (!char.IsDigit(e.Text, e.Text.Length - 1))
            {
                e.Handled = true; // Отменить ввод, если введенный символ не является цифрой
            }
        }

        private void textBox1_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            foreach (char c in e.Text)
            {
                if (char.IsDigit(c))
                {
                    e.Handled = true;
                    return;
                }
            }
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow m = new MainWindow();
            m.Show(); this.Close();
        }

        private void textBox4_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (textBox4.Text.Length > 20) // Укажите нужное количество символов
            {
                textBox4.Text = textBox4.Text.Substring(0, 20); // Ограничиваем количество символов
                textBox4.Select(textBox4.Text.Length, 0); // Перемещаем курсор в конец текста
            }
        }

        private void textBox5_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (textBox5.Text.Length > 15) // Укажите нужное количество символов
            {
                textBox5.Text = textBox5.Text.Substring(0, 15); // Ограничиваем количество символов
                textBox5.Select(textBox5.Text.Length, 0); // Перемещаем курсор в конец текста
            }
        }

        private void textBox6_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (textBox6.Text.Length > 15) // Укажите нужное количество символов
            {
                textBox6.Text = textBox6.Text.Substring(0, 15); // Ограничиваем количество символов
                textBox6.Select(textBox6.Text.Length, 0); // Перемещаем курсор в конец текста
            }
        }

        private void textBox3_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (textBox3.Text.Length > 30) // Укажите нужное количество символов
            {
                textBox3.Text = textBox3.Text.Substring(0, 30); // Ограничиваем количество символов
                textBox3.Select(textBox3.Text.Length, 0); // Перемещаем курсор в конец текста
            }
        }

        private void textBox1_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (textBox1.Text.Length > 60) // Укажите нужное количество символов
            {
                textBox1.Text = textBox1.Text.Substring(0, 60); // Ограничиваем количество символов
                textBox1.Select(textBox1.Text.Length, 0); // Перемещаем курсор в конец текста
            }
        }
    }
}
