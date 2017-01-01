using System;
using System.Collections.Generic;
using System.Linq;
using CVImageMatcher.Core.Models;
using Emgu.CV;
using Emgu.CV.CvEnum;
using System.Drawing;
using CVImageMatcher.Core.Extensions;

namespace CVImageMatcher.Core {
    public class Matcher {
        public int Threshold = 8;

        public Dictionary<Models.Image, int> FindMatch(string path) {
            var mat = new Mat(path, LoadImageType.Grayscale);
            //mat.ConvertTo(mat, MatType.CV_32FC1);
            
            return FindMatch(mat);
        }
        public Dictionary<Models.Image, int> FindMatch(Mat queryMat) {
            
            var queryDescriptors = DescriptorManager.ExtractDescriptor(queryMat);
            const int Knn = 12;
            using (var indices = new Mat(new Size(queryDescriptors.Rows, 2), DepthType.Cv32F,2))
            using (var dists = new Mat(new Size(queryDescriptors.Rows, 2), DepthType.Cv32F,2)) {

            IndexContext.CurrentFlannIndex.KnnSearch(queryDescriptors, indices, dists, Knn, 32);
            var result = new Dictionary<Models.Image, int>();

            for (int i = 0; i < indices.Rows; i++) {
                // filter out all inadequate pairs based on distance between pairs
                if (dists.GetFloatValue(i,0) < (0.6 * dists.GetFloatValue(i, 1))) {
                    var pos = indices.GetIntValue(i, 0);
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
