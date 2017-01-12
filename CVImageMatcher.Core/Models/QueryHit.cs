using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emgu.CV.Structure;

namespace CVImageMatcher.Core.Models {
    public class QueryHit {
        public int Hits { get; set; }
        public Image Image { get; set; }
    }
}
