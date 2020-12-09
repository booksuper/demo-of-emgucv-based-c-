namespace cornors_detect
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.imageBox1 = new Emgu.CV.UI.ImageBox();
            this.loadimgbtn = new System.Windows.Forms.Button();
            this.correcogbtn = new System.Windows.Forms.Button();
            this.imageBox2 = new Emgu.CV.UI.ImageBox();
            this.loadimg2btn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // imageBox1
            // 
            this.imageBox1.Location = new System.Drawing.Point(1, 12);
            this.imageBox1.Name = "imageBox1";
            this.imageBox1.Size = new System.Drawing.Size(250, 312);
            this.imageBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.imageBox1.TabIndex = 2;
            this.imageBox1.TabStop = false;
            // 
            // loadimgbtn
            // 
            this.loadimgbtn.Location = new System.Drawing.Point(66, 359);
            this.loadimgbtn.Name = "loadimgbtn";
            this.loadimgbtn.Size = new System.Drawing.Size(93, 23);
            this.loadimgbtn.TabIndex = 3;
            this.loadimgbtn.Text = "加载模板图片";
            this.loadimgbtn.UseVisualStyleBackColor = true;
            this.loadimgbtn.Click += new System.EventHandler(this.loadimgbtn_Click);
            // 
            // correcogbtn
            // 
            this.correcogbtn.Location = new System.Drawing.Point(858, 359);
            this.correcogbtn.Name = "correcogbtn";
            this.correcogbtn.Size = new System.Drawing.Size(75, 23);
            this.correcogbtn.TabIndex = 4;
            this.correcogbtn.Text = "开始匹配";
            this.correcogbtn.UseVisualStyleBackColor = true;
            this.correcogbtn.Click += new System.EventHandler(this.correcogbtn_Click);
            // 
            // imageBox2
            // 
            this.imageBox2.Location = new System.Drawing.Point(268, 12);
            this.imageBox2.Name = "imageBox2";
            this.imageBox2.Size = new System.Drawing.Size(684, 341);
            this.imageBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.imageBox2.TabIndex = 2;
            this.imageBox2.TabStop = false;
            // 
            // loadimg2btn
            // 
            this.loadimg2btn.Location = new System.Drawing.Point(569, 359);
            this.loadimg2btn.Name = "loadimg2btn";
            this.loadimg2btn.Size = new System.Drawing.Size(107, 23);
            this.loadimg2btn.TabIndex = 5;
            this.loadimg2btn.Text = "加载目标图片";
            this.loadimg2btn.UseVisualStyleBackColor = true;
            this.loadimg2btn.Click += new System.EventHandler(this.loadimg2btn_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(980, 450);
            this.Controls.Add(this.loadimg2btn);
            this.Controls.Add(this.imageBox2);
            this.Controls.Add(this.correcogbtn);
            this.Controls.Add(this.loadimgbtn);
            this.Controls.Add(this.imageBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.imageBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Emgu.CV.UI.ImageBox imageBox1;
        private System.Windows.Forms.Button loadimgbtn;
        private System.Windows.Forms.Button correcogbtn;
        private Emgu.CV.UI.ImageBox imageBox2;
        private System.Windows.Forms.Button loadimg2btn;
    }
}

