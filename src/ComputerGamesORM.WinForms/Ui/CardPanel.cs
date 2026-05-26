using System.Drawing.Drawing2D;

namespace ComputerGamesORM.WinForms.Ui;

internal sealed class CardPanel : Panel
{
    private readonly int _radius;

    public CardPanel(int radius = 10)
    {
        _radius = radius;
        BackColor = AppColors.Surface;
        DoubleBuffered = true;
        Padding = new Padding(20);
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);

        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
        using var borderPen = new Pen(AppColors.Border, 1F);
        using var path = CreateRoundedPath(new Rectangle(0, 0, Width - 1, Height - 1), _radius);
        e.Graphics.DrawPath(borderPen, path);
    }

    protected override void OnResize(EventArgs eventargs)
    {
        base.OnResize(eventargs);
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
