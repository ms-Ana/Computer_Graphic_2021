
namespace AffineTransformations
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
            this.buttonCurrent = new System.Windows.Forms.Button();
            this.buttonClear = new System.Windows.Forms.Button();
            this.comboBoxtransformation = new System.Windows.Forms.ComboBox();
            this.textBoxDx = new System.Windows.Forms.TextBox();
            this.textBoxDy = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonCurrent
            // 
            this.buttonCurrent.Location = new System.Drawing.Point(13, 13);
            this.buttonCurrent.Name = "buttonCurrent";
            this.buttonCurrent.Size = new System.Drawing.Size(202, 38);
            this.buttonCurrent.TabIndex = 0;
            this.buttonCurrent.Text = "Задать текущий примитив";
            this.buttonCurrent.UseVisualStyleBackColor = true;
            this.buttonCurrent.Click += new System.EventHandler(this.buttonCurrent_Click);
            // 
            // buttonClear
            // 
            this.buttonClear.Location = new System.Drawing.Point(13, 64);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(202, 38);
            this.buttonClear.TabIndex = 1;
            this.buttonClear.Text = "Очистить";
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // comboBoxtransformation
            // 
            this.comboBoxtransformation.FormattingEnabled = true;
            this.comboBoxtransformation.Items.AddRange(new object[] {
            "Смещение на dx, dy",
            "Поворот",
            "Масштабирование",
            "Точка пересечения отрезков",
            "Точка относительно ребра",
            "Точка в многоугольнике"});
            this.comboBoxtransformation.Location = new System.Drawing.Point(13, 115);
            this.comboBoxtransformation.Name = "comboBoxtransformation";
            this.comboBoxtransformation.Size = new System.Drawing.Size(202, 23);
            this.comboBoxtransformation.TabIndex = 2;
            this.comboBoxtransformation.Text = "Выберите метод";
            this.comboBoxtransformation.SelectedIndexChanged += new System.EventHandler(this.comboBoxtransformation_SelectedIndexChanged);
            // 
            // textBoxDx
            // 
            this.textBoxDx.Location = new System.Drawing.Point(13, 196);
            this.textBoxDx.Name = "textBoxDx";
            this.textBoxDx.PlaceholderText = "Введите dx";
            this.textBoxDx.Size = new System.Drawing.Size(100, 23);
            this.textBoxDx.TabIndex = 3;
            this.textBoxDx.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // textBoxDy
            // 
            this.textBoxDy.Location = new System.Drawing.Point(13, 226);
            this.textBoxDy.Name = "textBoxDy";
            this.textBoxDy.PlaceholderText = "Введите dy";
            this.textBoxDy.Size = new System.Drawing.Size(100, 23);
            this.textBoxDy.TabIndex = 4;
            this.textBoxDy.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(13, 256);
            this.textBox1.Name = "textBox1";
            this.textBox1.PlaceholderText = "Коэффициент x";
            this.textBox1.Size = new System.Drawing.Size(100, 23);
            this.textBox1.TabIndex = 5;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged_1);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(13, 286);
            this.textBox2.Name = "textBox2";
            this.textBox2.PlaceholderText = "Коэффициент y";
            this.textBox2.Size = new System.Drawing.Size(100, 23);
            this.textBox2.TabIndex = 6;
            this.textBox2.TextChanged += new System.EventHandler(this.textBox2_TextChanged_1);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(241, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 15);
            this.label1.TabIndex = 7;
            this.label1.Text = "label1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.textBoxDy);
            this.Controls.Add(this.textBoxDx);
            this.Controls.Add(this.comboBoxtransformation);
            this.Controls.Add(this.buttonClear);
            this.Controls.Add(this.buttonCurrent);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonCurrent;
        private System.Windows.Forms.Button buttonClear;
        private System.Windows.Forms.ComboBox comboBoxtransformation;
        private System.Windows.Forms.TextBox textBoxDx;
        private System.Windows.Forms.TextBox textBoxDy;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label1;
    }
}

