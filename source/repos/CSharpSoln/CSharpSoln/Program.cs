using System.Collections.Generic;
using static CSharpSoln.Shape;

namespace CSharpSoln
{
    public class Position 
    {

    }

    class Program
    {
        static void Main(string[] args)
        {
            var shapes = new List<Shape>();
            shapes.Add(new Circle());
            shapes.Add(new Rectangle());

            var canvas = new Canvas();
            canvas.Drawshapes(shapes);
        }
    }
}
