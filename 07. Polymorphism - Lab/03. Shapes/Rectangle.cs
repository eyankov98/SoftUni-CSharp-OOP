using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shapes
{
    public class Rectangle : Shape
    {
        private int height;
        private int width;

        public Rectangle(int height, int width)
        {
            this.Height = height;
            this.Width = width;
        }

        public int Height 
        { 
            get => height; 
            private set => height = value; 
        }
        public int Width 
        { 
            get => width; 
            private set => width = value; 
        }

        public override double CalculateArea()
        {
            return this.height * this.Width;
        }

        public override double CalculatePerimeter()
        {
            return (this.height * 2) + (this.width * 2);
        }
    }
}