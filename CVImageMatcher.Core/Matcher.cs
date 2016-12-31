using System;
using System.Collections.Generic;
using System.Linq;
using CVImageMatcher.Core.Models;
using OpenCvSharp;
using OpenCvSharp.Flann;

namespace CVImageMatcher.Core {
    public class Matcher {
        public int Threshold = 8;

        public Dictionary<Image, int> FindMatch(string path) {
            var mat = new Mat(path, ImreadModes.GrayScale);
            mat.ConvertTo(mat, MatType.CV_32FC1);
            
            return FindMatch(mat);
        }
        public Dictionary<Image, int> FindMatch(Mat queryMat) {
            
            var queryDescriptors = DescriptorManager.ExtractDescriptor(queryMat);
            const int Knn = 12;
            using (var indices = new Mat(queryDescriptors.Rows, 2, MatType.CV_32FC1))
            using (var dists = new Mat(queryDescriptors.Rows, 2, MatType.CV_32FC1)) {

            IndexContext.CurrentFlannIndex.KnnSearch(queryDescriptors, indices, dists, Knn, new SearchParams(32) );
            var result = new Dictionary<Image, int>();

            for (int i = 0; i < indices.Rows; i++) {
                // filter out all inadequate pairs based on distance between pairs
                if (dists.Get<float>(i, 0) < (0.6 * dists.Get<float>(i, 1))) {
                    var pos = indices.Get<int>(i, 0);
                    if (IndexContext.CurrentMappingIndex.ContainsKey(pos)) {
                        if (!result.ContainsKey(IndexContext.CurrentMappingIndex[pos])) {
                            result.Add(IndexContext.CurrentMappingIndex[pos], 0);
                        }
                        result[IndexContext.CurrentMappingIndex[pos]]++;
                    }
                }
            }

            return result.Where(x => x.Value > Threshold)
                .OrderByDescending(x => x.Value)
                .ToDictionary(x => x.Key, y => y.Value);



            }
        }


    }
}
