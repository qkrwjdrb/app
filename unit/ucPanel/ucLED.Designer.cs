
namespace unit.ucPanel
{
    partial class ucLED
    {
        /// <summary> 
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary> 
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.button16 = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.green1 = new System.Windows.Forms.Label();
            this.blue1 = new System.Windows.Forms.Label();
            this.red1 = new System.Windows.Forms.Label();
            this.red2 = new System.Windows.Forms.Label();
            this.blue2 = new System.Windows.Forms.Label();
            this.green2 = new System.Windows.Forms.Label();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // button16
            // 
            this.button16.Font = new System.Drawing.Font("맑은 고딕", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.button16.Location = new System.Drawing.Point(186, 140);
            this.button16.Name = "button16";
            this.button16.Size = new System.Drawing.Size(110, 27);
            this.button16.TabIndex = 49;
            this.button16.Text = "LED ON";
            this.button16.UseVisualStyleBackColor = true;
            this.button16.Click += new System.EventHandler(this.button16_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(167, 36);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(129, 23);
            this.comboBox1.TabIndex = 50;
            // 
            // comboBox2
            // 
            this.comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(24, 36);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(129, 23);
            this.comboBox2.TabIndex = 50;
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.LinkColor = System.Drawing.Color.DodgerBlue;
            this.linkLabel1.Location = new System.Drawing.Point(221, 205);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(75, 15);
            this.linkLabel1.TabIndex = 52;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "세부 색 설정";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 79);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(21, 15);
            this.label2.TabIndex = 53;
            this.label2.Text = "R :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 96);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(22, 15);
            this.label3.TabIndex = 54;
            this.label3.Text = "G :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(24, 113);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(21, 15);
            this.label4.TabIndex = 54;
            this.label4.Text = "B :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(167, 113);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(21, 15);
            this.label5.TabIndex = 57;
            this.label5.Text = "B :";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(167, 96);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(22, 15);
            this.label6.TabIndex = 56;
            this.label6.Text = "G :";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(167, 79);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(21, 15);
            this.label7.TabIndex = 55;
            this.label7.Text = "R :";
            // 
            // green1
            // 
            this.green1.AutoSize = true;
            this.green1.Location = new System.Drawing.Point(51, 96);
            this.green1.Name = "green1";
            this.green1.Size = new System.Drawing.Size(14, 15);
            this.green1.TabIndex = 58;
            this.green1.Text = "0";
            this.green1.Click += new System.EventHandler(this.label8_Click);
            // 
            // blue1
            // 
            this.blue1.AutoSize = true;
            this.blue1.Location = new System.Drawing.Point(51, 113);
            this.blue1.Name = "blue1";
            this.blue1.Size = new System.Drawing.Size(14, 15);
            this.blue1.TabIndex = 58;
            this.blue1.Text = "0";
            this.blue1.Click += new System.EventHandler(this.label9_Click);
            // 
            // red1
            // 
            this.red1.AutoSize = true;
            this.red1.Location = new System.Drawing.Point(51, 79);
            this.red1.Name = "red1";
            this.red1.Size = new System.Drawing.Size(14, 15);
            this.red1.TabIndex = 58;
            this.red1.Text = "0";
            this.red1.Click += new System.EventHandler(this.label10_Click);
            // 
            // red2
            // 
            this.red2.AutoSize = true;
            this.red2.Location = new System.Drawing.Point(194, 79);
            this.red2.Name = "red2";
            this.red2.Size = new System.Drawing.Size(14, 15);
            this.red2.TabIndex = 59;
            this.red2.Text = "0";
            // 
            // blue2
            // 
            this.blue2.AutoSize = true;
            this.blue2.Location = new System.Drawing.Point(194, 113);
            this.blue2.Name = "blue2";
            this.blue2.Size = new System.Drawing.Size(14, 15);
            this.blue2.TabIndex = 60;
            this.blue2.Text = "0";
            // 
            // green2
            // 
            this.green2.AutoSize = true;
            this.green2.Location = new System.Drawing.Point(194, 96);
            this.green2.Name = "green2";
            this.green2.Size = new System.Drawing.Size(14, 15);
            this.green2.TabIndex = 61;
            this.green2.Text = "0";
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(24, 157);
            this.trackBar1.Minimum = 1;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(117, 45);
            this.trackBar1.TabIndex = 62;
            this.trackBar1.Value = 1;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("맑은 고딕", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.button1.Location = new System.Drawing.Point(186, 173);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(110, 27);
            this.button1.TabIndex = 49;
            this.button1.Text = "LED OFF";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // ucLED
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.red2);
            this.Controls.Add(this.blue2);
            this.Controls.Add(this.green2);
            this.Controls.Add(this.red1);
            this.Controls.Add(this.blue1);
            this.Controls.Add(this.green1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button16);
            this.Name = "ucLED";
            this.Size = new System.Drawing.Size(318, 230);
            this.Load += new System.EventHandler(this.ucScreen3_Load);
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ComboBox comboBox6;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox comboBox7;
        private System.Windows.Forms.ListBox listBox4;
        private System.Windows.Forms.ListBox listBox3;
        private System.Windows.Forms.Button button16;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Label green1;
        private System.Windows.Forms.Label blue1;
        private System.Windows.Forms.Label red1;
        private System.Windows.Forms.Label red2;
        private System.Windows.Forms.Label blue2;
        private System.Windows.Forms.Label green2;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Button button1;
    }
}
