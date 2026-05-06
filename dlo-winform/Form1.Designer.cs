namespace dlo_winform;

partial class Form1
{
    private System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
            components.Dispose();
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
        groupBox3 = new System.Windows.Forms.GroupBox();
        cmbSampleGraphs = new System.Windows.Forms.ComboBox();
        buttonPrev = new System.Windows.Forms.Button();
        button5 = new System.Windows.Forms.Button();
        groupBox2 = new System.Windows.Forms.GroupBox();
        checkBoxEditWeight = new System.Windows.Forms.CheckBox();
        checkBox1 = new System.Windows.Forms.CheckBox();
        button3 = new System.Windows.Forms.Button();
        labelStartNode = new System.Windows.Forms.Label();
        txtStartNode = new System.Windows.Forms.TextBox();
        labelDestNode = new System.Windows.Forms.Label();
    txtDestNode = new System.Windows.Forms.TextBox();
    buttonRemoveNode = new System.Windows.Forms.Button();
    txtPacketSize = new System.Windows.Forms.TextBox();
    cmbPacketUnit = new System.Windows.Forms.ComboBox();
    labelPacketSize = new System.Windows.Forms.Label();
    pbxCanvas = new System.Windows.Forms.PictureBox();
        txtLog = new System.Windows.Forms.TextBox();
        txtEdgeWeightEditor = new System.Windows.Forms.TextBox();
        groupBoxCanvas = new System.Windows.Forms.GroupBox();
    groupBoxLogs = new System.Windows.Forms.GroupBox();
    groupBox4 = new System.Windows.Forms.GroupBox();
    groupBox1.SuspendLayout();
        groupBox3.SuspendLayout();
        groupBox2.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)pbxCanvas).BeginInit();
        groupBoxCanvas.SuspendLayout();
    groupBoxLogs.SuspendLayout();
    groupBox4.SuspendLayout();
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
        // groupBox3
        // 
        groupBox3.Controls.Add(cmbSampleGraphs);
        groupBox3.Controls.Add(buttonPrev);
        groupBox3.Controls.Add(button5);
        groupBox3.Location = new System.Drawing.Point(17, 137);
        groupBox3.Name = "groupBox3";
        groupBox3.Size = new System.Drawing.Size(235, 110);
        groupBox3.TabIndex = 7;
        groupBox3.TabStop = false;
        groupBox3.Text = "Sample graphs";
        // 
        // cmbSampleGraphs
        // 
        cmbSampleGraphs.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        cmbSampleGraphs.Location = new System.Drawing.Point(9, 25);
        cmbSampleGraphs.Name = "cmbSampleGraphs";
        cmbSampleGraphs.Size = new System.Drawing.Size(217, 28);
        cmbSampleGraphs.TabIndex = 4;
        // 
        // buttonPrev
        // 
        buttonPrev.Location = new System.Drawing.Point(9, 62);
        buttonPrev.Name = "buttonPrev";
        buttonPrev.Size = new System.Drawing.Size(105, 34);
        buttonPrev.TabIndex = 5;
        buttonPrev.Text = "Previous";
        buttonPrev.UseVisualStyleBackColor = true;
        buttonPrev.Click += buttonPrev_Click;
        // 
        // button5
        // 
        button5.Location = new System.Drawing.Point(120, 62);
        button5.Name = "button5";
        button5.Size = new System.Drawing.Size(106, 34);
        button5.TabIndex = 3;
        button5.Text = "Next sample";
        button5.UseVisualStyleBackColor = true;
        button5.Click += button5_Click;
        // 
        // groupBox2
        // 
        groupBox2.Controls.Add(checkBoxEditWeight);
        groupBox2.Controls.Add(checkBox1);
        groupBox2.Controls.Add(button3);
        groupBox2.Controls.Add(buttonRemoveNode);
        groupBox2.Controls.Add(labelStartNode);
        groupBox2.Controls.Add(txtStartNode);
        groupBox2.Controls.Add(labelDestNode);
        groupBox2.Controls.Add(txtDestNode);
        groupBox2.Location = new System.Drawing.Point(17, 253);
        groupBox2.Name = "groupBox2";
        groupBox2.Size = new System.Drawing.Size(235, 330);
        groupBox2.TabIndex = 1;
        groupBox2.TabStop = false;
        groupBox2.Text = "Test Parameters";
        // 
        // checkBoxEditWeight
        // 
        checkBoxEditWeight.Location = new System.Drawing.Point(17, 26);
        checkBoxEditWeight.Name = "checkBoxEditWeight";
        checkBoxEditWeight.Size = new System.Drawing.Size(150, 44);
        checkBoxEditWeight.TabIndex = 7;
        checkBoxEditWeight.Text = "Weight edit";
        checkBoxEditWeight.UseVisualStyleBackColor = true;
        checkBoxEditWeight.CheckedChanged += checkBoxEditWeight_CheckedChanged;
        // 
        // checkBox1
        // 
        checkBox1.Location = new System.Drawing.Point(17, 76);
        checkBox1.Name = "checkBox1";
        checkBox1.Size = new System.Drawing.Size(150, 44);
        checkBox1.TabIndex = 2;
        checkBox1.Text = "Edge edit";
        checkBox1.UseVisualStyleBackColor = true;
        checkBox1.CheckedChanged += checkBox1_CheckedChanged;
        // 
        // button3
        // 
        button3.Location = new System.Drawing.Point(17, 125);
        button3.Name = "button3";
        button3.Size = new System.Drawing.Size(204, 33);
        button3.TabIndex = 0;
        button3.Text = "Add node";
        button3.UseVisualStyleBackColor = true;
        button3.Click += button3_Click;
        // 
        // buttonRemoveNode
        // 
        buttonRemoveNode.Location = new System.Drawing.Point(17, 163);
        buttonRemoveNode.Name = "buttonRemoveNode";
        buttonRemoveNode.Size = new System.Drawing.Size(204, 33);
        buttonRemoveNode.TabIndex = 10;
        buttonRemoveNode.Text = "Remove node";
        buttonRemoveNode.UseVisualStyleBackColor = true;
        buttonRemoveNode.Click += buttonRemoveNode_Click;
        // 
        // labelStartNode
        // 
        labelStartNode.Location = new System.Drawing.Point(17, 245);
        labelStartNode.Name = "labelStartNode";
        labelStartNode.Size = new System.Drawing.Size(88, 27);
        labelStartNode.TabIndex = 3;
        labelStartNode.Text = "Start Node";
        // 
        // txtStartNode
        // 
        txtStartNode.Location = new System.Drawing.Point(111, 245);
        txtStartNode.Name = "txtStartNode";
        txtStartNode.Size = new System.Drawing.Size(85, 27);
        txtStartNode.TabIndex = 4;
        txtStartNode.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
        // 
        // labelDestNode
        // 
        labelDestNode.Location = new System.Drawing.Point(17, 278);
        labelDestNode.Name = "labelDestNode";
        labelDestNode.Size = new System.Drawing.Size(88, 27);
        labelDestNode.TabIndex = 5;
        labelDestNode.Text = "Dest Node";
        // 
        // txtDestNode
        // 
        txtDestNode.Location = new System.Drawing.Point(111, 278);
        txtDestNode.Name = "txtDestNode";
        txtDestNode.Size = new System.Drawing.Size(85, 27);
        txtDestNode.TabIndex = 6;
        txtDestNode.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
        // 
        // groupBox4
        // 
        groupBox4.Controls.Add(labelPacketSize);
        groupBox4.Controls.Add(txtPacketSize);
        groupBox4.Controls.Add(cmbPacketUnit);
        groupBox4.Location = new System.Drawing.Point(17, 588);
        groupBox4.Name = "groupBox4";
        groupBox4.Size = new System.Drawing.Size(235, 70);
        groupBox4.TabIndex = 14;
        groupBox4.TabStop = false;
        groupBox4.Text = "Packet Settings";
        // 
        // labelPacketSize
        // 
        labelPacketSize.Location = new System.Drawing.Point(17, 26);
        labelPacketSize.Name = "labelPacketSize";
        labelPacketSize.Size = new System.Drawing.Size(88, 27);
        labelPacketSize.TabIndex = 0;
        labelPacketSize.Text = "Packet size";
        // 
        // txtPacketSize
        // 
        txtPacketSize.Location = new System.Drawing.Point(111, 26);
        txtPacketSize.Name = "txtPacketSize";
        txtPacketSize.Size = new System.Drawing.Size(50, 27);
        txtPacketSize.TabIndex = 1;
        txtPacketSize.Text = "1500";
        txtPacketSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
        // 
        // cmbPacketUnit
        // 
        cmbPacketUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        cmbPacketUnit.Items.AddRange(new object[] { "bytes", "KB", "MB", "GB" });
        cmbPacketUnit.Location = new System.Drawing.Point(165, 26);
        cmbPacketUnit.Name = "cmbPacketUnit";
        cmbPacketUnit.Size = new System.Drawing.Size(56, 28);
        cmbPacketUnit.TabIndex = 2;
        cmbPacketUnit.SelectedIndex = 0;
        // 
        // pbxCanvas
        // 
        pbxCanvas.BackColor = System.Drawing.Color.White;
        pbxCanvas.Location = new System.Drawing.Point(15, 30);
        pbxCanvas.Name = "pbxCanvas";
        pbxCanvas.Size = new System.Drawing.Size(640, 420);
        pbxCanvas.TabIndex = 3;
        pbxCanvas.TabStop = false;
        pbxCanvas.Click += pictureBox1_Click;
        // 
        // txtLog
        // 
        txtLog.Location = new System.Drawing.Point(15, 25);
        txtLog.Multiline = true;
        txtLog.Name = "txtLog";
        txtLog.ReadOnly = true;
        txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
        txtLog.Size = new System.Drawing.Size(640, 195);
        txtLog.TabIndex = 4;
        // 
        // txtEdgeWeightEditor
        // 
        txtEdgeWeightEditor.Location = new System.Drawing.Point(0, 0);
        txtEdgeWeightEditor.Name = "txtEdgeWeightEditor";
        txtEdgeWeightEditor.Size = new System.Drawing.Size(50, 27);
        txtEdgeWeightEditor.TabIndex = 99;
        txtEdgeWeightEditor.Visible = false;
        txtEdgeWeightEditor.KeyPress += txtEdgeWeightEditor_KeyPress;
        txtEdgeWeightEditor.LostFocus += txtEdgeWeightEditor_LostFocus;
        // 
        // groupBoxCanvas
        // 
        groupBoxCanvas.Controls.Add(pbxCanvas);
        groupBoxCanvas.Location = new System.Drawing.Point(258, 19);
        groupBoxCanvas.Name = "groupBoxCanvas";
        groupBoxCanvas.Size = new System.Drawing.Size(670, 465);
        groupBoxCanvas.TabIndex = 5;
        groupBoxCanvas.TabStop = false;
        groupBoxCanvas.Text = "Simulation diagram";
        // 
        // groupBoxLogs
        // 
        groupBoxLogs.Controls.Add(txtLog);
        groupBoxLogs.Location = new System.Drawing.Point(258, 490);
        groupBoxLogs.Name = "groupBoxLogs";
        groupBoxLogs.Size = new System.Drawing.Size(670, 225);
        groupBoxLogs.TabIndex = 6;
        groupBoxLogs.TabStop = false;
        groupBoxLogs.Text = "Logs";
        // 
        // Form1
        // 
        AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        ClientSize = new System.Drawing.Size(944, 720);
        Controls.Add(groupBoxCanvas);
        Controls.Add(groupBoxLogs);
        Controls.Add(txtEdgeWeightEditor);
        Controls.Add(groupBox3);
        Controls.Add(groupBox2);
        Controls.Add(groupBox4);
        Controls.Add(groupBox1);
        Text = "Dijkstra LAN Engine";
        Load += Form1_Load;
        groupBox1.ResumeLayout(false);
        groupBox3.ResumeLayout(false);
        groupBox2.ResumeLayout(false);
        groupBox2.PerformLayout();
        groupBox4.ResumeLayout(false);
        groupBox4.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)pbxCanvas).EndInit();
        groupBoxCanvas.ResumeLayout(false);
        groupBoxLogs.ResumeLayout(false);
        groupBoxLogs.PerformLayout();
        ResumeLayout(false);
        PerformLayout();
    }

    private System.Windows.Forms.Button buttonRemoveNode;
    private System.Windows.Forms.TextBox txtPacketSize;
    private System.Windows.Forms.ComboBox cmbPacketUnit;
    private System.Windows.Forms.Label labelPacketSize;
    private System.Windows.Forms.CheckBox checkBoxEditWeight;
    private System.Windows.Forms.CheckBox checkBox1;
    private System.Windows.Forms.PictureBox pbxCanvas;
    private System.Windows.Forms.ComboBox cmbSampleGraphs;
    private System.Windows.Forms.Button buttonPrev;
    private System.Windows.Forms.Button button5;
    private System.Windows.Forms.GroupBox groupBox3;
    private System.Windows.Forms.Button button3;
    private System.Windows.Forms.Label labelStartNode;
    private System.Windows.Forms.TextBox txtStartNode;
    private System.Windows.Forms.Label labelDestNode;
    private System.Windows.Forms.TextBox txtDestNode;
    private System.Windows.Forms.TextBox txtLog;
    private System.Windows.Forms.TextBox txtEdgeWeightEditor;
    private System.Windows.Forms.Button button4;
    private System.Windows.Forms.Button start_button;
    private System.Windows.Forms.Button button2;
    private System.Windows.Forms.GroupBox groupBox2;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.GroupBox groupBoxCanvas;
    private System.Windows.Forms.GroupBox groupBoxLogs;
    private System.Windows.Forms.GroupBox groupBox4;

    #endregion
}
