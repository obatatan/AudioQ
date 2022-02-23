using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WMPLib;

namespace AudioQ
{
    public partial class PlayMusicForm : Form
    {
        WMPLib.WindowsMediaPlayer mediaPlayer = new WMPLib.WindowsMediaPlayer();
        Timer _timer = new Timer();

        [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
        protected override void WndProc(ref Message m)
        {
            const int WM_SYSCOMMAND = 0x112;
            const long SC_CLOSE = 0xF060L;

            if (m.Msg == WM_SYSCOMMAND &&
                (m.WParam.ToInt64() & 0xFFF0L) == SC_CLOSE)
            {
                return;
            }

            base.WndProc(ref m);
        }

        public PlayMusicForm(string path)
        {
            InitializeComponent();
            mediaPlayer.URL = path;
            mediaPlayer.controls.play();

            _timer.Interval = 300;
            _timer.Tick += delegate
            {
                if (mediaPlayer.playState == WMPPlayState.wmppsStopped)
                {
                    this.Close();
                }
            };
            _timer.Start();

            this.pictureBox1.Image = Image.FromFile(@"C:\Users\tankr\Desktop\animationGifs\images\icon_loader_a_bb_01_s1.gif");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            mediaPlayer.controls.stop();
        }
    }
}
