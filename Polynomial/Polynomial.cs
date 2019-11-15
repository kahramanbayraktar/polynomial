using System;
using System.Linq;
using System.Text;

namespace Polynomial
{
    public class Polynomial
    {
        public class Term
        {
            public int Coefficient;
            public int Power;
            public Term Next;

            public Term(int coefficient, int power, Term next)
            {
                Coefficient = coefficient;
                Power = power;
                Next = next;
            }

            public string ToString()
            {
                return "[" + Coefficient + "," + Power + "]";
            }
        }

        public Term Head;

        public Polynomial()
        {
            // zero polynomial
            Head = null;
        }

        public Polynomial(int coefficient, int power)
        {
            Head = new Term(coefficient, power, null);
        }

        public Polynomial(int[] coefficient, int[] power)
        {
            if (coefficient == null && power == null)
            {
                Head = null;
                return;
            }
            else if (coefficient.Length != power.Length)
            {
                Console.WriteLine("error: array of coefficient and power are of different sizes.");
                return;
            }
            else
            {
                Head = new Term(coefficient[0], power[0], null);
                Term tmp = Head;
                for (int i = 1; i < power.Length; i++)
                {
                    tmp.Next = new Term(coefficient[i], power[i], null);
                    tmp = tmp.Next;
                }
            }
        }

        /**
         * Polynomial addition
         * 
         * @param a First polynomial to be added.
         * @param b Second polynomial to be added.
         * @return New polynomial as the sum of polynomial a and polynomial b.
         */
        // Example: The result of addition of polynomial 2x0 + 0x1 + 3x2 and polynomial 0x0 + 1x1 + 0x2 should be 2x0 + 1x1 + 3x2
        public static Polynomial Add(Polynomial a, Polynomial b)
        {
            // DO_NOT_EDIT_ANYTHING_ABOVE_THIS_LINE V

            // TODO: for loops (i) unnecessary?

            var degreeA = a.Degree();
            var degreeB = b.Degree();
            var degree = degreeA > degreeB ? degreeA : degreeB;

            var coefficients = new int[degree + 1];
            var powers = new int[degree + 1];

            var term = a.Head;
            while (term != null)
            {
                for (var i = 0; i <= degree; i++)
                {
                    if (term.Power == i)
                    {
                        coefficients[i] = term.Coefficient;
                        powers[i] = term.Power;
                        term = term.Next;
                        break;
                    }
                }
            }

            term = b.Head;
            while (term != null)
            {
                for (var i = 0; i <= degree; i++)
                {
                    if (term.Power == i)
                    {
                        coefficients[i] += term.Coefficient;
                        term = term.Next;
                        break;
                    }
                }
            }

            return new Polynomial(coefficients, powers);

            // DO_NOT_EDIT_ANYTHING_BELOW_THIS_LINE A
        }

        /**
         * Polynomial multiplication
         * 
         * @param a First polynomial to be multiplied.
         * @param b Second polynomial to be multiplied.
         * @return New polynomial as the product of polynomial a and polynomial b.
         */
        // Example: The product of polynomial 2x0 + 3x1 and polynomial 1x2 + 1x3 should be 2x2 + 5x3 + 3x4
        // 2x⁰ + 3x¹  X  1x² + 1x³  =  2x² + 5x³ + 3x⁴

        // 2x⁰ + 3x¹
        // 1x² + 1x³
        // MULTIPLY
        // (2x⁰ * 1x²) + (2x⁰ * 1x³)     +     (3x¹ * 1x²) + (3x¹ * 1x³)
        // polyA.term0 * polyB.term0 = 2x⁰ * 1x²
        // polyA.term0 * polyB.term1 = 2x⁰ * 1x³
        // polyA.term1 * polyB.term0 = 3x¹ * 1x²
        // polyA.term1 * polyB.term1 = 3x¹ * 1x³

        // 2x² + 5x³ + 3x⁴
        public static Polynomial Multiply(Polynomial a, Polynomial b)
        {
            // DO_NOT_EDIT_ANYTHING_ABOVE_THIS_LINE V

            var degreeA = a.Degree();
            var degreeB = b.Degree();
            var degree = degreeA + degreeB;

            var coefficients = new int[degree + 1];
            var powers = new int[degree + 1];

            var termA = a.Head; // 2x⁰


            while (termA != null)
            {
                var termB = b.Head; // 1x²

                while (termB != null)
                {
                    // termA * termB => 2x⁰ * 1x²
                    var coefficientA = termA.Coefficient;
                    var coefficientB = termB.Coefficient;
                    var coefficientTotal = coefficientA * coefficientB;

                    coefficients[termA.Power + termB.Power] += coefficientTotal;
                    powers[termA.Power + termB.Power] = termA.Power + termB.Power;

                    termB = termB.Next;
                }

                termA = termA.Next;
            }


            while (termA != null)
            {
                var termB = b.Head; // 1x²

                while (termB != null)
                {
                    // termA * termB => 2x⁰ * 1x²
                    var coefficientA = termA.Coefficient;
                    var coefficientB = termB.Coefficient;
                    var coefficientTotal = coefficientA * coefficientB;

                    coefficients[termA.Power + termB.Power] += coefficientTotal;
                    powers[termA.Power + termB.Power] = termA.Power + termB.Power;

                    termB = termB.Next;
                }

                termA = termA.Next;
            }

            return new Polynomial(coefficients, powers);

            // DO_NOT_EDIT_ANYTHING_BELOW_THIS_LINE A
        }

