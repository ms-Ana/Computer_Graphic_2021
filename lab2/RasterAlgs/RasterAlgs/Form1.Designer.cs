
namespace RasterAlgs
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.buttonChooseImage = new System.Windows.Forms.Button();
            this.buttonFillImage = new System.Windows.Forms.Button();
            this.buttonClear = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Fill with Color",
            "Fill with Image",
            "Find border"});
            this.comboBox1.Location = new System.Drawing.Point(12, 54);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 0;
            this.comboBox1.Text = "Choose Alg";
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // buttonChooseImage
            // 
            this.buttonChooseImage.Location = new System.Drawing.Point(12, 12);
            this.buttonChooseImage.Name = "buttonChooseImage";
            this.buttonChooseImage.Size = new System.Drawing.Size(121, 23);
            this.buttonChooseImage.TabIndex = 1;
            this.buttonChooseImage.Text = "Choose image";
            this.buttonChooseImage.UseVisualStyleBackColor = true;
            this.buttonChooseImage.Click += new System.EventHandler(this.buttonChooseImage_Click);
            // 
            // buttonFillImage
            // 
            this.buttonFillImage.Location = new System.Drawing.Point(13, 104);
            this.buttonFillImage.Name = "buttonFillImage";
            this.buttonFillImage.Size = new System.Drawing.Size(120, 23);
            this.buttonFillImage.TabIndex = 2;
            this.buttonFillImage.Text = "Image to fill";
            this.buttonFillImage.UseVisualStyleBackColor = true;
            this.buttonFillImage.Click += new System.EventHandler(this.buttonFillImage_Click);
            // 
            // buttonClear
            // 
            this.buttonClear.Location = new System.Drawing.Point(13, 160);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(120, 23);
            this.buttonClear.TabIndex = 3;
            this.buttonClear.Text = "Clear";
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(13, 232);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(82, 17);
            this.checkBox1.TabIndex = 4;
            this.checkBox1.Text = "Draw slowly";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.buttonClear);
            this.Controls.Add(this.buttonFillImage);
            this.Controls.Add(this.buttonChooseImage);
            this.Controls.Add(this.comboBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button buttonChooseImage;
        private System.Windows.Forms.Button buttonFillImage;
        private System.Windows.Forms.Button buttonClear;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}

