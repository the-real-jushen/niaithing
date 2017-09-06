namespace TestAILib
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
            this.label1 = new System.Windows.Forms.Label();
            this.aiStatusTextBox = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.aiChannelListBox = new System.Windows.Forms.ListBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lastShotTimeTextBox = new System.Windows.Forms.TextBox();
            this.dataCountTextBox = new System.Windows.Forms.TextBox();
            this.sampleRateTextBox = new System.Windows.Forms.TextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.channelNoTextBox = new System.Windows.Forms.TextBox();
            this.shotNoTextBox = new System.Windows.Forms.TextBox();
            this.loadButton = new System.Windows.Forms.Button();
            this.stopButton = new System.Windows.Forms.Button();
            this.armButton = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.showWaveButton = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 22);
            this.label1.TabIndex = 0;
            this.label1.Text = "Status";
            // 
            // aiStatusTextBox
            // 
            this.aiStatusTextBox.Location = new System.Drawing.Point(62, 25);
            this.aiStatusTextBox.Name = "aiStatusTextBox";
            this.aiStatusTextBox.ReadOnly = true;
            this.aiStatusTextBox.Size = new System.Drawing.Size(101, 21);
            this.aiStatusTextBox.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.aiChannelListBox);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.lastShotTimeTextBox);
            this.panel1.Controls.Add(this.dataCountTextBox);
            this.panel1.Controls.Add(this.sampleRateTextBox);
            this.panel1.Controls.Add(this.aiStatusTextBox);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(-1, -1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(250, 237);
            this.panel1.TabIndex = 2;
            // 
            // aiChannelListBox
            // 
            this.aiChannelListBox.FormattingEnabled = true;
            this.aiChannelListBox.ItemHeight = 12;
            this.aiChannelListBox.Location = new System.Drawing.Point(62, 109);
            this.aiChannelListBox.Name = "aiChannelListBox";
            this.aiChannelListBox.Size = new System.Drawing.Size(141, 76);
            this.aiChannelListBox.TabIndex = 3;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(7, 193);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(53, 12);
            this.label10.TabIndex = 2;
            this.label10.Text = "数据个数";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 109);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 2;
            this.label5.Text = "AI通道";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 58);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(101, 12);
            this.label4.TabIndex = 2;
            this.label4.Text = "最新一炮开始时间";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 82);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "采样率";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "任务状态";
            // 
            // lastShotTimeTextBox
            // 
            this.lastShotTimeTextBox.Location = new System.Drawing.Point(114, 52);
            this.lastShotTimeTextBox.Name = "lastShotTimeTextBox";
            this.lastShotTimeTextBox.ReadOnly = true;
            this.lastShotTimeTextBox.Size = new System.Drawing.Size(101, 21);
            this.lastShotTimeTextBox.TabIndex = 1;
            // 
            // dataCountTextBox
            // 
            this.dataCountTextBox.Location = new System.Drawing.Point(62, 190);
            this.dataCountTextBox.Name = "dataCountTextBox";
            this.dataCountTextBox.ReadOnly = true;
            this.dataCountTextBox.Size = new System.Drawing.Size(101, 21);
            this.dataCountTextBox.TabIndex = 1;
            // 
            // sampleRateTextBox
            // 
            this.sampleRateTextBox.Location = new System.Drawing.Point(62, 79);
            this.sampleRateTextBox.Name = "sampleRateTextBox";
            this.sampleRateTextBox.ReadOnly = true;
            this.sampleRateTextBox.Size = new System.Drawing.Size(101, 21);
            this.sampleRateTextBox.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.label7);
            this.panel3.Controls.Add(this.label6);
            this.panel3.Controls.Add(this.channelNoTextBox);
            this.panel3.Controls.Add(this.shotNoTextBox);
            this.panel3.Controls.Add(this.loadButton);
            this.panel3.Controls.Add(this.stopButton);
            this.panel3.Controls.Add(this.armButton);
            this.panel3.Controls.Add(this.label11);
            this.panel3.Location = new System.Drawing.Point(7, 242);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(315, 189);
            this.panel3.TabIndex = 2;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(95, 102);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 12);
            this.label7.TabIndex = 6;
            this.label7.Text = "通道号";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(27, 102);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 12);
            this.label6.TabIndex = 6;
            this.label6.Text = "炮号";
            // 
            // channelNoTextBox
            // 
            this.channelNoTextBox.Location = new System.Drawing.Point(83, 117);
            this.channelNoTextBox.Name = "channelNoTextBox";
            this.channelNoTextBox.Size = new System.Drawing.Size(70, 21);
            this.channelNoTextBox.TabIndex = 5;
            // 
            // shotNoTextBox
            // 
            this.shotNoTextBox.Location = new System.Drawing.Point(7, 117);
            this.shotNoTextBox.Name = "shotNoTextBox";
            this.shotNoTextBox.Size = new System.Drawing.Size(70, 21);
            this.shotNoTextBox.TabIndex = 5;
            // 
            // loadButton
            // 
            this.loadButton.Location = new System.Drawing.Point(159, 115);
            this.loadButton.Name = "loadButton";
            this.loadButton.Size = new System.Drawing.Size(75, 23);
            this.loadButton.TabIndex = 4;
            this.loadButton.Text = "Load";
            this.loadButton.UseVisualStyleBackColor = true;
            this.loadButton.Click += new System.EventHandler(this.loadButton_Click);
            // 
            // stopButton
            // 
            this.stopButton.Location = new System.Drawing.Point(7, 64);
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(75, 23);
            this.stopButton.TabIndex = 3;
            this.stopButton.Text = "STOP RESET";
            this.stopButton.UseVisualStyleBackColor = true;
            this.stopButton.Click += new System.EventHandler(this.stopButton_Click);
            // 
            // armButton
            // 
            this.armButton.Location = new System.Drawing.Point(7, 32);
            this.armButton.Name = "armButton";
            this.armButton.Size = new System.Drawing.Size(75, 23);
            this.armButton.TabIndex = 3;
            this.armButton.Text = "ARM";
            this.armButton.UseVisualStyleBackColor = true;
            this.armButton.Click += new System.EventHandler(this.armButton_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label11.Location = new System.Drawing.Point(3, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(74, 22);
            this.label11.TabIndex = 0;
            this.label11.Text = "Method";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // showWaveButton
            // 
            this.showWaveButton.Location = new System.Drawing.Point(531, 296);
            this.showWaveButton.Name = "showWaveButton";
            this.showWaveButton.Size = new System.Drawing.Size(75, 23);
            this.showWaveButton.TabIndex = 3;
            this.showWaveButton.Text = "显示波形";
            this.showWaveButton.UseVisualStyleBackColor = true;
            this.showWaveButton.Click += new System.EventHandler(this.showWaveButton_Click);
            // 
            // Form1
            // 
            this.AcceptButton = this.armButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(693, 443);
            this.Controls.Add(this.showWaveButton);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox aiStatusTextBox;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox lastShotTimeTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox sampleRateTextBox;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button stopButton;
        private System.Windows.Forms.Button armButton;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ListBox aiChannelListBox;
        private System.Windows.Forms.Button loadButton;
        private System.Windows.Forms.Button showWaveButton;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox dataCountTextBox;
        private System.Windows.Forms.TextBox channelNoTextBox;
        private System.Windows.Forms.TextBox shotNoTextBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
    }
}

