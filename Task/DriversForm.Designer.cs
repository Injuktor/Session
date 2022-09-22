namespace Task
{
    partial class DriversForm
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
            this.driversGrid = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.driversGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // driversGrid
            // 
            this.driversGrid.AllowUserToAddRows = false;
            this.driversGrid.AllowUserToDeleteRows = false;
            this.driversGrid.AllowUserToOrderColumns = true;
            this.driversGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.driversGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.driversGrid.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.driversGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.driversGrid.Location = new System.Drawing.Point(12, 56);
            this.driversGrid.Name = "driversGrid";
            this.driversGrid.ReadOnly = true;
            this.driversGrid.Size = new System.Drawing.Size(896, 583);
            this.driversGrid.TabIndex = 0;
            this.driversGrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.driversGrid_CellClick);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(13, 13);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(176, 37);
            this.button1.TabIndex = 1;
            this.button1.Text = "Добавить водителя";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.addDriverButton_Click);
            // 
            // DriversForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(920, 651);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.driversGrid);
            this.Name = "DriversForm";
            this.Text = "Водители";
            ((System.ComponentModel.ISupportInitialize)(this.driversGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView driversGrid;
        private System.Windows.Forms.Button button1;
    }
}