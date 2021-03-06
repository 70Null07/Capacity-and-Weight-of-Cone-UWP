using Windows.UI.Xaml.Controls;

// Документацию по шаблону элемента "Диалоговое окно содержимого" см. по адресу https://go.microsoft.com/fwlink/?LinkId=234238

namespace HelloWorld
{
    public sealed partial class MyCustomDialog : ContentDialog
    {
        bool isCapacity, isWeight;

        public MyCustomDialog()
        {
            InitializeComponent();
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }

        private void CapacityBox_Checked(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            isCapacity = (bool)CapacityBox.IsChecked;
            isWeight = (bool)WeightBox.IsChecked;
        }
    }
}
