using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassBoxData.Models
{
    public class Box
    {
        private const string PropertyZeroNegativeExceptionMessage = "{0} cannot be zero or negative.";

        private double length;
        private double width;
        private double height;

        public Box(double length, double width, double height)
        {
            this.Length = length;
            this.Width = width;
            this.Height = height;
        }

        public double Length
        {
            get
            {
                return this.length;
            }
            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentException(string.Format(PropertyZeroNegativeExceptionMessage, nameof(this.Length))); //$"{nameof(this.Length)} cannot be zero or negative."
                }

                this.length = value;
            }
        }
        public double Width
        {
            get
            {
                return this.width;
            }
            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentException(string.Format(PropertyZeroNegativeExceptionMessage, nameof(this.Width))); //$"{nameof(this.Width)} cannot be zero or negative."
                }

                this.width = value;
            }
        }
        public double Height
        {
            get
            {
                return this.height;
            }
            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentException(string.Format(PropertyZeroNegativeExceptionMessage, nameof(this.Height))); //$"{nameof(this.Height)} cannot be zero or negative."
                }

                this.height = value;
            }
        }

        public double SurfaceArea()
            => 2 * Length * Width + LateraSurfaceArea(); // LateraSurfaceArea() = 2 * Length * Height + 2 * Width * Height;

        public double LateraSurfaceArea()
            => 2 * Length * Height + 2 * Width * Height;

        public double Volume()
            => Length * Width * Height;
    }
}