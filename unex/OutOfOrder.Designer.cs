using System;
using System.Drawing;
using System.Windows.Forms;

namespace UnExponat {
    partial class OutOfOrder {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OutOfOrder));
            this.imgOutOfOrder = new System.Windows.Forms.PictureBox();
            this.lbVersion = new System.Windows.Forms.Label();
            this.leftTopPanel = new System.Windows.Forms.Panel();
            this.rightTopPanel = new System.Windows.Forms.Panel();
            this.leftBottomPanel = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.imgOutOfOrder)).BeginInit();
            this.SuspendLayout();
            // 
            // imgOutOfOrder
            // 
            this.imgOutOfOrder.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.imgOutOfOrder.BackColor = System.Drawing.Color.White;
            this.imgOutOfOrder.BackgroundImage = global::UnExponat.Properties.Resources.outoforder;
            this.imgOutOfOrder.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.imgOutOfOrder.Location = new System.Drawing.Point(9, 9);
            this.imgOutOfOrder.Margin = new System.Windows.Forms.Padding(0);
            this.imgOutOfOrder.Name = "imgOutOfOrder";
            this.imgOutOfOrder.Size = new System.Drawing.Size(1353, 859);
            this.imgOutOfOrder.TabIndex = 0;
            this.imgOutOfOrder.TabStop = false;
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
            this.lbVersion.Click += new System.EventHandler(this.lbVersion_Click);
            // 
            // leftTopPanel
            // 
            this.leftTopPanel.Location = new System.Drawing.Point(0, 0);
            this.leftTopPanel.Name = "leftTopPanel";
            this.leftTopPanel.Size = new System.Drawing.Size(40, 40);
            this.leftTopPanel.TabIndex = 23;
            this.leftTopPanel.Click += new System.EventHandler(this.leftTopPanel_Click);
            // 
            // rightTopPanel
            // 
            this.rightTopPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.rightTopPanel.Location = new System.Drawing.Point(1333, 0);
            this.rightTopPanel.Name = "rightTopPanel";
            this.rightTopPanel.Size = new System.Drawing.Size(40, 40);
            this.rightTopPanel.TabIndex = 24;
            this.rightTopPanel.Click += new System.EventHandler(this.rightTopPanel_Click);
            // 
            // leftBottomPanel
            // 
            this.leftBottomPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.leftBottomPanel.Location = new System.Drawing.Point(0, 838);
            this.leftBottomPanel.Name = "leftBottomPanel";
            this.leftBottomPanel.Size = new System.Drawing.Size(40, 40);
            this.leftBottomPanel.TabIndex = 25;
            this.leftBottomPanel.Click += new System.EventHandler(this.leftBottomPanel_Click);
            // 
            // OutOfOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1371, 877);
            this.Controls.Add(this.lbVersion);
            this.Controls.Add(this.leftTopPanel);
            this.Controls.Add(this.rightTopPanel);
            this.Controls.Add(this.leftBottomPanel);
            this.Controls.Add(this.imgOutOfOrder);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "OutOfOrder";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "UNEX";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.imgOutOfOrder)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox imgOutOfOrder;
        private System.Windows.Forms.Label lbVersion;
        private Panel leftTopPanel;
        private Panel rightTopPanel;
        private Panel leftBottomPanel;
    }
}