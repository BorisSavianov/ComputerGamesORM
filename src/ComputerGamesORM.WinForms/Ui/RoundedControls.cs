using System.ComponentModel;
using System.Drawing.Drawing2D;

namespace ComputerGamesORM.WinForms.Ui;

public class RoundedPanel : Panel
{
    private int _borderRadius = 14;
    private Color _borderColor = Color.FromArgb(51, 65, 85);

    public RoundedPanel()
    {
        DoubleBuffered = true;
        BackColor = Color.FromArgb(17, 24, 39);
    }

    [Category("Appearance")]
    [DefaultValue(14)]
    public int BorderRadius
    {
        get => _borderRadius;
        set
        {
            _borderRadius = Math.Max(0, value);
            UpdateRegion();
            Invalidate();
        }
    }

    [Category("Appearance")]
    public Color BorderColor
    {
        get => _borderColor;
        set
        {
            _borderColor = value;
            Invalidate();
        }
    }

    protected override void OnResize(EventArgs eventargs)
    {
        base.OnResize(eventargs);
        UpdateRegion();
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);

        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
        using var path = CreateRoundPath(new Rectangle(0, 0, Width - 1, Height - 1), BorderRadius);
        using var pen = new Pen(BorderColor);
        e.Graphics.DrawPath(pen, path);
    }

    private void UpdateRegion()
    {
        if (Width <= 0 || Height <= 0)
        {
            return;
        }

        using var path = CreateRoundPath(ClientRectangle, BorderRadius);
        Region?.Dispose();
        Region = new Region(path);
    }

    private static GraphicsPath CreateRoundPath(Rectangle bounds, int radius)
    {
        var path = new GraphicsPath();
        if (radius <= 0)
        {
            path.AddRectangle(bounds);
            return path;
        }

        var diameter = radius * 2;
        var arc = new Rectangle(bounds.Location, new Size(diameter, diameter));
        path.AddArc(arc, 180, 90);
        arc.X = bounds.Right - diameter;
        path.AddArc(arc, 270, 90);
        arc.Y = bounds.Bottom - diameter;
        path.AddArc(arc, 0, 90);
        arc.X = bounds.Left;
        path.AddArc(arc, 90, 90);
        path.CloseFigure();
        return path;
    }
}

public class RoundedButton : Button
{
    private int _borderRadius = 10;

    public RoundedButton()
    {
        FlatStyle = FlatStyle.Flat;
        FlatAppearance.BorderSize = 0;
        BackColor = Color.FromArgb(37, 99, 235);
        ForeColor = Color.White;
        Cursor = Cursors.Hand;
    }

    [Category("Appearance")]
    [DefaultValue(10)]
    public int BorderRadius
    {
        get => _borderRadius;
        set
        {
            _borderRadius = Math.Max(0, value);
            UpdateRegion();
            Invalidate();
        }
    }

    protected override void OnResize(EventArgs e)
    {
        base.OnResize(e);
        UpdateRegion();
    }

    protected override void OnPaint(PaintEventArgs pevent)
    {
        pevent.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

        var fill = Enabled ? BackColor : Color.FromArgb(51, 65, 85);
        var text = Enabled ? ForeColor : Color.FromArgb(148, 163, 184);

        using var path = RoundedPanelPath(new Rectangle(0, 0, Width - 1, Height - 1), BorderRadius);
        using var brush = new SolidBrush(fill);
        pevent.Graphics.FillPath(brush, path);

        TextRenderer.DrawText(
            pevent.Graphics,
            Text,
            Font,
            ClientRectangle,
            text,
            TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis);
    }

    private void UpdateRegion()
    {
        if (Width <= 0 || Height <= 0)
        {
            return;
        }

        using var path = RoundedPanelPath(ClientRectangle, BorderRadius);
        Region?.Dispose();
        Region = new Region(path);
    }

    private static GraphicsPath RoundedPanelPath(Rectangle bounds, int radius)
    {
        var path = new GraphicsPath();
        if (radius <= 0)
        {
            path.AddRectangle(bounds);
            return path;
        }

        var diameter = radius * 2;
        var arc = new Rectangle(bounds.Location, new Size(diameter, diameter));
        path.AddArc(arc, 180, 90);
        arc.X = bounds.Right - diameter;
        path.AddArc(arc, 270, 90);
        arc.Y = bounds.Bottom - diameter;
        path.AddArc(arc, 0, 90);
        arc.X = bounds.Left;
        path.AddArc(arc, 90, 90);
        path.CloseFigure();
        return path;
    }
}
