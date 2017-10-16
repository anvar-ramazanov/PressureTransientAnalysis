using System;
using NUnit.Framework;

namespace Team76.PTA.Tests.Sandbox
{
    [TestFixture, Ignore("Doesn't work"), Explicit]
    public static class DataAnalysisTest
    {

        [Test]
        public static void DerivativeTest()
        {
            
            int k1;
            int k2;
            int k3;
            int k4;
            int n = 10;
            double L;
            double L1;
            double Lleft;
            double Lright;
            int left;
            int right;
            double[] f = new double[n]; ; // 'cartesian
            double[] g = new double[n]; ; // 'semilog
            double[] k = new double[n]; ; // 'powerlaw
            double[] delPcL = new double[n];
            double[] delPcR = new double[n];
            double[] deltcL = new double[n];
            double[] deltcR = new double[n];
            double[] delPsL = new double[n];
            double[] delPsR = new double[n];
            double[] delPpL = new double[n];
            double[] delPpR = new double[n];
            double[] deltsL = new double[n];
            double[] deltsR = new double[n];
            double[] delp = new double[]{
                0.57,
                3.81,
                6.55,
                10.03,
                13.27,
                16.77,
                20.01,
                23.25,
                26.49,
                29.48
            };
            double[] delt = new double[]{
                0.004168866,
                0.008325476,
                0.012489816,
                0.016651893,
                0.020801735,
                0.024959297,
                0.029114601,
                0.033257692,
                0.037408492,
                0.045693397
            };


            L1 = 0.2;
           

            k1 = 2;
            k2 = 1;
            k3 = 2;
            k4 = 1;

            deltcL[1] = delt[k1] - delt[1];
            deltcR[n] = delt[n] - delt[n - k2];
            deltsL[1] = Math.Log(delt[k3] / delt[1]) / Math.Log(10);
            deltsR[n] = Math.Log(delt[n] / delt[n - k4]) / Math.Log(10);

            while (deltcL[1] < L1)
            {
                k1 = k1 + 1;
                deltcL[1] = delt[k1] - delt[1];
            }

            while (deltcR[n] < L1)
            {
                k2 = k2 + 1;
                deltcR[n] = delt[n] - delt[n - k2];
            }

            while (deltsL[1] < L1)
            {
                k3 = k3 + 1;
                deltsL[1] = Math.Log(delt[k3] / delt[1]) / Math.Log(10);
            }

            while (deltsR[n] < L1)
            {
                k4 = k4 + 1;
                deltsR[n] = Math.Log(delt[n] / delt[n - k4]) / Math.Log(10);
            }

            f[1] = (delp[k1] - delp[1]) / (delt[k1] - delt[1]);
            f[n] = (delp[n] - delp[n - k2]) / (delt[n] - delt[n - k2]);


            //For i = 2 To n -1

            for (int i = 2; i < n-1; i++)
            {
                L = L1;

                left = 1;
                right = 1;

                deltcL[i] = (delt[i] - delt[i - left]);
                deltcR[i] = (delt[i + right] - delt[i]);
                delPcL[i] = delp[i] - delp[i - left];
                delPcR[i] = delp[i + right] - delp[i];

                while (deltcL[i]< L && (i-left)>1)
                {
                    left = left + 1;
                    deltcL[i] = (delt[i] - delt[i - left]);
                    delPcL[i] = delp[i] - delp[i - left];
                }

                left = 1;
                right = 1;

                while (deltcR[i] < L && (i + right) < n)
                {
                    right = right + 1;
                    deltcR[i] = (delt[i + right] - delt[i]);
                    delPcR[i] = delp[i + right] - delp[i];
                }
            }


            for (int i = 1; i < k3; i++)
            {
                L = L1;
                right = 1;
                deltsR[i] = Math.Log(delt[i + right] / delt[i]) / Math.Log(10);

                while (deltsR[i] < L)
                {
                    right = right + 1;
                    deltsR[i] = Math.Log(delt[i + right] / delt[i]) / Math.Log(10);
                }

                g[i] = (delp[i + right] - delp[1]) / (Math.Log(delt[i + right] / delt[1]));
                k[i] = (Math.Log(delp[i + right] / delp[1])) / (Math.Log(delt[i + right] / delt[1]));
            }

            for (int i = n- k4; i < n; i++)
            {
                L = L1;
                left = 1;
                deltsL[i] = Math.Log(delt[i] / delt[i - left]) / Math.Log(10);

                while (deltsL[i] < L && (i-left)>1)
                {
                    left = left + 1;
                    deltsL[i] = Math.Log(delt[i] / delt[i - left]) / Math.Log(10);
                }

                g[i] = (delp[n] - delp[i - left]) / (Math.Log(delt[n] / delt[i - left]));
                k[i] = (Math.Log(delp[n] / delp[i - left])) / (Math.Log(delt[n] / delt[i - left]));
            }

            for (int i = k3; i < n-k4; i++)
            {
                L = L1;
                left = 1;
                right = 1;
                deltsL[i] = Math.Log(delt[i] / delt[i - left]) / Math.Log(10);
                deltsR[i] = Math.Log(delt[i + right] / delt[i]) / Math.Log(10);
                delPpL[i] = Math.Log(delp[i] / delp[i - left]);
                delPpR[i] = Math.Log(delp[i + right] / delp[i]);
                delPsL[i] = delp[i] - delp[i - left];
                delPsR[i] = delp[i + right] - delp[i];

                while (deltsL[i] < L && (i - left) > 1)
                {
                    left = left + 1;
                    deltsL[i] = Math.Log(delt[i] / delt[i - left]) / Math.Log(10);
                    delPpL[i] = Math.Log(delp[i] / delp[i - left]);
                    delPsL[i] = delp[i] - delp[i - left];
                }

                deltsL[i] = deltsL[i] * Math.Log(10);

                while (deltsR[i] < L && (i + right) < n)
                {
                    right = right + 1;
                    deltsR[i] = Math.Log(delt[i + right] / delt[i]) / Math.Log(10);
                    delPpR[i] = Math.Log(delp[i + right] / delp[i]);
                    delPsR[i] = delp[i + right] - delp[i];
                }

                deltsR[i] = deltsR[i] * Math.Log(10);
            }

            for (int i = 2; i < n-1; i++)
            {
                f[i] = ((deltcR[i] / (deltcL[i] + deltcR[i])) * delPcL[i] / deltcL[i]) +
                       ((deltcL[i] / (deltcL[i] + deltcR[i])) * delPcR[i] / deltcR[i]);
            }

            for (int i = k3; i < n-k4; i++)
            {
                g[i] = ((deltsR[i] / ((deltsL[i] + deltsR[i]))) * delPsL[i] / deltsL[i]) +
                       ((deltsL[i] / ((deltsL[i] + deltsR[i]))) * delPsR[i] / deltsR[i]);
                k[i] = ((deltsR[i] / ((deltsL[i] + deltsR[i]))) * delPpL[i] / deltsL[i]) +
                       ((deltsL[i] / ((deltsL[i] + deltsR[i]))) * delPpR[i] / deltsR[i]);
            }
        }

