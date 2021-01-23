using System;
namespace CSharpSoln
{
    public class Shape 
    {
        public class Circle : Shape 
        {
            public override void Draw()
            {
                Console.WriteLine("Draw a Circle");
            }
        }

        public class Rectangle : Shape 
        {
            public override void Draw()
            {
                Console.WriteLine("Draw a Rectangle");
            }
        }
        public int Width { get; set; }
        public int Height { get; set; }
        public Position Position { get; set; }

        public virtual void Draw() 
        {
        }
    }
}
