using System;

namespace Calculator1
{
    
    
public interface ICalculator
    {
        int Add(int a, int b);

        int Subtract(int a, int b);

        int Multiply(int a, int b);

        double Divide(int a, int b);
    }

    public class Calculator : ICalculator
    {
        public int Add(int a, int b)
        {
            return a + b;
        }
        public int Subtract(int a, int b)
        {
            return a - b;
        }
        public int Multiply(int a, int b)
        {
            return a * b;
        }
        public double Divide(int a, int b)
        {
            if (b == 0)
                throw new DivideByZeroException("Cannot divide by zero.");
            return (double)a / b;
        }

        



        public static void Main()
        {
            


        }
    }
    
}

