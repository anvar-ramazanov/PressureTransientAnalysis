namespace Team76.PTA
{
    /// <summary>
    /// 
    /// </summary>
    public static class Dimensionless
    {
        /// <summary>
        /// Dimensionless pressure as defined for constant-rate production
        /// </summary>
        /// <param name="P">pressure, psi</param>
        /// <param name="Pi">original reservoir pressure, psi</param>
        /// <param name="q">flow rate at surface, STB/D</param>
        /// <param name="k">matrix permeability, md</param>
        /// <param name="h">net formation thickness, ft</param>
        /// <param name="B">formation volume factor, res vol/surface vol</param>
        /// <param name="mu">viscosity, cp</param>
        /// <returns></returns>
        public static double Pressure(double P, double Pi, double q, double k, double h, double B, double mu)
        {
            return k * h * (Pi - P) / (141.2 * q * B * mu);
        }

        /// <summary>
        /// Dimensionless time
        /// </summary>
        /// <param name="t">elapsed time, hours</param>
        /// <param name="k">matrix permeability, md</param>
        /// <param name="poro">porosity, dimensionless</param>
        /// <param name="mu">viscosity, cp</param>
        /// <param name="ct">total compressibility, 1/psi</param>
        /// <param name="rw">wellbore radius, ft</param>
        /// <returns></returns>
        public static double Time(double t, double k, double poro, double mu, double ct, double rw)
        {
            return 0.0002637 * k * t / (poro * mu * ct * rw * rw);
        }

        /// <summary>
        /// Dimensionless radius
        /// </summary>
        /// <param name="r">distance from the center of wellbore, ft</param>
        /// <param name="rw">wellbore radius, ft</param>
        /// <returns></returns>
        public static double Radius(double r, double rw)
        {
            return r / rw;
        }

        /// <summary>
        /// Dimensionless wellbore storage coefficient
        /// </summary>
        /// <param name="C">wellbore storage coefficient, bbl/psi</param>
        /// <param name="poro">porosity, dimensionless</param>
        /// <param name="ct">total compressibility, 1/psi</param>
        /// <param name="h">net formation thickness, ft</param>
        /// <param name="rw">wellbore radius, ft</param>
        /// <returns></returns>
        public static double WellboreStorageCoefficient(double C, double poro, double ct, double h, double rw)
        {
            return 0.8936 * C / (poro * ct * h * rw * rw);
        }

        /// <summary>
        /// Dimensionless distance
        /// </summary>
        /// <param name="l">distance from the center of wellbore, ft</param>
        /// <param name="rw">wellbore radius, ft</param>
        /// <returns></returns>
        public static double Distance(double l, double rw)
        {
            return l / rw;
        }
    }
}
