namespace UnExponat {
    partial class PresentationScreen {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.webPanel = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // webPanel
            // 
            this.webPanel.Location = new System.Drawing.Point(0, 0);
            this.webPanel.Name = "webPanel";
            this.webPanel.Size = new System.Drawing.Size(1920, 1080);
            this.webPanel.TabIndex = 0;
            this.webPanel.Visible = false;
            // 
            // PresentationScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = global::UnExponat.Properties.Resources.splashscreen;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ClientSize = new System.Drawing.Size(1920, 1080);
            this.ControlBox = false;
            this.Controls.Add(this.webPanel);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Location = new System.Drawing.Point(9, 9);
            this.Name = "PresentationScreen";
            this.Text = "UNEX_TWO";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel webPanel;
    }
}