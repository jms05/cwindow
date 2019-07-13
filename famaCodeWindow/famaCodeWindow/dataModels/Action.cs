using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using System.Windows.Media.Imaging;

namespace famaCodeWindow.dataModels
{

    public class Model
    {
        public enum modelType
        {
            Action,
            Player,
            Term,
        }
        public string id;
        public List<string> incompatibleBNTS;
        public System.Windows.Controls.Button assingedBTN;
        public modelType type;
    }
    public class Action : Model
    {
        public string text;
        
        public Action(string id, string text, List<string> toDisable)
        {
            base.incompatibleBNTS = toDisable;
            base.id = id;
            base.type = modelType.Action;
            this.text = text;
        }
    }

    public class Term : Model
    {
        public string text;

        public Term(string id, string text, List<string> toDisable)
        {
            base.incompatibleBNTS = toDisable;
            base.id = id;
            base.type = modelType.Term;
            this.text = text;
        }
    }

    public class Player : Model
    {
        public string name;
        public string number;
        public byte[] image;

        public Player(string id, string name, string number, byte[] image, List<string> toDisable)
        {
            base.incompatibleBNTS = toDisable;
            base.id = id;
            base.type = modelType.Player;
            this.name = name;
            this.number = number;
            this.image = image;
        }

        public Player(string id, string name, string number, string imagepath, List<string> toDisable) : this(id, name, number, new byte[1], toDisable)
        {
            var imagearray = GetImageFromPath(imagepath);
            this.image = imagearray;
        }

        private byte[] GetImageFromPath(string imagepath)
        {
            MemoryStream ms = new MemoryStream();
            Image img = Image.FromFile(imagepath);
            img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);

            return ms.ToArray();
        }

        public BitmapImage GetImage()
        {
            MemoryStream ms = new MemoryStream(this.image);
            ms.Position = 0;
            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.StreamSource = ms;
            bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
            bitmapImage.EndInit();

            return bitmapImage;
        }
    }
}
