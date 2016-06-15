using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        private readonly Stopwatch _stopwatch = new Stopwatch();

        public Form1()
        {
            InitializeComponent();
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            btnGo.Enabled = false;
            timer1.Start();
            backgroundWorker1.WorkerReportsProgress = true;

            backgroundWorker1.RunWorkerAsync();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            _stopwatch.Restart();
            for (int i = 0; i < 100; i++)
            {
                Thread.Sleep(300); // simulating work
                backgroundWorker1.ReportProgress(i+1);
            }
            _stopwatch.Stop();
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            lblProgress.Text = e.ProgressPercentage + @"%";
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            timer1.Stop();
            btnGo.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblElapsed.Text = _stopwatch.Elapsed.ToString(@"hh\:mm\:ss\.f");
        }
    }
}
