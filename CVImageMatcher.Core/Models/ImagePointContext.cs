using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emgu.CV.Structure;

namespace CVImageMatcher.Core.Models {
    public class ImagePointContext {
        public Image Image { get; set; }
        public MKeyPoint KeyPoint { get; set; }
        public ImagePointContext(Image image, MKeyPoint keyPoint) {
            Image = image;
            KeyPoint = keyPoint;
        }
    }
}
