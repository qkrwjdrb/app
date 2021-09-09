
namespace unit.device
{
    partial class ucAdd
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
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.addGateBox = new System.Windows.Forms.TextBox();
            this.addDevBox = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label12.Location = new System.Drawing.Point(11, 24);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(100, 20);
            this.label12.TabIndex = 51;
            this.label12.Text = "Add Gateway";
            this.label12.Click += new System.EventHandler(this.label12_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label13.Location = new System.Drawing.Point(11, 60);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(87, 20);
            this.label13.TabIndex = 50;
            this.label13.Text = "Add Device";
            // 
            // addGateBox
            // 
            this.addGateBox.Location = new System.Drawing.Point(131, 22);
            this.addGateBox.Name = "addGateBox";
            this.addGateBox.Size = new System.Drawing.Size(174, 23);
            this.addGateBox.TabIndex = 54;
            this.addGateBox.TextChanged += new System.EventHandler(this.addGateBox_TextChanged);
            // 
            // addDevBox
            // 
            this.addDevBox.Location = new System.Drawing.Point(131, 59);
            this.addDevBox.Name = "addDevBox";
            this.addDevBox.Size = new System.Drawing.Size(174, 23);
            this.addDevBox.TabIndex = 55;
            this.addDevBox.TextChanged += new System.EventHandler(this.addDevBox_TextChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(230, 88);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 56;
            this.button1.Text = "add";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // add
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.button1);
            this.Controls.Add(this.addGateBox);
            this.Controls.Add(this.addDevBox);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label13);
            this.Name = "add";
            this.Size = new System.Drawing.Size(318, 125);
            this.Load += new System.EventHandler(this.add_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox addGateBox;
        private System.Windows.Forms.TextBox addDevBox;
        private System.Windows.Forms.Button button1;
    }
}
