
namespace SolidsOfRevolution
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonAddPoints = new System.Windows.Forms.Button();
            this.textBoxRotations = new System.Windows.Forms.TextBox();
            this.buttonX = new System.Windows.Forms.Button();
            this.buttonY = new System.Windows.Forms.Button();
            this.buttonZ = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonAddPoints
            // 
            this.buttonAddPoints.Location = new System.Drawing.Point(13, 12);
            this.buttonAddPoints.Name = "buttonAddPoints";
            this.buttonAddPoints.Size = new System.Drawing.Size(154, 33);
            this.buttonAddPoints.TabIndex = 0;
            this.buttonAddPoints.Text = "Задать образующую";
            this.buttonAddPoints.UseVisualStyleBackColor = true;
            this.buttonAddPoints.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBoxRotations
            // 
            this.textBoxRotations.Location = new System.Drawing.Point(13, 82);
            this.textBoxRotations.Name = "textBoxRotations";
            this.textBoxRotations.PlaceholderText = "Кол-во вращений";
            this.textBoxRotations.Size = new System.Drawing.Size(154, 23);
            this.textBoxRotations.TabIndex = 1;
            this.textBoxRotations.TextChanged += new System.EventHandler(this.textBoxRotations_TextChanged);
            // 
            // buttonX
            // 
            this.buttonX.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.buttonX.Location = new System.Drawing.Point(13, 127);
            this.buttonX.Name = "buttonX";
            this.buttonX.Size = new System.Drawing.Size(153, 33);
            this.buttonX.TabIndex = 2;
            this.buttonX.Text = "Относительно X";
            this.buttonX.UseVisualStyleBackColor = false;
            this.buttonX.Click += new System.EventHandler(this.buttonX_Click);
            // 
            // buttonY
            // 
            this.buttonY.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.buttonY.Location = new System.Drawing.Point(13, 167);
            this.buttonY.Name = "buttonY";
            this.buttonY.Size = new System.Drawing.Size(153, 33);
            this.buttonY.TabIndex = 3;
            this.buttonY.Text = "Относительно Y";
            this.buttonY.UseVisualStyleBackColor = false;
            this.buttonY.Click += new System.EventHandler(this.buttonY_Click);
            // 
            // buttonZ
            // 
            this.buttonZ.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.buttonZ.Location = new System.Drawing.Point(13, 207);
            this.buttonZ.Name = "buttonZ";
            this.buttonZ.Size = new System.Drawing.Size(153, 33);
            this.buttonZ.TabIndex = 4;
            this.buttonZ.Text = "Относительно Z";
            this.buttonZ.UseVisualStyleBackColor = false;
            this.buttonZ.Click += new System.EventHandler(this.buttonZ_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.buttonZ);
            this.Controls.Add(this.buttonY);
            this.Controls.Add(this.buttonX);
            this.Controls.Add(this.textBoxRotations);
            this.Controls.Add(this.buttonAddPoints);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonAddPoints;
        private System.Windows.Forms.TextBox textBoxRotations;
        private System.Windows.Forms.Button buttonX;
        private System.Windows.Forms.Button buttonY;
        private System.Windows.Forms.Button buttonZ;
    }
}

