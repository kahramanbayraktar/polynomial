using System;

namespace Polynomial
{
    class Program
    {
        static void Main(string[] args)
        {
            // 2x0 + 0x1 + 3x2 and polynomial 0x0 + 1x1 + 0x2
            var p1 = new Polynomial(new[] { 2, 0, 3 }, new[] { 0, 1, 2 }); // 2 + 0x + 3x²
            var p2 = new Polynomial(new[] { 0, 1, 0 }, new[] { 0, 1, 2 }); // 0 + 1x + 0x²

            // Add
            Console.WriteLine("Add:");
            var p3 = Polynomial.Add(p1, p2); // -> 6 + 5x (6, 0; 5, 1)
            Console.WriteLine(p1.ToString());
            Console.WriteLine(p2.ToString());
            Console.WriteLine(p3.ToString());

            // 2x⁰ + 3x¹  X  1x² + 1x³  =  2x² + 5x³ + 3x⁴
            var p4 = new Polynomial(new[] { 2, 3 }, new[] { 0, 1 }); // 2x⁰ + 3x¹
            var p5 = new Polynomial(new[] { 1, 1 }, new[] { 2, 3 }); // 1x² + 1x³

            // Multiply
            Console.WriteLine("\r\nMultiply:");
            var p6 = Polynomial.Multiply(p4, p5); // 2x² + 5x³ + 3x⁴
            Console.WriteLine(p4.ToString());
            Console.WriteLine(p5.ToString());
            Console.WriteLine(p6.ToString());

            // Multiply with Remove Zero Coefficients
            Console.WriteLine("\r\nMultiply with Remove Zero Coefficients:");
            var p7 = Polynomial.MultiplyRemoveZeroCoefficient(p4, p5); // 2x² + 5x³ + 3x⁴
            Console.WriteLine(p7.ToString());

            // Remove Zero Coefficients:
            Console.WriteLine("\r\nRemove Zero Coefficients:");
            var p8 = new Polynomial(new[] { 0, 2, 0, 3 }, new[] { 0, 1, 2, 3 });
            Console.WriteLine(p8.ToString());
            p8.RemoveZeroCoefficient();
            Console.WriteLine(p8.ToString());

            // Zero polynomial
            Console.WriteLine("\r\nZero polynomial:");
            var p9 = new Polynomial(new [] { 0, 0, 0, 0 }, new [] { 0, 1, 2, 3 });
            Console.WriteLine(p9.ToString());
            p9.RemoveZeroCoefficient();
            Console.WriteLine(p9.ToString());

            Console.ReadLine();
        }
    }
}
