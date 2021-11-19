using System;
using System.Collections.Generic;
using System.Text;

namespace AffineTransformations3D
{
    class Polygon3D
    {
        public List<Line3D> lines { get; set; }

        public Polygon3D(List<Line3D> lines)
        {
            this.lines = new List<Line3D>();
            this.lines.AddRange(lines);
        }
    }
}
