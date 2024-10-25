using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;

namespace Avtosalon
{
    /// <summary>
    /// Логика взаимодействия для Automob.xaml
    /// </summary>
    public partial class Automob : Window
    {
        public AvtosalonEntities13 db = new AvtosalonEntities13();
        public Сотрудники user;
        List<Коробка_передач> kor = new List<Коробка_передач>();
        List<Кузов> kuzov = new List<Кузов>();
        List<Список_моделей> spis = new List<Список_моделей>();
        List<Характеристики> hara = new List<Характеристики>();
        public static string rootPath = AppDomain.CurrentDomain.BaseDirectory;  // Путь к папке Debug
        public static string imageName = "";    // Имя файла текущего изображения
        public static string imageSource = "";  // Путь к файлу текущего изображения

        public static System.Windows.Controls.Image image_Image;
        string mod, speed, mass, usl, stoim, opisanie, num, sum;
        public Automob(Сотрудники user)
        {
            InitializeComponent();
            hara = db.Характеристики.ToList();
            spis = db.Список_моделей.ToList();
            kor = db.Коробка_передач.ToList();
            kuzov = db.Кузов.ToList();
            for (int i = 0; i < kor.Count; i++)
            {
                cb1.Items.Add(kor[i].Наименование);
            }
            for (int i = 0; i < kuzov.Count; i++)
            {
                cb2.Items.Add(kuzov[i].Наименование);
            }
            for (int i = 0; i < spis.Count; i++)
            {
                cb3.Items.Add(spis[i].Наименование);
            }
            for (int i = 0; i < hara.Count; i++)
            {
                cb4.Items.Add(hara[i].Номер);
            }
            image_Image = image;
            this.user = user;
        }

        private void butt1_Click(object sender, RoutedEventArgs e)
        {
            Model.Visibility = Visibility.Visible;
        }

        private void bt1_Click(object sender, RoutedEventArgs e)
        {
            mod = tx1.Text;
            if (mod != "")
            {
                Список_моделей spi = new Список_моделей
                {
                    Наименование = mod
                };
                db.Список_моделей.Add(spi);
                db.SaveChanges();
                MessageBox.Show("Сохранение прошло успешно!");
                Model.Visibility = Visibility.Hidden;
            }
            else
            {
                MessageBox.Show("Не все данные введены!");
            }
        }


        private void butt2_Click(object sender, RoutedEventArgs e)
        {
            Haracter.Visibility = Visibility.Visible;
        }

        private void bt2_Click(object sender, RoutedEventArgs e)
        {
            speed = t1.Text;
            mass = t2.Text;
            num = t11.Text;
            if (speed != "" && mass != "" && cb1.Text != null && cb2.Text != null && num != "")
            {
                var nomer = db.Характеристики.Any(x => x.Номер == t11.Text);
                if (nomer)
                {
                    MessageBox.Show("Такой номер уже существует!");
                }
                else
                {
                    Коробка_передач korob = db.Коробка_передач.Where(x => x.Наименование == cb1.Text).FirstOrDefault();
                    Кузов kuz = db.Кузов.Where(x => x.Наименование == cb2.Text).FirstOrDefault();
                    Характеристики ha = new Характеристики
                    {
                        Скорость = speed,
                        Масса = mass,
                        Код_типа_коробки_передач = korob.Код_типа_коробки_передач,
                        Код_типа_кузова = kuz.Код_типа_кузова,
                        Номер = num
                    };
                    db.Характеристики.Add(ha);
                    db.SaveChanges();
                    MessageBox.Show("Сохранение прошло успешно!");
                    Haracter.Visibility = Visibility.Hidden;
                }
            }
            else
            {
                MessageBox.Show("Не все данные введены!");
            }
        }

        private void butt3_Click(object sender, RoutedEventArgs e)
        {
            Dop.Visibility = Visibility.Visible;
        }

        private void bt3_Click(object sender, RoutedEventArgs e)
        {
            usl = txt1.Text;
            stoim = txt2.Text;
            if(usl != "" && stoim != "")
            {
                Доп_услуги dopol = new Доп_услуги
                {
                    Наименование = usl,
                    Стоимость = stoim
                };
                db.Доп_услуги.Add(dopol);
                db.SaveChanges();
                MessageBox.Show("Сохранение прошло успешно!");
                Dop.Visibility = Visibility.Hidden;
            }
            else
            {
                MessageBox.Show("Не все данные введены!");
            }
        }

