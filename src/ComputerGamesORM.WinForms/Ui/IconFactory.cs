using System.Drawing.Drawing2D;

namespace ComputerGamesORM.WinForms.Ui;

internal static class IconFactory
{
    public static Bitmap Create(IconKind kind, Color color)
    {
        var bitmap = new Bitmap(18, 18);
        using var graphics = Graphics.FromImage(bitmap);
        using var pen = new Pen(color, 2.2F)
        {
            StartCap = LineCap.Round,
            EndCap = LineCap.Round,
            LineJoin = LineJoin.Round
        };
        using var brush = new SolidBrush(color);

        graphics.SmoothingMode = SmoothingMode.AntiAlias;
        graphics.Clear(Color.Transparent);

        switch (kind)
        {
            case IconKind.Add:
                graphics.DrawLine(pen, 9, 3, 9, 15);
                graphics.DrawLine(pen, 3, 9, 15, 9);
                break;
            case IconKind.Edit:
                graphics.DrawLine(pen, 5, 13, 13, 5);
                graphics.DrawLine(pen, 11, 3, 15, 7);
                graphics.FillPolygon(brush, new[] { new Point(4, 14), new Point(7, 13), new Point(5, 11) });
                break;
            case IconKind.Delete:
                graphics.DrawLine(pen, 5, 7, 6, 15);
                graphics.DrawLine(pen, 13, 7, 12, 15);
                graphics.DrawLine(pen, 3, 5, 15, 5);
                graphics.DrawLine(pen, 7, 3, 11, 3);
                graphics.DrawRectangle(pen, 5, 6, 8, 10);
                break;
            case IconKind.Save:
                graphics.DrawRectangle(pen, 4, 3, 10, 12);
                graphics.DrawLine(pen, 6, 3, 6, 8);
                graphics.DrawLine(pen, 12, 3, 12, 8);
                graphics.DrawLine(pen, 6, 12, 12, 12);
                break;
            case IconKind.Cancel:
                graphics.DrawLine(pen, 5, 5, 13, 13);
                graphics.DrawLine(pen, 13, 5, 5, 13);
                break;
            case IconKind.Search:
                graphics.DrawEllipse(pen, 3, 3, 9, 9);
                graphics.DrawLine(pen, 11, 11, 15, 15);
                break;
            case IconKind.Refresh:
                graphics.DrawArc(pen, 3, 3, 12, 12, 25, 270);
                graphics.DrawLine(pen, 13, 3, 15, 7);
                graphics.DrawLine(pen, 13, 3, 9, 3);
                break;
        }

        return bitmap;
    }
}

internal enum IconKind
{
    Add,
    Edit,
    Delete,
    Save,
    Cancel,
    Search,
    Refresh
}
