using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AudioQ
{
    static class Program
    {
        private static string ImportDir { get { return Environment.CurrentDirectory + @"\import"; } }
        private static string MusicZipFilePath { get { return $@"{ImportDir}\music.zip"; } }
        private static string MusicDir { get { return Environment.CurrentDirectory + @"\music"; } }


        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {

            string mutexName = "AudioQ";
            System.Threading.Mutex mutex = new System.Threading.Mutex(false, mutexName);

            bool hasHandle = false;
            try
            {
                try
                {
                    hasHandle = mutex.WaitOne(0, false);
                }
                catch (System.Threading.AbandonedMutexException)
                {
                    hasHandle = true;
                }
                if (hasHandle == false)
                {
                    MessageBox.Show("多重起動はできません。");
                    return;
                }

                if (!Directory.Exists(MusicZipFilePath))
                {
                    if (!Directory.Exists(MusicDir))
                    {
                        MessageBox.Show(@"MusicDirが存在しません。");
                    }
                    else
                    {
                        var result = MessageBox.Show(@"musicフォルダのファイルを削除してZIPファイルを解凍しますか？
※フォルダをZIP化したZIPファイルを利用してください。
　ファイルを直接ZIPした場合、正常に動作しません。",
                            "質問",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Exclamation,
                            MessageBoxDefaultButton.Button1);
                        if (result == DialogResult.Yes)
                        {
                            if (!UnzipMusicFile())
                            {
                                MessageBox.Show(@"解凍に失敗しました");
                            }
                        }
                    }

                }

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new FileListForm());
            }
            finally
            {
                if (hasHandle)
                {
                    mutex.ReleaseMutex();
                }
                mutex.Close();
            }
        }

        static bool UnzipMusicFile ()
        {
            string[] musicFilesPath = Directory.GetFiles(MusicDir, "*");
            if (0 < musicFilesPath.Count())
            {
                foreach (var path in musicFilesPath)
                {
                    File.Delete(path);
                }
            }

            ZipFile.ExtractToDirectory(MusicZipFilePath, Environment.CurrentDirectory);

            return true;
        }
    }
}
