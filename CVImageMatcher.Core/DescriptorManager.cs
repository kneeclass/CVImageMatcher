using System.Collections.Generic;
using System.Linq;
using CVImageMatcher.Core.Extensions;
using CVImageMatcher.Core.Models;

using Emgu.CV;
using System.Drawing;
using Emgu.CV.Features2D;
using Emgu.CV.CvEnum;
using Emgu.CV.Util;

namespace CVImageMatcher.Core {
    public class DescriptorManager {
        
        private static ORBDetector _detector;
        public static ORBDetector Detector => _detector ?? (_detector = new ORBDetector(700, 1.2f, 2, 8, 0))/*.Create(nFeatures: 700, scaleFactor: 1.2F, nLevels: 10, edgeThreshold: 0))*/;

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
            VectorOfKeyPoint keyPoints = null;
            Detector.DetectAndCompute(mat, null, keyPoints, akazeDescriptors,false);
            akazeDescriptors.ConvertTo(akazeDescriptors, DepthType.Cv32F);

            //Cv2.DrawKeypoints(mat, kazeKeyPoints, mat, Scalar.BlueViolet, DrawMatchesFlags.Default);
            //Cv2.ImShow("webcam", mat);
            //Cv2.WaitKey(0);

            return akazeDescriptors;
        }


        public static Mat ConcatDescriptors(IEnumerable<Mat> descriptors) {
            int cols = descriptors.FirstOrDefault().Cols;
            int rows = descriptors.Sum(a => a.Rows);
            var mat = new Mat(new Size(cols, rows), Emgu.CV.CvEnum.DepthType.Cv32F,2);

            foreach(var descriptor in descriptors) {
                CvInvoke.VConcat(mat, descriptor, mat);
            }

            //mat.ConvertTo(mat, MatType.CV_32FC1);
            //mat.IsEnabledDispose = false;
            return mat;
        }

    }
}
