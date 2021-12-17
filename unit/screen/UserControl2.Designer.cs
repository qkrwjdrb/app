
namespace unit.screen
{
    partial class UserControl2
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
            this.gatewayListBox = new System.Windows.Forms.ListBox();
            this.deviceListBox = new System.Windows.Forms.ListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.addGateBox = new System.Windows.Forms.TextBox();
            this.addDevBox = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.gatewayDeleteButton = new System.Windows.Forms.Button();
            this.deviceDeleteButton = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.deviceBox = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.gatewayBox = new System.Windows.Forms.ComboBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // gatewayListBox
            // 
            this.gatewayListBox.FormattingEnabled = true;
            this.gatewayListBox.ItemHeight = 15;
            this.gatewayListBox.Location = new System.Drawing.Point(0, 103);
            this.gatewayListBox.Name = "gatewayListBox";
            this.gatewayListBox.Size = new System.Drawing.Size(228, 364);
            this.gatewayListBox.TabIndex = 1;
            // 
            // deviceListBox
            // 
            this.deviceListBox.FormattingEnabled = true;
            this.deviceListBox.ItemHeight = 15;
            this.deviceListBox.Location = new System.Drawing.Point(234, 103);
            this.deviceListBox.Name = "deviceListBox";
            this.deviceListBox.Size = new System.Drawing.Size(228, 364);
            this.deviceListBox.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(352, 22);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(110, 61);
            this.button1.TabIndex = 61;
            this.button1.Text = "add";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // addGateBox
            // 
            this.addGateBox.Location = new System.Drawing.Point(154, 24);
            this.addGateBox.Name = "addGateBox";
            this.addGateBox.Size = new System.Drawing.Size(192, 23);
            this.addGateBox.TabIndex = 59;
            // 
            // addDevBox
            // 
            this.addDevBox.Location = new System.Drawing.Point(154, 61);
            this.addDevBox.Name = "addDevBox";
            this.addDevBox.Size = new System.Drawing.Size(192, 23);
            this.addDevBox.TabIndex = 60;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label12.Location = new System.Drawing.Point(5, 26);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(143, 20);
            this.label12.TabIndex = 58;
            this.label12.Text = "Add Gateway (HEX)";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label13.Location = new System.Drawing.Point(5, 62);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(130, 20);
            this.label13.TabIndex = 57;
            this.label13.Text = "Add Device (HEX)";
            // 
            // gatewayDeleteButton
            // 
            this.gatewayDeleteButton.Location = new System.Drawing.Point(0, 474);
            this.gatewayDeleteButton.Name = "gatewayDeleteButton";
            this.gatewayDeleteButton.Size = new System.Drawing.Size(228, 23);
            this.gatewayDeleteButton.TabIndex = 62;
            this.gatewayDeleteButton.Text = "delete";
            this.gatewayDeleteButton.UseVisualStyleBackColor = true;
            this.gatewayDeleteButton.Click += new System.EventHandler(this.button2_Click);
            // 
            // deviceDeleteButton
            // 
            this.deviceDeleteButton.Location = new System.Drawing.Point(234, 474);
            this.deviceDeleteButton.Name = "deviceDeleteButton";
            this.deviceDeleteButton.Size = new System.Drawing.Size(228, 23);
            this.deviceDeleteButton.TabIndex = 63;
            this.deviceDeleteButton.Text = "delete";
            this.deviceDeleteButton.UseVisualStyleBackColor = true;
            this.deviceDeleteButton.Click += new System.EventHandler(this.button3_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(499, 82);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(98, 15);
            this.label5.TabIndex = 66;
            this.label5.Text = "Gateway Address";
            // 
            // deviceBox
            // 
            this.deviceBox.FormattingEnabled = true;
            this.deviceBox.Location = new System.Drawing.Point(499, 288);
            this.deviceBox.Name = "deviceBox";
            this.deviceBox.Size = new System.Drawing.Size(157, 23);
            this.deviceBox.TabIndex = 64;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(499, 270);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(89, 15);
            this.label6.TabIndex = 67;
            this.label6.Text = "Device Address";
            // 
            // gatewayBox
            // 
            this.gatewayBox.FormattingEnabled = true;
            this.gatewayBox.Location = new System.Drawing.Point(499, 103);
            this.gatewayBox.Name = "gatewayBox";
            this.gatewayBox.Size = new System.Drawing.Size(157, 23);
            this.gatewayBox.TabIndex = 65;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(499, 132);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(104, 23);
            this.textBox1.TabIndex = 68;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(499, 317);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(104, 23);
            this.textBox2.TabIndex = 69;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(609, 131);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 70;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(609, 316);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 71;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // UserControl2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.deviceBox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.gatewayBox);
            this.Controls.Add(this.deviceDeleteButton);
            this.Controls.Add(this.gatewayDeleteButton);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.addGateBox);
            this.Controls.Add(this.addDevBox);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.deviceListBox);
            this.Controls.Add(this.gatewayListBox);
            this.Name = "UserControl2";
            this.Size = new System.Drawing.Size(718, 497);
            this.Load += new System.EventHandler(this.UserControl2_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox addGateBox;
        private System.Windows.Forms.TextBox addDevBox;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button gatewayDeleteButton;
        private System.Windows.Forms.Button deviceDeleteButton;
        public System.Windows.Forms.ListBox deviceListBox;
        public System.Windows.Forms.ListBox gatewayListBox;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.ComboBox deviceBox;
        private System.Windows.Forms.Label label6;
        public System.Windows.Forms.ComboBox gatewayBox;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
    }
}
