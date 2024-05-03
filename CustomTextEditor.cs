using System;
using ICSharpCode.AvalonEdit.Highlighting;
using ICSharpCode.AvalonEdit;
using System.IO;
using ICSharpCode.AvalonEdit.Highlighting.Xshd;
using System.Xml;
using System.Windows.Media;

namespace LeafCope
{
    public class CustomTextEditor : TextEditor
    {
        public CustomTextEditor()
        {
            // Get the base directory of the application
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

            // Construct the full path to the Dracula XSHD file using relative path
            string relativePath = @"..\..\..\Themes\Dracula.xshd";
            string fullPath = Path.GetFullPath(Path.Combine(baseDirectory, relativePath));

            // Load the Dracula XSHD file from the relative path
            using (var stream = File.OpenRead(fullPath))
            {
                using (var reader = new XmlTextReader(stream))
                {
                    SyntaxHighlighting = HighlightingLoader.Load(reader, HighlightingManager.Instance);
                }
            }

            // Add any other customizations or configurations here
            FontFamily = new FontFamily("Consolas");
            FontSize = 10;
        }
    }
}
