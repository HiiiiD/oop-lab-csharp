using System;

namespace ExtensionMethods
{
    /// <summary>
    /// The static class declares extension methods for complex numbers.
    /// </summary>
    public static class ComplexExtensions
    {
        /// <summary>
        /// Add two complex numbers.
        /// </summary>
        /// <param name="c1">the first operand.</param>
        /// <param name="c2">the second operand.</param>
        /// <returns>the sum.</returns>
        public static IComplex Add(this IComplex c1, IComplex c2)
        {
            return new Complex(c1.Real + c2.Real, c1.Imaginary + c2.Imaginary);
        }

        /// <summary>
        /// Substract <paramref name="c2"/> from <paramref name="c1"/>.
        /// </summary>
        /// <param name="c1">the first operand.</param>
        /// <param name="c2">the second operand.</param>
        /// <returns>the difference.</returns>
        public static IComplex Subtract(this IComplex c1, IComplex c2)
        {
            return new Complex(c1.Real - c2.Real, c1.Imaginary - c2.Imaginary);
        }

        /// <summary>
        /// Multiply two complex numbers.
        /// </summary>
        /// <param name="c1">the first operand.</param>
        /// <param name="c2">the second operand.</param>
        /// <returns>the product.</returns>
        public static IComplex Multiply(this IComplex c1, IComplex c2)
        {
            double firstTerm = c1.Real * c2.Real;
            double secondTerm = c1.Imaginary * c2.Imaginary;
            double realPart = firstTerm - secondTerm;
            double imaginaryPart = firstTerm + secondTerm;
            return new Complex(realPart, imaginaryPart);
        }

        /// <summary>
        /// Divide two complex numbers.
        /// </summary>
        /// <param name="c1">the first operand.</param>
        /// <param name="c2">the second operand.</param>
        /// <returns>the quotient.</returns>
        public static IComplex Divide(this IComplex c1, IComplex c2)
        {
            double inverseOfSquaredModulus = 1 / Math.Pow(c2.Modulus, 2);
            double realPart = ((c1.Real * c2.Real) + (c1.Imaginary * c2.Imaginary)) / inverseOfSquaredModulus;
            double imaginaryPart = ((c1.Imaginary * c2.Real) - (c1.Real * c2.Imaginary)) / inverseOfSquaredModulus;
            return new Complex(realPart, imaginaryPart);
        }

        /// <summary>
        /// Get the complex conjugate of a complex number.
        /// </summary>
        /// <remarks>
        /// <para>
        /// The complex conjugate of a complex number is the number with an equal real part
        /// and an imaginary part equal in magnitude, but opposite in sign.
        /// </para>
        /// </remarks>
        /// <param name="c1">the complex operand.</param>
        /// <returns>the complex conjugate.</returns>
        public static IComplex Conjugate(this IComplex c1)
        {
            return new Complex(c1.Real, -c1.Imaginary);
        }

        /// <summary>
        /// Get the reciprocal of a complex number.
        /// </summary>
        /// <remarks>
        /// <para>
        /// The multiplicative inverse (or reciprocal) of a complex number is a number,
        /// that when multiplied with that complex number, gives an answer of 1.
        /// </para>
        /// </remarks>
        /// <param name="c1">the complex operand.</param>
        /// <returns>the complex reciprocal.</returns>
        public static IComplex Reciprocal(this IComplex c1)
        {
            double inverseOfSquaredModulus = 1 / Math.Pow(c1.Modulus, 2);
            return new Complex(c1.Real / inverseOfSquaredModulus, -(c1.Imaginary / inverseOfSquaredModulus));
        }
    }
}
