namespace prt
{
    partial class Prestamos
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
            this.botonPrestamo = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.prestarISBN = new System.Windows.Forms.Label();
            this.campoISBN = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // botonPrestamo
            // 
            this.botonPrestamo.Location = new System.Drawing.Point(205, 293);
            this.botonPrestamo.Name = "botonPrestamo";
            this.botonPrestamo.Size = new System.Drawing.Size(141, 64);
            this.botonPrestamo.TabIndex = 23;
            this.botonPrestamo.Text = "Prestamo";
            this.botonPrestamo.UseVisualStyleBackColor = true;
            this.botonPrestamo.Click += new System.EventHandler(this.botonPrestamo_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(454, 293);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(141, 64);
            this.button3.TabIndex = 22;
            this.button3.Text = "Devolución";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // prestarISBN
            // 
            this.prestarISBN.AutoSize = true;
            this.prestarISBN.Location = new System.Drawing.Point(348, 94);
            this.prestarISBN.Name = "prestarISBN";
            this.prestarISBN.Size = new System.Drawing.Size(100, 16);
            this.prestarISBN.TabIndex = 21;
            this.prestarISBN.Text = "Ingrese el ISBN";
            // 
            // campoISBN
            // 
            this.campoISBN.Location = new System.Drawing.Point(351, 140);
            this.campoISBN.Name = "campoISBN";
            this.campoISBN.Size = new System.Drawing.Size(100, 22);
            this.campoISBN.TabIndex = 20;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(351, 204);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(97, 47);
            this.button2.TabIndex = 19;
            this.button2.Text = "Menú principal";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Prestamos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.botonPrestamo);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.prestarISBN);
            this.Controls.Add(this.campoISBN);
            this.Controls.Add(this.button2);
            this.Name = "Prestamos";
            this.Text = "Prestamos";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button botonPrestamo;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label prestarISBN;
        private System.Windows.Forms.TextBox campoISBN;
        private System.Windows.Forms.Button button2;
    }
}