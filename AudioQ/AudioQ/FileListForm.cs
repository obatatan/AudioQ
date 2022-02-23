using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AudioQ
{
    public partial class FileListForm : Form
    {
        private string MusicDir { get { return System.Environment.CurrentDirectory + @"\music"; } }
        private List<Music> MusicList { get; set; }

        [System.Runtime.InteropServices.DllImport("winmm.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        private static extern int mciSendString(string command, System.Text.StringBuilder buffer, int bufferSize, IntPtr hwndCallback);

        public FileListForm()
        {
            InitializeComponent();

            listView1.FullRowSelect = true;

            if (!System.IO.Directory.Exists(MusicDir))
            {
                MessageBox.Show(@"MusicDirが存在しません。");
            }

            string[] musicFilesPath = Directory.GetFiles(MusicDir, "*");
            if (0 < musicFilesPath.Count())
            {
                foreach (var path in musicFilesPath)
                {
                    var music = new Music(path);
                    string[] item1 = { music.FileName, music.FilePath };
                    listView1.Items.Add(new ListViewItem(item1));
                }
            }
            else
            {
                MessageBox.Show(@"音楽ファイルが存在しません。");
                this.Close();
            }
        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            WMPLib.WindowsMediaPlayer mediaPlayer = new WMPLib.WindowsMediaPlayer();

            //オーディオファイルを指定する（自動的に再生される）
            //mediaPlayer.URL = listView1.SelectedItems[0].SubItems[1].Text;
            ////再生する
            //mediaPlayer.controls.play();

            var palaeydiga = new PlayMusicForm(listView1.SelectedItems[0].SubItems[1].Text);
            palaeydiga.ShowDialog();

            var que = new Questionary(listView1.SelectedItems[0].SubItems[1].Text);
            que.ShowDialog();
        }
    }
}
