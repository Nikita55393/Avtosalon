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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using static System.Net.Mime.MediaTypeNames;

namespace Avtosalon
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Объявление и инициализация DispatcherTimer
        DispatcherTimer timer1 = new DispatcherTimer();

        // Объявление и инициализация объекта для работы с базой данных
        public AvtosalonEntities13 db = new AvtosalonEntities13();

        // Объявление private переменных для работы с таймером и временем
        private DispatcherTimer timer;
        private TimeSpan elapsedTime;
        int stat = 0; // Переменная для отслеживания статуса

        // Конструктор главного окна
        public MainWindow()
        {
            InitializeComponent(); // Инициализация компонентов окна
            timer = new DispatcherTimer(); // Создание таймера
            timer.Interval = TimeSpan.FromSeconds(1); // Интервал таймера - 1 секунда
            timer.Tick += timerTick; // Привязка обработчика события Tick к таймеру
        }

        // Метод для сброса таймера
        private void ResetTimer()
        {
            elapsedTime = new TimeSpan(0, 0, 0); // Сброс времени таймера
            UpdateTimerLabel(); // Обновление отображения времени
        }

        // Метод для обновления отображения времени
        private void UpdateTimerLabel()
        {
            timerLabel.Content = string.Format("{0:D2}:{1:D2}:{2:D2}", elapsedTime.Hours, elapsedTime.Minutes, elapsedTime.Seconds); // Форматированное отображение времени
        }

        // Обработчик события Tick таймера
        private void timerTick(object sender, EventArgs e)
        {
            elapsedTime = elapsedTime.Add(TimeSpan.FromSeconds(1)); // Увеличение времени на 1 секунду
            UpdateTimerLabel(); // Обновление отображения времени

            // Проверка условия остановки таймера
            if (elapsedTime.Hours == 0 && elapsedTime.Minutes == 0 && elapsedTime.Seconds == 10)
            {
                timer.Stop(); // Остановка таймера
                textBox7.IsEnabled = true; // Включение текстовых полей
                textBox8.IsEnabled = true;
                textBox9.IsEnabled = true;
                ResetTimer(); // Сброс таймера
            }
        }
        string pwd = "";
        // Метод для генерации капчи
        public void Capcha()
        {
            String allowchar = " "; // Строка с допустимыми символами
            allowchar = "A,B,C,D,E,F,G,H,J,K,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z";
            allowchar += "a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,y,z";
            allowchar += "1,2,3,4,5,6,7,8,9,0";

            char[] a = { ',' }; // Массив для разделения строки на символы
            String[] ar = allowchar.Split(a); // Разделение строки на массив символов
            string temp = " "; // Переменная для хранения случайного символа
            pwd = ""; // Инициализация строки капчи
            Random r = new Random(); // Создание объекта для генерации случайных чисел
            int kol = 4; // Количество символов в капче

            for (int i = 0; i < kol; i++)
            {
                temp = ar[(r.Next(0, ar.Length))]; // Выбор случайного символа из массива
                pwd += temp; // Добавление символа в капчу
            }
            label2.Content = pwd; // Отображение сгенерированной капчи
        }

        // Обработчик нажатия на кнопку
        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            var user = db.Сотрудники.FirstOrDefault(x => x.Логин == textBox7.Text && x.Пароль == textBox8.Text); // Поиск пользователя в базе данных
            if (user != null)
            {
                int doc = user.Код_сотрудника; // Код сотрудника
                var usering = db.Сотрудники.FirstOrDefault(x => x.Код_сотрудника == doc); // Поиск сотрудника по коду
                if (stat == 0)
                {
                    usering.Статус = true; // Установка статуса в true
                }
                else
                {
                    usering.Статус = false; // Установка статуса в false
                }
                db.SaveChanges(); // Сохранение изменений в базе данных

                if (textBox9.Visibility == Visibility.Visible)
                {
                    if (textBox9.Text == pwd) // Проверка введенной капчи
                    {
                        // Выбор действия в зависимости от типа пользователя
                        if (user.Код_типа_пользователя == 1)
                        {
                            MessageBox.Show("Авторизация успешна");
                            Prodav usr = new Prodav(user);
                            usr.Show(); this.Hide();
                        }
                        else if (user.Код_типа_пользователя == 2)
                        {
                            MessageBox.Show("Авторизация успешна");
                            Manager usr = new Manager(user);
                            usr.Show(); this.Hide();
                        }
                        else if (user.Код_типа_пользователя == 3)
                        {
                            MessageBox.Show("Авторизация успешна");
                            Mexanik usr = new Mexanik(user);
                            usr.Show(); this.Hide();
                        }
                        else if (user.Код_типа_пользователя == 4)
                        {
                            MessageBox.Show("Авторизация успешна");
                            Admin usr = new Admin(user);
                            usr.Show(); this.Hide();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Неправельная капча, повторите попытку через 10 секунд.");
                        timer.Start(); // Запуск таймера
                        timerLabel.Visibility = Visibility.Visible; // Отображение таймера
                        textBox7.Clear(); // Очистка текстовых полей
                        textBox8.Clear();
                        textBox9.Clear();
                        textBox7.IsEnabled = false; // Отключение текстовых полей
                        textBox8.IsEnabled = false;
                        textBox9.IsEnabled = false;
                        Capcha(); // Генерация новой капчи
                    }
                }
                else
                {
                    // Выбор действия в зависимости от типа пользователя
                    if (user.Код_типа_пользователя == 1)
                    {
                        MessageBox.Show("Авторизация успешна");
                        Prodav usr = new Prodav(user);
                        usr.Show(); this.Hide();
                    }
                    else if (user.Код_типа_пользователя == 2)
                    {
                        MessageBox.Show("Авторизация успешна");
                        Manager usr = new Manager(user);
                        usr.Show(); this.Hide();
                    }
                    else if (user.Код_типа_пользователя == 3)
                    {
                        MessageBox.Show("Авторизация успешна");
                        Mexanik usr = new Mexanik(user);
                        usr.Show(); this.Hide();
                    }
                    else if (user.Код_типа_пользователя == 4)
                    {
                        MessageBox.Show("Авторизация успешна");
                        Admin usr = new Admin(user);
                        usr.Show(); this.Hide();
                    }
                }
            }
            else
            {
                MessageBox.Show("Такого пользователя не существует");
                textBox9.Visibility = Visibility.Visible; // Отображение поля капчи
                lab1.Visibility = Visibility.Visible; // Отображение метки
                Capcha(); // Генерация капчи
                stat = stat + 1; // Увеличение сче
            }
        }
        public int n;
        private void Butpar_Click(object sender, RoutedEventArgs e)
        {
            if (n != 2)
            {
                n++;
                textBox8.FontFamily = new System.Windows.Media.FontFamily("Segoe UI");
            }
            else
            {
                n--;
                textBox8.FontFamily = new System.Windows.Media.FontFamily("Wingdings");
            }
        }
        private void textBox9_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (textBox9.Text.Length > 5) // Укажите нужное количество символов
            {
                textBox9.Text = textBox9.Text.Substring(0, 5); // Ограничиваем количество символов
                textBox9.Select(textBox9.Text.Length, 0); // Перемещаем курсор в конец текста
            }
        }

        private void textBox7_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (textBox7.Text.Length > 20) // Укажите нужное количество символов
            {
                textBox7.Text = textBox7.Text.Substring(0, 20); // Ограничиваем количество символов
                textBox7.Select(textBox7.Text.Length, 0); // Перемещаем курсор в конец текста
            }
        }

        private void textBox8_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (textBox8.Text.Length > 15) // Укажите нужное количество символов
            {
                textBox8.Text = textBox8.Text.Substring(0, 15); // Ограничиваем количество символов
                textBox8.Select(textBox8.Text.Length, 0); // Перемещаем курсор в конец текста
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Log log = new Log();
            log.Show();
            this.Hide();
        }
    }
}
