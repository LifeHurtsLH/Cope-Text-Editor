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
            Assembly assembly = Assembly.GetExecutingAssembly();

            string resourceName = "LeafCope.Themes.Dracula.xshd";

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

            // for future me (you are 0iq if you need a comment here again)
            FontFamily = new FontFamily("Consolas");
            FontSize = 12;
        }
    }
}
