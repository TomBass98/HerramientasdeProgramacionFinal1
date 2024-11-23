namespace prt
{
    partial class Historial
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
            System.Windows.Forms.ColumnHeader Isbn;
            this.listView1 = new System.Windows.Forms.ListView();
            this.Nombre = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Fecha_creacion = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Cantidad_registrada = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Cantidad_actual = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.button2 = new System.Windows.Forms.Button();
            Isbn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // Isbn
            // 
            Isbn.Text = "ISBN";
            Isbn.Width = 100;
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            Isbn,
            this.Nombre,
            this.Fecha_creacion,
            this.Cantidad_registrada,
            this.Cantidad_actual});
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(8, 12);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(785, 426);
            this.listView1.TabIndex = 18;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // Nombre
            // 
            this.Nombre.Text = "Nombre";
            this.Nombre.Width = 100;
            // 
            // Fecha_creacion
            // 
            this.Fecha_creacion.Text = "Fecha Creación";
            this.Fecha_creacion.Width = 118;
            // 
            // Cantidad_registrada
            // 
            this.Cantidad_registrada.Text = "Cantidad Registrada";
            this.Cantidad_registrada.Width = 140;
            // 
            // Cantidad_actual
            // 
            this.Cantidad_actual.Text = "Cantidad Actual";
            this.Cantidad_actual.Width = 155;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(818, 349);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(81, 89);
            this.button2.TabIndex = 19;
            this.button2.Text = "Menu principal";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Historial
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(903, 444);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.listView1);
            this.Name = "Historial";
            this.Text = "Historial";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader Nombre;
        private System.Windows.Forms.ColumnHeader Fecha_creacion;
        private System.Windows.Forms.ColumnHeader Cantidad_registrada;
        private System.Windows.Forms.ColumnHeader Cantidad_actual;
        private System.Windows.Forms.Button button2;
    }
}