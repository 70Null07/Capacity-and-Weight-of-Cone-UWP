using System;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x419

// 2.Создать меню с командами Ввод, Вычисления и Выход. 
// Команда Вычисления после запуска программы недоступна.
// При выборе команды Ввод должно открываться диалоговое окно, содержащее:
// – три поля ввода типа TextBox с метками Радиус, Высота, Плотность;
// – группу из двух флажков Объем и Масса типа CheckBox;
// – кнопку типа Button.
// Необходимо обеспечить возможность:
// – ввода радиуса, высоты и плотности конуса;
// – выбора режима с помощью флажков: подсчет объема и/или массы конуса. 
// При выборе команды Вычисления должно открываться окно сообщений с результатами.
// При выборе команды Выход приложение должно завершить работу.

namespace HelloWorld
{
    public sealed partial class MainPage : Page
    {
        private double radius, height, density;
        internal bool capacity, weight;

        public MainPage()
        {
            InitializeComponent();

            button1.IsEnabled = false;

            ApplicationView.PreferredLaunchViewSize = new Size(500, 500);
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;
        }

        private async void button_Click(object sender, RoutedEventArgs e)
        {
            MyCustomDialog dialog = new MyCustomDialog();
            await dialog.ShowAsync();
            try
            {
                radius = double.Parse(dialog.RadiusBox.Text);
                height = double.Parse(dialog.HeightBox.Text);
                density = double.Parse(dialog.DensityBox.Text);
            }
            catch (FormatException ex)
            {
                ContentDialog contentDialog = new ContentDialog()
                {
                    Title = "Возникла ошибка при вводе данных",
                    Content = ex.Message,
                    PrimaryButtonText = "Повторить ввод",
                };
                await contentDialog.ShowAsync();
            }

            if (dialog.WeightBox.IsChecked == false && dialog.CapacityBox.IsChecked == false)
            {
                ContentDialog contentDialog = new ContentDialog()
                {
                    Title = "Ошибка ввода данных, задействованы оба флажка",
                    PrimaryButtonText = "Повторить ввод",
                };
                await contentDialog.ShowAsync();
            }
            else
            {
                capacity = (bool)dialog.CapacityBox.IsChecked;
                weight = (bool)dialog.WeightBox.IsChecked;

                button1.IsEnabled = true;
            }
        }

        private async void button1_Click(object sender, RoutedEventArgs e)
        {
            if (capacity == true)
            {
                ContentDialog contentDialog = new ContentDialog()
                {
                    Title = "Результатат вычисления объема",
                    Content = (3.14 * radius * radius * height) / 3,
                    PrimaryButtonText = "Принять"
                };
                await contentDialog.ShowAsync();
            }
            if (weight == true)
            {
                ContentDialog contentDialog = new ContentDialog()
                {
                    Title = "Результатат вычисления массы",
                    Content = ((3.14 * radius * radius * height) * density) / 3,
                    PrimaryButtonText = "Принять"
                };
                await contentDialog.ShowAsync();
            }
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            CoreApplication.Exit();
        }
    }
}
