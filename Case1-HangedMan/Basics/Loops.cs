using System;
using System.Collections.Generic;
using System.ComponentModel.Design;

namespace Case1_HangedMan.Basics
{
    public static class Loops
    {
        public static ulong Factorial(ulong n)
        {
            if (n == 0 || n == 1)
                return 1;
            ulong fact = 1;
            for (ulong i = 1; i <= n; i++)
            {
                fact *= i;
            }

            return fact;
        }

        public static int Power(int a, int b)
        {
            int pow = 1;
            switch (b)
            {
                case < 0:
                    Console.Error.WriteLine("B must be positive or null !");
                    return 0;
                default:
                    for (int i = 0; i < b; i++) {
                        pow *= a;
                    }
                    break;
            }
            return pow;
        }
        
        public static int DivisorSum(int n)
        {
            if (n <= 0) {
                Console.Error.WriteLine("N must be positive !");
                return -1;
            }

            List<int> divisors = new List<int>();
            for (int i = 1; i < n ; i++)
                if (n % i == 0)
                    divisors.Add(i);

            int sum = 0;
            for (int i = 0; i < divisors.Count; i++) 
                sum += divisors[i];

            return sum;
        }

        public static bool PerfectNumber(int c)
        {
            return DivisorSum(c) == c;
        }
        
        public static int DecodeBinary(string s)
        {
            int length = s.Length;
            int i = 0;
            int decode = 0;
            if (s == "")
                return 0;
            while (i < length)
            {
                string str = s[i].ToString();
                decode = decode * 2 + Int32.Parse(str);
                i += 1;
            }

            return decode;
        }
        
        public static int CrackTheCode(string code)
        {
            int decode = DecodeBinary(code);
         
            int perfect = 0;

            for (int i = 1; i <= decode; i++) 
            {
                if(i % 2 == 0)
                    if (PerfectNumber(i))
                        perfect = i;
            }
            return perfect;

        }
    }
}