using System.Drawing;
using System;
using System.Drawing.Imaging;

namespace WpfApp1
{
    public class VioletFilter : Filters
    {
        public ColorMatrix colorMatrix { get; }
        public override void Create(Bitmap newBitMap, ColorMatrix colorMatrix)
        {
            if (newBitMap == null) throw new ArgumentException("Изображение не загружено");
            ColorMatrix NewColors = new ColorMatrix(new float[][] { new float[] { 1f, 0f, 1f, 0f, 0f }, new float[] { 0f, 1f, 0f, 0f, 0f }, new float[] { 1f, 0f, 1f, 0f, 0f }, new float[] { 0f, 0f, 0f, 1f, 0f }, new float[] { 0f, 0f, 0f, 0f, 1f }});
            ;
            base.Create(newBitMap, NewColors);
        }

        public VioletFilter(Bitmap newBitMap, ColorMatrix colorMatrix) : base(newBitMap)
        {
            this.colorMatrix = colorMatrix;
        }
    }
}
