namespace UnExponat {
    partial class SplashScreen {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SplashScreen));
            this.imgSplash = new System.Windows.Forms.PictureBox();
            this.lbVersion = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.imgSplash)).BeginInit();
            this.SuspendLayout();
            // 
            // imgSplash
            // 
            this.imgSplash.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.imgSplash.BackColor = System.Drawing.Color.White;
            this.imgSplash.BackgroundImage = global::UnExponat.Properties.Resources.splashscreen;
            this.imgSplash.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.imgSplash.Location = new System.Drawing.Point(9, 9);
            this.imgSplash.Margin = new System.Windows.Forms.Padding(0);
            this.imgSplash.Name = "imgSplash";
            this.imgSplash.Size = new System.Drawing.Size(1353, 859);
            this.imgSplash.TabIndex = 0;
            this.imgSplash.TabStop = false;
            this.imgSplash.Click += new System.EventHandler(this.imgSplash_Click);
            // 
            // lbVersion
            // 
            this.lbVersion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lbVersion.BackColor = System.Drawing.Color.White;
            this.lbVersion.Font = new System.Drawing.Font("Dimenze RB", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbVersion.Location = new System.Drawing.Point(1239, 828);
            this.lbVersion.Name = "lbVersion";
            this.lbVersion.Size = new System.Drawing.Size(120, 40);
            this.lbVersion.TabIndex = 23;
            this.lbVersion.Text = "vX.YY.ZZ";
            this.lbVersion.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // SplashScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1371, 877);
            this.Controls.Add(this.lbVersion);
            this.Controls.Add(this.imgSplash);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SplashScreen";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "UNEX";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Shown += new System.EventHandler(this.SplashScreen_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.imgSplash)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox imgSplash;
        private System.Windows.Forms.Label lbVersion;
    }
}