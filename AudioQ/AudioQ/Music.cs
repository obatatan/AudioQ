using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AudioQ
{
    public class Music
    {
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string FileExtension { get; set; }

        public Music(string path)
        {
            if (!System.IO.File.Exists(path))
            {
                MessageBox.Show(@"ファイルが存在しません。");
            }

            FilePath = path;
            FileName = System.IO.Path.GetFileNameWithoutExtension(path);
            FileExtension = System.IO.Path.GetExtension(path);
        }
    }
}
