﻿
namespace unit
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.button14 = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.button15 = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.button17 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button18 = new System.Windows.Forms.Button();
            this.button20 = new System.Windows.Forms.Button();
            this.button19 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.button22 = new System.Windows.Forms.Button();
            this.button21 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.SuspendLayout();
            // 
            // richTextBox2
            // 
            this.richTextBox2.Location = new System.Drawing.Point(6, 19);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.Size = new System.Drawing.Size(273, 421);
            this.richTextBox2.TabIndex = 5;
            this.richTextBox2.Text = "TxMbRtu";
            this.richTextBox2.TextChanged += new System.EventHandler(this.richTextBox2_TextChanged);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.richTextBox2);
            this.groupBox5.Controls.Add(this.button14);
            this.groupBox5.Controls.Add(this.richTextBox1);
            this.groupBox5.Controls.Add(this.button15);
            this.groupBox5.Location = new System.Drawing.Point(366, 75);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(564, 485);
            this.groupBox5.TabIndex = 51;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "RESPONSE box";
            this.groupBox5.Enter += new System.EventHandler(this.groupBox5_Enter);
            // 
            // button14
            // 
            this.button14.Location = new System.Drawing.Point(6, 446);
            this.button14.Name = "button14";
            this.button14.Size = new System.Drawing.Size(273, 23);
            this.button14.TabIndex = 27;
            this.button14.Text = "Clear";
            this.button14.UseVisualStyleBackColor = true;
            this.button14.Click += new System.EventHandler(this.button14_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(284, 19);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(273, 421);
            this.richTextBox1.TabIndex = 4;
            this.richTextBox1.Text = "RxMbRtu";
            this.richTextBox1.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);
            // 
            // button15
            // 
            this.button15.Location = new System.Drawing.Point(284, 446);
            this.button15.Name = "button15";
            this.button15.Size = new System.Drawing.Size(273, 23);
            this.button15.TabIndex = 28;
            this.button15.Text = "Clear";
            this.button15.UseVisualStyleBackColor = true;
            this.button15.Click += new System.EventHandler(this.button15_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.button17);
            this.groupBox4.Controls.Add(this.panel1);
            this.groupBox4.Controls.Add(this.button18);
            this.groupBox4.Controls.Add(this.button20);
            this.groupBox4.Controls.Add(this.button19);
            this.groupBox4.Location = new System.Drawing.Point(11, 265);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(345, 295);
            this.groupBox4.TabIndex = 50;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "SEND box";
            this.groupBox4.Enter += new System.EventHandler(this.groupBox4_Enter);
            // 
            // button17
            // 
            this.button17.Location = new System.Drawing.Point(13, 21);
            this.button17.Name = "button17";
            this.button17.Size = new System.Drawing.Size(75, 23);
            this.button17.TabIndex = 25;
            this.button17.Text = "Modbus";
            this.button17.UseVisualStyleBackColor = true;
            this.button17.Click += new System.EventHandler(this.button17_Click);
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(14, 49);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(318, 230);
            this.panel1.TabIndex = 54;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // button18
            // 
            this.button18.Location = new System.Drawing.Point(94, 21);
            this.button18.Name = "button18";
            this.button18.Size = new System.Drawing.Size(75, 23);
            this.button18.TabIndex = 26;
            this.button18.Text = "7Segment";
            this.button18.UseVisualStyleBackColor = true;
            this.button18.Click += new System.EventHandler(this.button18_Click);
            // 
            // button20
            // 
            this.button20.Location = new System.Drawing.Point(257, 21);
            this.button20.Name = "button20";
            this.button20.Size = new System.Drawing.Size(75, 23);
            this.button20.TabIndex = 55;
            this.button20.Text = "Text";
            this.button20.UseVisualStyleBackColor = true;
            this.button20.Click += new System.EventHandler(this.button20_Click);
            // 
            // button19
            // 
            this.button19.Location = new System.Drawing.Point(176, 21);
            this.button19.Name = "button19";
            this.button19.Size = new System.Drawing.Size(75, 23);
            this.button19.TabIndex = 27;
            this.button19.Text = "LED";
            this.button19.UseVisualStyleBackColor = true;
            this.button19.Click += new System.EventHandler(this.button19_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.label1.Location = new System.Drawing.Point(24, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(208, 29);
            this.label1.TabIndex = 46;
            this.label1.Text = "modbus test App";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.button22);
            this.groupBox6.Controls.Add(this.button21);
            this.groupBox6.Controls.Add(this.button3);
            this.groupBox6.Controls.Add(this.panel2);
            this.groupBox6.Location = new System.Drawing.Point(12, 75);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(344, 184);
            this.groupBox6.TabIndex = 56;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "device";
            // 
            // button22
            // 
            this.button22.Location = new System.Drawing.Point(229, 24);
            this.button22.Name = "button22";
            this.button22.Size = new System.Drawing.Size(102, 23);
            this.button22.TabIndex = 2;
            this.button22.Text = "삭제";
            this.button22.UseVisualStyleBackColor = true;
            this.button22.Click += new System.EventHandler(this.button22_Click);
            // 
            // button21
            // 
            this.button21.Location = new System.Drawing.Point(13, 24);
            this.button21.Name = "button21";
            this.button21.Size = new System.Drawing.Size(102, 23);
            this.button21.TabIndex = 1;
            this.button21.Text = "선텍";
            this.button21.UseVisualStyleBackColor = true;
            this.button21.Click += new System.EventHandler(this.button21_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(121, 24);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(102, 23);
            this.button3.TabIndex = 0;
            this.button3.Text = "추가";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // panel2
            // 
            this.panel2.Location = new System.Drawing.Point(13, 53);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(318, 125);
            this.panel2.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(939, 569);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "`";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox5.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button button14;
        private System.Windows.Forms.Button button15;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button17;
        private System.Windows.Forms.Button button18;
        private System.Windows.Forms.Button button19;
        private System.Windows.Forms.Button button20;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Button button22;
        private System.Windows.Forms.Button button21;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Panel panel2;
        public System.Windows.Forms.RichTextBox richTextBox2;
        public System.Windows.Forms.RichTextBox richTextBox1;
    }
}

