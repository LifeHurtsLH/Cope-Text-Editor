using System.IO;
using Microsoft.Win32;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;

namespace LeafCope
{
    public class MenuHandler
    {
        private MainWindow mainWindow;

        public MenuHandler(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
        }

        public void FilesText_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // Create a context menu
            ContextMenu contextMenu = new ContextMenu();

            // Add menu items
            MenuItem newItem = new MenuItem();
            newItem.Header = "New";
            contextMenu.Items.Add(newItem);

            MenuItem openItem = new MenuItem();
            openItem.Header = "Open";
            openItem.Click += OpenItem_Click; // Subscribe to open menu item click event
            contextMenu.Items.Add(openItem);

            MenuItem saveItem = new MenuItem();
            saveItem.Header = "Save";
            saveItem.Click += SaveItem_Click; // Subscribe to save menu item click event
            contextMenu.Items.Add(saveItem);

            // Show the context menu
            contextMenu.IsOpen = true;
        }

        private void OpenItem_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;
                mainWindow.textEditor.Load(filePath);
            }
        }

        private void SaveItem_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            if (saveFileDialog.ShowDialog() == true)
            {
                string filePath = saveFileDialog.FileName;
                File.WriteAllText(filePath, mainWindow.textEditor.Text);
            }
        }
    }
}
