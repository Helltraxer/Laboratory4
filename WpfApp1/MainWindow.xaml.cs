using System;
using System.IO;
using Microsoft.Win32;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Drawing;
using System.Drawing.Imaging;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        Bitmap BitMap;
        private bool changesMade = false;

        public MainWindow()
        {
            InitializeComponent();
            Closing += MainWindow_Closing;
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (changesMade)
            {
                MessageBoxResult result = MessageBox.Show("Хотите сохранить изменения?", "Сохранение", MessageBoxButton.YesNoCancel);

                if (result == MessageBoxResult.Yes)
                {
                    // Ваш код сохранения
                    e.Cancel = true; // Отменяем закрытие окна, чтобы пользователь мог продолжить сохранение
                }
                else if (result == MessageBoxResult.Cancel)
                {
                    e.Cancel = true; // Отменяем закрытие окна
                }
            }
        }

        public static BitmapImage ToBitmapImage(Bitmap bitmap)
        {
            using (var memory = new MemoryStream())
            {
                bitmap.Save(memory, ImageFormat.Png);
                memory.Position = 0;

                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memory;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
                bitmapImage.Freeze();

                return bitmapImage;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            if (openFileDialog.ShowDialog() == true)
            {
                BitMap = new Bitmap(openFileDialog.FileName);

                BitmapImage newBitmapImage = ToBitmapImage(BitMap);

                IMAGE.Source = newBitmapImage;
            }

        }

        private void Rotate_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Rotate rotate = new Rotate(BitMap, RotateFlipType.Rotate90FlipNone);
                rotate.RotateImage(BitMap, RotateFlipType.Rotate90FlipNone);
                BitmapImage newBitmapImage = ToBitmapImage(BitMap);
                IMAGE.Source = newBitmapImage;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}");
            }
        }

        private void CHB_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ColorMatrix colorMatrix = new ColorMatrix();
                CHBFilter bw = new CHBFilter(BitMap, colorMatrix);
                bw.Create(BitMap, colorMatrix);
                BitmapImage newBitmapImage = ToBitmapImage(BitMap);
                IMAGE.Source = newBitmapImage;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}");
            }
        }

        private void Light_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ColorMatrix colorMatrix = new ColorMatrix();
                LightFilter light = new LightFilter(BitMap, colorMatrix);
                light.Create(BitMap, colorMatrix);
                BitmapImage newBitmapImage = ToBitmapImage(BitMap);
                IMAGE.Source = newBitmapImage;
            }
            catch (Exception err)
            {
                MessageBox.Show($"Произошла ошибка: {err.Message}");
            }
        }

        private void Dark_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ColorMatrix colorMatrix = new ColorMatrix();
                DarkFilter Dark = new DarkFilter(BitMap, colorMatrix);
                Dark.Create(BitMap, colorMatrix);
                BitmapImage newBitmapImage = ToBitmapImage(BitMap);
                IMAGE.Source = newBitmapImage;
            }
            catch (Exception err)
            {
                MessageBox.Show($"Произошла ошибка: {err.Message}");
            }
        }

        private void Negative_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ColorMatrix colorMatrix = new ColorMatrix();
                NegativeFilter negative = new NegativeFilter(BitMap, colorMatrix);
                negative.Create(BitMap, colorMatrix);
                BitmapImage newBitmapImage = ToBitmapImage(BitMap);
                IMAGE.Source = newBitmapImage;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}");
            }
        }

        private void Green_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ColorMatrix colorMatrix = new ColorMatrix();
                GreenFilter Green = new GreenFilter(BitMap, colorMatrix);
                Green.Create(BitMap, colorMatrix);
                BitmapImage newBitmapImage = ToBitmapImage(BitMap);
                IMAGE.Source = newBitmapImage;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}");
            }
        }

        private void Violet_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ColorMatrix colorMatrix = new ColorMatrix();
                VioletFilter Pink = new VioletFilter(BitMap, colorMatrix);
                Pink.Create(BitMap, colorMatrix);
                BitmapImage newBitmapImage = ToBitmapImage(BitMap);
                IMAGE.Source = newBitmapImage;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}");
            }

        }

        private void Crop_Button_Click(object sender, RoutedEventArgs e)
        {
            FON.Visibility = Visibility.Visible;
            LABEL.Visibility = Visibility.Visible;
            SHIRINA.Visibility = Visibility.Visible;
            VISOTA.Visibility = Visibility.Visible;
            KNOPKA.Visibility = Visibility.Visible;
        }

        private void Croping_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (BitMap == null) throw new ArgumentException("Изображение не загружено");
                else
                {
                    int x = Convert.ToInt32(SHIRINA.Text.ToString());
                    int y = Convert.ToInt32(VISOTA.Text.ToString());
                    Crop crop = new Crop(BitMap, x, y);

                    if (x > BitMap.Width || y > BitMap.Height || x < 100 || y < 100)
                    {
                        throw new ArgumentException("Неверный формат ввода");
                    }
                    else
                    {
                        Rectangle cropArea = new Rectangle(0, 0, x, y);
                        Bitmap croppedImage = new Bitmap(BitMap);
                        BitMap = new Bitmap(cropArea.Width, cropArea.Height);
                        using (Graphics graphics = Graphics.FromImage(BitMap))
                        {
                            graphics.DrawImage(croppedImage, new Rectangle(0, 0, cropArea.Width, cropArea.Height), cropArea, GraphicsUnit.Pixel);
                        }
                    }


                    BitmapImage newBitmapImage = ToBitmapImage(BitMap);

                    IMAGE.Source = newBitmapImage;

                    FON.Visibility = Visibility.Hidden;
                    LABEL.Visibility = Visibility.Hidden;
                    SHIRINA.Visibility = Visibility.Hidden;
                    VISOTA.Visibility = Visibility.Hidden;
                    KNOPKA.Visibility = Visibility.Hidden;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}");
            }
        }


        private void Collage_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (MessageBox.Show("Выберите второе изображение для коллажа") == MessageBoxResult.OK)
                {
                    OpenFileDialog openFileDialog1 = new OpenFileDialog();

                    string Input2 = "";
                    if (openFileDialog1.ShowDialog() == true) Input2 = openFileDialog1.FileName;
                    Bitmap bitmap2 = new Bitmap(Input2);

                    Collages collage = new Collages(BitMap, Input2);
                    collage.Collage(BitMap, Input2);

                    using (Bitmap image2 = new Bitmap(Input2))
                    {
                        Bitmap cImage = new Bitmap(BitMap);
                        int NewWidth = Math.Max(BitMap.Width, image2.Width);
                        int NewHeight = Math.Max(BitMap.Height, image2.Height);
                        BitMap = new Bitmap(NewWidth * 2, NewHeight);
                        using (Graphics g = Graphics.FromImage(BitMap))
                        {
                            g.DrawImage(cImage, new Rectangle(0, 0, cImage.Width, cImage.Height));
                            g.DrawImage(image2, new Rectangle(cImage.Width, 0, NewWidth, NewHeight));
                        }
                    }

                    BitmapImage newBitmapImage = ToBitmapImage(BitMap);

                    IMAGE.Source = newBitmapImage;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}");
            }
        }



        private void Nadpis_Button_Click(object sender, RoutedEventArgs e)
        {
            FON.Visibility = Visibility.Visible;
            LABEL2.Visibility = Visibility.Visible;
            LABEL3.Visibility = Visibility.Visible;
            BUTTON.Visibility = Visibility.Visible;
            POSITION.Visibility = Visibility.Visible;
            TEXT.Visibility = Visibility.Visible;
        }

        private void Dobavlenie_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Nadpis nadpis = new Nadpis(BitMap, TEXT.Text.ToString(), POSITION.Text.ToString());
                nadpis.Add(BitMap, TEXT.Text.ToString(), POSITION.Text.ToString());

                BitmapImage newBitmapImage = ToBitmapImage(BitMap);
                IMAGE.Source = newBitmapImage;

                FON.Visibility = Visibility.Hidden;
                LABEL2.Visibility = Visibility.Hidden;
                LABEL3.Visibility = Visibility.Hidden;
                BUTTON.Visibility = Visibility.Hidden;
                POSITION.Visibility = Visibility.Hidden;
                TEXT.Visibility = Visibility.Hidden;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}");
            }
        }

        private void Exit_Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void SaveButton(object sender, RoutedEventArgs e)
        {
            try
            {
                // Указываем путь к файлу для сохранения
                string savePath = "save.jpg";

                // Проверяем наличие файла и создаем его, если отсутствует
                if (!File.Exists(savePath))
                {
                    File.Create(savePath).Close();
                }

                // Сохраняем изображение
                JpegBitmapEncoder encoder = new JpegBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(IMAGE.Source as BitmapSource));

                using (FileStream stream = new FileStream(savePath, FileMode.Create))
                {
                    encoder.Save(stream);
                }

                // Выводим сообщение об успешном сохранении
                MessageBox.Show("Изображение успешно сохранено", "Сохранение");

                // Операция выполнена успешно, устанавливаем флаг изменений
                changesMade = false;
            }
            catch (Exception err)
            {
                MessageBox.Show("Ошибка при сохранении изображения: " + err.Message);
            }
        }


        private void SaveAsButton(object sender, RoutedEventArgs e)
        {
            try
            {
                SaveFileDialog save = new SaveFileDialog
                {
                    Filter = "JPG Files (*.jpg)|*.jpg"
                };
                if (save.ShowDialog() == true)
                {
                    JpegBitmapEncoder sav = new JpegBitmapEncoder();
                    sav.Frames.Add(BitmapFrame.Create(IMAGE.Source as BitmapSource));
                    using (FileStream stream = new FileStream(save.FileName, FileMode.Create))
                        sav.Save(stream);
                    MessageBox.Show("Изображение успешно сохранено", "Сохранение");
                }
            }
            catch (Exception err)
            {
                MessageBox.Show("Ошибка: " + err);
                return;
            }
        }

    }
}
