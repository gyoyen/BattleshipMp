namespace BattleshipMpClient
{
    partial class Form12_ShipThemeSelection
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnLightTheme = new System.Windows.Forms.Button();
            this.btnDarkTheme = new System.Windows.Forms.Button();
            this.themeLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnLightTheme
            // 
            this.btnLightTheme.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLightTheme.Location = new System.Drawing.Point(46, 98);
            this.btnLightTheme.Name = "btnLightTheme";
            this.btnLightTheme.Size = new System.Drawing.Size(127, 32);
            this.btnLightTheme.TabIndex = 3;
            this.btnLightTheme.Text = "Light theme";
            this.btnLightTheme.UseVisualStyleBackColor = true;
            this.btnLightTheme.Click += new System.EventHandler(this.btnLightTheme_Click);
            // 
            // btnDarkTheme
            // 
            this.btnDarkTheme.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDarkTheme.Location = new System.Drawing.Point(191, 98);
            this.btnDarkTheme.Name = "btnDarkTheme";
            this.btnDarkTheme.Size = new System.Drawing.Size(136, 32);
            this.btnDarkTheme.TabIndex = 4;
            this.btnDarkTheme.Text = "Dark theme";
            this.btnDarkTheme.UseVisualStyleBackColor = true;
            this.btnDarkTheme.Click += new System.EventHandler(this.btnDarkTheme_Click);
            // 
            // themeLabel
            // 
            this.themeLabel.AutoSize = true;
            this.themeLabel.Location = new System.Drawing.Point(13, 32);
            this.themeLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.themeLabel.Name = "themeLabel";
            this.themeLabel.Size = new System.Drawing.Size(298, 16);
            this.themeLabel.TabIndex = 6;
            this.themeLabel.Text = "Which coloring theme do you want for your ships?";
            // 
            // Form12_ShipThemeSelection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(358, 164);
            this.Controls.Add(this.themeLabel);
            this.Controls.Add(this.btnDarkTheme);
            this.Controls.Add(this.btnLightTheme);
            this.Name = "Form12_ShipThemeSelection";
            this.Text = "Form12_ShipThemeSelection";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnLightTheme;
        private System.Windows.Forms.Button btnDarkTheme;
        private System.Windows.Forms.Label themeLabel;
    }
}