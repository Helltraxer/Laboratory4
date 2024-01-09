using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace WpfApp1
{
    internal class Rotate : Coll
    {
        public RotateFlipType rotateFlipType { get; }
        public void RotateImage(Bitmap newBitMap, RotateFlipType rotateFlipType)
        {
            if (newBitMap == null)
            {
                throw new ArgumentException("Изображение не загружено");
            }
            newBitMap.RotateFlip(rotateFlipType);
        }

        public Rotate(Bitmap newBitMap, RotateFlipType rotateFlipType) : base(newBitMap)
        {
            this.rotateFlipType = rotateFlipType;
        }
    }
}
