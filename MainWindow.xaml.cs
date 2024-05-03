using System.Windows;
using System.Windows.Input;

namespace LeafCope
{
    public partial class MainWindow : Window
    {
        private readonly MenuHandler menuHandler;

        public MainWindow()
        {
            InitializeComponent();

            menuHandler = new MenuHandler(this);
        }

        // Event handler for mouse left button down on the title bar
        private void TitleBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        // Event handler for mouse down on the files text
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

