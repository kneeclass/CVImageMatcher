using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;
using CVImageMatcher.Core;
using Emgu.CV;
using Emgu.CV.CvEnum;

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

            var cam = new Capture(0);
            cam.SetCaptureProperty(CapProp.FrameWidth, 640);
            cam.SetCaptureProperty(CapProp.FrameHeight, 480);
            while (true) {

                var success = cam.Grab(); ;
                if (!success) throw new Exception("Failed to grab image");
                Mat frame = null;
                cam.Retrieve(frame);
                CvInvoke.CvtColor(frame, frame, ColorConversion.BayerGr2Gray);

                var bitmap = frame.Bitmap;
                Invoke((MethodInvoker)delegate {
                    listBox1.Items.Clear();
                    camImage.BackgroundImage = bitmap;
                    camImage.Size = bitmap.Size;
                });
                Dictionary<CVImageMatcher.Core.Models.Image, int> matches = null;
                TimeSpan elapsed = TimeSpan.Zero;

                if (FindMatched(frame, out matches, out elapsed)) {
                    Invoke((MethodInvoker)delegate {
                        foreach (var keyValuePair in matches) {
                            this.listBox1.Items.Add(keyValuePair.Key.LocalPath + " " + keyValuePair.Value);
                        }
                    });
                }

                Thread.Sleep(200);
            }
            


        }

        
        private bool FindMatched(Mat image, out Dictionary<CVImageMatcher.Core.Models.Image, int> matches, out TimeSpan took) {
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
