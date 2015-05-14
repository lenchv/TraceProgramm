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
            this.tsbDrawWire = new System.Windows.Forms.ToolStripButton();
            this.dropDownMode = new System.Windows.Forms.ToolStripDropDownButton();
            this.demonstrationItem = new System.Windows.Forms.ToolStripMenuItem();
            this.workItem = new System.Windows.Forms.ToolStripMenuItem();
            this.workStepItem = new System.Windows.Forms.ToolStripMenuItem();
            this.workAutoItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pbMainGrid = new System.Windows.Forms.PictureBox();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBoxLayers = new System.Windows.Forms.GroupBox();
            this.MenuFile = new System.Windows.Forms.ToolStripDropDownButton();
            this.FileMenuOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.FileMenuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbMainGrid)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuFile,
            this.dropDownMode,
            this.tsbDrawWire,
            this.tsbClearGrid});
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
            // tsbDrawWire
            // 
            this.tsbDrawWire.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbDrawWire.Image = ((System.Drawing.Image)(resources.GetObject("tsbDrawWire.Image")));
            this.tsbDrawWire.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDrawWire.Name = "tsbDrawWire";
            this.tsbDrawWire.Size = new System.Drawing.Size(127, 22);
            this.tsbDrawWire.Text = "Провести проводник";
            // 
            // dropDownMode
            // 
            this.dropDownMode.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.dropDownMode.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.demonstrationItem,
            this.workItem});
            this.dropDownMode.Image = ((System.Drawing.Image)(resources.GetObject("dropDownMode.Image")));
            this.dropDownMode.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.dropDownMode.Name = "dropDownMode";
            this.dropDownMode.Size = new System.Drawing.Size(58, 22);
            this.dropDownMode.Text = "Режим";
            // 
            // demonstrationItem
            // 
            this.demonstrationItem.Checked = true;
            this.demonstrationItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.demonstrationItem.Name = "demonstrationItem";
            this.demonstrationItem.Size = new System.Drawing.Size(186, 22);
            this.demonstrationItem.Tag = "0";
            this.demonstrationItem.Text = "Демонстрационный";
            // 
            // workItem
            // 
            this.workItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.workStepItem,
            this.workAutoItem});
            this.workItem.Name = "workItem";
            this.workItem.Size = new System.Drawing.Size(186, 22);
            this.workItem.Tag = "1";
            this.workItem.Text = "Рабочий";
            // 
            // workStepItem
            // 
            this.workStepItem.Checked = true;
            this.workStepItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.workStepItem.Name = "workStepItem";
            this.workStepItem.Size = new System.Drawing.Size(159, 22);
            this.workStepItem.Tag = "2";
            this.workStepItem.Text = "Пошагово";
            // 
            // workAutoItem
            // 
            this.workAutoItem.Name = "workAutoItem";
            this.workAutoItem.Size = new System.Drawing.Size(159, 22);
            this.workAutoItem.Tag = "3";
            this.workAutoItem.Text = "Автоматически";
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
            // MenuFile
            // 
            this.MenuFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.MenuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileMenuOpen,
            this.FileMenuExit});
            this.MenuFile.Image = ((System.Drawing.Image)(resources.GetObject("MenuFile.Image")));
            this.MenuFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.MenuFile.Name = "MenuFile";
            this.MenuFile.Size = new System.Drawing.Size(49, 22);
            this.MenuFile.Text = "Файл";
            // 
            // FileMenuOpen
            // 
            this.FileMenuOpen.Name = "FileMenuOpen";
            this.FileMenuOpen.Size = new System.Drawing.Size(152, 22);
            this.FileMenuOpen.Text = "Открыть";
            this.FileMenuOpen.Click += new System.EventHandler(this.FileMenuOpen_Click);
            // 
            // FileMenuExit
            // 
            this.FileMenuExit.Name = "FileMenuExit";
            this.FileMenuExit.Size = new System.Drawing.Size(152, 22);
            this.FileMenuExit.Text = "Выход";
            this.FileMenuExit.Click += new System.EventHandler(this.FileMenuExit_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.DefaultExt = "xml";
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.InitialDirectory = ".";
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
        private System.Windows.Forms.ToolStripButton tsbDrawWire;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBoxLayers;
        private System.Windows.Forms.ToolStripDropDownButton dropDownMode;
        private System.Windows.Forms.ToolStripMenuItem demonstrationItem;
        private System.Windows.Forms.ToolStripMenuItem workItem;
        private System.Windows.Forms.ToolStripMenuItem workStepItem;
        private System.Windows.Forms.ToolStripMenuItem workAutoItem;
        private System.Windows.Forms.ToolStripDropDownButton MenuFile;
        private System.Windows.Forms.ToolStripMenuItem FileMenuOpen;
        private System.Windows.Forms.ToolStripMenuItem FileMenuExit;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}

