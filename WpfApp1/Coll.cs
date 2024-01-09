using System.Drawing;

namespace WpfApp1
{
    public abstract class Coll
    {
        public Bitmap newBitmap;

        public Coll(Bitmap newBitmap)
        {
            this.newBitmap = newBitmap;
        }
    }
}
