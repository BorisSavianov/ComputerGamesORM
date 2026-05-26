using System.Drawing.Drawing2D;

namespace ComputerGamesORM.WinForms.Ui;

internal sealed class ModernButton : Button
{
    private readonly Color _normalColor;
    private readonly Color _hoverColor;
    private readonly int _radius;

    public ModernButton(Color normalColor, Color hoverColor, int radius = 8)
    {
        _normalColor = normalColor;
        _hoverColor = hoverColor;
        _radius = radius;

        BackColor = _normalColor;
        FlatStyle = FlatStyle.Flat;
        FlatAppearance.BorderSize = 0;
        ForeColor = Color.White;
        Cursor = Cursors.Hand;
        Height = 42;
        Padding = new Padding(10, 0, 10, 0);
        TextImageRelation = TextImageRelation.ImageBeforeText;
        ImageAlign = ContentAlignment.MiddleLeft;
        TextAlign = ContentAlignment.MiddleCenter;
        Font = new Font("Segoe UI Semibold", 8.8F, FontStyle.Bold);
        UseVisualStyleBackColor = false;
    }

    protected override void OnMouseEnter(EventArgs e)
    {
        base.OnMouseEnter(e);
        BackColor = _hoverColor;
    }

    protected override void OnMouseLeave(EventArgs e)
    {
        base.OnMouseLeave(e);
        BackColor = _normalColor;
    }

    protected override void OnResize(EventArgs e)
    {
        base.OnResize(e);
        using var path = CreateRoundedPath(ClientRectangle, _radius);
        Region?.Dispose();
        Region = new Region(path);
    }

    private static GraphicsPath CreateRoundedPath(Rectangle bounds, int radius)
    {
        var path = new GraphicsPath();
        var diameter = radius * 2;
        var rect = new Rectangle(bounds.X, bounds.Y, diameter, diameter);

        path.AddArc(rect, 180, 90);
        rect.X = bounds.Right - diameter;
        path.AddArc(rect, 270, 90);
        rect.Y = bounds.Bottom - diameter;
        path.AddArc(rect, 0, 90);
        rect.X = bounds.Left;
        path.AddArc(rect, 90, 90);
        path.CloseFigure();

        return path;
    }
}
