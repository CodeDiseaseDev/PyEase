using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PyEase
{
    class Drawing
    {
        public static GraphicsPath RoundedRect(Rectangle bounds, int radius, bool top = true, bool bottom = true)
        {
            int diameter = radius * 2;
            Size size = new Size(diameter, diameter);
            Rectangle arc = new Rectangle(bounds.Location, size);
            GraphicsPath path = new GraphicsPath();

            if (radius == 0)
            {
                path.AddRectangle(bounds);
                return path;
            }

            // top left arc  
            path.AddArc(arc, 180, 90);

            // top right arc  
            arc.X = bounds.Right - (top ? diameter : 0);
            path.AddArc(arc, 270, 90);

            // bottom right arc  
            arc.Y = bounds.Bottom - (bottom ? diameter : 0);
            path.AddArc(arc, 0, 90);

            // bottom left arc 
            arc.X = bounds.Left;
            path.AddArc(arc, 90, 90);

            path.CloseFigure();
            return path;
        }

        private static int max(int val, int max) => val > max ? max : val;
        private static int min(int val, int min) => val < min ? min : val;

        public static Color Brighten(Color c, int offset)
            => Color.FromArgb(
                max(c.R + offset, 255),
                max(c.G + offset, 255),
                max(c.B + offset, 255));

        public static Color Darken(Color c, int offset)
            => Color.FromArgb(
                min(c.R - offset, 0),
                min(c.G - offset, 0),
                min(c.B - offset, 0));
    }
}
