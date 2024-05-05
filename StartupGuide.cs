using System;
using System.Text;

namespace LeafCope
{
    public static class StartupGuide
    {
        public static string GetStartupGuideText()
        {
            string[] lines = new string[]
            {
                " Welcome to LeafCope Text Editor ",
                "",
                " Shortcuts:",
                " Ctrl + S: Save",
                " Ctrl + Shift + S: Save As",
                " Ctrl + O: Open",
                " Ctrl + N: New Tab",
                " Ctrl + W: Close Tab",
                " Ctrl + Q: Close Text Editor",
                " Warning closing does not save!",
                "",
                " Enjoy and use at own risk!"
            };

            int maxLength = 0;
            foreach (string line in lines)
            {
                if (line.Length > maxLength)
                {
                    maxLength = line.Length;
                }
            }

            StringBuilder guideText = new StringBuilder();
            guideText.AppendLine("                             ┌" + new string('─', maxLength) + "┐");
            foreach (string line in lines)
            {
                guideText.AppendLine("                             │" + line.PadRight(maxLength) + "│");
            }
            guideText.AppendLine("                             └" + new string('─', maxLength) + "┘");
            guideText.AppendLine();
            return guideText.ToString();
        }
    }
}
