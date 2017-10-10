using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows;

using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;
using Emgu.Util;

using SharpGL.SceneGraph;
using SharpGL;
using Emgu.CV.Util;

namespace GraphicsVisionFusion
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private VideoCapture cap = null;
        private Mat frame;

        private List<Vertex> points;
        private bool rotateIt = false;
        private float rotation = 0.0f;

        double low1 = 28;
        double up1 = 32;
        Color drawingColor;

        public MainWindow()
        {
            InitializeComponent();

            try
            {
                cap = new VideoCapture(2);
                cap.ImageGrabbed += ProcessFrame;
                cap.Start();
            }
            catch (NullReferenceException e)
            {
                MessageBox.Show(e.Message);
            }

            frame = new Mat();

            points = new List<Vertex>();
            //points.Add(new Vertex(0.0f, 1.0f, 0.0f));

            drawingColor = Color.White;
        }

        private void ProcessFrame(object sender, EventArgs e)
        {
            if (cap != null && cap.Ptr != IntPtr.Zero)
            {
                cap.Retrieve(frame);

                IImage hsv = (IImage)new Mat();
                CvInvoke.CvtColor(frame, hsv, ColorConversion.Bgr2Hsv);
                IOutputArray mask = (IImage)new Mat();
                //CvInvoke.ExtractChannel(hsv, mask, 0);
                //IImage s = (IImage)new Mat();
                //CvInvoke.ExtractChannel(hsv, s, 1);


                ScalarArray lower = new ScalarArray(new MCvScalar(low1, 40, 40));
                ScalarArray upper = new ScalarArray(new MCvScalar(up1, 255, 255));
                //ScalarArray lower = new ScalarArray(170);
                //ScalarArray upper = new ScalarArray(180);
                CvInvoke.InRange(hsv, lower, upper, mask);

                //CvInvoke.BitwiseNot(mask, mask);
                //CvInvoke.Threshold(s, s, 10, 255, ThresholdType.Binary);
                //CvInvoke.BitwiseAnd(mask, s, mask, null);


                IOutputArray result = (IImage)new Mat();
                CvInvoke.BitwiseAnd(frame, frame, result, mask);
                IInputArray kernel = new Matrix<byte>(new byte[5, 5] {
                    {1,1,1,1,1},
                    {1,1,1,1,1},
                    {1,1,1,1,1},
                    {1,1,1,1,1},
                    {1,1,1,1,1}
                });
                IOutputArray opening = (IImage)new Mat();
                CvInvoke.MorphologyEx(result, opening, MorphOp.Open, kernel, new System.Drawing.Point(-1, -1), 1, BorderType.Default, CvInvoke.MorphologyDefaultBorderValue);
                IOutputArray smooth = (IImage)new Mat();
                CvInvoke.MedianBlur(opening, smooth, 35);
                IOutputArray framegray = (IImage)new Mat();
                CvInvoke.CvtColor(smooth, framegray, ColorConversion.Bgr2Gray);
                IInputOutputArray threshold = (IImage)new Mat();
                CvInvoke.Threshold(framegray, threshold, 10, 255, ThresholdType.Binary);
                VectorOfVectorOfPoint contours = new VectorOfVectorOfPoint();
                CvInvoke.FindContours(threshold, contours, null, RetrType.List, ChainApproxMethod.ChainApproxSimple);
                //CvInvoke.DrawContours(frame, contours, -1, new MCvScalar(0,0,255), 1);
                
                if(contours.Size > 0)
                {
                    //Console.WriteLine(CvInvoke.ContourArea(contours[0]));
                    CircleF cf = CvInvoke.MinEnclosingCircle(contours[0]);
                    points.Add(new Vertex( (cf.Center.X - 320), -(cf.Center.Y - 240), ((float)cf.Area/50)) );
                    //Console.WriteLine(cf.Center.X.ToString() + " " + cf.Center.Y.ToString() + " " + cf.Area.ToString());
                    //xa.Text = cf.Center.X.ToString();
                    //ya.Text = cf.Center.Y.ToString();
                    //za.Text = cf.Area.ToString();
                }

                imgBox.Image = (IImage)frame;
                
            }
        }

        private void openGLControl_OpenGLDraw(object sender, OpenGLEventArgs args)
        {
            OpenGL gl = openGLControl.OpenGL;
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            gl.LoadIdentity();
            //gl.Ortho(0, 640, 0, 480, 0, 500);
            //gl.Translate(20,0,2);
            gl.Rotate(rotation, 0.0f, 1.0f, 0.0f);

            gl.Color(drawingColor);
            gl.LineWidth(3);

            gl.PushMatrix();
            gl.Begin(OpenGL.GL_LINE_STRIP);
            foreach (Vertex v in points)
                gl.Vertex(v);
            gl.End();
            gl.PopMatrix();

            gl.PushMatrix();
            //gl.Begin(OpenGL.GL_POLYGON);
            gl.Begin(OpenGL.GL_POINTS);
                gl.Vertex(0, 0, 0);
                gl.Vertex(320, 0, 0);
                gl.Vertex(-320, 0, 0);
                gl.Vertex(0, 240, 0);
                gl.Vertex(0, -240, 0);
            gl.End();
            gl.PopMatrix();

            if (rotateIt)
                rotation += -3.0f;
        }

        private void openGLControl_OpenGLInitialized(object sender, OpenGLEventArgs args)
        {
            OpenGL gl = openGLControl.OpenGL;
            gl.ClearColor(0, 0, 0, 0);
        }

        private void openGLControl_Resized(object sender, OpenGLEventArgs args)
        {
            OpenGL gl = openGLControl.OpenGL;
            gl.MatrixMode(OpenGL.GL_PROJECTION);
            gl.LoadIdentity();
            gl.Perspective(60.0f, (double)Width / (double)Height, 0.01, 1000.0);
            gl.LookAt(0, 0, 500, 0, 0, 0, 0, 1, 0);
            gl.MatrixMode(OpenGL.GL_MODELVIEW);
        }

        private void destroyAll(object sender, EventArgs e)
        {
            cap.Stop();
            cap.Dispose();
        }

        private void rotateButton(object sender, RoutedEventArgs e)
        {
            rotateIt = !rotateIt;
        }

        private void clearButton(object sender, RoutedEventArgs e)
        {
            points.Clear();
        }

        private void col1up_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            //up1 = col1up.Value;
        }

        private void col1low_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            //low1 = col1low.Value;
        }

        private void ColorPicker_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<System.Windows.Media.Color?> e)
        {
            drawingColor = Color.FromName(colorPicker.SelectedColorText);
        }
    }
}
