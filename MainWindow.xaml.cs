using Microsoft.Win32;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace LeafCope
{
    public partial class MainWindow : Window
    {
        public string CurrentFilePath { get; set; }

        private readonly MenuHandler menuHandler;

        public MainWindow()
        {
            InitializeComponent();
            menuHandler = new MenuHandler(this);
        }

        private void TitleBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void FilesText_MouseDown(object sender, MouseButtonEventArgs e)
        {
            menuHandler.FilesText_MouseDown(sender, e);
        }

        private void QuitText_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }

        public void NewTab(string fileName)
        {
            TabItem tabItem = new TabItem();
            CustomTextEditor customTextEditor = new CustomTextEditor();
            tabItem.Header = string.IsNullOrEmpty(fileName) ? "Leaf" : fileName;
            tabItem.Content = customTextEditor;
            tabControl.Items.Add(tabItem);
            tabControl.SelectedItem = tabItem;
        }

        public void LoadFileContent(string filePath)
        {
            if (File.Exists(filePath))
            {
                ((CustomTextEditor)((TabItem)tabControl.SelectedItem).Content).Load(filePath);
            }
        }

        public void SaveFileContent()
        {
            if (!string.IsNullOrEmpty(CurrentFilePath))
            {
                File.WriteAllText(CurrentFilePath, ((CustomTextEditor)((TabItem)tabControl.SelectedItem).Content).Text);
            }
            else
            {
                SaveFileContentAs();
            }
        }

        public void SaveFileContentAs()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            if (saveFileDialog.ShowDialog() == true)
            {
                string filePath = saveFileDialog.FileName;
                File.WriteAllText(filePath, ((CustomTextEditor)((TabItem)tabControl.SelectedItem).Content).Text);
                CurrentFilePath = filePath;
                NewTab(Path.GetFileName(filePath));
            }
        }
    }
}
