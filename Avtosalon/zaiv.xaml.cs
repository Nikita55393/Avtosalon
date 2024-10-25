using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.ConstrainedExecution;
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
    /// Логика взаимодействия для zaiv.xaml
    /// </summary>
    public partial class zaiv : Window
    {
        // Определение контекста Entity Framework для взаимодействия с базой данных
        public AvtosalonEntities13 db = new AvtosalonEntities13();
        // Текущий авторизованный сотрудник
        public Сотрудники user;
        // Списки для хранения данных из таблиц
        List<Автомобили> car;
        List<Клиенты> kli;
        List<Сотрудники> sot;
        List<Список_моделей> mod;
        List<Доп_услуги> dop;
        // Начальная цена услуги
        int nach_cell = 0;
        // Код выбранного автомобиля или услуги
        private int code;
        private int selectedId;
        public static string rootPath = AppDomain.CurrentDomain.BaseDirectory;  // Путь к папке Debug

        // Конструктор класса zaiv, инициализирует списки данными из базы и заполняет ComboBox данными о клиентах и сотрудниках
        public zaiv(Сотрудники user)
        {
            this.user = user;
            car = db.Автомобили.ToList();
            kli = db.Клиенты.ToList();
            sot = db.Сотрудники.ToList();
            mod = db.Список_моделей.ToList();
            dop = db.Доп_услуги.ToList();
            InitializeComponent();
            for (int i = 0; i < kli.Count; i++)
            {
                cb1.Items.Add(kli[i].ФИО);
            }
            for (int i = 0; i < sot.Count; i++)
            {
                if (sot[i].Код_типа_пользователя == 1) // замените "Какой-то тип" на нужное вам значение
                {
                    cb3.Items.Add(sot[i].ФИО);
                }
            }
        }

        // Обработчик нажатия кнопки Button1, увеличивает количество услуг и общую стоимость
        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            int a = Convert.ToInt32(kol.Text); // Текущее количество услуг
            int b = Convert.ToInt32(txt2.Text); // Текущая общая стоимость
            a++; // Увеличиваем количество на 1
            kol.Text = Convert.ToString(a); // Обновляем текстовое поле количества
            txt2.Text = Convert.ToString(b + nach_cell); // Обновляем текстовое поле общей стоимости
        }

        // Обработчик нажатия кнопки Button2, уменьшает количество услуг и общую стоимость
        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            int a = Convert.ToInt32(kol.Text); // Текущее количество услуг
            if (a <= 0) // Проверяем, что значение не нулевое или отрицательное
            {
                return; // Выходим из метода, не изменяя значения
            }

            int b = Convert.ToInt32(txt2.Text); // Текущая общая стоимость
            a--; // Уменьшаем количество на 1
            kol.Text = Convert.ToString(a); // Обновляем текстовое поле количества
            txt2.Text = Convert.ToString(b - nach_cell); // Обновляем текстовое поле общей стоимости
        }
        // Обработчик события загрузки списка автомобилей
        private void list1_Loaded(object sender, RoutedEventArgs e)
        {
            // Загрузка данных автомобилей и моделей из базы данных
            car = db.Автомобили.ToList<Автомобили>();
            mod = db.Список_моделей.ToList<Список_моделей>();
            // Создание элементов интерфейса для каждого автомобиля
            for (int i = 0; i < car.Count; i++)
            {
                WrapPanel wp = new WrapPanel();
                System.Windows.Controls.Image img = new System.Windows.Controls.Image();
                System.Windows.Controls.Label LabelName = new System.Windows.Controls.Label();

                // Настройка размеров панели и добавление изображения и названия автомобиля
                wp.Height = 100;
                wp.Width = 200;

                LabelName.Content = "Nissan " + mod[i].Наименование;

                // Получение пути к изображению и его загрузка
                string savePath = System.IO.Path.Combine(rootPath, "img");
                savePath = savePath + "\\" + car[i].Фото;
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(savePath);
                bitmap.EndInit();
                img.Source = bitmap;
                img.MouseDown += new MouseButtonEventHandler(MyImage_MouseDown);

                // Настройка размеров изображения
                img.Height = 150;
                img.Width = 100;

                // Установка идентификатора автомобиля для последующего использования
                wp.Uid = car[i].Код_авто.ToString();

                // Добавление изображения и названия в панель и добавление панели в список
                wp.Children.Add(img);
                wp.Children.Add(LabelName);

                list1.Items.Add(wp);
            }
        }

        // Обработчик нажатия на изображение автомобиля
        private void MyImage_MouseDown(object sender, MouseButtonEventArgs e)
        {
                // Приведение отправителя к типу Image и получение родительского элемента WrapPanel
                System.Windows.Controls.Image img = (System.Windows.Controls.Image)sender;
                WrapPanel wp = (WrapPanel)img.Parent;
                // Получение идентификатора автомобиля из Uid элемента WrapPanel
                code = Convert.ToInt32(wp.Uid);
                // Получение полного пути к папке с ресурсами
                string savePath = System.IO.Path.Combine(rootPath, "img");
                BitmapImage bitmap = new BitmapImage();
                // Получение информации об автомобиле из базы данных по коду
                var selectedCar = db.Автомобили.FirstOrDefault(c => c.Код_авто == code);
                // Получение связанных данных о модели, характеристиках, кузове и коробке передач
                Список_моделей spisok = db.Список_моделей.Where(x => x.Код_модели == selectedCar.Код_модели).FirstOrDefault();
                Характеристики har = db.Характеристики.Where(x => x.Код_характеристик == selectedCar.Код_характеристик).FirstOrDefault();
                Кузов to = db.Кузов.Where(x => x.Код_типа_кузова == selectedCar.Код_характеристик).FirstOrDefault();
                Коробка_передач ko = db.Коробка_передач.Where(x => x.Код_типа_коробки_передач == selectedCar.Код_характеристик).FirstOrDefault();

                // Если автомобиль найден, отображение его цены в текстовом поле
                if (selectedCar != null)
                {
                    txt1.Text = selectedCar.Цена;
                }
        }

        // Обработчик события загрузки списка дополнительных услуг
        private void list3_Loaded(object sender, RoutedEventArgs e)
        {
            // Загрузка списка дополнительных услуг из базы данных
            dop = db.Доп_услуги.ToList<Доп_услуги>();
            // Для каждой услуги создается элемент интерфейса
            for (int i = 0; i < dop.Count; i++)
            {
                WrapPanel wpi = new WrapPanel();
                System.Windows.Controls.Label LabName = new System.Windows.Controls.Label();

                // Настройка размеров и добавление обработчика событий
                wpi.Height = 100;
                wpi.Width = 200;
                wpi.MouseDown += new MouseButtonEventHandler(MyImage_MouseDo);
                // Установка названия услуги
                LabName.Content = dop[i].Наименование;

                // Установка идентификатора услуги
                wpi.Uid = dop[i].Код_доп_услуг.ToString();

                // Добавление названия в панель и панели в список
                wpi.Children.Add(LabName);

                list3.Items.Add(wpi);
            }
        }

        // Обработчик нажатия на элемент списка дополнительных услуг
        private void MyImage_MouseDo(object sender, MouseButtonEventArgs e)
        {
            // Получение идентификатора выбранной услуги
            WrapPanel wpi = (WrapPanel)sender;
            int selectedId = Convert.ToInt32(wpi.Uid);
            // Получение информации о выбранной услуге из базы данных
            var selectedDop = db.Доп_услуги.FirstOrDefault(c => c.Код_доп_услуг == selectedId);

            // Если услуга найдена, отображение ее стоимости и установка количества
            if (selectedDop != null)
            {
                nach_cell = Convert.ToInt32(selectedDop.Стоимость);
                txt2.Text = selectedDop.Стоимость;
                kol.Text = "1";
            }
        }

        // Обработчик нажатия кнопки для расчета итоговой цены
        private void btn2_Click(object sender, RoutedEventArgs e)
        {
            
            // Проверка наличия данных в текстовых полях
            if (txt1.Text != "" && txt2.Text != "")
            {
                // Расчет и отображение суммы цен
                int number1 = int.Parse(txt1.Text.ToString());
                int number2 = int.Parse(txt2.Text.ToString());
                int sum = number1 + number2;
                txt3.Text = sum.ToString();
            }else if(txt1.Text != "" && txt2.Text == "")
            {
                txt3.Text = txt1.Text;
            }
            else 
            {
                // Если данных нет, отображение сообщения об этом
                txt3.Text = "Нет данных";
            }
        }

        // Обработчик нажатия кнопки "Назад"
        private void back_Click(object sender, RoutedEventArgs e)
        {
            // Создание и отображение формы продавца, скрытие текущей формы
            Prodav pro = new Prodav(user);
            pro.Show(); this.Hide();
        }

        // Обработчик нажатия кнопки для создания заявки
        private void bt_Click(object sender, RoutedEventArgs e)
        {
            if (cb1.Text != null && cb3.Text != null && dp1.SelectedDate != null && txt1.Text != "" && txt3.Text != "")
            {
                // Получение данных выбранного автомобиля, клиента, сотрудника и услуги
                Автомобили avt = db.Автомобили.Where(x => x.Код_авто == code).FirstOrDefault();
                Клиенты har = db.Клиенты.Where(x => x.ФИО == cb1.Text).FirstOrDefault();
                Сотрудники to = db.Сотрудники.Where(x => x.ФИО == cb3.Text).FirstOrDefault();
                Доп_услуги ko = db.Доп_услуги.Where(x => x.Код_доп_услуг == selectedId).FirstOrDefault();
                // Получение выбранной даты
                DateTime selectedDate = dp1.SelectedDate.Value;
                // Создание записи о дополнительной услуге автомобиля
                Доп_услуги_авто dopol = new Доп_услуги_авто
                {
                    Доп_услуги = ko,
                    Код_авто = avt.Код_авто,
                    Количество = kol.Text
                };
                // Добавление записи в базу данных и сохранение изменений
                db.Доп_услуги_авто.Add(dopol);
                db.SaveChanges();
                // Создание заявки
                Заявка za = new Заявка
                {
                    Код_клиента = har.Код_клиента,
                    Код_авто = avt.Код_авто,
                    Код_сотрудника = to.Код_сотрудника,
                    Итоговая_цена = txt3.Text,
                    Дата = selectedDate
                };
                // Добавление заявки в базу данных и сохранение изменений
                db.Заявка.Add(za);
                db.SaveChanges();
                txt3.Text = "";
                txt3.Clear();
                // Отображение сообщения об успешном создании заявки
                MessageBox.Show("Заявка создана");
                
            }
            else
            {
                MessageBox.Show("Не все данные введены!");
            }
        }
    }
}
