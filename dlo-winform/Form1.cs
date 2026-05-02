using System.Drawing;
using Graph;
using Microsoft.VisualBasic.Devices;

namespace dlo_winform;

public partial class Form1 : Form
{
    public GraphData GD = new GraphData();
    public Form1()
    {
        InitializeComponent();
        this.DoubleBuffered = true;
    }

    private void button1_Click(object sender, EventArgs e)
    {
        if (GD.nodeList.Count <= 0 || GD.edgeList.Count <= 0)
            MessageBox.Show("Graph data cannot be empty.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        
    }

    private void button2_Click(object sender, EventArgs e)
    {
    }

    private void groupBox1_Enter(object sender, EventArgs e)
    {
    }

    private void pictureBox1_Click(object sender, EventArgs e)
    {
    }

    private void button3_Click(object sender, EventArgs e)
    {
    }

    private void checkBox1_CheckedChanged(object sender, EventArgs e)
    {
    }

    private void groupBox3_Enter(object sender, EventArgs e)
    {
    }

    private void label1_Click(object sender, EventArgs e)
    {
    }

    private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
    {
    }

    private void button5_Click(object sender, EventArgs e)
    {
    }

    private void button4_Click(object sender, EventArgs e)
    {
    }

    private void groupBox2_Enter(object sender, EventArgs e)
    {
    }

    private void Form1_Load(object sender, EventArgs e)
    {
    }
}

public class Renderer
{
    //canvas renderer
    public void pbxCanvas_Paint(object sender, PaintEventArgs e)
    {
        Graphics g = e.Graphics;
        GraphData grp = new GraphData();
        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias; //anti-alias
        Font DefaultFont = SystemFonts.DefaultFont;

        Pen edgePen = new Pen(Color.Black, 3); //Edge properties

        foreach (NetworkEdge edge in grp.edgeList)
        {
            g.DrawLine(edgePen, edge.StartNode.Position.X, edge.StartNode.Position.Y, edge.EndNode.Position.X,
                edge.EndNode.Position.Y);
            //Draw weight text
            PointF midPoint = new PointF((edge.StartNode.Position.X + edge.EndNode.Position.X) / 2,
                (edge.EndNode.Position.Y + edge.StartNode.Position.Y) / 2);
            g.DrawString(edge.Weight.ToString(), DefaultFont, Brushes.Black, midPoint);
        }

        //Draw node
        foreach (NetworkNode node in grp.nodeList)
        {
            float x = node.Position.X - node.Radius;
            float y = node.Position.Y - node.Radius;
            g.FillEllipse(Brushes.LightBlue, x, y, node.Radius * 2, node.Radius * 2);
            g.DrawEllipse(Pens.DarkBlue, x, y, node.Radius * 2, node.Radius * 2);
        }
    }
}

public class mouse
{
    public GraphData grp = new GraphData();
    public NetworkNode draggedNode = null;

    private void Event_MouseDown(object sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left)
        {
            draggedNode = grp.GetNode(e.Location);
        }
    }

    private void Event_MouseMove(object sender, MouseEventArgs e)
    {
        if (draggedNode != null)
        {
            draggedNode.Position = e.Location;
            ((Control)sender).Invalidate();
        }
    }

    private void Event_MouseUp(object sender, MouseEventArgs e)
    {
        draggedNode = null;
    }
}

public class Function
{
    
}