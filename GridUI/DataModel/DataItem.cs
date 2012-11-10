using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Bitmap
using Windows.UI.Xaml.Media.Imaging;
// Colors
using Windows.UI;


namespace GridUI.DataModel
{
    /// <summary>
    /// Generic item data model.
    /// </summary>
    public abstract class DataItem : DataCommon
    {
        public DataItem(String uniqueId, String title, String imagePath, DataGroup group)
            : base(uniqueId, title, imagePath)
        {
            this._group = group;
        }

        private string _content = string.Empty;
        public string Content
        {
            get { return this._content; }
            set { this.SetProperty(ref this._content, value); }
        }

        private DataGroup _group;
        public DataGroup Group
        {
            get { return this._group; }
            set { this.SetProperty(ref this._group, value); }
        }

        public abstract void drawContent(WriteableBitmap bmp);
    }
}
