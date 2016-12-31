using System.Collections.Generic;
using CVImageMatcher.Core.Models;
using OpenCvSharp;
using OpenCvSharp.Flann;

namespace CVImageMatcher.Core {
    public class IndexContext {
        public static Index CurrentFlannIndex { get; set; }
        public static Dictionary<int, Image> CurrentMappingIndex { get; set; }
        public static Mat ConcatDescriptors { get; set; }
    }
}
