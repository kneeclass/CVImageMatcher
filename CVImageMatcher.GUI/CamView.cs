using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;
using CVImageMatcher.Core;
using OpenCvSharp;
using Image = CVImageMatcher.Core.Models.Image;

namespace CVImageMatcher.GUI {
    public partial class CamView : Form {
        public CamView() {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e) {
            camImage.BackgroundImageLayout = ImageLayout.Stretch;
            var indexBuilder = new IndexBuilder();
            indexBuilder.BuildIndex();
            Task.Run(() => RunCamera());
        }

        private void RunCamera() {

            using (var capture = VideoCapture.FromCamera(0)) {
                capture.Set(CaptureProperty.FrameWidth, 640);
                capture.Set(CaptureProperty.FrameHeight, 480);
                while (true) {

                    var success = capture.Grab();
                    if (!success) throw new Exception("Failed to grab image");
                    using (var image = capture.RetrieveMat()) { 
                        Cv2.CvtColor(image,image, ColorConversionCodes.BGR2GRAY);
                        
                        var bitmap = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(image);
                        Invoke((MethodInvoker)delegate {
                            listBox1.Items.Clear();
                            camImage.BackgroundImage = bitmap;
                            camImage.Size = bitmap.Size;
                        });
                        Dictionary<Image, int> matches = null;
                        TimeSpan elapsed = TimeSpan.Zero;
                        
                        if (FindMatched(image, out matches, out elapsed)) {
                            Invoke((MethodInvoker)delegate {
                                foreach (var keyValuePair in matches) {
                                    this.listBox1.Items.Add(keyValuePair.Key.LocalPath + " " + keyValuePair.Value);
                                }
                            });
                        }


                    }
                    Thread.Sleep(200);
                    
                }
            }

        }
        
        private bool FindMatched(Mat image, out Dictionary<Image, int> matches, out TimeSpan took) {
            var stopwatch = Stopwatch.StartNew();
            matches = null;

            var matcher = new Matcher();
            var result = matcher.FindMatch(image);
            took = stopwatch.Elapsed;

            if (result != null && result.Any()) {
                matches = result;
                return true;
            }
            else {
                return false;
            }

        }


        private void panel1_Paint(object sender, PaintEventArgs e) {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e) {

        }
    }
}
