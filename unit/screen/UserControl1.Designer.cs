
namespace unit.screen
{
    partial class UserControl1
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
            this.uc1textBox1 = new System.Windows.Forms.RichTextBox();
            this.uc1textBox2 = new System.Windows.Forms.RichTextBox();
            this.uc1textBox3 = new System.Windows.Forms.RichTextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.deviceBox = new System.Windows.Forms.ComboBox();
            this.gatewayBox = new System.Windows.Forms.ComboBox();
            this.deviceNumberBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.emptyDeviceCheck = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.datetypeBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // uc1textBox1
            // 
            this.uc1textBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.uc1textBox1.Location = new System.Drawing.Point(0, 79);
            this.uc1textBox1.Name = "uc1textBox1";
            this.uc1textBox1.Size = new System.Drawing.Size(240, 418);
            this.uc1textBox1.TabIndex = 1;
            this.uc1textBox1.Text = "";
            // 
            // uc1textBox2
            // 
            this.uc1textBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.uc1textBox2.Location = new System.Drawing.Point(246, 79);
            this.uc1textBox2.Name = "uc1textBox2";
            this.uc1textBox2.Size = new System.Drawing.Size(240, 418);
            this.uc1textBox2.TabIndex = 2;
            this.uc1textBox2.Text = "";
            // 
            // uc1textBox3
            // 
            this.uc1textBox3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.uc1textBox3.Location = new System.Drawing.Point(492, 79);
            this.uc1textBox3.Name = "uc1textBox3";
            this.uc1textBox3.Size = new System.Drawing.Size(226, 418);
            this.uc1textBox3.TabIndex = 3;
            this.uc1textBox3.Text = "";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(630, 37);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "get data";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // deviceBox
            // 
            this.deviceBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.deviceBox.FormattingEnabled = true;
            this.deviceBox.Location = new System.Drawing.Point(144, 38);
            this.deviceBox.Name = "deviceBox";
            this.deviceBox.Size = new System.Drawing.Size(192, 23);
            this.deviceBox.TabIndex = 5;
            this.deviceBox.SelectedIndexChanged += new System.EventHandler(this.deviceBox_SelectedIndexChanged);
            // 
            // gatewayBox
            // 
            this.gatewayBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.gatewayBox.FormattingEnabled = true;
            this.gatewayBox.Location = new System.Drawing.Point(9, 38);
            this.gatewayBox.Name = "gatewayBox";
            this.gatewayBox.Size = new System.Drawing.Size(125, 23);
            this.gatewayBox.TabIndex = 6;
            this.gatewayBox.SelectedIndexChanged += new System.EventHandler(this.gatewayBox_SelectedIndexChanged);
            // 
            // deviceNumberBox
            // 
            this.deviceNumberBox.Location = new System.Drawing.Point(450, 37);
            this.deviceNumberBox.Name = "deviceNumberBox";
            this.deviceNumberBox.Size = new System.Drawing.Size(65, 23);
            this.deviceNumberBox.TabIndex = 7;
            this.deviceNumberBox.Text = "30";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 15);
            this.label1.TabIndex = 8;
            this.label1.Text = "Gateway Address";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(144, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 15);
            this.label2.TabIndex = 9;
            this.label2.Text = "Device Address";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(450, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 15);
            this.label3.TabIndex = 10;
            this.label3.Text = "장치 수 ";
            // 
            // emptyDeviceCheck
            // 
            this.emptyDeviceCheck.AutoSize = true;
            this.emptyDeviceCheck.Location = new System.Drawing.Point(526, 38);
            this.emptyDeviceCheck.Name = "emptyDeviceCheck";
            this.emptyDeviceCheck.Size = new System.Drawing.Size(94, 19);
            this.emptyDeviceCheck.TabIndex = 11;
            this.emptyDeviceCheck.Text = "빈 장비 표시";
            this.emptyDeviceCheck.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(347, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 15);
            this.label4.TabIndex = 13;
            this.label4.Text = "데이터 종류";
            // 
            // datetypeBox
            // 
            this.datetypeBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.datetypeBox.FormattingEnabled = true;
            this.datetypeBox.Location = new System.Drawing.Point(347, 38);
            this.datetypeBox.Name = "datetypeBox";
            this.datetypeBox.Size = new System.Drawing.Size(94, 23);
            this.datetypeBox.TabIndex = 12;
            // 
            // UserControl1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label4);
            this.Controls.Add(this.datetypeBox);
            this.Controls.Add(this.emptyDeviceCheck);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.deviceNumberBox);
            this.Controls.Add(this.gatewayBox);
            this.Controls.Add(this.deviceBox);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.uc1textBox3);
            this.Controls.Add(this.uc1textBox2);
            this.Controls.Add(this.uc1textBox1);
            this.Name = "UserControl1";
            this.Size = new System.Drawing.Size(718, 497);
            this.Load += new System.EventHandler(this.UserControl1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button button1;
        public System.Windows.Forms.ComboBox deviceBox;
        public System.Windows.Forms.ComboBox gatewayBox;
        public System.Windows.Forms.RichTextBox uc1textBox1;
        public System.Windows.Forms.RichTextBox uc1textBox2;
        public System.Windows.Forms.RichTextBox uc1textBox3;
        private System.Windows.Forms.TextBox deviceNumberBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox emptyDeviceCheck;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.ComboBox datetypeBox;
    }
}
