using System.Collections.Generic;
using System.Linq;
using CVImageMatcher.Core.Extensions;
using CVImageMatcher.Core.Models;

using Emgu.CV;
using System.Drawing;
using Emgu.CV.Features2D;
using Emgu.CV.CvEnum;
using Emgu.CV.Util;
using Emgu.CV.XFeatures2D;

namespace CVImageMatcher.Core {
    public class DescriptorManager {
        
        private static ORBDetector _detector;

        public static ORBDetector Detector
            => _detector ?? (_detector = new ORBDetector(numberOfFeatures: 700, scaleFactor: 1.2F, nLevels: 10, edgeThreshold: 0));

        public static Mat ExtractDescriptor(Models.Image image) {
            if (!string.IsNullOrWhiteSpace(image.LocalPath)) {
                using (var mat = new Mat(image.LocalPath, LoadImageType.Grayscale)) {

                    mat.ResizeKeepAspect(640, 480);
                    //Cv2.ImShow("webcam", mat);
                    //Cv2.WaitKey(0);
                    return ExtractDescriptor(mat);
                }
            }
            return null;
        }
        public static Mat ExtractDescriptor(Mat mat) {
            
            var akazeDescriptors = new Mat();
            var keyPoints = new VectorOfKeyPoint();
            Detector.DetectAndCompute(mat, null, keyPoints, akazeDescriptors,false);
            akazeDescriptors.ConvertTo(akazeDescriptors, DepthType.Cv8U);

            //Cv2.DrawKeypoints(mat, kazeKeyPoints, mat, Scalar.BlueViolet, DrawMatchesFlags.Default);
            //Cv2.ImShow("webcam", mat);
            //Cv2.WaitKey(0);

            return akazeDescriptors;
        }


        public static Mat ConcatDescriptors(IEnumerable<Mat> descriptors) {
            var mat = descriptors.FirstOrDefault();
            if (mat == null) return null;
            descriptors = descriptors.Skip(1);
            foreach(var descriptor in descriptors) {
                    var next = new Mat();
                    CvInvoke.VConcat(mat, descriptor, next);
                
                    next.ConvertTo(mat, DepthType.Cv8U);
            }

            //mat.ConvertTo(mat, DepthType.Cv32F);
            mat.ConvertTo(mat, DepthType.Cv8U);
            //mat.IsEnabledDispose = false;
            return mat;
        }

    }
}
