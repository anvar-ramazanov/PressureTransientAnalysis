using System;

namespace Team76.PTA.SpecialFunctions
{
    internal static class StehfestCoefficients
    {
        public static double Vi(int i, int n)
        {
            int j, k, min;
            double M, Vi;
            j = (int)((i + 1) / 2.0);
            M = 0;

            if (i <= (n / 2)) min = i;
            else min = n / 2;

            for (k = j; k <= min; k++)
            {
                Vi = (Math.Pow(k, n / 2) * Li(2 * k)) / (Li(n / 2 - k) * Li(k) * Li(k - 1) * Li(i - k) * Li(2 * k - i));
                M = M + Vi;
            }
            Vi = Math.Pow(-1, n / 2 + i) * M;
            return Vi;
        }

        private static double Li(int k)
        {
            double Li = 1;
            for (int i = 2; i <= k; i++)
            {
                Li = Li * i;
            }
            return Li;
        }
    }
}
