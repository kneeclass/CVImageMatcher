using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emgu.CV.Util;

namespace CVImageMatcher.Core.Models {
    public class MatchResult {
        public Dictionary<Image, QueryHit> Matches { get; set; }
        public VectorOfKeyPoint KeyPoints { get; set; }
    }
}
