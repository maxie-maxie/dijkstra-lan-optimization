using System.Reflection;
using Xunit;
using System.Drawing;
using System.Windows.Forms;

namespace dlo_winform.Tests;

public class FormDesignerTests
{
    [Fact]
    public void Constructor_DoesNotThrow()
    {
        var exception = Record.Exception(() => new Form1());
        Assert.Null(exception);
    }

    [Fact]
    public void Form_HasGraphData()
    {
        using var form = new Form1();
        Assert.NotNull(form.GD);
        Assert.NotNull(form.GD.nodeList);
        Assert.NotNull(form.GD.edgeList);
    }

    [Fact]
    public void Form_HasAllExpectedControls()
    {
        using var form = new Form1();
        Assert.NotNull(GetControl(form, "pbxCanvas"));
        Assert.NotNull(GetControl(form, "groupBox2"));
        Assert.NotNull(GetControl(form, "groupBox1"));
        Assert.NotNull(GetControl(form, "groupBoxCanvas"));
        Assert.NotNull(GetControl(form, "groupBoxLogs"));
        Assert.NotNull(GetControl(form, "txtEdgeWeightEditor"));
        Assert.NotNull(GetControl(form, "cmbSampleGraphs"));
        Assert.NotNull(GetControl(form, "buttonPrev"));
        Assert.NotNull(GetControl(form, "checkBoxEditWeight"));
    }

    [Fact]
    public void Canvas_InsideGroupBoxCanvas()
    {
        using var form = new Form1();
        var canvas = GetControl(form, "pbxCanvas");
        var groupBox = Assert.IsType<GroupBox>(GetControl(form, "groupBoxCanvas"));
        Assert.Contains(canvas, groupBox.Controls.Cast<Control>().ToList());
    }

    [Fact]
    public void Log_InsideGroupBoxLogs()
    {
        using var form = new Form1();
        var txtLog = GetControl(form, "txtLog");
        var logBox = Assert.IsType<GroupBox>(GetControl(form, "groupBoxLogs"));
        Assert.Contains(txtLog, logBox.Controls.Cast<Control>().ToList());
    }

    [Fact]
    public void FormClientSize_IsPositive()
    {
        using var form = new Form1();
        Assert.True(form.ClientSize.Width > 0);
        Assert.True(form.ClientSize.Height > 0);
    }

    [Fact]
    public void FormClientSize_IsTallEnough()
    {
        using var form = new Form1();
        Assert.True(form.ClientSize.Height >= 500, $"Form height {form.ClientSize.Height} < 500");
    }

    [Fact]
    public void FormClientWidth_IsAtLeast700()
    {
        using var form = new Form1();
        Assert.True(form.ClientSize.Width >= 700, $"Form width {form.ClientSize.Width} < 700");
    }

    [Fact]
    public void AllControlLocations_AreValid()
    {
        using var form = new Form1();
        foreach (Control control in form.Controls)
        {
            Assert.True(control.Location.X >= -1,
                $"Control '{control.Name}' has X={control.Location.X}");
            Assert.True(control.Location.Y >= -1,
                $"Control '{control.Name}' has Y={control.Location.Y}");
        }
    }

    [Fact]
    public void FormTitle_IsSet()
    {
        using var form = new Form1();
        Assert.Equal("Dijkstra LAN Engine", form.Text);
    }

    [Fact]
    public void StartStopButtons_ArePresent()
    {
        using var form = new Form1();
        var gb1 = Assert.IsType<GroupBox>(GetControl(form, "groupBox1"));
        Assert.NotNull(gb1.Controls["start_button"]);
        Assert.NotNull(gb1.Controls["button2"]);
        Assert.NotNull(gb1.Controls["button4"]);
    }

