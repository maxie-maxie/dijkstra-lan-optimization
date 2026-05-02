namespace dlo_winform;

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
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        groupBox1 = new System.Windows.Forms.GroupBox();
        button4 = new System.Windows.Forms.Button();
        button2 = new System.Windows.Forms.Button();
        start_button = new System.Windows.Forms.Button();
        groupBox2 = new System.Windows.Forms.GroupBox();
        checkBox1 = new System.Windows.Forms.CheckBox();
        groupBox3 = new System.Windows.Forms.GroupBox();
        label1 = new System.Windows.Forms.Label();
        maskedTextBox1 = new System.Windows.Forms.MaskedTextBox();
        button5 = new System.Windows.Forms.Button();
        button3 = new System.Windows.Forms.Button();
        pbxCanvas = new System.Windows.Forms.PictureBox();
        groupBox1.SuspendLayout();
        groupBox2.SuspendLayout();
        groupBox3.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)pbxCanvas).BeginInit();
        SuspendLayout();
        // 
        // groupBox1
        // 
        groupBox1.Controls.Add(button4);
        groupBox1.Controls.Add(button2);
        groupBox1.Controls.Add(start_button);
        groupBox1.Location = new System.Drawing.Point(17, 19);
        groupBox1.Name = "groupBox1";
        groupBox1.Size = new System.Drawing.Size(235, 112);
        groupBox1.TabIndex = 0;
        groupBox1.TabStop = false;
        groupBox1.Text = "Test Activation";
        groupBox1.Enter += groupBox1_Enter;
        // 
        // button4
        // 
        button4.Location = new System.Drawing.Point(73, 66);
        button4.Name = "button4";
        button4.Size = new System.Drawing.Size(85, 34);
        button4.TabIndex = 2;
        button4.Text = "Pause";
        button4.UseVisualStyleBackColor = true;
        button4.Click += button4_Click;
        // 
        // button2
        // 
        button2.Location = new System.Drawing.Point(126, 26);
        button2.Name = "button2";
        button2.Size = new System.Drawing.Size(85, 34);
        button2.TabIndex = 1;
        button2.Text = "Stop";
        button2.UseVisualStyleBackColor = true;
        button2.Click += button2_Click;
        // 
        // start_button
        // 
        start_button.Location = new System.Drawing.Point(22, 26);
        start_button.Name = "start_button";
        start_button.Size = new System.Drawing.Size(85, 34);
        start_button.TabIndex = 0;
        start_button.Text = "Start";
        start_button.UseVisualStyleBackColor = true;
        start_button.Click += button1_Click;
        // 
    // labelStartNode
    // 
    labelStartNode = new System.Windows.Forms.Label();
    labelStartNode.Location = new System.Drawing.Point(17, 235);
    labelStartNode.Name = "labelStartNode";
    labelStartNode.Size = new System.Drawing.Size(88, 27);
    labelStartNode.TabIndex = 3;
    labelStartNode.Text = "Start Node";
    // 
    // txtStartNode
    // 
    txtStartNode = new System.Windows.Forms.TextBox();
    txtStartNode.Location = new System.Drawing.Point(111, 235);
    txtStartNode.Name = "txtStartNode";
    txtStartNode.Size = new System.Drawing.Size(85, 27);
    txtStartNode.TabIndex = 4;
    txtStartNode.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
    // 
    // labelDestNode
    // 
    labelDestNode = new System.Windows.Forms.Label();
    labelDestNode.Location = new System.Drawing.Point(17, 268);
    labelDestNode.Name = "labelDestNode";
    labelDestNode.Size = new System.Drawing.Size(88, 27);
    labelDestNode.TabIndex = 5;
    labelDestNode.Text = "Dest Node";
    // 
    // txtDestNode
    // 
    txtDestNode = new System.Windows.Forms.TextBox();
    txtDestNode.Location = new System.Drawing.Point(111, 268);
    txtDestNode.Name = "txtDestNode";
    txtDestNode.Size = new System.Drawing.Size(85, 27);
    txtDestNode.TabIndex = 6;
    txtDestNode.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
    // 
    // groupBox2
    // 
    groupBox2.Controls.Add(checkBox1);
    groupBox2.Controls.Add(groupBox3);
    groupBox2.Controls.Add(button3);
    groupBox2.Controls.Add(labelStartNode);
    groupBox2.Controls.Add(txtStartNode);
    groupBox2.Controls.Add(labelDestNode);
    groupBox2.Controls.Add(txtDestNode);
    groupBox2.Location = new System.Drawing.Point(17, 137);
    groupBox2.Name = "groupBox2";
    groupBox2.Size = new System.Drawing.Size(235, 350);
    groupBox2.TabIndex = 1;
    groupBox2.TabStop = false;
    groupBox2.Text = "Test Parameters";
    groupBox2.Enter += groupBox2_Enter;
        // 
        // checkBox1
        // 
        checkBox1.Location = new System.Drawing.Point(17, 65);
        checkBox1.Name = "checkBox1";
        checkBox1.Size = new System.Drawing.Size(108, 44);
        checkBox1.TabIndex = 2;
        checkBox1.Text = "Edge edit";
        checkBox1.UseVisualStyleBackColor = true;
        checkBox1.CheckedChanged += checkBox1_CheckedChanged;
        // 
        // groupBox3
        // 
        groupBox3.Controls.Add(label1);
        groupBox3.Controls.Add(txtNodeCount);
        groupBox3.Controls.Add(button5);
        groupBox3.Location = new System.Drawing.Point(17, 115);
        groupBox3.Name = "groupBox3";
        groupBox3.Size = new System.Drawing.Size(208, 114);
        groupBox3.TabIndex = 1;
        groupBox3.TabStop = false;
        groupBox3.Text = "Generate random LAN map";
        groupBox3.Enter += groupBox3_Enter;
        // 
        // label1
        // 
        label1.Location = new System.Drawing.Point(8, 33);
        label1.Name = "label1";
        label1.Size = new System.Drawing.Size(88, 27);
        label1.TabIndex = 5;
        label1.Text = "Node count";
        label1.Click += label1_Click;
        // 
        // txtNodeCount
        // 
        txtNodeCount = new System.Windows.Forms.TextBox();
        txtNodeCount.Location = new System.Drawing.Point(102, 30);
        txtNodeCount.Name = "txtNodeCount";
        txtNodeCount.Size = new System.Drawing.Size(85, 27);
        txtNodeCount.TabIndex = 4;
        txtNodeCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
        // 
        // button5
        // 
        button5.Location = new System.Drawing.Point(9, 68);
        button5.Name = "button5";
        button5.Size = new System.Drawing.Size(189, 34);
        button5.TabIndex = 3;
        button5.Text = "Generate";
        button5.UseVisualStyleBackColor = true;
        button5.Click += button5_Click;
        // 
        // button3
        // 
        button3.Location = new System.Drawing.Point(17, 26);
        button3.Name = "button3";
        button3.Size = new System.Drawing.Size(204, 33);
        button3.TabIndex = 0;
        button3.Text = "Add node";
        button3.UseVisualStyleBackColor = true;
        button3.Click += button3_Click;
        // 
    // pbxCanvas
    // 
    pbxCanvas.Location = new System.Drawing.Point(270, 29);
    pbxCanvas.Name = "pbxCanvas";
    pbxCanvas.Size = new System.Drawing.Size(659, 450);
    pbxCanvas.TabIndex = 3;
    pbxCanvas.TabStop = false;
    pbxCanvas.Click += pictureBox1_Click;
    // 
    // txtLog
    // 
    txtLog = new System.Windows.Forms.TextBox();
    txtLog.Location = new System.Drawing.Point(270, 485);
    txtLog.Multiline = true;
    txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
    txtLog.Name = "txtLog";
    txtLog.Size = new System.Drawing.Size(659, 100);
    txtLog.TabIndex = 4;
    txtLog.ReadOnly = true;
        // 
    // Form1
    // 
    AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
    AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
    ClientSize = new System.Drawing.Size(944, 600);
    Controls.Add(txtLog);
    Controls.Add(pbxCanvas);
    Controls.Add(groupBox2);
    Controls.Add(groupBox1);
    Text = "Dijkstra LAN Engine";
    Load += Form1_Load;
        groupBox1.ResumeLayout(false);
        groupBox2.ResumeLayout(false);
        groupBox3.ResumeLayout(false);
        groupBox3.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)pbxCanvas).EndInit();
        ResumeLayout(false);
    }

    private System.Windows.Forms.CheckBox checkBox1;

    private System.Windows.Forms.PictureBox pbxCanvas;

    private System.Windows.Forms.Label label1;

    private System.Windows.Forms.TextBox txtNodeCount;

    private System.Windows.Forms.Button button5;

    private System.Windows.Forms.GroupBox groupBox3;

    private System.Windows.Forms.Button button3;
    private System.Windows.Forms.Label labelStartNode;
    private System.Windows.Forms.TextBox txtStartNode;
    private System.Windows.Forms.Label labelDestNode;
    private System.Windows.Forms.TextBox txtDestNode;
    private System.Windows.Forms.TextBox txtLog;
    private System.Windows.Forms.Button button4;

    private System.Windows.Forms.Button start_button;
    private System.Windows.Forms.Button button2;

    private System.Windows.Forms.GroupBox groupBox2;

    private System.Windows.Forms.GroupBox groupBox1;

    #endregion
}