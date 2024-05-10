using System.IO;

namespace LeafCope
{
    public class TabInfo
    {
        public string FilePath { get; set; }

        public string GetFileExtension()
        {
            return Path.GetExtension(FilePath);
        }
    }
}
