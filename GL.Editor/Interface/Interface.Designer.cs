namespace GL.Editor.Interface
{
    using System.ComponentModel;
    using System.Windows.Forms;

    partial class Interface
    {
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Interface));
            this.Button_Load = new System.Windows.Forms.Button();
            this.Picture = new System.Windows.Forms.PictureBox();
            this.Picture_Box = new System.Windows.Forms.Panel();
            this.Menu1 = new System.Windows.Forms.MenuStrip();
            this.Menu_File = new System.Windows.Forms.ToolStripMenuItem();
            this.Tab_File_Open = new System.Windows.Forms.ToolStripMenuItem();
            this.Tab_File_Save = new System.Windows.Forms.ToolStripMenuItem();
            this.Tab_File_Close = new System.Windows.Forms.ToolStripMenuItem();
            this.Menu_Shapes = new System.Windows.Forms.ToolStripMenuItem();
            this.Menu_Shapes_Add = new System.Windows.Forms.ToolStripMenuItem();
            this.Menu_Shapes_Clone = new System.Windows.Forms.ToolStripMenuItem();
            this.Menu_Shapes_Export = new System.Windows.Forms.ToolStripMenuItem();
            this.Menu_Shapes_Import = new System.Windows.Forms.ToolStripMenuItem();
            this.Menu_Shapes_Rename = new System.Windows.Forms.ToolStripMenuItem();
            this.Menu_Shapes_Remove = new System.Windows.Forms.ToolStripMenuItem();
            this.Menu_Exports = new System.Windows.Forms.ToolStripMenuItem();
            this.Menu_Exports_Add = new System.Windows.Forms.ToolStripMenuItem();
            this.Menu_Exports_Clone = new System.Windows.Forms.ToolStripMenuItem();
            this.Menu_Exports_Export = new System.Windows.Forms.ToolStripMenuItem();
            this.Menu_Exports_Import = new System.Windows.Forms.ToolStripMenuItem();
            this.Menu_Exports_Rename = new System.Windows.Forms.ToolStripMenuItem();
            this.Menu_Exports_Remove = new System.Windows.Forms.ToolStripMenuItem();
            this.Menu_Textures = new System.Windows.Forms.ToolStripMenuItem();
            this.Menu_Textures_Change = new System.Windows.Forms.ToolStripMenuItem();
            this.Menu_Textures_Export = new System.Windows.Forms.ToolStripMenuItem();
            this.Menu_About = new System.Windows.Forms.ToolStripMenuItem();
            this.Menu_About_Us = new System.Windows.Forms.ToolStripMenuItem();
            this.Menu_About_Licence = new System.Windows.Forms.ToolStripMenuItem();
            this.Status_Bar = new System.Windows.Forms.StatusStrip();
            this.Status_Bar_Progress = new System.Windows.Forms.ToolStripStatusLabel();
            this.Status_Bar_Progress_Bar = new System.Windows.Forms.ToolStripProgressBar();
            this.Status_Bar_File = new System.Windows.Forms.ToolStripStatusLabel();
            this.Status_Bar_File_Name = new System.Windows.Forms.ToolStripStatusLabel();
            this.List_Exports = new System.Windows.Forms.TreeView();
            this.Console_Box = new System.Windows.Forms.TextBox();
            this.Menu_Fonts_Add = new System.Windows.Forms.ToolStripMenuItem();
            this.addToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.Menu_Fonts_Remove = new System.Windows.Forms.ToolStripMenuItem();
            this.Menu_Fonts_Rename = new System.Windows.Forms.ToolStripMenuItem();
            this.Menu_Spirites = new System.Windows.Forms.ToolStripMenuItem();
            this.Menu_Spirites_Add = new System.Windows.Forms.ToolStripMenuItem();
            this.Menu_Spirites_Remove = new System.Windows.Forms.ToolStripMenuItem();
            this.Menu_Spirites_Clone = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.Picture)).BeginInit();
            this.Picture_Box.SuspendLayout();
            this.Menu1.SuspendLayout();
            this.Status_Bar.SuspendLayout();
            this.SuspendLayout();
            // 
            // Button_Load
            // 
            this.Button_Load.Location = new System.Drawing.Point(0, 0);
            this.Button_Load.Name = "Button_Load";
            this.Button_Load.Size = new System.Drawing.Size(75, 23);
            this.Button_Load.TabIndex = 8;
            // 
            // Picture
            // 
            this.Picture.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.Picture.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.Picture.Location = new System.Drawing.Point(3, 1);
            this.Picture.Margin = new System.Windows.Forms.Padding(0);
            this.Picture.Name = "Picture";
            this.Picture.Size = new System.Drawing.Size(10, 10);
            this.Picture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.Picture.TabIndex = 0;
            this.Picture.TabStop = false;
            // 
            // Picture_Box
            // 
            this.Picture_Box.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Picture_Box.AutoScroll = true;
            this.Picture_Box.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Picture_Box.BackColor = System.Drawing.Color.LightGray;
            this.Picture_Box.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.Picture_Box.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Picture_Box.Controls.Add(this.Picture);
            this.Picture_Box.Location = new System.Drawing.Point(223, 30);
            this.Picture_Box.Margin = new System.Windows.Forms.Padding(0);
            this.Picture_Box.Name = "Picture_Box";
            this.Picture_Box.Size = new System.Drawing.Size(774, 544);
            this.Picture_Box.TabIndex = 5;
            // 
            // Menu1
            // 
            this.Menu1.AllowMerge = false;
            this.Menu1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Menu1.AutoSize = false;
            this.Menu1.BackColor = System.Drawing.SystemColors.Menu;
            this.Menu1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.Menu1.Dock = System.Windows.Forms.DockStyle.None;
            this.Menu1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Menu_File,
            this.Menu_Shapes,
            this.Menu_Exports,
            this.Menu_Textures,
            this.Menu_Spirites,
            this.Menu_Fonts_Add,
            this.Menu_About});
            this.Menu1.Location = new System.Drawing.Point(0, 0);
            this.Menu1.Name = "Menu1";
            this.Menu1.Size = new System.Drawing.Size(1009, 24);
            this.Menu1.TabIndex = 7;
            this.Menu1.Text = "File";
            // 
            // Menu_File
            // 
            this.Menu_File.AutoSize = false;
            this.Menu_File.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Tab_File_Open,
            this.Tab_File_Save,
            this.Tab_File_Close});
            this.Menu_File.Name = "Menu_File";
            this.Menu_File.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.Menu_File.Size = new System.Drawing.Size(50, 20);
            this.Menu_File.Text = "File";
            // 
            // Tab_File_Open
            // 
            this.Tab_File_Open.Name = "Tab_File_Open";
            this.Tab_File_Open.Size = new System.Drawing.Size(103, 22);
            this.Tab_File_Open.Text = "Open";
            this.Tab_File_Open.Click += new System.EventHandler(this.Open_OnClick);
            // 
            // Tab_File_Save
            // 
            this.Tab_File_Save.Name = "Tab_File_Save";
            this.Tab_File_Save.Size = new System.Drawing.Size(103, 22);
            this.Tab_File_Save.Text = "Save";
            // 
            // Tab_File_Close
            // 
            this.Tab_File_Close.Name = "Tab_File_Close";
            this.Tab_File_Close.Size = new System.Drawing.Size(103, 22);
            this.Tab_File_Close.Text = "Close";
            this.Tab_File_Close.Click += new System.EventHandler(this.Close_OnClick);
            // 
            // Menu_Shapes
            // 
            this.Menu_Shapes.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Menu_Shapes_Add,
            this.Menu_Shapes_Clone,
            this.Menu_Shapes_Export,
            this.Menu_Shapes_Import,
            this.Menu_Shapes_Rename,
            this.Menu_Shapes_Remove});
            this.Menu_Shapes.Name = "Menu_Shapes";
            this.Menu_Shapes.Size = new System.Drawing.Size(56, 20);
            this.Menu_Shapes.Text = "Shapes";
            // 
            // Menu_Shapes_Add
            // 
            this.Menu_Shapes_Add.Name = "Menu_Shapes_Add";
            this.Menu_Shapes_Add.Size = new System.Drawing.Size(152, 22);
            this.Menu_Shapes_Add.Text = "Add";
            // 
            // Menu_Shapes_Clone
            // 
            this.Menu_Shapes_Clone.Name = "Menu_Shapes_Clone";
            this.Menu_Shapes_Clone.Size = new System.Drawing.Size(152, 22);
            this.Menu_Shapes_Clone.Text = "Clone";
            // 
            // Menu_Shapes_Export
            // 
            this.Menu_Shapes_Export.Name = "Menu_Shapes_Export";
            this.Menu_Shapes_Export.Size = new System.Drawing.Size(152, 22);
            this.Menu_Shapes_Export.Text = "Export";
            this.Menu_Shapes_Export.Click += this.Menu_Shapes_Export_OnClick;
            // 
            // Menu_Shapes_Import
            // 
            this.Menu_Shapes_Import.Name = "Menu_Shapes_Import";
            this.Menu_Shapes_Import.Size = new System.Drawing.Size(152, 22);
            this.Menu_Shapes_Import.Text = "Import";
            // 
            // Menu_Shapes_Rename
            // 
            this.Menu_Shapes_Rename.Name = "Menu_Shapes_Rename";
            this.Menu_Shapes_Rename.Size = new System.Drawing.Size(152, 22);
            this.Menu_Shapes_Rename.Text = "Rename";
            // this.Menu_Shapes_Rename.Click += this.Menu_Shapes_Rename_OnClick;
            // 
            // Menu_Shapes_Remove
            // 
            this.Menu_Shapes_Remove.Name = "Menu_Shapes_Remove";
            this.Menu_Shapes_Remove.Size = new System.Drawing.Size(152, 22);
            this.Menu_Shapes_Remove.Text = "Remove";
            // 
            // Menu_Exports
            // 
            this.Menu_Exports.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Menu_Exports_Add,
            this.Menu_Exports_Clone,
            this.Menu_Exports_Export,
            this.Menu_Exports_Import,
            this.Menu_Exports_Rename,
            this.Menu_Exports_Remove});
            this.Menu_Exports.Name = "Menu_Exports";
            this.Menu_Exports.Size = new System.Drawing.Size(57, 20);
            this.Menu_Exports.Text = "Exports";
            // 
            // Menu_Exports_Add
            // 
            this.Menu_Exports_Add.Name = "Menu_Exports_Add";
            this.Menu_Exports_Add.Size = new System.Drawing.Size(152, 22);
            this.Menu_Exports_Add.Text = "Add";
            // 
            // Menu_Exports_Clone
            // 
            this.Menu_Exports_Clone.Name = "Menu_Exports_Clone";
            this.Menu_Exports_Clone.Size = new System.Drawing.Size(152, 22);
            this.Menu_Exports_Clone.Text = "Clone";
            // 
            // Menu_Exports_Export
            // 
            this.Menu_Exports_Export.Name = "Menu_Exports_Export";
            this.Menu_Exports_Export.Size = new System.Drawing.Size(152, 22);
            this.Menu_Exports_Export.Text = "Export";
            // 
            // Menu_Exports_Import
            // 
            this.Menu_Exports_Import.Name = "Menu_Exports_Import";
            this.Menu_Exports_Import.Size = new System.Drawing.Size(152, 22);
            this.Menu_Exports_Import.Text = "Import";
            // 
            // Menu_Exports_Rename
            // 
            this.Menu_Exports_Rename.Name = "Menu_Exports_Rename";
            this.Menu_Exports_Rename.Size = new System.Drawing.Size(152, 22);
            this.Menu_Exports_Rename.Text = "Rename";
            // 
            // Menu_Exports_Remove
            // 
            this.Menu_Exports_Remove.Name = "Menu_Exports_Remove";
            this.Menu_Exports_Remove.Size = new System.Drawing.Size(152, 22);
            this.Menu_Exports_Remove.Text = "Remove";
            // 
            // Menu_Textures
            // 
            this.Menu_Textures.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Menu_Textures_Change,
            this.Menu_Textures_Export});
            this.Menu_Textures.Name = "Menu_Textures";
            this.Menu_Textures.Size = new System.Drawing.Size(62, 20);
            this.Menu_Textures.Text = "Textures";
            // 
            // Menu_Textures_Change
            // 
            this.Menu_Textures_Change.Name = "Menu_Textures_Change";
            this.Menu_Textures_Change.Size = new System.Drawing.Size(152, 22);
            this.Menu_Textures_Change.Text = "Change";
            this.Menu_Textures_Change.Click += this.Menu_Textures_Change_OnClick;
            // 
            // Menu_Textures_Export
            // 
            this.Menu_Textures_Export.Name = "Menu_Textures_Export";
            this.Menu_Textures_Export.Size = new System.Drawing.Size(152, 22);
            this.Menu_Textures_Export.Text = "Export";
            this.Menu_Textures_Export.Click += this.Menu_Textures_Export_OnClick;
            // 
            // Menu_About
            // 
            this.Menu_About.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Menu_About_Us,
            this.Menu_About_Licence});
            this.Menu_About.Name = "Menu_About";
            this.Menu_About.Size = new System.Drawing.Size(52, 20);
            this.Menu_About.Text = "About";
            // 
            // Menu_About_Us
            // 
            this.Menu_About_Us.Name = "Menu_About_Us";
            this.Menu_About_Us.Size = new System.Drawing.Size(152, 22);
            this.Menu_About_Us.Text = "About Us";
            // 
            // Menu_About_Licence
            // 
            this.Menu_About_Licence.Name = "Menu_About_Licence";
            this.Menu_About_Licence.Size = new System.Drawing.Size(152, 22);
            this.Menu_About_Licence.Text = "Licence";
            // 
            // Status_Bar
            // 
            this.Status_Bar.AllowMerge = false;
            this.Status_Bar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Status_Bar.AutoSize = false;
            this.Status_Bar.Dock = System.Windows.Forms.DockStyle.None;
            this.Status_Bar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Status_Bar_Progress,
            this.Status_Bar_Progress_Bar,
            this.Status_Bar_File,
            this.Status_Bar_File_Name});
            this.Status_Bar.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.Status_Bar.Location = new System.Drawing.Point(0, 577);
            this.Status_Bar.Name = "Status_Bar";
            this.Status_Bar.Size = new System.Drawing.Size(1009, 22);
            this.Status_Bar.Stretch = false;
            this.Status_Bar.TabIndex = 9;
            this.Status_Bar.Text = "Status_Bar";
            // 
            // Status_Bar_Progress
            // 
            this.Status_Bar_Progress.Name = "Status_Bar_Progress";
            this.Status_Bar_Progress.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.Status_Bar_Progress.Size = new System.Drawing.Size(78, 17);
            this.Status_Bar_Progress.Text = "Progress :";
            // 
            // Status_Bar_Progress_Bar
            // 
            this.Status_Bar_Progress_Bar.Name = "Status_Bar_Progress_Bar";
            this.Status_Bar_Progress_Bar.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.Status_Bar_Progress_Bar.Size = new System.Drawing.Size(110, 16);
            this.Status_Bar_Progress_Bar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            // 
            // Status_Bar_File
            // 
            this.Status_Bar_File.Name = "Status_Bar_File";
            this.Status_Bar_File.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.Status_Bar_File.Size = new System.Drawing.Size(51, 17);
            this.Status_Bar_File.Text = "File :";
            // 
            // Status_Bar_File_Name
            // 
            this.Status_Bar_File_Name.Name = "Status_Bar_File_Name";
            this.Status_Bar_File_Name.Size = new System.Drawing.Size(42, 17);
            this.Status_Bar_File_Name.Text = "-------";
            // 
            // List_Exports
            // 
            this.List_Exports.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.List_Exports.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.List_Exports.Location = new System.Drawing.Point(12, 30);
            this.List_Exports.MinimumSize = new System.Drawing.Size(203, 200);
            this.List_Exports.Name = "List_Exports";
            this.List_Exports.ShowNodeToolTips = true;
            this.List_Exports.Size = new System.Drawing.Size(203, 544);
            this.List_Exports.TabIndex = 10;
            this.List_Exports.NodeMouseDoubleClick += this.List_Exports_NodeDoubleClick;
            // 
            // Console_Box
            // 
            this.Console_Box.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Console_Box.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Console_Box.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.Console_Box.Enabled = false;
            this.Console_Box.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Italic);
            this.Console_Box.Location = new System.Drawing.Point(12, 398);
            this.Console_Box.Multiline = true;
            this.Console_Box.Name = "Console_Box";
            this.Console_Box.ReadOnly = true;
            this.Console_Box.Size = new System.Drawing.Size(203, 176);
            this.Console_Box.TabIndex = 11;
            this.Console_Box.Visible = false;
            // 
            // Menu_Fonts_Add
            // 
            this.Menu_Fonts_Add.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addToolStripMenuItem1,
            this.Menu_Fonts_Rename,
            this.Menu_Fonts_Remove});
            this.Menu_Fonts_Add.Name = "Menu_Fonts_Add";
            this.Menu_Fonts_Add.Size = new System.Drawing.Size(48, 20);
            this.Menu_Fonts_Add.Text = "Fonts";
            // 
            // addToolStripMenuItem1
            // 
            this.addToolStripMenuItem1.Name = "addToolStripMenuItem1";
            this.addToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.addToolStripMenuItem1.Text = "Add";
            // 
            // Menu_Fonts_Remove
            // 
            this.Menu_Fonts_Remove.Name = "Menu_Fonts_Remove";
            this.Menu_Fonts_Remove.Size = new System.Drawing.Size(152, 22);
            this.Menu_Fonts_Remove.Text = "Remove";
            // 
            // Menu_Fonts_Rename
            // 
            this.Menu_Fonts_Rename.Name = "Menu_Fonts_Rename";
            this.Menu_Fonts_Rename.Size = new System.Drawing.Size(152, 22);
            this.Menu_Fonts_Rename.Text = "Rename";
            // 
            // Menu_Spirites
            // 
            this.Menu_Spirites.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Menu_Spirites_Add,
            this.Menu_Spirites_Clone,
            this.Menu_Spirites_Remove});
            this.Menu_Spirites.Name = "Menu_Spirites";
            this.Menu_Spirites.Size = new System.Drawing.Size(60, 20);
            this.Menu_Spirites.Text = "Spirities";
            // 
            // Menu_Spirites_Add
            // 
            this.Menu_Spirites_Add.Name = "Menu_Spirites_Add";
            this.Menu_Spirites_Add.Size = new System.Drawing.Size(152, 22);
            this.Menu_Spirites_Add.Text = "Add";
            // 
            // Menu_Spirites_Remove
            // 
            this.Menu_Spirites_Remove.Name = "Menu_Spirites_Remove";
            this.Menu_Spirites_Remove.Size = new System.Drawing.Size(152, 22);
            this.Menu_Spirites_Remove.Text = "Remove";
            // 
            // Menu_Spirites_Clone
            // 
            this.Menu_Spirites_Clone.Name = "Menu_Spirites_Clone";
            this.Menu_Spirites_Clone.Size = new System.Drawing.Size(152, 22);
            this.Menu_Spirites_Clone.Text = "Clone";
            // 
            // Interface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.ClientSize = new System.Drawing.Size(1009, 599);
            this.Controls.Add(this.Console_Box);
            this.Controls.Add(this.List_Exports);
            this.Controls.Add(this.Status_Bar);
            this.Controls.Add(this.Menu1);
            this.Controls.Add(this.Picture_Box);
            this.Controls.Add(this.Button_Load);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(10, 0);
            this.MainMenuStrip = this.Menu1;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Interface";
            this.Opacity = 0.95D;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "  [GL]  Kosmos - SC Editor   -/-   GobelinLand - 2016 © ";
            ((System.ComponentModel.ISupportInitialize)(this.Picture)).EndInit();
            this.Picture_Box.ResumeLayout(false);
            this.Picture_Box.PerformLayout();
            this.Menu1.ResumeLayout(false);
            this.Menu1.PerformLayout();
            this.Status_Bar.ResumeLayout(false);
            this.Status_Bar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="_Disposing">True if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool _Disposing)
        {
            if (_Disposing && (this.Components != null))
            {
                this.Components.Dispose();
            }

            base.Dispose(_Disposing);
        }
        
        private IContainer Components;
        private Button Button_Load;
        private PictureBox Picture;
        private Panel Picture_Box;
        private MenuStrip Menu1;
        private StatusStrip Status_Bar;
        private ToolStripMenuItem Menu_File;
        private ToolStripMenuItem Tab_File_Open;
        private ToolStripMenuItem Tab_File_Save;
        private ToolStripMenuItem Tab_File_Close;
        private ToolStripMenuItem Menu_Shapes;
        private ToolStripMenuItem Menu_Exports;
        private ToolStripMenuItem Menu_About;
        private ToolStripMenuItem Menu_About_Us;
        private ToolStripMenuItem Menu_About_Licence;
        private ToolStripProgressBar Status_Bar_Progress_Bar;
        private ToolStripStatusLabel Status_Bar_File;
        private ToolStripStatusLabel Status_Bar_Progress;
        private ToolStripStatusLabel Status_Bar_File_Name;
        private TreeView List_Exports;
        private TextBox Console_Box;
        private ToolStripMenuItem Menu_Shapes_Add;
        private ToolStripMenuItem Menu_Shapes_Clone;
        private ToolStripMenuItem Menu_Shapes_Export;
        private ToolStripMenuItem Menu_Shapes_Import;
        private ToolStripMenuItem Menu_Shapes_Rename;
        private ToolStripMenuItem Menu_Shapes_Remove;
        private ToolStripMenuItem Menu_Exports_Add;
        private ToolStripMenuItem Menu_Exports_Clone;
        private ToolStripMenuItem Menu_Exports_Export;
        private ToolStripMenuItem Menu_Exports_Import;
        private ToolStripMenuItem Menu_Exports_Rename;
        private ToolStripMenuItem Menu_Exports_Remove;
        private ToolStripMenuItem Menu_Textures;
        private ToolStripMenuItem Menu_Textures_Change;
        private ToolStripMenuItem Menu_Textures_Export;
        private ToolStripMenuItem Menu_Spirites;
        private ToolStripMenuItem Menu_Spirites_Add;
        private ToolStripMenuItem Menu_Spirites_Clone;
        private ToolStripMenuItem Menu_Spirites_Remove;
        private ToolStripMenuItem Menu_Fonts_Add;
        private ToolStripMenuItem addToolStripMenuItem1;
        private ToolStripMenuItem Menu_Fonts_Rename;
        private ToolStripMenuItem Menu_Fonts_Remove;
    }
}

