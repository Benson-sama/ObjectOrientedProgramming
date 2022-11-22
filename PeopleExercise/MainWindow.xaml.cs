using Microsoft.Win32;
using System.Windows;

namespace PeopleExercise
{
    /// <summary>
    /// Interactionlogic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        // Beginner approach to the value of the slider.
        //private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        //{
        //    this.mySlider.Content = $"{e.NewValue.ToString("000")} cm";
        //}

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            
            if (saveFileDialog.ShowDialog() == true)
            {
                this.filePathTextBox.Text = saveFileDialog.FileName;
            }
        }
    }
}
