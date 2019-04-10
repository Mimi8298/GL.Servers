namespace GL.Editor.Interface
{
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Windows.Forms;

    using GL.Editor.Core;
    using GL.Editor.Logic.Slots.Items;

    using SCFile    = GL.Editor.Logic.Slots.Items.SCFile;

    public partial class Interface : Form
    {
        internal SCFile SCFile;
        internal Resources Resources;

        internal uint SelectedTexture;
        internal uint SelectedShape;
        internal uint SelectedExport;

        /// <summary>
        /// Initializes a new instance of the <see cref="Interface"/> class.
        /// </summary>
        public Interface()
        {
            this.InitializeComponent();
            this.Resources = new Resources();
        }

        /// <summary>
        /// Event on mouse double click.
        /// </summary>
        /// <param name="_Sender">The sender.</param>
        /// <param name="_Event">The <see cref="MouseEventArgs"/> instance containing the event data.</param>
        private void List_Exports_NodeDoubleClick(object _Sender, TreeNodeMouseClickEventArgs _Event)
        {
            if (_Event.Node.Level == 0)
            {
                this.SelectedExport = uint.Parse(_Event.Node.ImageKey);
            }
            else if (_Event.Node.Level == 1)
            {
                this.SelectedExport = uint.Parse(_Event.Node.Parent.ImageKey);
                this.SelectedShape  = uint.Parse(_Event.Node.ImageKey);
                this.Picture.Image  = this.SCFile.SCInfo.Exports[this.SelectedExport].Shapes[this.SelectedShape].Image;
            }
        }

        /// <summary>
        /// Handles the OnClick event of the Menu_Shapes_Export control.
        /// </summary>
        /// <param name="_Sender">The source of the event.</param>
        /// <param name="_Event">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Menu_Shapes_Export_OnClick(object _Sender, EventArgs _Event)
        {
            SaveFileDialog _Dialog      = new SaveFileDialog();
            _Dialog.AddExtension        = true;
            _Dialog.DefaultExt          = "png";
            _Dialog.InitialDirectory    = Directory.GetCurrentDirectory() + "/Images/";
            _Dialog.FileName            = this.SCFile.SCTextureI.Name.Replace(this.SCFile.SCTextureI.Extension, "") + "_shape_" + this.SelectedShape;
            _Dialog.Filter              = "Images|*.png";
            DialogResult _Result        = _Dialog.ShowDialog(this.Owner);

            if (_Result == DialogResult.OK)
            {
                this.SCFile.SCInfo.Exports[this.SelectedExport].Shapes[this.SelectedShape].Image.Save(_Dialog.FileName, ImageFormat.Png);
            }
        }

        /// <summary>
        /// Handles the OnClick event of the Open control.
        /// </summary>
        /// <param name="_Sender">The source of the event.</param>
        /// <param name="_Event">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Open_OnClick(object _Sender, EventArgs _Event)
        {
            OpenFileDialog Browser = new OpenFileDialog();
            Browser.Title = "Open SC Tex File";
            Browser.Filter = "SC Texture Files | *_tex.sc";
            Browser.InitialDirectory = Directory.GetCurrentDirectory();

            this.Status_Bar_Progress_Bar.Value = 0;

            if (Browser.ShowDialog() == DialogResult.OK)
            {
                this.Status_Bar_Progress_Bar.Value = 10;

                this.SCFile = Resources.Files[Browser.SafeFileName];

                this.Status_Bar_Progress_Bar.Value = 20;

                this.SCFile.SCTexture.Load();

                this.Status_Bar_Progress_Bar.Value = 40;

                if (this.SCFile.SCTexture.Images.Count > 0)
                {
                    this.Picture.Image  = this.SCFile.SCTexture.Images[0];
                    this.Picture.Size   = this.SCFile.SCTexture.Sizes[0];

                    this.Status_Bar_Progress_Bar.Value = 60;

                    this.Status_Bar_File_Name.Text = this.SCFile.SCInfoI.Name;
                }

                this.SCFile.SCInfo.Load();
                
                this.Status_Bar_Progress_Bar.Value = 80;

                this.Refresh();

                this.Status_Bar_Progress_Bar.Value = 100;
            }
        }

        /// <summary>
        /// Populates the exports list with the specified exports.
        /// </summary>
        private void List_Exports_Populate()
        {
            this.List_Exports.Nodes.Clear();
            this.List_Exports.BeginUpdate();

            foreach (Export Export in this.SCFile.SCInfo.Exports.Values)
            {
                this.List_Exports.Nodes.Add(Export.Identifier.ToString(), string.IsNullOrEmpty(Export.Name) ? "Export #" + Export.Identifier : Export.Name, Export.Identifier.ToString());

                foreach (Shape Shape in Export.Shapes.Values)
                {
                    this.List_Exports.Nodes[Export.Identifier.ToString()].Nodes.Add(Shape.Identifier.ToString(), string.IsNullOrEmpty(Shape.Name) ? "Shape #" + Shape.Identifier : Shape.Name, Shape.Identifier.ToString());
                }
            }

            this.List_Exports.EndUpdate();
        }

        /// <summary>
        /// Populates the texture list with the specified textures.
        /// </summary>
        private void Menu_Textures_Change_Populate()
        {
            this.Menu_Textures_Change.DropDownItems.Clear();

            for (int Index = 0; Index < this.SCFile.SCTexture.Images.Count; Index++)
            {
                Image _Image = this.SCFile.SCTexture.Images[Index];

                this.Menu_Textures_Change.DropDownItems.Add("Texture #" + Index, _Image, this.Menu_Textures_Change_OnClick);
                this.Menu_Textures_Change.DropDownItems[Index].Tag = Index;
            }
        }

        /// <summary>
        /// Populates the font list with the specified fonts.
        /// </summary>
        private void Menu_Fonts_Populate()
        {
            this.Menu_Fonts_Rename.DropDownItems.Clear();

            foreach (Logic.Slots.Items.Font Font in this.SCFile.SCInfo.Temporary.Fonts.Values)
            {
                this.Menu_Fonts_Rename.DropDownItems.Add(Font.Name);
                this.Menu_Fonts_Rename.DropDownItems[this.Menu_Fonts_Rename.DropDownItems.Count - 1].Tag = Font.Identifier;

                if (this.Menu_Fonts_Rename.DropDownItems.Count > 200)
                {
                    break;
                }
            }
        }

        /// <summary>
        /// Refreshes this instance.
        /// </summary>
        private void Refresh()
        {
            this.List_Exports_Populate();
            this.Menu_Textures_Change_Populate();
            this.Menu_Fonts_Populate();

            this.SelectedTexture    = 0;
            this.SelectedShape      = 0;
            this.SelectedExport     = 0;
        }

        /// <summary>
        /// Handles the OnClick event of the Menu_Textures_Change control.
        /// </summary>
        /// <param name="_Sender">The source of the event.</param>
        /// <param name="_Event">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Menu_Textures_Change_OnClick(object _Sender, EventArgs _Event)
        {
            ToolStripMenuItem _Control = _Sender as ToolStripMenuItem;

            if (_Control.Tag != null)
            {
                bool _Success = uint.TryParse(((int) _Control.Tag).ToString(), out this.SelectedTexture);

                if (_Success)
                {
                    this.Picture.Image = this.SCFile.SCTexture.Images[(int) this.SelectedTexture];
                }
            }
        }

        /// <summary>
        /// Handles the OnClick event of the Menu_Textures_Export control.
        /// </summary>
        /// <param name="_Sender">The source of the event.</param>
        /// <param name="_Event">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Menu_Textures_Export_OnClick(object _Sender, EventArgs _Event)
        {
            SaveFileDialog _Dialog      = new SaveFileDialog();
            _Dialog.AddExtension        = true;
            _Dialog.DefaultExt          = "png";
            _Dialog.InitialDirectory    = Directory.GetCurrentDirectory() + "/Images/";
            _Dialog.FileName            = this.SCFile.SCTextureI.Name.Replace(this.SCFile.SCTextureI.Extension, "") + "_texture_" + this.SelectedTexture;
            _Dialog.Filter              = "Images|*.png";
            DialogResult _Result        = _Dialog.ShowDialog(this.Owner);

            if (_Result == DialogResult.OK)
            {
                this.SCFile.SCTexture.Images[(int) this.SelectedTexture].Save(_Dialog.FileName, ImageFormat.Png);
            }
        }

        /// <summary>
        /// Handles the OnClick event of the Menu_About_Us control.
        /// </summary>
        /// <param name="_Sender">The source of the event.</param>
        /// <param name="_Event">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Menu_About_Us_OnClick(object _Sender, EventArgs _Event)
        {
            AboutUs _AboutUS = new AboutUs();
            _AboutUS.Show(this.Owner);
        }

        /// <summary>
        /// Handles the OnClick event of the Close control.
        /// </summary>
        /// <param name="_Sender">The source of the event.</param>
        /// <param name="_Event">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Close_OnClick(object _Sender, EventArgs _Event)
        {
            this.SCFile                         = null;
            this.Picture.Image                  = null;
            this.Picture.Size                   = Size.Empty;
            this.Status_Bar_File_Name.Text      = "-------";
            this.Status_Bar_Progress_Bar.Value  = 0;
            this.List_Exports.Nodes.Clear();
        }
    }
}