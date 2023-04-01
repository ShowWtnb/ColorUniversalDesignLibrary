using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ColorUniversalDesignLibrary.ColorBlindnessSimulator
{
    /// <summary>
    /// CUD_MirroringWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class CUD_MirroringWindow : Window
    {
        readonly MirroringPage _page;
        public CUD_MirroringWindow()
        {
            InitializeComponent();

            _page = new MirroringPage();
            this.MainFrame.Navigate(_page);
        }

        RenderTargetBitmap _bmp = null;
        FormatConvertedBitmap _bitmap = null;
        int width;
        int height;
        int stride;
        byte[] originalPixels = null;
        internal Result GetBitmapElement(FrameworkElement mirrorTarget)
        {
            Dispatcher.Invoke((Action)(() =>
            {
                _bmp = new RenderTargetBitmap(
                    (int)mirrorTarget.ActualWidth,
                    (int)mirrorTarget.ActualHeight,
                    96, 96, // DPI
                    PixelFormats.Pbgra32);
                _bmp.Render(mirrorTarget);

                _bitmap = new FormatConvertedBitmap(_bmp, PixelFormats.Bgra32, null, 0);

                // 画像の大きさに従った配列を作る
                width = _bitmap.PixelWidth;
                height = _bitmap.PixelHeight;
                originalPixels = new byte[width * height * 4];

                // BitmapSourceから配列にコピー
                stride = (width * _bitmap.Format.BitsPerPixel + 7) / 8;
                _bitmap.CopyPixels(originalPixels, stride, 0);
            }));

            //return _bitmap;
            return new Result(originalPixels, width, height, stride);
        }

        internal void SetImages(BitmapSource original, BitmapSource protanopia, BitmapSource tritanopia, BitmapSource deuteranopia)
        {
            _page.SetImages(original, protanopia, tritanopia, deuteranopia);
        }
    }

    internal struct Result
    {
        public byte[] originalPixels;
        public int width;
        public int height;
        public int stride;

        public Result(byte[] originalPixels, int width, int height, int stride)
        {
            this.originalPixels = originalPixels;
            this.width = width;
            this.height = height;
            this.stride = stride;
        }

        public override bool Equals(object obj)
        {
            return obj is Result other &&
                   EqualityComparer<byte[]>.Default.Equals(originalPixels, other.originalPixels) &&
                   width == other.width &&
                   height == other.height &&
                   stride == other.stride;
        }

        public override int GetHashCode()
        {
            //int hashCode = 1349393232;
            //hashCode = hashCode * -1521134295 + EqualityComparer<byte[]>.Default.GetHashCode(originalPixels);
            //hashCode = hashCode * -1521134295 + width.GetHashCode();
            //hashCode = hashCode * -1521134295 + height.GetHashCode();
            //hashCode = hashCode * -1521134295 + stride.GetHashCode();
            //return hashCode;
            return HashCode.Combine(originalPixels, width, height, stride);
        }

        public void Deconstruct(out byte[] originalPixels, out int width, out int height, out int stride)
        {
            originalPixels = this.originalPixels;
            width = this.width;
            height = this.height;
            stride = this.stride;
        }

        public static implicit operator (byte[] originalPixels, int width, int height, int stride)(Result value)
        {
            return (value.originalPixels, value.width, value.height, value.stride);
        }

        public static implicit operator Result((byte[] originalPixels, int width, int height, int stride) value)
        {
            return new Result(value.originalPixels, value.width, value.height, value.stride);
        }
    }

}
