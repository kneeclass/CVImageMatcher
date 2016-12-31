using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCvSharp;

namespace CVImageMatcher.Core.Extensions {
    public static class MatExtensions {


        public static void ResizeKeepAspect(this Mat mat, int maxWidth, int maxHeight) {
            int newHeight = mat.Size().Height;
            int newWidth = mat.Size().Width;
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
            Cv2.Resize(mat, mat, new Size {Width = newWidth, Height = newHeight});


        }
    }
}
