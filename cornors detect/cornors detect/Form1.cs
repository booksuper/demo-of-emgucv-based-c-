using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.Features2D;
using Emgu.CV.CvEnum;
using Emgu.CV.Util;
using Emgu.CV.XFeatures2D;




namespace cornors_detect
{
    public partial class Form1 : Form
    {
       
        public Form1()
        {
            InitializeComponent();
        }
        //定义所需的变量及实例化对象
        Mat scr = new Mat();
        Mat dst = new Mat();
        VectorOfKeyPoint scr_keypoints = new VectorOfKeyPoint();
        VectorOfKeyPoint dst_keypoints = new VectorOfKeyPoint();
        Mat scr_descriptor = new Mat();
        Mat dst_descriptor = new Mat();
        Mat result = new Mat();
        VectorOfVectorOfDMatch matchs = new VectorOfVectorOfDMatch();
        Mat transmat = new Mat();
        BFMatcher match = new BFMatcher(DistanceType.L2);

        //实例化sift和surf对象
        SIFT sift = new SIFT();
        SURF surf = new SURF(300);
        //实例化其他角点检测对象，可根据需要自行选择
        //GFTTDetector _gftt = new GFTTDetector();
        //AKAZE _akaze = new AKAZE();
        //Brisk _brisk = new Brisk();
        //SimpleBlobDetector blob = new SimpleBlobDetector();
        //ORBDetector orb = new ORBDetector();
        //BriefDescriptorExtractor brief = new BriefDescriptorExtractor();
        
       



        private void loadimgbtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            if(op.ShowDialog()==DialogResult.OK)
            {
                scr = new Mat(op.FileName,LoadImageType.AnyColor);
               
            }
            //显示加载的模板图片
            imageBox1.Image = scr;
            
        }

        private void correcogbtn_Click(object sender, EventArgs e)
        {
            //用sift或者surf方法获得模板和匹配图像的关键点和描述子
            //surf.DetectAndCompute(scr, null, scr_keypoints, scr_descriptor, false);
            //surf.DetectAndCompute(dst, null, dst_keypoints, dst_descriptor, false);
            sift.DetectAndCompute(scr, null, scr_keypoints, scr_descriptor, false);
            sift.DetectAndCompute(dst, null, dst_keypoints, dst_descriptor, false);
            //在模板和匹配图像上分别绘制关键点
            Features2DToolbox.DrawKeypoints(scr, scr_keypoints, scr, new Bgr(0, 0, 255), Features2DToolbox.KeypointDrawType.Default);
            Features2DToolbox.DrawKeypoints(dst, dst_keypoints, dst, new Bgr(0, 0, 255), Features2DToolbox.KeypointDrawType.Default);
           
            //将模板描述子加入匹配方法中
            match.Add(scr_descriptor);
            //利用knn方法对模板和匹配图像进行关键点匹配
            match.KnnMatch(dst_descriptor, matchs,2,null);
            
            //过滤一些匹配不准确的点
            Mat mask = new Mat(matchs.Size, 1, DepthType.Cv8U, 1);
            mask.SetTo(new MCvScalar(1));
            //投票阈值筛选掉重复的点
            Features2DToolbox.VoteForUniqueness(matchs, 0.8, mask);
            //进行尺度旋转筛选
            Features2DToolbox.VoteForSizeAndOrientation(scr_keypoints, dst_keypoints, matchs, mask, 1.5, 20);
            //绘制匹配图像
            Features2DToolbox.DrawMatches(scr, scr_keypoints, dst, dst_keypoints, matchs, result, new MCvScalar(255, 255, 255), new MCvScalar(0, 0, 255), mask);
            
            //得到模板和匹配图片的仿射矩阵
            transmat = Features2DToolbox.GetHomographyMatrixFromMatchedFeatures(scr_keypoints, dst_keypoints, matchs, mask, 3);
            // 得到模板图像中书的矩形区域
            Rectangle rec = new Rectangle(Point.Empty, scr.Size);
            //提取矩形区域的四个顶点，为后面定位匹配图像中的书做准备
            PointF[] pts = new PointF[]
            {
                new PointF(rec.Left,rec.Bottom),
                new PointF(rec.Right,rec.Bottom),
                new PointF(rec.Right,rec.Top),
                new PointF(rec.Left,rec.Top)
                
            };
            //定位匹配图像中的书
            pts = CvInvoke.PerspectiveTransform(pts, transmat);
            //绘制出匹配图像中的书
            Point[] points = Array.ConvertAll<PointF, Point>(pts, Point.Round);
            using (VectorOfPoint vp = new VectorOfPoint(points))
            {
                CvInvoke.Polylines(result, vp, true, new MCvScalar(255, 0, 0), 4);
            }

            imageBox1.Image = scr;
            imageBox2.Image = result;

        }

        private void loadimg2btn_Click(object sender, EventArgs e)
        {
            OpenFileDialog op2 = new OpenFileDialog();
            if(op2.ShowDialog()==DialogResult.OK)
            {
                dst = new Mat(op2.FileName, LoadImageType.AnyColor);

            }
            //加载匹配图片
            imageBox2.Image = dst;

        }
    }
}
