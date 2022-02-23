using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AudioQ
{
    public partial class Questionary : Form
    {
        private string ExportDir { get { return Environment.CurrentDirectory + @"\export"; } }
        private string ExporFile { get { return ExportDir + $@"\{ DateTime.Now.ToString("yyyyMMdd_HHmmss")}.csv"; } }
        private string FileName { get; set; }

        public Questionary(string filePath)
        {
            InitializeComponent();
            FileName = Path.GetFileName(filePath);
        }

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

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(textBox1.Text))
                {
                    MessageBox.Show(@"未入力です。");
                    return;
                }
                StreamWriter file = new StreamWriter(ExporFile, false, Encoding.UTF8);
                var answer = textBox1.Text;
                answer = answer.Replace(Environment.NewLine, @"\r\n");
                file.WriteLine($@"{FileName},この曲を聞いてどう思いましたか,{answer}");
                file.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            this.Close();
        }
    }
}
