
using Microsoft.Win32;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace PureNote
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.PreviewKeyDown += new KeyEventHandler(HandleEsc);
            TextBox.ContextMenu = null;
            Point startPosition = new Point();
            this.PreviewMouseRightButtonDown += (sender, e) =>
            {
                startPosition = e.GetPosition(this);
            };
            this.PreviewMouseMove += (sender, e) =>
            {
                if (e.RightButton == MouseButtonState.Pressed)
                {
                    Point endPosition = e.GetPosition(this);
                    Vector vector = endPosition - startPosition;
                    this.Left += vector.X;
                    this.Top += vector.Y;
                }
            };
        }
        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                FileExit();
        }
        
        private void FileExit()
        {
            if (TextBox.Text == "")
            {
                Close();
            }
            else WindowDialog();
        }
        private void WindowDialog()
        {
            MessageBoxResult msbr = MessageBox.Show("ウインドウを閉じますか？", "PureNote", MessageBoxButton.YesNo);
            if (msbr == MessageBoxResult.Yes)
            {
                Close();
            }
        }
        private void SaveAsCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.AddExtension = true; 
            sfd.FileName = @"untitled.txt";
            sfd.Filter = "Text Files(*.txt)|*.txt|All(*.*)|*";
            if (sfd.ShowDialog() == true)
            {
                    File.WriteAllText(sfd.FileName, this.TextBox.Text);
            }
        }
        private void OpenCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == true)
            {
                string content = File.ReadAllText(ofd.FileName);
                this.TextBox.Text = content;
            }
        }
    }
}