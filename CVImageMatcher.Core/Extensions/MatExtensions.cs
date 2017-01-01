using Emgu.CV;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CVImageMatcher.Core.Extensions {
    public static class MatExtensions {


        public static void ResizeKeepAspect(this Mat mat, int maxWidth, int maxHeight) {
            int newHeight = mat.Size.Height;
            int newWidth = mat.Size.Width;
            if (maxWidth > 0 && newWidth > maxWidth) //WidthResize
            {
                Decimal divider = Math.Abs((Decimal) newWidth/(Decimal) maxWidth);
                newWidth = maxWidth;
                newHeight = (int) Math.Round((Decimal) (newHeight/divider));
            }
            if (maxHeight > 0 && newHeight > maxHeight) //HeightResize
            {
                Decimal divider = Math.Abs((Decimal) newHeight/(Decimal) maxHeight);
                newHeight = maxHeight;
                newWidth = (int) Math.Round((Decimal) (newWidth/divider));
            }
            CvInvoke.Resize(mat, mat, new Size {Width = newWidth, Height = newHeight});

        }

        public static int GetIntValue(this Mat mat, int row, int col)
        {
            var value = new int[1];
            Marshal.Copy(mat.DataPointer + (row * mat.Cols + col) * mat.ElementSize, value, 0, 1);
            return value[0];
        }
        public static float GetFloatValue(this Mat mat, int row, int col)
        {
            var value = new float[1];
            Marshal.Copy(mat.DataPointer + (row * mat.Cols + col) * mat.ElementSize, value, 0, 1);
            return value[0];
        }
    }
}
