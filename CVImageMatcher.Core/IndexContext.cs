using System.Collections.Generic;
using CVImageMatcher.Core.Models;
using Emgu.CV.Flann;
using Emgu.CV;

namespace CVImageMatcher.Core {
    public class IndexContext {
        public static Index CurrentFlannIndex { get; set; }
        public static Dictionary<int, Image> CurrentMappingIndex { get; set; }
        public static Mat ConcatDescriptors { get; set; }
    }
}