        [Test]
        public static void DerivativeTestCart()
        {
            int n = 10;

            double[] f = new double[n]; ; // 'cartesian

            double[] delPcL = new double[n];
            double[] delPcR = new double[n];
            double[] deltcL = new double[n];
            double[] deltcR = new double[n];

            double[] delp = new double[]{
                0.57,
                3.81,
                6.55,
                10.03,
                13.27,
                16.77,
                20.01,
                23.25,
                26.49,
                29.48
            };
            double[] delt = new double[]{
                0.004168866,
                0.008325476,
                0.012489816,
                0.016651893,
                0.020801735,
                0.024959297,
                0.029114601,
                0.033257692,
                0.037408492,
                0.045693397
            };


            var L1 = 0.2;
            var k1 = 2;
            var k2 = 1;


            deltcL[1] = delt[k1] - delt[1];
            deltcR[n] = delt[n] - delt[n - k2];

            while (deltcL[1] < L1)
            {
                k1 = k1 + 1;
                deltcL[1] = delt[k1] - delt[1];
            }

            while (deltcR[n] < L1)
            {
                k2 = k2 + 1;
                deltcR[n] = delt[n] - delt[n - k2];
            }


            f[1] = (delp[k1] - delp[1]) / (delt[k1] - delt[1]);
            f[n] = (delp[n] - delp[n - k2]) / (delt[n] - delt[n - k2]);


            for (int i = 2; i < n - 1; i++)
            {
                var L = L1;

                var left = 1;
                var right = 1;

                deltcL[i] = (delt[i] - delt[i - left]);
                deltcR[i] = (delt[i + right] - delt[i]);
                delPcL[i] = delp[i] - delp[i - left];
                delPcR[i] = delp[i + right] - delp[i];

                while (deltcL[i] < L && (i - left) > 1)
                {
                    left = left + 1;
                    deltcL[i] = (delt[i] - delt[i - left]);
                    delPcL[i] = delp[i] - delp[i - left];
                }

                left = 1;
                right = 1;

                while (deltcR[i] < L && (i + right) < n)
                {
                    right = right + 1;
                    deltcR[i] = (delt[i + right] - delt[i]);
                    delPcR[i] = delp[i + right] - delp[i];
                }
            }



            for (int i = 2; i < n - 1; i++)
            {
                f[i] = ((deltcR[i] / (deltcL[i] + deltcR[i])) * delPcL[i] / deltcL[i]) +
                       ((deltcL[i] / (deltcL[i] + deltcR[i])) * delPcR[i] / deltcR[i]);
            }


        }

    }
}