        private void butt4_Click(object sender, RoutedEventArgs e)
        {
            Autocar.Visibility = Visibility.Visible;
        }
        private void bt4_Click(object sender, RoutedEventArgs e)
        {
            opisanie = text2.Text;
            sum = ttxx.Text;

            if (opisanie != "" && cb3.Text != null && cb4.Text != null && image != null && sum != "")
            {
                try
                {
                    // Проверка, существует ли такое изображение в БД
                    if (db.Автомобили.Where(i => i.Фото == imageName).FirstOrDefault() != null)
                    {
                        MessageBox.Show("Такое изображение уже есть", "Выберите другое!", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    // Получение изображения из элемента Image
                    BitmapImage bitmapImage = new BitmapImage();
                    bitmapImage.BeginInit();
                    bitmapImage.UriSource = new Uri(imageSource);
                    bitmapImage.EndInit();

                    // Настройка энкодера для png файлов
                    BitmapEncoder encoder = new PngBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create(bitmapImage));

                    // Путь для сохранения изображения
                    string imagePath = System.IO.Path.Combine(rootPath, "img", imageName);


                    // Сохранение изображения
                    using (FileStream fileStream = new FileStream(imagePath, FileMode.Create))
                    {
                        encoder.Save(fileStream);
                    }

                    // Оповещение пользователя об успешном сохранении
                    MessageBox.Show("Данные успешно сохранены!", "Мои поздравления!!!", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка сохранения изображения", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Не все данные введены!");
            }
                Список_моделей spiso = db.Список_моделей.Where(x => x.Наименование == cb3.Text).FirstOrDefault();
                Характеристики haracte = db.Характеристики.Where(x => x.Номер == cb4.Text).FirstOrDefault(); 
                Автомобили aut = new Автомобили
                {
                    Код_модели = spiso.Код_модели,
                    Код_характеристик = haracte.Код_характеристик,
                    Цена = sum,
                    Описание = opisanie,
                    Фото = imageName,
                };
                db.Автомобили.Add(aut);
                db.SaveChanges();
                MessageBox.Show("Сохранение прошло успешно!");
                Autocar.Visibility = Visibility.Hidden;
                
        }
        private void butt5_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Добавлять изображения только формата (.png и .jpg)");
            try
            {
                // Настройка диалогового окна
                OpenFileDialog openFileDialog = new OpenFileDialog();

                //openFileDialog.Filter = "png (*.png)|*.png|All files (*.*)|*.*";

                openFileDialog.Filter = "PNG files (*.png)|*.png|JPEG files (*.jpg)|*.jpg|All files (*.*)|*.*";
                openFileDialog.CheckFileExists = true;
                openFileDialog.CheckPathExists = true;

                // Открытие диалогового окна
                if (openFileDialog.ShowDialog() == true)
                {
                    // Установка изображения в элемент Image
                    image.Source = new BitmapImage(new Uri(openFileDialog.FileName));

                    // Сохранение данных изображения
                    imageName = openFileDialog.SafeFileName;
                    imageSource = openFileDialog.FileName;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка загрузки изображения", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            Admin ad = new Admin(user);
            ad.Show(); this.Close();
        }

        private void b1_Click(object sender, RoutedEventArgs e)
        {
            Model.Visibility = Visibility.Hidden;
        }

        private void b2_Click(object sender, RoutedEventArgs e)
        {
            Haracter.Visibility = Visibility.Hidden;
        }

        private void b3_Click(object sender, RoutedEventArgs e)
        {
            Dop.Visibility = Visibility.Hidden;
        }

        private void b4_Click(object sender, RoutedEventArgs e)
        {
            Autocar.Visibility = Visibility.Hidden;
        }

        private void tx1_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            int maxLength = 20; // Максимальное количество цифр, которое можно ввести

            if (tx1 != null && tx1.Text.Length >= maxLength)
            {
                e.Handled = true; // Отменить ввод, если достигнуто максимальное количество символов
            }
            foreach (char c in e.Text)
            {
                if (!char.IsLetterOrDigit(c) && c != '-')
                {
                    e.Handled = true; // Запрещаем ввод символов, отличных от букв, цифр и тире
                    break; // Останавливаем проверку после первого недопустимого символа
                }
            }
        }

        private void t1_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            int maxLength = 3; // Максимальное количество цифр, которое можно ввести

            if (t1 != null && t1.Text.Length >= maxLength)
            {
                e.Handled = true; // Отменить ввод, если достигнуто максимальное количество символов
            }
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
            {
                e.Handled = true; // Отменить ввод, если введенный символ не является цифрой
            }
        }

        private void t2_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            int maxLength = 4; // Максимальное количество цифр, которое можно ввести

            if (t2 != null && t2.Text.Length >= maxLength)
            {
                e.Handled = true; // Отменить ввод, если достигнуто максимальное количество символов
            }
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
            {
                e.Handled = true; // Отменить ввод, если введенный символ не является цифрой
            }
        }

        private void txt1_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            foreach (char c in e.Text)
            {
                if (char.IsDigit(c))
                {
                    e.Handled = true;
                    return;
                }
            }
            int maxLength = 30; // Максимальное количество цифр, которое можно ввести

            if (txt1 != null && txt1.Text.Length >= maxLength)
            {
                e.Handled = true; // Отменить ввод, если достигнуто максимальное количество символов
            }
        }

        private void txt2_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            int maxLength = 6; // Максимальное количество цифр, которое можно ввести

            if (txt2 != null && txt2.Text.Length >= maxLength)
            {
                e.Handled = true; // Отменить ввод, если достигнуто максимальное количество символов
            }
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
            {
                e.Handled = true; // Отменить ввод, если введенный символ не является цифрой
            }
        }

        private void ttxx_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            int maxLength = 7; // Максимальное количество цифр, которое можно ввести

            if (ttxx != null && ttxx.Text.Length >= maxLength)
            {
                e.Handled = true; // Отменить ввод, если достигнуто максимальное количество символов
            }
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
            {
                e.Handled = true; // Отменить ввод, если введенный символ не является цифрой
            }
        }

        private void text2_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            foreach (char c in e.Text)
            {
                if (char.IsDigit(c))
                {
                    e.Handled = true;
                    return;
                }
            }
            int maxLength = 300; // Максимальное количество цифр, которое можно ввести

            if (text2 != null && text2.Text.Length >= maxLength)
            {
                e.Handled = true; // Отменить ввод, если достигнуто максимальное количество символов
            }
        }
    }
}
