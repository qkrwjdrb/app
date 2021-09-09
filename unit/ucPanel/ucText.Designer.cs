
namespace unit.ucPanel
{
    partial class ucText
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
            this.textBox12 = new System.Windows.Forms.TextBox();
            this.button8 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBox12
            // 
            this.textBox12.Location = new System.Drawing.Point(19, 104);
            this.textBox12.MaxLength = 10;
            this.textBox12.Name = "textBox12";
            this.textBox12.Size = new System.Drawing.Size(192, 23);
            this.textBox12.TabIndex = 53;
            // 
            // button8
            // 
            this.button8.Cursor = System.Windows.Forms.Cursors.SizeAll;
            this.button8.Location = new System.Drawing.Point(227, 104);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(75, 23);
            this.button8.TabIndex = 52;
            this.button8.Text = "text";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // ucText
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.textBox12);
            this.Controls.Add(this.button8);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.Name = "ucText";
            this.Size = new System.Drawing.Size(318, 230);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox textBox12;
        private System.Windows.Forms.Button button8;
    }
}
