namespace CVImageMatcher.GUI {
    partial class CamView {
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
            this.camImage = new System.Windows.Forms.PictureBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            ((System.ComponentModel.ISupportInitialize)(this.camImage)).BeginInit();
            this.SuspendLayout();
            // 
            // camImage
            // 
            this.camImage.Location = new System.Drawing.Point(12, 12);
            this.camImage.Name = "camImage";
            this.camImage.Size = new System.Drawing.Size(640, 480);
            this.camImage.TabIndex = 0;
            this.camImage.TabStop = false;
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 25;
            this.listBox1.Location = new System.Drawing.Point(669, 12);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(593, 479);
            this.listBox1.TabIndex = 1;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // CamView
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1274, 518);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.camImage);
            this.Name = "CamView";
            this.Text = "CVImageMatcher";
            ((System.ComponentModel.ISupportInitialize)(this.camImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox camImage;
        private System.Windows.Forms.ListBox listBox1;
    }
}

