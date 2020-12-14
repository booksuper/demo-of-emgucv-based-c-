using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Face;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace facerecognize
{
    public partial class Form1 : Form
    {
        //在方法外面定义各个需要的变量和类，这样所有的方法都可调用相应的变量
        Capture _capture;
        //实例化人脸检测级联分类器
        CascadeClassifier face_detect = new CascadeClassifier(@"haarcascade\haarcascade_frontalface_alt2.xml");//haarcascade_frontalface_alt2
        Mat face_image = new Mat();
        VectorOfInt image_lable = new VectorOfInt();
        VectorOfMat image_data = new VectorOfMat();
        string[] name;
        //实例化局部二值模式人脸识别器
        FaceRecognizer face_recognize = new LBPHFaceRecognizer();
        bool after_train = false;
        bool catch_face = false;
        ASCIIEncoding ascii = new ASCIIEncoding();


        public Form1()
        {
            InitializeComponent();
        }
        //定义摄像头捕捉到图片时调用的方法
        void frame(object sender,EventArgs e)
        {
            Mat scr = _capture.QueryFrame();
            //裁剪图片，提高电脑运行速度
            CvInvoke.Resize(scr, scr, new Size(320, 240));
            //检测人脸
            Rectangle[] recs = face_detect.DetectMultiScale(scr);
            foreach(Rectangle rec in recs)
            {
                CvInvoke.Rectangle(scr, rec, new MCvScalar(0, 0, 255));
                //将人脸区域图片显示在imagebox2中，整张图片显示在imagebox1中
                imageBox2.Image = new Mat(scr, rec);
                imageBox1.Image = scr;

            }

            if(catch_face)
            {
                if(labeltextBox.Text!=null)
                {
                    //这里输入的只能是英文字符，若是中文，则会出现乱码，笔者一开始以为是编码问题，后面尝试好几种解码方法，都没能解决这个问题。
                    string labeltext = labeltextBox.Text;
                    face_image = new Image<Gray, byte>(new Bitmap(imageBox2.Image.Bitmap)).Resize(120, 120, Inter.Area).Mat;
                    //将图片以image_data-文本框中输入的名字-系统时间.jpg格式保存，方便后面将文本框中名字取出
                    face_image.Save("Image_data" + "\\" + labeltext + "_" + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day + ".jpg");
                    MessageBox.Show("保存成功");
                    catch_face = false;
                }
            }

            if(after_train)
            {
                foreach(Rectangle rec in recs)
                {
                    //局部变量text存放人脸名字
                    string text = "";
                    Mat scr_image = new Mat(scr, rec);
                    //将人脸图片缩减，并且转为mat格式
                    scr_image = scr_image.ToImage<Gray, byte>().Resize(120, 120, Inter.Area).Mat;
                    //对人脸进行识别
                    FaceRecognizer.PredictionResult result = face_recognize.Predict(scr_image);
                    //name中存的是上面文本框存的各个人脸对应的名字，与label也是一一对应的
                    text = name[result.Label];
                    if(result.Distance>3000)
                    {
                        text = "Miss";

                    }
                    CvInvoke.PutText(scr, text, rec.Location, FontFace.HersheyComplex, 1, new MCvScalar(0, 0, 255));
                    imageBox1.Image = scr;

                }
            }
        }

        private void imageBox1_Click(object sender, EventArgs e)
        {

        }

        //打开摄像头
        private void opencap_Click(object sender, EventArgs e)
        {
            _capture = new Capture(0);
            Application.Idle += frame;

        }

        //保存图片，并将catch_face重置为true
        private void savebtn_Click(object sender, EventArgs e)
        {
            catch_face = true;
        }

        //开始训练图片
        private void trainbtn_Click(object sender, EventArgs e)
        {
            //将iamge_data中获得所有文件存在path中，后面对其进行切片处理，并且将取得的人脸名字存在name中
            string[] path = Directory.GetFiles("Image_data");
            Mat[] data = new Mat[path.Length];
            int[] label = new int[path.Length];
            name = new string[path.Length];
            for(int i =0;i<path.Length;i++)
            {
                Image<Gray, byte> image = new Image<Gray, byte>(path[i]);
                data[i] = image.Mat;
                label[i] = i;
                //返回最后出现\\的索引
                int a = path[i].LastIndexOf('\\');
                //返回最后出现_的索引
                int b = path[i].LastIndexOf('_');
                //从a+1处开始截取字符，截取b-a-1个，这就是人脸名字在的区域
                name[i] = path[i].Substring(a+1, b - a - 1);

            }
            image_data.Push(data);
            image_lable.Push(label);
            //训练图片
            face_recognize.Train(image_data, image_lable);
            face_recognize.Save("face\\data.txt");
            MessageBox.Show("训练成功");
            after_train = true;
            

        }
    }
}
