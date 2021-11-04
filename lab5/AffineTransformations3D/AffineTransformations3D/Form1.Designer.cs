
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
            this.SuspendLayout();
            // 
            // buttonAxonometric
            // 
            this.buttonAxonometric.Location = new System.Drawing.Point(12, 75);
            this.buttonAxonometric.Name = "buttonAxonometric";
            this.buttonAxonometric.Size = new System.Drawing.Size(134, 52);
            this.buttonAxonometric.TabIndex = 0;
            this.buttonAxonometric.Text = "Аксонометрическая проекция";
            this.buttonAxonometric.UseVisualStyleBackColor = true;
            this.buttonAxonometric.Click += new System.EventHandler(this.buttonHexahedron_Click);
            // 
            // buttonPerspective
            // 
            this.buttonPerspective.Location = new System.Drawing.Point(12, 133);
            this.buttonPerspective.Name = "buttonPerspective";
            this.buttonPerspective.Size = new System.Drawing.Size(134, 52);
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
            "Октаэдр"});
            this.comboBox1.Location = new System.Drawing.Point(13, 13);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 23);
            this.comboBox1.TabIndex = 2;
            this.comboBox1.Text = "Многогранник";
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.buttonPerspective);
            this.Controls.Add(this.buttonAxonometric);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonAxonometric;
        private System.Windows.Forms.Button buttonPerspective;
        private System.Windows.Forms.ComboBox comboBox1;
    }
}

