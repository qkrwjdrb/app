
namespace unit.ucPanel
{
    partial class ucModbus
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
            this.label4 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.slaBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.funBox = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.staBox = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.lenBox = new System.Windows.Forms.TextBox();
            this.crcBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(22, 27);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(81, 15);
            this.label4.TabIndex = 41;
            this.label4.Text = "Slave Address";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.CornflowerBlue;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.button1.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.button1.Location = new System.Drawing.Point(186, 195);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(110, 27);
            this.button1.TabIndex = 40;
            this.button1.Text = "send";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // slaBox
            // 
            this.slaBox.Location = new System.Drawing.Point(116, 24);
            this.slaBox.Name = "slaBox";
            this.slaBox.Size = new System.Drawing.Size(180, 23);
            this.slaBox.TabIndex = 42;
            this.slaBox.TextChanged += new System.EventHandler(this.textBox5_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(22, 60);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(86, 15);
            this.label5.TabIndex = 43;
            this.label5.Text = "Function Code";
            // 
            // funBox
            // 
            this.funBox.FormattingEnabled = true;
            this.funBox.Location = new System.Drawing.Point(116, 57);
            this.funBox.Name = "funBox";
            this.funBox.Size = new System.Drawing.Size(180, 23);
            this.funBox.TabIndex = 44;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(22, 94);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(78, 15);
            this.label6.TabIndex = 45;
            this.label6.Text = "Start Address";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.label7.Location = new System.Drawing.Point(22, 128);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(44, 15);
            this.label7.TabIndex = 46;
            this.label7.Text = "Length";
            // 
            // staBox
            // 
            this.staBox.Location = new System.Drawing.Point(116, 91);
            this.staBox.Name = "staBox";
            this.staBox.Size = new System.Drawing.Size(180, 23);
            this.staBox.TabIndex = 47;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.label10.Location = new System.Drawing.Point(22, 160);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(30, 15);
            this.label10.TabIndex = 50;
            this.label10.Text = "CRC";
            // 
            // lenBox
            // 
            this.lenBox.Location = new System.Drawing.Point(116, 125);
            this.lenBox.Name = "lenBox";
            this.lenBox.Size = new System.Drawing.Size(180, 23);
            this.lenBox.TabIndex = 48;
            // 
            // crcBox
            // 
            this.crcBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.crcBox.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.crcBox.Location = new System.Drawing.Point(116, 157);
            this.crcBox.MaxLength = 4;
            this.crcBox.Name = "crcBox";
            this.crcBox.Size = new System.Drawing.Size(180, 23);
            this.crcBox.TabIndex = 49;
            // 
            // ucModbus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.slaBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.funBox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.staBox);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.lenBox);
            this.Controls.Add(this.crcBox);
            this.Name = "ucModbus";
            this.Size = new System.Drawing.Size(318, 230);
            this.Load += new System.EventHandler(this.ucScreen1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox slaBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox funBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox staBox;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox lenBox;
        private System.Windows.Forms.TextBox crcBox;
    }
}