        public static Polynomial MultiplyRemoveZeroCoefficient(Polynomial a, Polynomial b)
        {
            // DO_NOT_EDIT_ANYTHING_ABOVE_THIS_LINE V

            var termA = a.Head; // 2x⁰

            // Find powers to exist in the result polynomial.
            var availablePowersString = "";
            while (termA != null)
            {
                var termB = b.Head; // 1x²

                while (termB != null)
                {
                    var power = termA.Power + termB.Power;
                    if (!availablePowersString.Contains(power + ","))
                        availablePowersString += termA.Power + termB.Power + ",";

                    termB = termB.Next;
                }

                termA = termA.Next;
            }

            // Put powers to exist in the result polynomial in an array.
            var availablePowers = availablePowersString.Split(",", StringSplitOptions.RemoveEmptyEntries);
            var availablePowersArray = new int[availablePowers.Length];
            for (var i = 0; i < availablePowers.Length; i++)
            {
                availablePowersArray[i] = Convert.ToInt32(availablePowers[i]);
            }

            // Create and initialize arrays to store coefficients and powers.
            var coefficients = new int[availablePowersArray.Length];
            var powers = new int[availablePowersArray.Length];

            termA = a.Head;

            while (termA != null)
            {
                var termB = b.Head; // 1x²

                while (termB != null)
                {
                    // termA * termB => 2x⁰ * 1x²
                    var coefficientA = termA.Coefficient;
                    var coefficientB = termB.Coefficient;
                    var coefficientTotal = coefficientA * coefficientB;

                    var index = 0;
                    for (var i = 0; i < availablePowers.Length; i++)
                    {
                        if (termA.Power + termB.Power == availablePowersArray[i])
                        {
                            index = i;
                            break;
                        }
                    }

                    coefficients[index] += coefficientTotal;
                    powers[index] = termA.Power + termB.Power;

                    termB = termB.Next;
                }

                termA = termA.Next;
            }

            return new Polynomial(coefficients, powers);

            // DO_NOT_EDIT_ANYTHING_BELOW_THIS_LINE A
        }

        /**
         * Removes the terms with zero coefficients.
         * 
         * If all the coefficients are zero, returns a zero polynomial, i.e., 0x0
         */
        public void RemoveZeroCoefficient()
        {
            // DO_NOT_EDIT_ANYTHING_ABOVE_THIS_LINE V

            var term = Head;
            Term previous = null;

            while (term != null)
            {
                if (term.Coefficient == 0)
                {
                    if (previous == null)
                    {
                        Head = term.Next;
                    }
                    else
                    {
                        previous.Next = term.Next;
                    }
                }
                else
                {
                    previous = term;
                }

                term = term.Next;
            }

            // If all coefficients are 0, then return a zero polynomial.
            if (Head == null)
                Head = new Term(0, 0, null);

            // DO_NOT_EDIT_ANYTHING_BELOW_THIS_LINE A
        }

        /* degree returns -1 for null polynomial */
        private const int PolynomialDegreeNull = -1;

        /**
         * @return Returns the degree of the polynomial. If the polynomial is null, then
         *         returns -1.
         */
        public int Degree()
        {
            int degree = PolynomialDegreeNull;
            if (Head == null)
            {
                return degree;
            }
            else
            {
                degree = 0; // If the polynomial has a head, then its degree is set to 0.
            }

            Term tmp = Head;
            if (tmp.Coefficient != 0)
            {
                degree = degree > tmp.Power ? degree : tmp.Power;
            }

            tmp = tmp.Next;
            while (tmp != null)
            {
                if (tmp.Coefficient != 0)
                {
                    degree = degree > tmp.Power ? degree : tmp.Power;
                }
                tmp = tmp.Next;
            }
            return degree;
        }

        /* canonical of a null polynomial */
        private const string PolynomialEmpty = "NULL";

        public string ToCanonical()
        {
            Term tmpNode = Head;
            if (tmpNode == null)
            {
                return PolynomialEmpty;
            }
            else
            {
                StringBuilder str = new StringBuilder();
                str.Append(tmpNode.Coefficient);
                str.Append("x");
                str.Append(tmpNode.Power);
                tmpNode = tmpNode.Next;
                while (tmpNode != null)
                {
                    str.Append("+");
                    str.Append(tmpNode.Coefficient);
                    str.Append("x");
                    str.Append(tmpNode.Power);
                    tmpNode = tmpNode.Next;
                }
                //str.Append(".");
                return str.ToString();
            }
        }

        public string ToString()
        {
            StringBuilder str = new StringBuilder();
            str.Append("[Polynomial:");
            str.Append(ToCanonical());
            str.Append("]");
            return str.ToString();
        }
    }
}
