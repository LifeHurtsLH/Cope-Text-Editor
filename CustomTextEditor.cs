using System;
using System.IO;
using System.Windows;
using System.Reflection;
using ICSharpCode.AvalonEdit.Highlighting;
using ICSharpCode.AvalonEdit;
using System.Xml;
using System.Windows.Media;
using ICSharpCode.AvalonEdit.Highlighting.Xshd;

namespace LeafCope
{
    public class CustomTextEditor : TextEditor
    {
        public CustomTextEditor()
        {
            // Get the assembly that contains the embedded resource
            Assembly assembly = Assembly.GetExecutingAssembly();

            // Define the resource name (namespace + filename)
            string resourceName = "LeafCope.Themes.Dracula.xshd";

            // Load the resource stream
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {
                if (stream != null)
                {
                    using (var reader = new XmlTextReader(stream))
                    {
                        SyntaxHighlighting = HighlightingLoader.Load(reader, HighlightingManager.Instance);
                    }
                }
                else
                {
                    MessageBox.Show("Dracula XSHD resource not found.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            // Add any other customizations or configurations here
            FontFamily = new FontFamily("Consolas");
            FontSize = 10;
        }
    }
}
