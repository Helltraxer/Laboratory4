using System.Drawing;
using System;
using System.Drawing.Imaging;

namespace WpfApp1
{
    public class DarkFilter : Filters
    {
        public ColorMatrix colorMatrix { get; }
        public override void Create(Bitmap newBitMap, ColorMatrix colorMatrix)
        {
            if (newBitMap == null) throw new ArgumentException("Изображение не загружено");
            ColorMatrix NewColors = new ColorMatrix(new float[][] { new float[] { 0.5f, 0, 0, 0, 0 }, new float[] { 0, 0.5f, 0, 0, 0 }, new float[] { 0, 0, 0.5f, 0, 0 }, new float[] { 0, 0, 0, 1, 0 }, new float[] { 0, 0, 0, 0, 1 } });
            base.Create(newBitMap, NewColors);
        }

        public DarkFilter(Bitmap newBitMap, ColorMatrix colorMatrix) : base(newBitMap)
        {
            this.colorMatrix = colorMatrix;
        }
    }
}
