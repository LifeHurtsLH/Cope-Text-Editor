
using System.Windows;
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
        }
    }

