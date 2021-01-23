using System.Collections.Generic;
using System;

namespace CSharpSoln
{
    public class Canvas 
    {
        public void Drawshapes(List<Shape> shapes) 
        {
            foreach (var shape in shapes)
            {
                shape.Draw();
            }
        }
    }
}
