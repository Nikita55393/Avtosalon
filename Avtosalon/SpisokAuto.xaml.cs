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
    /// Логика взаимодействия для SpisokAuto.xaml
    /// </summary>
    public partial class SpisokAuto : Window
    {
        // Объявление переменных для работы с базой данных и списками объектов
        public AvtosalonEntities13 db = new AvtosalonEntities13();
        List<Автомобили> car; // Список автомобилей
        List<ТО> stat; // Список обслуживаний
        List<Список_моделей> mod; // Список моделей
        public Сотрудники user; // Текущий сотрудник
        public Кузов to;
        public Коробка_передач ko;
        public static string rootPath = AppDomain.CurrentDomain.BaseDirectory;  // Путь к папке Debug
        // Конструктор класса SpisokAuto, принимающий сотрудника
        public SpisokAuto(Сотрудники user)
        {
            InitializeComponent(); // Инициализация компонентов формы
            this.user = user; // Установка текущего сотрудника
        }

        // Метод, вызываемый при загрузке списка автомобилей
        private void list1_Loaded(object sender, RoutedEventArgs e)
        {
            car = db.Автомобили.ToList<Автомобили>(); // Загрузка списка автомобилей из базы данных
            mod = db.Список_моделей.ToList<Список_моделей>(); // Загрузка списка моделей из базы данных

            // Цикл по каждому автомобилю
            for (int i = 0; i < car.Count; i++)
            {
                WrapPanel wp = new WrapPanel(); // Создание контейнера для отображения информации об автомобиле
                System.Windows.Controls.Image img = new System.Windows.Controls.Image(); // Изображение автомобиля
                Label LabelName = new Label(); // Метка для названия автомобиля

                wp.Height = 100; // Установка высоты контейнера
                wp.Width = 200; // Установка ширины контейнера

                LabelName.Content = "Nissan " + mod[i].Наименование; // Установка названия автомобиля

                string savePath = System.IO.Path.Combine(rootPath, "img"); // Путь к папке с изображениями
                savePath = savePath + "\\" + car[i].Фото; // Формирование пути к изображению автомобиля
                BitmapImage bitmap = new BitmapImage(); // Создание объекта изображения
                bitmap.BeginInit(); // Начало инициализации изображения
                bitmap.UriSource = new Uri(savePath); // Установка источника изображения
                bitmap.EndInit(); // Конец инициализации изображения
                img.Source = bitmap; // Установка изображения
                img.MouseDown += new MouseButtonEventHandler(MyImage_MouseDown); // Привязка обработчика события нажатия на изображение

                img.Height = 150; // Установка высоты изображения
                img.Width = 100; // Установка ширины изображения

                wp.Uid = car[i].Код_авто.ToString(); // Установка идентификатора автомобиля в контейнере

                wp.Children.Add(img); // Добавление изображения в контейнер
                wp.Children.Add(LabelName); // Добавление названия автомобиля в контейнер

                list1.Items.Add(wp); // Добавление контейнера в список отображения
            }
        }

        // Обработчик события нажатия на изображение автомобиля
        private void MyImage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            System.Windows.Controls.Image img = (System.Windows.Controls.Image)sender; // Получение изображения
            WrapPanel wp = (WrapPanel)img.Parent; // Получение контейнера
            int code = Convert.ToInt32(wp.Uid); // Получение кода автомобиля
            string savePath = System.IO.Path.Combine(rootPath, "img"); // Путь к папке с изображениями
            BitmapImage bitmap = new BitmapImage(); // Создание объекта изображения

            // Поиск автомобиля в базе данных по коду
            var selectedCar = db.Автомобили.FirstOrDefault(c => c.Код_авто == code);
            Список_моделей spisok = db.Список_моделей.Where(x => x.Код_модели == selectedCar.Код_модели).FirstOrDefault();
            Характеристики har = db.Характеристики.Where(x => x.Код_характеристик == selectedCar.Код_характеристик).FirstOrDefault();
            to = db.Кузов.Where(x => x.Код_типа_кузова == har.Код_типа_кузова).FirstOrDefault();
            ko = db.Коробка_передач.Where(x => x.Код_типа_коробки_передач == har.Код_типа_коробки_передач).FirstOrDefault();
            ТО tex = db.ТО.Where(x => x.Код_ТО == selectedCar.Код_ТО).FirstOrDefault();

            if (selectedCar != null)
            {
                savePath = savePath + "\\" + selectedCar.Фото; // Формирование пути к изображению автомобиля
                bitmap.BeginInit(); // Начало инициализации изображения
                bitmap.UriSource = new Uri(savePath); // Установка источника изображения
                bitmap.EndInit(); // Конец инициализации изображения
                img.Source = bitmap; // Установка изображения
                opis.Text = selectedCar.Описание; // Установка описания автомобиля
                kat.Text = Convert.ToString(spisok.Наименование); // Установка названия модели
                jan.Text = Convert.ToString("Масса: " + har.Масса + " кг; \n" + "Скорость: " + har.Скорость + " км/ч; \n" + "Коробка передач: " + ko.Наименование + "\n" + "Кузов: " + to.Наименование); // Установка характеристик автомобиля
                if(selectedCar.Код_ТО != null)
                {
                    tem.Text = Convert.ToString(tex.Статус); // Установка статуса обслуживания
                }
                else
                {
                    MessageBox.Show("Автомобиль не проходил ТО");
                }
                
            }
        }

        // Обработчик события нажатия на кнопку "Назад"
        private void back_Click_1(object sender, RoutedEventArgs e)
        {
            Prodav pr = new Prodav(user); // Создание экземпляра формы продавца
            pr.Show(); // Отображение формы продавца
            this.Close(); // Закрытие текущей формы
        }
    }
}
