
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
            this.label1 = new System.Windows.Forms.Label();
            this.uc1textBox1 = new System.Windows.Forms.RichTextBox();
            this.uc1textBox2 = new System.Windows.Forms.RichTextBox();
            this.uc1textBox3 = new System.Windows.Forms.RichTextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(14, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "1";
            // 
            // uc1textBox1
            // 
            this.uc1textBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.uc1textBox1.Location = new System.Drawing.Point(0, 50);
            this.uc1textBox1.Name = "uc1textBox1";
            this.uc1textBox1.Size = new System.Drawing.Size(240, 447);
            this.uc1textBox1.TabIndex = 1;
            this.uc1textBox1.Text = "";
            // 
            // uc1textBox2
            // 
            this.uc1textBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.uc1textBox2.Location = new System.Drawing.Point(246, 50);
            this.uc1textBox2.Name = "uc1textBox2";
            this.uc1textBox2.Size = new System.Drawing.Size(240, 447);
            this.uc1textBox2.TabIndex = 2;
            this.uc1textBox2.Text = "";
            // 
            // uc1textBox3
            // 
            this.uc1textBox3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.uc1textBox3.Location = new System.Drawing.Point(492, 50);
            this.uc1textBox3.Name = "uc1textBox3";
            this.uc1textBox3.Size = new System.Drawing.Size(226, 447);
            this.uc1textBox3.TabIndex = 3;
            this.uc1textBox3.Text = "";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(404, 21);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(205, 21);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(192, 23);
            this.comboBox1.TabIndex = 5;
            // 
            // comboBox2
            // 
            this.comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(0, 21);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(192, 23);
            this.comboBox2.TabIndex = 6;
            // 
            // UserControl1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.uc1textBox3);
            this.Controls.Add(this.uc1textBox2);
            this.Controls.Add(this.uc1textBox1);
            this.Controls.Add(this.label1);
            this.Name = "UserControl1";
            this.Size = new System.Drawing.Size(718, 497);
            this.Load += new System.EventHandler(this.UserControl1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        public System.Windows.Forms.ComboBox comboBox1;
        public System.Windows.Forms.ComboBox comboBox2;
        public System.Windows.Forms.RichTextBox uc1textBox1;
        public System.Windows.Forms.RichTextBox uc1textBox2;
        public System.Windows.Forms.RichTextBox uc1textBox3;
    }
}
