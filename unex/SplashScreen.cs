using System;
using System.Windows.Forms;

namespace UnExponat {
    public partial class SplashScreen : Form {

        private Timer mTimer;
        private const int TIMER = 5000;

        public SplashScreen(String version) {
            InitializeComponent();
            lbVersion.Text = version;
        }

        private void SplashScreen_Shown(object sender, EventArgs e) {
            mTimer = new Timer();
            mTimer.Interval = TIMER;
            mTimer.Start();
            mTimer.Tick += timer_Tick;
        }

        void timer_Tick(object sender, EventArgs e) {
            mTimer.Stop();
            this.Hide();
            Program.startUnex();
        }

        private void imgSplash_Click(object sender, EventArgs e) {
            mTimer.Stop();            
            this.Hide();
            Program.startUnex();
        }
    }
}
