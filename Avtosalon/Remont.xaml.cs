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
    /// Логика взаимодействия для Remont.xaml
    /// </summary>
    public partial class Remont : Window
    {
        // Объявление переменных для работы с базой данных и данными
        public AvtosalonEntities13 db = new AvtosalonEntities13();
        List<Автомобили> car; // Список автомобилей
        List<ТО> stat; // Список ТО
        List<Список_моделей> mod; // Список моделей
        public Кузов to;
        public Коробка_передач ko;
        public Сотрудники user; // Текущий сотрудник
        public Автомобили avto; // Текущий автомобиль
        public static string rootPath = AppDomain.CurrentDomain.BaseDirectory;
        // Конструктор класса Remont
        public Remont(Сотрудники user, int kodsp)
        {
            InitializeComponent(); // Инициализация компонентов формы
            this.user = user; // Установка текущего сотрудника
            stat = db.ТО.ToList(); // Загрузка списка ТО из базы данных
                                   // Заполнение комбобокса статусами ТО
            for (int i = 0; i < stat.Count; i++)
            {
                cb1.Items.Add(stat[i].Статус);
            }
        }

        // Обработчик загрузки списка автомобилей
        private void list1_Loaded(object sender, RoutedEventArgs e)
        {
            car = db.Автомобили.ToList<Автомобили>(); // Загрузка списка автомобилей
            mod = db.Список_моделей.ToList<Список_моделей>(); // Загрузка списка моделей
                                                              // Цикл по всем автомобилям для отображения их в списке
            for (int i = 0; i < car.Count; i++)
            {
                WrapPanel wp = new WrapPanel(); // Панель для отображения изображения и названия
                System.Windows.Controls.Image img = new System.Windows.Controls.Image(); // Изображение автомобиля
                Label LabelName = new Label(); // Метка для названия автомобиля

                wp.Height = 100; // Высота панели
                wp.Width = 200; // Ширина панели

                LabelName.Content = "Nissan " + mod[i].Наименование; // Название автомобиля

                string savePath = System.IO.Path.Combine(rootPath, "img"); // Путь к изображениям
                savePath = savePath + "\\" + car[i].Фото; // Путь к конкретному изображению
                BitmapImage bitmap = new BitmapImage(); // Битовая маска изображения
                bitmap.BeginInit(); // Начало инициализации
                bitmap.UriSource = new Uri(savePath); // Установка источника изображения
                bitmap.EndInit(); // Конец инициализации
                img.Source = bitmap; // Установка изображения
                img.MouseDown += new MouseButtonEventHandler(MyImage_MouseDown); // Подписка на событие клика по изображению

                img.Height = 150; // Высота изображения
                img.Width = 100; // Ширина изображения

                wp.Uid = car[i].Код_авто.ToString(); // Уникальный идентификатор панели

                wp.Children.Add(img); // Добавление изображения на панель
                wp.Children.Add(LabelName); // Добавление названия на панель

                list1.Items.Add(wp); // Добавление панели в список
            }
        }

        // Обработчик клика по изображению
        private void MyImage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            System.Windows.Controls.Image img = (System.Windows.Controls.Image)sender; // Получение изображения
            WrapPanel wp = (WrapPanel)img.Parent; // Получение родительской панели
            int code = Convert.ToInt32(wp.Uid); // Код автомобиля
            string savePath = System.IO.Path.Combine(rootPath, "img"); // Путь к изображениям
                                                                           // Поиск автомобиля по коду
            avto = db.Автомобили.FirstOrDefault(c => c.Код_авто == code);
            Список_моделей spisok = db.Список_моделей.Where(x => x.Код_модели == avto.Код_модели).FirstOrDefault();
            Характеристики har = db.Характеристики.Where(x => x.Код_характеристик == avto.Код_характеристик).FirstOrDefault();
            to = db.Кузов.Where(x => x.Код_типа_кузова == har.Код_типа_кузова).FirstOrDefault();
            ko = db.Коробка_передач.Where(x => x.Код_типа_коробки_передач == har.Код_типа_коробки_передач).FirstOrDefault();

            if (avto != null)
            {
                BitmapImage bitmap = new BitmapImage(); // Битовая маска изображения
                savePath = savePath + "\\" + avto.Фото; // Путь к изображению автомобиля
                bitmap.BeginInit(); // Начало инициализации
                bitmap.UriSource = new Uri(savePath); // Установка источника изображения
                bitmap.EndInit(); // Конец инициализации
                imgMain.Source = bitmap; // Установка изображения на главное место
                opis.Text = avto.Описание; // Отображение описания автомобиля
                kat.Text = Convert.ToString(spisok.Наименование); // Отображение названия модели
                jan.Text = Convert.ToString("Масса: " + har.Масса + " кг; \n" + "Скорость: " + har.Скорость + " км/ч; \n" + "Коробка передач: " + ko.Наименование + "\n" + "Кузов: " + to.Наименование); // Отображение характеристик
            }
        }

        // Обработчик нажатия кнопки изменения ТО
        private void bt12_Click(object sender, RoutedEventArgs e)
        {
            if (cb1.Text != null)
            {
                Автомобили RedactAvto = db.Автомобили.Where(x => x.Код_авто == avto.Код_авто).FirstOrDefault(); // Поиск автомобиля по коду
                RedactAvto.Код_ТО = stat[cb1.SelectedIndex].Код_ТО; // Изменение кода ТО
                db.SaveChanges(); // Сохранение изменений в базе данных
                MessageBox.Show("ТО измененно!"); // Вывод сообщения об успешном изменении
                cb1.Items.Clear(); // Очистка комбобокса
                cb1.Text = ""; // Сброс текста в комбобоксе
            }
            else
                MessageBox.Show("Выберите ТО!"); // Вывод сообщения об ошибке

        }

        // Обработчик нажатия кнопки "Назад"
        private void back_Click(object sender, RoutedEventArgs e)
        {
            Mexanik me = new Mexanik(user); // Создание нового экземпляра формы Mexanik
            me.Show(); // Отображение формы Mexanik
            this.Close(); // Закрытие текущей формы
        }

    }
}
