using System;
using System.Collections.Generic;
using System.Linq;
using CVImageMatcher.Core.Models;
using Emgu.CV;
using Emgu.CV.CvEnum;
using System.Drawing;
using CVImageMatcher.Core.Extensions;
using Emgu.CV.Util;

namespace CVImageMatcher.Core {
    public class Matcher {
        public int Threshold = 4;

        public MatchResult FindMatch(string path) {
            var mat = new Mat(path, LoadImageType.Grayscale);
            //mat.ConvertTo(mat, MatType.CV_32FC1);
            
            return FindMatch(mat);
        }

        public MatchResult FindMatch(Mat queryMat) {
            try {
                var queryDescriptors = DescriptorManager.ExtractDescriptor(queryMat);
                const int Knn = 11;
                using (var indices = new Mat(new Size(queryDescriptors.Rows, 2), DepthType.Cv32F, 2))
                using (var dists = new Mat(new Size(queryDescriptors.Rows, 2), DepthType.Cv32F, 2)) {

                    IndexContext.CurrentFlannIndex.KnnSearch(queryDescriptors, indices, dists, Knn, 32);
                    var result = new Dictionary<Models.Image, QueryHit>();

                    for (int i = 0; i < indices.Rows; i++) {
                        // filter out all inadequate pairs based on distance between pairs
                        if (dists.GetFloatValue(i, 0) < (0.6*dists.GetFloatValue(i, 1))) {
                            var pos = indices.GetIntValue(i, 0);
                            //CvInvoke.FindHomography();
                            if (IndexContext.CurrentMappingIndex.ContainsKey(pos)) {
                                var img = IndexContext.CurrentMappingIndex[pos];
                                if (!result.ContainsKey(img)) {
                                    result.Add(img, new QueryHit {Image = img});
                                }
                                result[img].Hits++;
                            }
                        }
                    }

                    return new MatchResult {
                        Matches = result.Where(x => x.Value.Hits > Threshold).OrderByDescending(x => x.Value.Hits).ToDictionary(x => x.Key, y => y.Value),
                    };



                }
            }
            catch (Exception e) {

                return null;
            }
        }
    }
}