    [Fact]
    public void TestParametersGroup_HasAllControls()
    {
        using var form = new Form1();
        var gb2 = Assert.IsType<GroupBox>(GetControl(form, "groupBox2"));
        Assert.NotNull(gb2.Controls["checkBoxEditWeight"]);
        Assert.NotNull(gb2.Controls["checkBox1"]);
        Assert.NotNull(gb2.Controls["button3"]);
        Assert.NotNull(gb2.Controls["labelStartNode"]);
        Assert.NotNull(gb2.Controls["txtStartNode"]);
        Assert.NotNull(gb2.Controls["labelDestNode"]);
        Assert.NotNull(gb2.Controls["txtDestNode"]);
    }

    [Fact]
    public void SampleGraphGroup_HasAllControls()
    {
        using var form = new Form1();
        var gb3 = Assert.IsType<GroupBox>(GetControl(form, "groupBox3"));
        Assert.NotNull(gb3.Controls["cmbSampleGraphs"]);
        Assert.NotNull(gb3.Controls["buttonPrev"]);
        Assert.NotNull(gb3.Controls["button5"]);
    }

    [Fact]
    public void SampleGraphGroup_TitleIsCorrect()
    {
        using var form = new Form1();
        var gb3 = Assert.IsType<GroupBox>(GetControl(form, "groupBox3"));
        Assert.Equal("Sample graphs", gb3.Text);
    }

    [Fact]
    public void GraphData_CanAddAndRoute()
    {
        using var form = new Form1();
        form.GD = SampleGraphs.CreateAt(8);
        var route = DijkstraGraphService.FindRoute(form.GD, 1, 16);
        Assert.True(route.Reachable);
    }

    [Fact]
    public void Canvas_HasWhiteBackground()
    {
        using var form = new Form1();
        var canvas = GetControl(form, "pbxCanvas");
        Assert.Equal(Color.White, canvas.BackColor);
    }

    [Fact]
    public void CanvasSize_IsSufficient()
    {
        using var form = new Form1();
        var canvas = GetControl(form, "pbxCanvas");
        Assert.True(canvas.Width >= 400, $"Canvas width {canvas.Width} too small");
        Assert.True(canvas.Height >= 300, $"Canvas height {canvas.Height} too small");
    }

    [Fact]
    public void GroupBoxes_AllHaveTitles()
    {
        using var form = new Form1();
        foreach (Control c in form.Controls)
        {
            if (c is GroupBox gb)
                Assert.False(string.IsNullOrEmpty(gb.Text), $"GroupBox '{gb.Name}' missing title");
        }
    }

    [Fact]
    public void EdgeWeightEditor_IsHidden()
    {
        using var form = new Form1();
        var editor = (TextBox)GetControl(form, "txtEdgeWeightEditor");
        Assert.False(editor.Visible);
    }

    [Fact]
    public void Log_IsReadOnly()
    {
        using var form = new Form1();
        var txtLog = (TextBox)GetControl(form, "txtLog");
        Assert.True(txtLog.ReadOnly);
        Assert.True(txtLog.Multiline);
    }

    [Fact]
    public void LogBox_HeightExtended()
    {
        using var form = new Form1();
        var logBox = Assert.IsType<GroupBox>(GetControl(form, "groupBoxLogs"));
        Assert.True(logBox.Height >= 140, $"Log box height {logBox.Height} < 140");
    }

    [Fact]
    public void LogBox_IsBelowCanvas()
    {
        using var form = new Form1();
        var canvasBox = Assert.IsType<GroupBox>(GetControl(form, "groupBoxCanvas"));
        var logBox = Assert.IsType<GroupBox>(GetControl(form, "groupBoxLogs"));
        Assert.True(logBox.Top >= canvasBox.Top + canvasBox.Height - 10);
    }

    private static Control GetControl(Form form, string name)
    {
        var field = typeof(Form1).GetField(name,
            BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
        if (field != null)
            return field.GetValue(form) as Control ?? throw new KeyNotFoundException(
                $"Field '{name}' is not a Control");

        var prop = typeof(Form1).GetProperty(name,
            BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
        if (prop != null)
            return prop.GetValue(form) as Control ?? throw new KeyNotFoundException(
                $"Property '{name}' is not a Control");

        return form.Controls[name] ?? throw new KeyNotFoundException(
            $"Control '{name}' not found as field, property, or in Controls collection");
    }
}
