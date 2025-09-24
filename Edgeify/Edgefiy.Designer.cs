namespace Edgeify
{
    partial class Edgefiy
    {

        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kod generowany przez Projektanta formularzy systemu Windows

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Edgefiy));
            this.bannerPic = new System.Windows.Forms.PictureBox();
            this.uninstallEdge = new System.Windows.Forms.Button();
            this.closeBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.bannerPic)).BeginInit();
            this.SuspendLayout();
            this.bannerPic.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("bannerPic.BackgroundImage")));
            this.bannerPic.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.bannerPic.Dock = System.Windows.Forms.DockStyle.Top;
            this.bannerPic.InitialImage = ((System.Drawing.Image)(resources.GetObject("bannerPic.InitialImage")));
            this.bannerPic.Location = new System.Drawing.Point(0, 0);
            this.bannerPic.Name = "bannerPic";
            this.bannerPic.Size = new System.Drawing.Size(394, 161);
            this.bannerPic.TabIndex = 0;
            this.bannerPic.TabStop = false;
            this.uninstallEdge.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(36)))));
            this.uninstallEdge.ForeColor = System.Drawing.Color.White;
            this.uninstallEdge.Location = new System.Drawing.Point(12, 122);
            this.uninstallEdge.Name = "uninstallEdge";
            this.uninstallEdge.Size = new System.Drawing.Size(370, 32);
            this.uninstallEdge.TabIndex = 1;
            this.uninstallEdge.Text = "Uninstall Microsoft Edge";
            this.uninstallEdge.UseVisualStyleBackColor = false;
            this.uninstallEdge.Click += new System.EventHandler(this.uninstallEdge_Click);
            this.closeBtn.BackColor = System.Drawing.Color.Black;
            this.closeBtn.ForeColor = System.Drawing.Color.White;
            this.closeBtn.Location = new System.Drawing.Point(364, 0);
            this.closeBtn.Name = "closeBtn";
            this.closeBtn.Size = new System.Drawing.Size(30, 29);
            this.closeBtn.TabIndex = 10;
            this.closeBtn.Text = "X";
            this.closeBtn.UseVisualStyleBackColor = false;
            this.closeBtn.Click += new System.EventHandler(this.closeBtn_Click_1);
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(394, 164);
            this.ControlBox = false;
            this.Controls.Add(this.closeBtn);
            this.Controls.Add(this.uninstallEdge);
            this.Controls.Add(this.bannerPic);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Edgefiy";
            this.Opacity = 0.8D;
            this.ShowIcon = false;
            ((System.ComponentModel.ISupportInitialize)(this.bannerPic)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox bannerPic;
        private System.Windows.Forms.Button uninstallEdge;
        private System.Windows.Forms.Button closeBtn;
    }
}

