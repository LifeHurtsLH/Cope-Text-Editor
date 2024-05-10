using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace LeafCope
{
    public partial class MainWindow : Window
    {
        public string? CurrentFilePath { get; set; }

        private List<TabInfo> tabInfos = new List<TabInfo>();

        private readonly MenuHandler menuHandler;

        public MainWindow()
        {
            InitializeComponent();
            menuHandler = new MenuHandler(this);
            tabControl.SelectionChanged += (sender, e) =>
            {
                if (tabControl.SelectedItem != null)
                {
                    TabItem selectedTab = (TabItem)tabControl.SelectedItem;
                    TabInfo tabInfo = (TabInfo)selectedTab.Tag;

                }
            };
            KeyDown += MainWindow_KeyDown;
            CreateStartupGuideTab();
        }

        private void CreateStartupGuideTab()
        {
            TabItem startupGuideTab = new TabItem();
            CustomTextEditor startupGuideEditor = new CustomTextEditor();
            startupGuideEditor.Text = StartupGuide.GetStartupGuideText();
            startupGuideTab.Header = "Startup Guide";
            startupGuideTab.Content = startupGuideEditor;
            tabControl.Items.Add(startupGuideTab);
            tabControl.SelectedItem = startupGuideTab;
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

            TabInfo tabInfo = new TabInfo { FilePath = null };
            tabItem.Tag = tabInfo;

            tabItem.Header = string.IsNullOrEmpty(fileName) ? "Leaf" : fileName;
            tabItem.Content = customTextEditor;

            tabControl.Items.Add(tabItem);
            tabControl.SelectedItem = tabItem;
        }

        public void LoadFileContent(string filePath)
        {
            if (File.Exists(filePath))
            {
                TabItem selectedTab = (TabItem)tabControl.SelectedItem;
                CustomTextEditor customTextEditor = (CustomTextEditor)selectedTab.Content;

                string fileExtension = Path.GetExtension(filePath);
                if (fileExtension == ".cs")
                {
                    customTextEditor.ApplySyntaxHighlighting("DraculaCS");
                }
                else if (fileExtension == ".java")
                {
                    customTextEditor.ApplySyntaxHighlighting("GruvboxJava");
                }
                customTextEditor.Load(filePath);

                TabInfo tabInfo = (TabInfo)selectedTab.Tag;
                if (tabInfo != null)
                {
                    tabInfo.FilePath = filePath;
                }
            }
        }

        public void SaveFileContent(TabInfo tabInfo)
        {
            if (tabInfo != null && !string.IsNullOrEmpty(tabInfo.FilePath))
            {
                TabItem selectedTab = (TabItem)tabControl.SelectedItem;
                CustomTextEditor customTextEditor = (CustomTextEditor)selectedTab.Content;

                string content = customTextEditor.Text;
                if (!string.IsNullOrEmpty(content))
                {
                    try
                    {
                        File.WriteAllText(tabInfo.FilePath, content);
                        MessageBox.Show("File saved successfully at: " + tabInfo.FilePath, "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error saving file: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Content is empty. Nothing to save.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show("Invalid file path.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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

        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.Modifiers == (ModifierKeys.Control | ModifierKeys.Shift) && e.Key == Key.S)
            {
                menuHandler.SaveAsItem_Click(null, null);
            }
            else if (Keyboard.Modifiers == ModifierKeys.Control)
            {
                switch (e.Key)
                {
                    case Key.S:
                        menuHandler.SaveItem_Click(null, null);
                        break;
                    case Key.E:
                        menuHandler.OpenItem_Click(null, null);
                        break;
                    case Key.N:
                        menuHandler.NewItem_Click(null, null);
                        break;
                    case Key.W:
                        menuHandler.CloseTab();
                        break;
                    case Key.Q:
                        QuitText_MouseDown(null, null);
                        break;

                }
            }

        }
    }
}