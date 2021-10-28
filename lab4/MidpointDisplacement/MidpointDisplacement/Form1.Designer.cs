namespace MidpointDisplacement
{
    partial class Form1
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
            this.DrawRocks = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // DrawRocks
            // 
            this.DrawRocks.Location = new System.Drawing.Point(13, 13);
            this.DrawRocks.Name = "DrawRocks";
            this.DrawRocks.Size = new System.Drawing.Size(75, 23);
            this.DrawRocks.TabIndex = 0;
            this.DrawRocks.Text = "DrawRocks";
            this.DrawRocks.UseVisualStyleBackColor = true;
            this.DrawRocks.Click += new System.EventHandler(this.DrawRocks_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(13, 56);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "DrawSteps";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.DrawRocks);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button DrawRocks;
        private System.Windows.Forms.Button button1;
    }
}

