using System.Collections.Generic;
using System.Linq;
using CVImageMatcher.Core.Extensions;
using CVImageMatcher.Core.Models;
using OpenCvSharp;
using OpenCvSharp.XFeatures2D;

namespace CVImageMatcher.Core {
    public class DescriptorManager {
        private static ORB _detector;
        public static ORB Detector => _detector ?? (_detector = ORB.Create(nFeatures: 700, scaleFactor: 1.2F, nLevels: 10, edgeThreshold: 0));

        public static Mat ExtractDescriptor(Image image) {
            if (!string.IsNullOrWhiteSpace(image.LocalPath)) {
                using (var mat = new Mat(image.LocalPath, ImreadModes.GrayScale)) {

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
            KeyPoint[] kazeKeyPoints = null;
            Detector.DetectAndCompute(mat, null, out kazeKeyPoints, akazeDescriptors);
            akazeDescriptors.ConvertTo(akazeDescriptors, MatType.CV_32FC1);

            //Cv2.DrawKeypoints(mat, kazeKeyPoints, mat, Scalar.BlueViolet, DrawMatchesFlags.Default);
            //Cv2.ImShow("webcam", mat);
            //Cv2.WaitKey(0);

            return akazeDescriptors;
        }


        public static Mat ConcatDescriptors(IEnumerable<Mat> descriptors) {
            int cols = descriptors.FirstOrDefault().Cols;
            int rows = descriptors.Sum(a => a.Rows);
            var mat = new Mat(cols, rows, MatType.CV_32FC1);
            Cv2.VConcat(descriptors.ToArray(), mat);
            mat.ConvertTo(mat, MatType.CV_32FC1);
            mat.IsEnabledDispose = false;
            return mat;
        }

    }
}
