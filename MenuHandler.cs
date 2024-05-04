using Microsoft.Win32;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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
            newItem.Click += NewItem_Click;
            contextMenu.Items.Add(newItem);

            MenuItem openItem = new MenuItem();
            openItem.Header = "Open";
            openItem.Click += OpenItem_Click;
            contextMenu.Items.Add(openItem);

            MenuItem saveItem = new MenuItem();
            saveItem.Header = "Save";
            saveItem.Click += SaveItem_Click;
            contextMenu.Items.Add(saveItem);

            MenuItem saveAsItem = new MenuItem();
            saveAsItem.Header = "Save As";
            saveAsItem.Click += SaveAsItem_Click;
            contextMenu.Items.Add(saveAsItem);

            // Show the context menu
            contextMenu.IsOpen = true;
        }

        private void NewItem_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.NewTab(null);
        }

        private void OpenItem_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;
                mainWindow.CurrentFilePath = filePath;
                mainWindow.NewTab(Path.GetFileName(filePath));
                mainWindow.LoadFileContent(filePath);
            }
        }

        public void SaveItem_Click(object sender, RoutedEventArgs e)
        {
            if (mainWindow.tabControl.SelectedItem != null)
            {
                TabItem selectedTab = (TabItem)mainWindow.tabControl.SelectedItem;
                string filePath = mainWindow.CurrentFilePath;

                if (!string.IsNullOrEmpty(filePath))
                {
                    File.WriteAllText(filePath, ((CustomTextEditor)selectedTab.Content).Text);
                }
                else
                {
                    SaveText();
                }
            }
        }
        private void SaveText()
        {
            if (mainWindow.tabControl.SelectedItem != null)
            {
                TabItem selectedTab = (TabItem)mainWindow.tabControl.SelectedItem;
                string filePath = mainWindow.CurrentFilePath;

                if (!string.IsNullOrEmpty(filePath))
                {
                    File.WriteAllText(filePath, ((CustomTextEditor)selectedTab.Content).Text);
                }
            }
        }

        private void SaveAsItem_Click(object sender, RoutedEventArgs e)
        {
            SaveTextAs();
        }

        private void SaveTextAs()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            saveFileDialog.InitialDirectory = Path.GetDirectoryName(mainWindow.CurrentFilePath);
            saveFileDialog.FileName = Path.GetFileName(mainWindow.CurrentFilePath);

            string extension = Path.GetExtension(mainWindow.CurrentFilePath);
            if (!string.IsNullOrEmpty(extension))
            {
                saveFileDialog.DefaultExt = extension;
                saveFileDialog.Filter = $"{extension} files (*{extension})|*{extension}|All files (*.*)|*.*";
            }
            else
            {
                saveFileDialog.Filter = "All files (*.*)|*.*";
            }

            if (saveFileDialog.ShowDialog() == true)
            {
                string filePath = saveFileDialog.FileName;
                File.WriteAllText(filePath, ((CustomTextEditor)((TabItem)mainWindow.tabControl.SelectedItem).Content).Text);
                mainWindow.CurrentFilePath = filePath;
            }
        }
    }
}
