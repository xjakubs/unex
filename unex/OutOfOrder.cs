using System;
using System.Windows.Forms;

namespace UnExponat {
    public partial class OutOfOrder : Form {

        private int mRestartCombination = 0;

        public OutOfOrder(String version) {
            InitializeComponent();
            lbVersion.Text = version;
#if !TESTING
            // ALWAYS ON TOP
            this.TopMost = true;
#endif
        }

        private void lbVersion_Click(object sender, EventArgs e) {
            if (mRestartCombination == 4) {
                Program.logInfo("OutOfOrder (disable): Restart manually!");
                lbVersion.Text = "RESTARTING...";
                mRestartCombination = 0;
                Program.setOutOfOrder(false, true);
                return;
            }

            mRestartCombination = 1;
        }

        private void leftTopPanel_Click(object sender, EventArgs e) {
            if (mRestartCombination == 2) {
                mRestartCombination++;
                Program.logInfo("OutOfOrder (disable) combination +2");
            } else {
                mRestartCombination = 0;
            }
        }

        private void rightTopPanel_Click(object sender, EventArgs e) {
            if (mRestartCombination == 1) {
                mRestartCombination++;
                Program.logInfo("OutOfOrder (disable) combination +1");
            } else {
                mRestartCombination = 0;
            }
        }

        private void leftBottomPanel_Click(object sender, EventArgs e) {
            if (mRestartCombination == 3) {
                mRestartCombination++;
                Program.logInfo("OutOfOrder (disable) combination +3");
            } else {
                mRestartCombination = 0;
            }
        }

    }
}
