
namespace ColorConverting
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
            this.buttonSelectImage = new System.Windows.Forms.Button();
            this.checkBoxHistorgam = new System.Windows.Forms.CheckBox();
            this.comboBoxOptions = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // buttonSelectImage
            // 
            this.buttonSelectImage.Location = new System.Drawing.Point(10, 20);
            this.buttonSelectImage.Name = "buttonSelectImage";
            this.buttonSelectImage.Size = new System.Drawing.Size(700, 23);
            this.buttonSelectImage.TabIndex = 0;
            this.buttonSelectImage.Text = "Выберите изображение";
            this.buttonSelectImage.UseVisualStyleBackColor = true;
            this.buttonSelectImage.Click += new System.EventHandler(this.buttonSelectImage_Click);
            // 
            // checkBoxHistorgam
            // 
            this.checkBoxHistorgam.AutoSize = true;
            this.checkBoxHistorgam.Checked = true;
            this.checkBoxHistorgam.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxHistorgam.Location = new System.Drawing.Point(980, 23);
            this.checkBoxHistorgam.Name = "checkBoxHistorgam";
            this.checkBoxHistorgam.Size = new System.Drawing.Size(149, 19);
            this.checkBoxHistorgam.TabIndex = 1;
            this.checkBoxHistorgam.Text = "Строить гистограммы";
            this.checkBoxHistorgam.UseVisualStyleBackColor = true;
            this.checkBoxHistorgam.CheckedChanged += new System.EventHandler(this.checkBoxHistorgam_CheckedChanged);
            // 
            // comboBoxOptions
            // 
            this.comboBoxOptions.FormattingEnabled = true;
            this.comboBoxOptions.Items.AddRange(new object[] {
            "Оттенки серого",
            "R, G, B каналы"});
            this.comboBoxOptions.Location = new System.Drawing.Point(750, 20);
            this.comboBoxOptions.Name = "comboBoxOptions";
            this.comboBoxOptions.Size = new System.Drawing.Size(190, 23);
            this.comboBoxOptions.TabIndex = 2;
            this.comboBoxOptions.Text = "Выберите преобразование";
            this.comboBoxOptions.SelectedIndexChanged += new System.EventHandler(this.comboBoxOptions_SelectedIndexChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1484, 561);
            this.Controls.Add(this.comboBoxOptions);
            this.Controls.Add(this.checkBoxHistorgam);
            this.Controls.Add(this.buttonSelectImage);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = " ";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonSelectImage;
        private System.Windows.Forms.CheckBox checkBoxHistorgam;
        private System.Windows.Forms.ComboBox comboBoxOptions;
    }
}

