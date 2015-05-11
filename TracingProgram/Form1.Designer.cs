namespace TracingProgram
{
    partial class Form1
    {
        /// <summary>
        /// Требуется переменная конструктора.
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
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbClearGrid = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.tsbDrawWire = new System.Windows.Forms.ToolStripButton();
            this.pbMainGrid = new System.Windows.Forms.PictureBox();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBoxLayers = new System.Windows.Forms.GroupBox();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbMainGrid)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbClearGrid,
            this.toolStripButton1,
            this.tsbDrawWire});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(669, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip2";
            // 
            // tsbClearGrid
            // 
            this.tsbClearGrid.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbClearGrid.Image = ((System.Drawing.Image)(resources.GetObject("tsbClearGrid.Image")));
            this.tsbClearGrid.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbClearGrid.Name = "tsbClearGrid";
            this.tsbClearGrid.Size = new System.Drawing.Size(63, 22);
            this.tsbClearGrid.Text = "Очистить";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton1.Text = "toolStripButton1";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // tsbDrawWire
            // 
            this.tsbDrawWire.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbDrawWire.Image = ((System.Drawing.Image)(resources.GetObject("tsbDrawWire.Image")));
            this.tsbDrawWire.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDrawWire.Name = "tsbDrawWire";
            this.tsbDrawWire.Size = new System.Drawing.Size(127, 22);
            this.tsbDrawWire.Text = "Провести проводник";
            // 
            // pbMainGrid
            // 
            this.pbMainGrid.Location = new System.Drawing.Point(3, 3);
            this.pbMainGrid.Name = "pbMainGrid";
            this.pbMainGrid.Size = new System.Drawing.Size(534, 311);
            this.pbMainGrid.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbMainGrid.TabIndex = 1;
            this.pbMainGrid.TabStop = false;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "gridCell16x16.png");
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.Controls.Add(this.pbMainGrid);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 28);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(554, 326);
            this.flowLayoutPanel1.TabIndex = 2;
            // 
            // groupBoxLayers
            // 
            this.groupBoxLayers.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxLayers.Location = new System.Drawing.Point(560, 31);
            this.groupBoxLayers.Name = "groupBoxLayers";
            this.groupBoxLayers.Size = new System.Drawing.Size(100, 311);
            this.groupBoxLayers.TabIndex = 3;
            this.groupBoxLayers.TabStop = false;
            this.groupBoxLayers.Text = "Слои";
            // 
            // Form1
            // 
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(669, 354);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.groupBoxLayers);
            this.Controls.Add(this.toolStrip1);
            this.Name = "Form1";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbMainGrid)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.PictureBox pbMainGrid;
        private System.Windows.Forms.ToolStripButton tsbClearGrid;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton tsbDrawWire;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBoxLayers;
    }
}

