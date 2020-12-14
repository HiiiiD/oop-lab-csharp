namespace ExtensionMethods
{
    using System;

    /// <inheritdoc cref="IComplex"/>
    public class Complex : IComplex, IEquatable<Complex>
    {
        private readonly double re;
        private readonly double im;

        /// <summary>
        /// Initializes a new instance of the <see cref="Complex"/> class.
        /// </summary>
        /// <param name="re">the real part.</param>
        /// <param name="im">the imaginary part.</param>
        public Complex(double re, double im)
        {
            this.re = re;
            this.im = im;
        }

        /// <inheritdoc cref="IComplex.Real"/>
        public double Real => this.re;

        /// <inheritdoc cref="IComplex.Imaginary"/>
        public double Imaginary => this.im;

        /// <inheritdoc cref="IComplex.Modulus"/>
        public double Modulus => Math.Sqrt(Math.Pow(this.re, 2) + Math.Pow(this.im, 2));

        /// <inheritdoc cref="IComplex.Phase"/>
        public double Phase
        {
            get
            {
                /// <see cref="https://en.wikipedia.org/wiki/Complex_number"/>
                if (this.Real > 0 || this.Imaginary != 0)
                {
                   return 2 * Math.Atan(this.Imaginary / (Math.Sqrt(Math.Pow(this.Real, 2) + Math.Pow(this.Imaginary, 2)) + this.Real));
                }
                if (this.Real < 0 && this.Imaginary == 0)
                {
                    return Math.PI;
                }
                return Double.NaN;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is Complex complex)
            {
                return this.Equals(complex);
            }
            return base.Equals(obj);
        }


        public bool Equals(Complex other)
        {
            return other != null &&
                   this.Real == other.Real &&
                   this.Imaginary == other.Imaginary;
        }

        public bool Equals(IComplex other)
        {
            return other != null &&
                   this.Real == other.Real &&
                   this.Imaginary == other.Imaginary;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Real, Imaginary);
        }

        /// <inheritdoc cref="IComplex.ToString"/>
        public override string ToString()
        {
            bool hasNonZeroReal = this.Real != 0;
            bool hasNonZeroImaginary = this.Imaginary != 0;

            return hasNonZeroReal ?
                (hasNonZeroImaginary ? $"{this.Real} + {this.Imaginary}i" : this.Real.ToString())
                : (hasNonZeroImaginary ? $"{this.Imaginary}i" : "0");
        }

        

        
    }
}
