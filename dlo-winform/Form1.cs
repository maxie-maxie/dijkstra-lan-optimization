using System.Drawing;
using Graph;

namespace dlo_winform;

public partial class Form1 : Form
{
    private const int AnimationTickMilliseconds = 500;
    public GraphData GD = new GraphData();
    private System.Windows.Forms.Timer animationTimer;
    private DijkstraRouteResult? currentRoute;
    private PacketSimulation? currentSimulation;
    private bool isPaused = false;
    private bool isAddNodeMode = false;
    private NetworkEdge? editingEdge = null;

    public Form1()
    {
        InitializeComponent();
        this.DoubleBuffered = true;
        pbxCanvas.Paint += pbxCanvas_Paint;
        pbxCanvas.MouseDown += pbxCanvas_MouseDown;
        pbxCanvas.MouseMove += pbxCanvas_MouseMove;
        pbxCanvas.MouseUp += pbxCanvas_MouseUp;
        pbxCanvas.MouseDoubleClick += pbxCanvas_MouseDoubleClick;

        animationTimer = new System.Windows.Forms.Timer();
        animationTimer.Interval = AnimationTickMilliseconds;
        animationTimer.Tick += AnimationTimer_Tick;
    }

    private void button1_Click(object sender, EventArgs e)
    {
        if (GD.nodeList.Count <= 0 || GD.edgeList.Count <= 0)
        {
            MessageBox.Show("Graph data cannot be empty.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        if (!int.TryParse(txtStartNode.Text, out int startId) || !int.TryParse(txtDestNode.Text, out int destId))
        {
            MessageBox.Show("Please enter valid integer values for start and destination nodes.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        var startNode = GD.GetNodeById(startId);
        var destNode = GD.GetNodeById(destId);

        if (startNode == null || destNode == null)
        {
            MessageBox.Show("Start or destination node not found in graph.", "Node Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        currentRoute = DijkstraGraphService.FindRoute(GD, startId, destId);

        if (!currentRoute.Reachable)
        {
            Log("Route not found from node " + startId + " to node " + destId);
            MessageBox.Show("No route found between the specified nodes.", "Unreachable", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        currentSimulation = new PacketSimulation(currentRoute);
        isPaused = false;
        txtLog.Clear();
        Log("Route found: " + string.Join(" -> ", currentRoute.PathNodeIds) + ", total time " + currentRoute.TotalTime);

        animationTimer.Start();
    }

    private void AnimationTimer_Tick(object? sender, EventArgs e)
    {
        if (currentSimulation == null || isPaused) return;

        var tick = currentSimulation.Tick();

        if (tick.IsComplete)
        {
            Log("Packet delivered to node " + currentRoute?.DestinationNodeId + " in " + tick.TotalElapsedTime + " ms");
            animationTimer.Stop();
        }
        else if (tick.IsMove)
        {
            Log("Tick: packet moved " + tick.FromNodeId + " -> " + tick.ToNodeId + ", edge time " + tick.TickTravelTime + " ms, elapsed time " + tick.TotalElapsedTime + " ms");
        }

        pbxCanvas.Invalidate();
    }

    private void button2_Click(object sender, EventArgs e)
    {
        animationTimer.Stop();
        currentSimulation = null;
        currentRoute = null;
        pbxCanvas.Invalidate();
    }

    private void button4_Click(object sender, EventArgs e)
    {
        isPaused = !isPaused;
        button4.Text = isPaused ? "Resume" : "Pause";
    }

    private void pbxCanvas_Paint(object? sender, PaintEventArgs e)
    {
        Graphics g = e.Graphics;
        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
        Font defaultFont = SystemFonts.DefaultFont;
        Font boldFont = new Font(defaultFont.FontFamily, defaultFont.Size, FontStyle.Bold);

        Pen edgePen = new Pen(Color.Black, 2);
        Pen pathPen = new Pen(Color.DodgerBlue, 3);

        foreach (NetworkEdge edge in GD.edgeList)
        {
            bool isInPath = currentRoute?.PathEdges.Contains(edge) == true;
            Pen pen = isInPath ? pathPen : edgePen;
            g.DrawLine(pen, edge.StartNode.Position.X, edge.StartNode.Position.Y, edge.EndNode.Position.X, edge.EndNode.Position.Y);

            PointF midPoint = new PointF(
                (edge.StartNode.Position.X + edge.EndNode.Position.X) / 2,
                (edge.StartNode.Position.Y + edge.EndNode.Position.Y) / 2);
            g.DrawString(edge.Weight.ToString(), defaultFont, Brushes.Black, midPoint);
        }

        if (currentSimulation != null && currentRoute != null && currentSimulation.CurrentEdgeIndex >= 0 && currentSimulation.CurrentEdgeIndex < currentRoute.PathEdges.Count)
        {
            var edge = currentRoute.PathEdges[currentSimulation.CurrentEdgeIndex];
            PointF start = edge.StartNode.Position;
            PointF end = edge.EndNode.Position;
            float packetX = (start.X + end.X) / 2;
            float packetY = (start.Y + end.Y) / 2;
            g.FillEllipse(Brushes.Red, packetX - 5, packetY - 5, 10, 10);
        }

        foreach (NetworkNode node in GD.nodeList)
        {
            float x = node.Position.X - node.Radius;
            float y = node.Position.Y - node.Radius;
            float diameter = node.Radius * 2;
            Brush nodeBrush = Brushes.LightBlue;

            if (currentRoute != null && node.Id == currentRoute.StartNodeId)
                nodeBrush = Brushes.Green;
            else if (currentRoute != null && node.Id == currentRoute.DestinationNodeId)
                nodeBrush = Brushes.Orange;

            g.FillEllipse(nodeBrush, x, y, diameter, diameter);
            g.DrawEllipse(Pens.DarkBlue, x, y, diameter, diameter);

            string idText = node.Id.ToString();
            SizeF textSize = g.MeasureString(idText, boldFont);
            float textX = node.Position.X - textSize.Width / 2;
            float textY = node.Position.Y - textSize.Height / 2;
            g.DrawString(idText, boldFont, Brushes.Black, textX, textY);
        }

        edgePen.Dispose();
        pathPen.Dispose();
        boldFont.Dispose();
    }

    private void pbxCanvas_MouseDown(object? sender, MouseEventArgs e)
    {
        if (isAddNodeMode && e.Button == MouseButtons.Left)
        {
            int id = GD.nodeList.Count + 1;
            GD.nodeList.Add(new NetworkNode { Id = id, Position = e.Location });
            pbxCanvas.Invalidate();
            Log("Added node " + id + " at (" + e.Location.X + ", " + e.Location.Y + ")");
            isAddNodeMode = false;
            return;
        }

        if (checkBox1.Checked && e.Button == MouseButtons.Left)
        {
            var node = GD.GetNode(e.Location);
            if (node != null)
            {
                pbxCanvas.Tag = node;
            }
        }
    }

    private void pictureBox1_Click(object sender, EventArgs e)
    {
    }

    private void pbxCanvas_MouseMove(object? sender, MouseEventArgs e)
    {
    }

    private void pbxCanvas_MouseUp(object? sender, MouseEventArgs e)
    {
        if (checkBox1.Checked && pbxCanvas.Tag is NetworkNode startNode && e.Button == MouseButtons.Left)
        {
            var endNode = GD.GetNode(e.Location);
            if (endNode != null && startNode != endNode)
            {
                GraphEditor.AddEdgeIfMissing(GD, startNode, endNode, 1);
                pbxCanvas.Invalidate();
            }
            pbxCanvas.Tag = null;
        }
    }

    private void button3_Click(object sender, EventArgs e)
    {
        isAddNodeMode = true;
        Log("Click on the canvas to place the new node.");
    }

    private void button5_Click(object sender, EventArgs e)
    {
        if (!int.TryParse(txtNodeCount.Text, out int nodeCount) || nodeCount <= 0)
        {
            MessageBox.Show("Please enter a valid positive number of nodes.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        GD = RandomGraphGenerator.Generate(nodeCount, new Size(650, 440));
        txtLog.Clear();
        Log("Generated random graph with " + nodeCount + " nodes");
        pbxCanvas.Invalidate();
    }

    private void Log(string message)
    {
        txtLog.AppendText(message + Environment.NewLine);
    }

    private void checkBox1_CheckedChanged(object sender, EventArgs e)
    {
    }

    private void groupBox1_Enter(object sender, EventArgs e)
    {
    }

    private void groupBox2_Enter(object sender, EventArgs e)
    {
    }

    private void groupBox3_Enter(object sender, EventArgs e)
    {
    }

    private void label1_Click(object sender, EventArgs e)
    {
    }

    private void Form1_Load(object sender, EventArgs e)
    {
    }

    private void pbxCanvas_MouseDoubleClick(object? sender, MouseEventArgs e)
    {
        if (isAddNodeMode || checkBox1.Checked) return;

        var edge = GraphEditor.TryGetEdgeNearPoint(GD, e.Location, 6f);
        if (edge == null) return;

        editingEdge = edge;
        PointF mid = new PointF(
            (edge.StartNode.Position.X + edge.EndNode.Position.X) / 2,
            (edge.StartNode.Position.Y + edge.EndNode.Position.Y) / 2);
        txtEdgeWeightEditor.Location = new Point((int)mid.X, (int)mid.Y);
        txtEdgeWeightEditor.Text = edge.Weight.ToString();
        txtEdgeWeightEditor.Visible = true;
        txtEdgeWeightEditor.Focus();
        txtEdgeWeightEditor.SelectAll();
    }

    private void txtEdgeWeightEditor_KeyPress(object? sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)Keys.Enter)
        {
            txtEdgeWeightEditor_LostFocus(sender, EventArgs.Empty);
            e.Handled = true;
        }
        else if (e.KeyChar == (char)Keys.Escape)
        {
            txtEdgeWeightEditor.Visible = false;
            editingEdge = null;
            e.Handled = true;
        }
    }

    private void txtEdgeWeightEditor_LostFocus(object? sender, EventArgs e)
    {
        if (!txtEdgeWeightEditor.Visible || editingEdge == null) return;

        if (int.TryParse(txtEdgeWeightEditor.Text, out int newWeight) && newWeight >= 0)
        {
            editingEdge.Weight = newWeight;
            pbxCanvas.Invalidate();
        }

        txtEdgeWeightEditor.Visible = false;
        editingEdge = null;
    }
}
