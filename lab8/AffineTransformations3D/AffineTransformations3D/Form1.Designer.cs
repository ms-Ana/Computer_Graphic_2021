
namespace AffineTransformations3D
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
            this.buttonAxonometric = new System.Windows.Forms.Button();
            this.buttonPerspective = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.textBoxDx = new System.Windows.Forms.TextBox();
            this.textBoxDy = new System.Windows.Forms.TextBox();
            this.textBoxDz = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxMx = new System.Windows.Forms.TextBox();
            this.textBoxMy = new System.Windows.Forms.TextBox();
            this.textBoxMz = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.buttonTranslation = new System.Windows.Forms.Button();
            this.buttonScale = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.buttonReflectXy = new System.Windows.Forms.Button();
            this.buttonReflectXz = new System.Windows.Forms.Button();
            this.buttonReflectYz = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.buttonRotateX = new System.Windows.Forms.Button();
            this.buttonRotateY = new System.Windows.Forms.Button();
            this.buttonRotateZ = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.textBoxLine = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.buttonRotateLine = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.buttonX = new System.Windows.Forms.Button();
            this.buttonY = new System.Windows.Forms.Button();
            this.buttonZ = new System.Windows.Forms.Button();
            this.textBoxRotations = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.checkBoxRemove = new System.Windows.Forms.CheckBox();
            this.buttonCamera = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonAxonometric
            // 
            this.buttonAxonometric.Location = new System.Drawing.Point(12, 42);
            this.buttonAxonometric.Name = "buttonAxonometric";
            this.buttonAxonometric.Size = new System.Drawing.Size(134, 41);
            this.buttonAxonometric.TabIndex = 0;
            this.buttonAxonometric.Text = "Аксонометрическая проекция";
            this.buttonAxonometric.UseVisualStyleBackColor = true;
            this.buttonAxonometric.Click += new System.EventHandler(this.buttonHexahedron_Click);
            // 
            // buttonPerspective
            // 
            this.buttonPerspective.Location = new System.Drawing.Point(12, 89);
            this.buttonPerspective.Name = "buttonPerspective";
            this.buttonPerspective.Size = new System.Drawing.Size(134, 39);
            this.buttonPerspective.TabIndex = 1;
            this.buttonPerspective.Text = "Перспективная проекция";
            this.buttonPerspective.UseVisualStyleBackColor = true;
            this.buttonPerspective.Click += new System.EventHandler(this.buttonPerspective_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Гексаэдр",
            "Тетраэдр",
            "Октаэдр",
            "Икосаэдр",
            "Додекаэдр",
            "График: sin(x)*cos(y)",
            "График: x^2 + y^2",
            "Фигура вращения"});
            this.comboBox1.Location = new System.Drawing.Point(13, 13);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 23);
            this.comboBox1.TabIndex = 2;
            this.comboBox1.Text = "Многогранник";
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // textBoxDx
            // 
            this.textBoxDx.Location = new System.Drawing.Point(12, 173);
            this.textBoxDx.Name = "textBoxDx";
            this.textBoxDx.Size = new System.Drawing.Size(60, 23);
            this.textBoxDx.TabIndex = 3;
            // 
            // textBoxDy
            // 
            this.textBoxDy.Location = new System.Drawing.Point(78, 173);
            this.textBoxDy.Name = "textBoxDy";
            this.textBoxDy.Size = new System.Drawing.Size(60, 23);
            this.textBoxDy.TabIndex = 4;
            // 
            // textBoxDz
            // 
            this.textBoxDz.Location = new System.Drawing.Point(144, 173);
            this.textBoxDz.Name = "textBoxDz";
            this.textBoxDz.Size = new System.Drawing.Size(60, 23);
            this.textBoxDz.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 139);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 15);
            this.label1.TabIndex = 6;
            this.label1.Text = "Смещение";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 155);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(20, 15);
            this.label2.TabIndex = 7;
            this.label2.Text = "dx";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(78, 155);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(20, 15);
            this.label3.TabIndex = 8;
            this.label3.Text = "dy";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(145, 155);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(19, 15);
            this.label4.TabIndex = 9;
            this.label4.Text = "dz";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 248);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(112, 15);
            this.label5.TabIndex = 10;
            this.label5.Text = "Масштабирование";
            // 
            // textBoxMx
            // 
            this.textBoxMx.Location = new System.Drawing.Point(12, 281);
            this.textBoxMx.Name = "textBoxMx";
            this.textBoxMx.Size = new System.Drawing.Size(60, 23);
            this.textBoxMx.TabIndex = 11;
            // 
            // textBoxMy
            // 
            this.textBoxMy.Location = new System.Drawing.Point(78, 281);
            this.textBoxMy.Name = "textBoxMy";
            this.textBoxMy.Size = new System.Drawing.Size(60, 23);
            this.textBoxMy.TabIndex = 12;
            // 
            // textBoxMz
            // 
            this.textBoxMz.Location = new System.Drawing.Point(144, 281);
            this.textBoxMz.Name = "textBoxMz";
            this.textBoxMz.Size = new System.Drawing.Size(60, 23);
            this.textBoxMz.TabIndex = 13;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 263);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(24, 15);
            this.label6.TabIndex = 14;
            this.label6.Text = "mx";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(78, 263);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(24, 15);
            this.label7.TabIndex = 15;
            this.label7.Text = "my";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(145, 263);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(23, 15);
            this.label8.TabIndex = 16;
            this.label8.Text = "mz";
            // 
            // buttonTranslation
            // 
            this.buttonTranslation.Location = new System.Drawing.Point(12, 203);
            this.buttonTranslation.Name = "buttonTranslation";
            this.buttonTranslation.Size = new System.Drawing.Size(192, 23);
            this.buttonTranslation.TabIndex = 17;
            this.buttonTranslation.Text = "Применить";
            this.buttonTranslation.UseVisualStyleBackColor = true;
            this.buttonTranslation.Click += new System.EventHandler(this.buttonTranslation_Click);
            // 
            // buttonScale
            // 
            this.buttonScale.Location = new System.Drawing.Point(12, 311);
            this.buttonScale.Name = "buttonScale";
            this.buttonScale.Size = new System.Drawing.Size(192, 23);
            this.buttonScale.TabIndex = 18;
            this.buttonScale.Text = "Применить";
            this.buttonScale.UseVisualStyleBackColor = true;
            this.buttonScale.Click += new System.EventHandler(this.buttonScale_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(13, 356);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(69, 15);
            this.label9.TabIndex = 19;
            this.label9.Text = "Отражение";
            // 
            // buttonReflectXy
            // 
            this.buttonReflectXy.Location = new System.Drawing.Point(13, 375);
            this.buttonReflectXy.Name = "buttonReflectXy";
            this.buttonReflectXy.Size = new System.Drawing.Size(191, 23);
            this.buttonReflectXy.TabIndex = 20;
            this.buttonReflectXy.Text = "Относительно xy";
            this.buttonReflectXy.UseVisualStyleBackColor = true;
            this.buttonReflectXy.Click += new System.EventHandler(this.buttonReflectXy_Click);
            // 
            // buttonReflectXz
            // 
            this.buttonReflectXz.Location = new System.Drawing.Point(12, 405);
            this.buttonReflectXz.Name = "buttonReflectXz";
            this.buttonReflectXz.Size = new System.Drawing.Size(192, 23);
            this.buttonReflectXz.TabIndex = 21;
            this.buttonReflectXz.Text = "Относительно xz";
            this.buttonReflectXz.UseVisualStyleBackColor = true;
            this.buttonReflectXz.Click += new System.EventHandler(this.buttonReflectXz_Click);
            // 
            // buttonReflectYz
            // 
            this.buttonReflectYz.Location = new System.Drawing.Point(12, 435);
            this.buttonReflectYz.Name = "buttonReflectYz";
            this.buttonReflectYz.Size = new System.Drawing.Size(192, 23);
            this.buttonReflectYz.TabIndex = 22;
            this.buttonReflectYz.Text = "Относительно yz";
            this.buttonReflectYz.UseVisualStyleBackColor = true;
            this.buttonReflectYz.Click += new System.EventHandler(this.buttonReflectYz_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(12, 480);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(185, 15);
            this.label10.TabIndex = 23;
            this.label10.Text = "Вращение относительно центра";
            // 
            // buttonRotateX
            // 
            this.buttonRotateX.BackColor = System.Drawing.SystemColors.ControlLight;
            this.buttonRotateX.Location = new System.Drawing.Point(13, 498);
            this.buttonRotateX.Name = "buttonRotateX";
            this.buttonRotateX.Size = new System.Drawing.Size(191, 23);
            this.buttonRotateX.TabIndex = 24;
            this.buttonRotateX.Text = "Вращать параллельно X";
            this.buttonRotateX.UseVisualStyleBackColor = false;
            this.buttonRotateX.Click += new System.EventHandler(this.buttonRotateX_Click);
            // 
            // buttonRotateY
            // 
            this.buttonRotateY.Location = new System.Drawing.Point(13, 528);
            this.buttonRotateY.Name = "buttonRotateY";
            this.buttonRotateY.Size = new System.Drawing.Size(191, 23);
            this.buttonRotateY.TabIndex = 25;
            this.buttonRotateY.Text = "Вращать параллельно Y";
            this.buttonRotateY.UseVisualStyleBackColor = true;
            this.buttonRotateY.Click += new System.EventHandler(this.buttonRotateY_Click);
            // 
            // buttonRotateZ
            // 
            this.buttonRotateZ.Location = new System.Drawing.Point(13, 558);
            this.buttonRotateZ.Name = "buttonRotateZ";
            this.buttonRotateZ.Size = new System.Drawing.Size(191, 23);
            this.buttonRotateZ.TabIndex = 26;
            this.buttonRotateZ.Text = "Вращать параллельно Z";
            this.buttonRotateZ.UseVisualStyleBackColor = true;
            this.buttonRotateZ.Click += new System.EventHandler(this.buttonRotateZ_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(12, 603);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(142, 15);
            this.label11.TabIndex = 27;
            this.label11.Text = "Вращение вокруг линии";
            // 
            // textBoxLine
            // 
            this.textBoxLine.Location = new System.Drawing.Point(12, 640);
            this.textBoxLine.Name = "textBoxLine";
            this.textBoxLine.Size = new System.Drawing.Size(191, 23);
            this.textBoxLine.TabIndex = 28;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(13, 622);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(92, 15);
            this.label12.TabIndex = 29;
            this.label12.Text = "x1;y1;z1;x2;y2;z2";
            // 
            // buttonRotateLine
            // 
            this.buttonRotateLine.Location = new System.Drawing.Point(11, 669);
            this.buttonRotateLine.Name = "buttonRotateLine";
            this.buttonRotateLine.Size = new System.Drawing.Size(192, 23);
            this.buttonRotateLine.TabIndex = 30;
            this.buttonRotateLine.Text = "Вращать вокруг линии";
            this.buttonRotateLine.UseVisualStyleBackColor = true;
            this.buttonRotateLine.Click += new System.EventHandler(this.buttonRotateLine_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(164, 13);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(152, 23);
            this.button1.TabIndex = 31;
            this.button1.Text = "Задать образующую";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // buttonX
            // 
            this.buttonX.Location = new System.Drawing.Point(164, 73);
            this.buttonX.Name = "buttonX";
            this.buttonX.Size = new System.Drawing.Size(152, 23);
            this.buttonX.TabIndex = 32;
            this.buttonX.Text = "Относительно X";
            this.buttonX.UseVisualStyleBackColor = true;
            this.buttonX.Click += new System.EventHandler(this.buttonX_Click);
            // 
            // buttonY
            // 
            this.buttonY.Location = new System.Drawing.Point(164, 102);
            this.buttonY.Name = "buttonY";
            this.buttonY.Size = new System.Drawing.Size(152, 23);
            this.buttonY.TabIndex = 33;
            this.buttonY.Text = "Относительно Y";
            this.buttonY.UseVisualStyleBackColor = true;
            this.buttonY.Click += new System.EventHandler(this.buttonY_Click);
            // 
            // buttonZ
            // 
            this.buttonZ.Location = new System.Drawing.Point(164, 131);
            this.buttonZ.Name = "buttonZ";
            this.buttonZ.Size = new System.Drawing.Size(152, 23);
            this.buttonZ.TabIndex = 34;
            this.buttonZ.Text = "Относительно Z";
            this.buttonZ.UseVisualStyleBackColor = true;
            this.buttonZ.Click += new System.EventHandler(this.buttonZ_Click);
            // 
            // textBoxRotations
            // 
            this.textBoxRotations.Location = new System.Drawing.Point(164, 43);
            this.textBoxRotations.Name = "textBoxRotations";
            this.textBoxRotations.Size = new System.Drawing.Size(60, 23);
            this.textBoxRotations.TabIndex = 35;
            this.textBoxRotations.TextChanged += new System.EventHandler(this.textBoxRotations_TextChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(231, 43);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(108, 15);
            this.label13.TabIndex = 36;
            this.label13.Text = "Кол-во разбиений";
            // 
            // checkBoxRemove
            // 
            this.checkBoxRemove.AutoSize = true;
            this.checkBoxRemove.Checked = true;
            this.checkBoxRemove.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxRemove.Location = new System.Drawing.Point(11, 720);
            this.checkBoxRemove.Name = "checkBoxRemove";
            this.checkBoxRemove.Size = new System.Drawing.Size(190, 19);
            this.checkBoxRemove.TabIndex = 37;
            this.checkBoxRemove.Text = "Отсечение нелицевых граней";
            this.checkBoxRemove.UseVisualStyleBackColor = true;
            this.checkBoxRemove.CheckedChanged += new System.EventHandler(this.checkBoxRemove_CheckedChanged);
            // 
            // buttonCamera
            // 
            this.buttonCamera.Location = new System.Drawing.Point(9, 745);
            this.buttonCamera.Name = "buttonCamera";
            this.buttonCamera.Size = new System.Drawing.Size(195, 31);
            this.buttonCamera.TabIndex = 43;
            this.buttonCamera.Text = "Камера";
            this.buttonCamera.UseVisualStyleBackColor = true;
            this.buttonCamera.Click += new System.EventHandler(this.buttonCamera_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 804);
            this.Controls.Add(this.buttonCamera);
            this.Controls.Add(this.checkBoxRemove);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.textBoxRotations);
            this.Controls.Add(this.buttonZ);
            this.Controls.Add(this.buttonY);
            this.Controls.Add(this.buttonX);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.buttonRotateLine);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.textBoxLine);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.buttonRotateZ);
            this.Controls.Add(this.buttonRotateY);
            this.Controls.Add(this.buttonRotateX);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.buttonReflectYz);
            this.Controls.Add(this.buttonReflectXz);
            this.Controls.Add(this.buttonReflectXy);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.buttonScale);
            this.Controls.Add(this.buttonTranslation);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textBoxMz);
            this.Controls.Add(this.textBoxMy);
            this.Controls.Add(this.textBoxMx);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxDz);
            this.Controls.Add(this.textBoxDy);
            this.Controls.Add(this.textBoxDx);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.buttonPerspective);
            this.Controls.Add(this.buttonAxonometric);
            this.KeyPreview = true;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonAxonometric;
        private System.Windows.Forms.Button buttonPerspective;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.TextBox textBoxDx;
        private System.Windows.Forms.TextBox textBoxDy;
        private System.Windows.Forms.TextBox textBoxDz;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxMx;
        private System.Windows.Forms.TextBox textBoxMy;
        private System.Windows.Forms.TextBox textBoxMz;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button buttonTranslation;
        private System.Windows.Forms.Button buttonScale;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button buttonReflectXy;
        private System.Windows.Forms.Button buttonReflectXz;
        private System.Windows.Forms.Button buttonReflectYz;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button buttonRotateX;
        private System.Windows.Forms.Button buttonRotateY;
        private System.Windows.Forms.Button buttonRotateZ;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox textBoxLine;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button buttonRotateLine;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button buttonX;
        private System.Windows.Forms.Button buttonY;
        private System.Windows.Forms.Button buttonZ;
        private System.Windows.Forms.TextBox textBoxRotations;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.CheckBox checkBoxRemove;
        private System.Windows.Forms.Button buttonCamera;
    }
}

