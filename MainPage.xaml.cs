using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

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
// При выборе команды Выход приложение должно завершить 
// работу. 

namespace HelloWorld
{
    public sealed partial class MainPage : Page
    {
        private double radius, height, density;
        private bool capacity, weight;
        public MainPage()
        {
            this.InitializeComponent();
            this.button1.IsEnabled = false;
            ApplicationView.PreferredLaunchViewSize = new Size(480, 800);
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;
        }

        private static async Task<string> ShowAddDialogAsync()
        {
            TextBox inputTextBox = new TextBox { AcceptsReturn = false };
            TextBox inputHight = new TextBox { AcceptsReturn = false };
            ContentDialog contentDialog = new ContentDialog()
            {
                Title = "Введите радиус",
                Content = inputTextBox,
                IsSecondaryButtonEnabled = true,
                PrimaryButtonText = "Ok",
                SecondaryButtonText = "Cancel"
            };
            if (await contentDialog.ShowAsync() == ContentDialogResult.Primary)
                return await Task.FromResult(inputTextBox.Text);
            else
                return default(string);
        }

        private async void button_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new MyCustomDialog();
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
                contentDialog.ShowAsync();
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
               if (dialog.CapacityBox.IsChecked == true && dialog.WeightBox.IsChecked == true)
                {
                    capacity = true;
                    weight = true;
                }
               else if(dialog.WeightBox.IsChecked == true)
                {
                    weight = true;
                    capacity = false;
                }
               else
                {
                    capacity = true;
                    weight = false;
                }
            }
            if (radius != default && height != default && density != default && !(capacity == false && weight == false))
            {
                this.button1.IsEnabled = true;
            }
        }

        private async void button1_Click(object sender, RoutedEventArgs e)
        {
            if (capacity == true)
            {
                var contentDialog = new ContentDialog()
                {
                    Title = "Результатат вычисления объема",
                    Content = (3.14 * radius * radius * height)/3,
                    PrimaryButtonText = "Принять"
                };
                await contentDialog.ShowAsync();
            }
            if (weight == true)
            {
                var contentDialog = new ContentDialog()
                {
                    Title = "Результатат вычисления массы",
                    Content = ((3.14 * radius * radius * height)*density)/3,
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
