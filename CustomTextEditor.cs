
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
            ApplySyntaxHighlighting("DraculaCS");
            FontFamily = new FontFamily("Consolas");
            FontSize = 12;
        }

        public void ApplySyntaxHighlighting(string themeName)
        {
            string resourceName = $"LeafCope.Themes.{themeName}.xshd";
            using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))
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
                    MessageBox.Show($"{themeName} XSHD resource not found.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}