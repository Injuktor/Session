namespace Task
{
    partial class LicencesForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LicencesForm));
            this.licencesGrid = new System.Windows.Forms.DataGridView();
            this.Indicator = new System.Windows.Forms.DataGridViewImageColumn();
            this.addDriverLicenceButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.licencesGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // licencesGrid
            // 
            this.licencesGrid.AllowUserToAddRows = false;
            this.licencesGrid.AllowUserToDeleteRows = false;
            this.licencesGrid.AllowUserToOrderColumns = true;
            this.licencesGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.licencesGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.licencesGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Indicator});
            this.licencesGrid.Location = new System.Drawing.Point(12, 56);
            this.licencesGrid.MultiSelect = false;
            this.licencesGrid.Name = "licencesGrid";
            this.licencesGrid.ReadOnly = true;
            this.licencesGrid.Size = new System.Drawing.Size(896, 583);
            this.licencesGrid.TabIndex = 0;
            this.licencesGrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.licencesGrid_CellClick);
            // 
            // Indicator
            // 
            this.Indicator.DataPropertyName = "System.Drawing.Bitmap";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Red;
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Red;
            dataGridViewCellStyle2.NullValue = ((object)(resources.GetObject("dataGridViewCellStyle2.NullValue")));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Red;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Red;
            this.Indicator.DefaultCellStyle = dataGridViewCellStyle2;
            this.Indicator.HeaderText = "Indicator";
            this.Indicator.Image = global::Task.Properties.Resources.gray_status;
            this.Indicator.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.Indicator.Name = "Indicator";
            this.Indicator.ReadOnly = true;
            // 
            // addDriverLicenceButton
            // 
            this.addDriverLicenceButton.Location = new System.Drawing.Point(12, 12);
            this.addDriverLicenceButton.Name = "addDriverLicenceButton";
            this.addDriverLicenceButton.Size = new System.Drawing.Size(189, 38);
            this.addDriverLicenceButton.TabIndex = 1;
            this.addDriverLicenceButton.Text = "Добавить водительское удостоверение";
            this.addDriverLicenceButton.UseVisualStyleBackColor = true;
            this.addDriverLicenceButton.Click += new System.EventHandler(this.addDriverLicenceButton_Click);
            // 
            // LicencesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(920, 651);
            this.Controls.Add(this.addDriverLicenceButton);
            this.Controls.Add(this.licencesGrid);
            this.Name = "LicencesForm";
            this.Text = "DriverLicences";
            ((System.ComponentModel.ISupportInitialize)(this.licencesGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView licencesGrid;
        private System.Windows.Forms.Button addDriverLicenceButton;
        private System.Windows.Forms.DataGridViewImageColumn Indicator;
    }
}