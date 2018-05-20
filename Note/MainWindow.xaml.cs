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

namespace Note
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : Window
    {
        Brush DarkBg = (SolidColorBrush)new BrushConverter().ConvertFromString("#202020");
        Brush DarkFg = Brushes.DarkGray;

        Boolean isDark = false;
        Boolean Maximized = false;

        string filePath = "";

        // Generate saved file path
        Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void NewBtn_Click(object sender, MouseButtonEventArgs e)
        {
            if (TextArea.Text != "")
            {
                var SaveResult = MessageBox.Show("Do you want to save?", "Do you want to save?", MessageBoxButton.YesNoCancel);
                if (SaveResult == MessageBoxResult.Yes) {
                    SaveAs();
                    NewFile();
                }
                else if (SaveResult == MessageBoxResult.No) {
                    NewFile();
                }
            }
            else
            {
                NewFile();
            }

        }

        private void OpenBtn_Click(object sender, RoutedEventArgs e)
        {
            if (TextArea.Text != "")
            {
                var SaveResult = MessageBox.Show("Do you want to save?", "Do you want to save?", MessageBoxButton.YesNoCancel);
                if (SaveResult == MessageBoxResult.Yes)
                {
                    SaveAs();
                    Open();
                }
                else if (SaveResult == MessageBoxResult.No)
                {
                    Open();
                }
            }
            else {
                Open();
            }

        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (filePath != "")
            {
                System.IO.File.WriteAllText(filePath, TextArea.Text);
            }
            else {
                SaveAs();
            }
        }

        private void SaveAsBtn_Click(object sender, MouseButtonEventArgs e)
        {
            SaveAs();
        }

        void NewFile(){
            // Clear everything, make new file
            filePath = "";
            TextArea.Text = "";
            Title.Text = "New File";
        }

        void Open() {
            // Choose file to open
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                // Display text from a chosen file
                filePath = dlg.FileName;
                TextArea.Text = System.IO.File.ReadAllText(dlg.FileName);
            }
        }

        void SaveAs() {
            // Choose path to save
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();

            Nullable<bool> result = dlg.ShowDialog();
            // Save context to a created file
            if (result == true)
            {
                filePath = dlg.FileName;
                System.IO.File.WriteAllText(dlg.FileName, TextArea.Text);
            }
        }
        // Different font sizes
        private void SmallFont_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            TextArea.FontSize = 14;
        }

        private void MidFont_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            TextArea.FontSize = 18;
        }

        private void BigFont_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            TextArea.FontSize = 22;
        }

        // If at white theme, change to dark theme and vice versa
        private void ThemeBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (isDark == false)
            {
                // Goes to dark theme
                isDark = !isDark;
                ThemeBtn.Fill = Brushes.White;
                TxtBorder.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#202020");
                ChangeTheme(Brushes.Gray, DarkFg);

            }
            else
            {
                // Goes back to normal
                isDark = !isDark;
                ThemeBtn.Fill = (SolidColorBrush)new BrushConverter().ConvertFromString("#202020");
                TxtBorder.Background = Brushes.White;
                ChangeTheme(Brushes.DarkGray, Brushes.Black);
            }
            
        }

        void ChangeTheme(Brush buttonStroke, Brush InputColor) {
            ThemeBtn.Stroke = buttonStroke;
            TextArea.Foreground = InputColor;
            TextArea.CaretBrush = InputColor;
            Title.Foreground = InputColor;
            Title.CaretBrush = InputColor;
            minBtn.Foreground = InputColor;
            MaxBtn.Foreground = InputColor;
            CloseBtn.Foreground = InputColor;
        }
        
        private void MinBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void MaxBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // if the window is maximized, the button normalizes the window and vice versa
            if (Maximized == false)
            {
                this.WindowState = WindowState.Maximized;
                MaxBtn.Text = "2";
            }
            else {
                this.WindowState = WindowState.Normal;
                MaxBtn.Text = "1";
            }
            Maximized = !Maximized;
        }

        private void CloseBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void BaseGrid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //this.DragMove();
        }
    }
}
