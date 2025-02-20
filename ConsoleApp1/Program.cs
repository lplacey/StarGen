using System;

public class StellarRotation
{
    public static double CalculateRotationalPeriod(double massInSolarMasses, double ageInMillionYears)
    {
        if (massInSolarMasses <= 0 || ageInMillionYears <= 0)
        {
            throw new ArgumentException("Mass and age must be greater than zero.");
        }

        // Constants for the gyrochronology model
        const double a = 0.7725;
        const double b = 0.601;
        const double c = 0.4;
        const double n = 0.5189;

        // Convert age from million years to days
        double ageInDays = ageInMillionYears * 1e6 * 365.25;

        // Estimate B-V color index from mass (approximation)
        double bMinusV = 0.4 + (massInSolarMasses - 1.0) * 0.31;

        // Calculate rotational period using the gyrochronology formula
        double rotationalPeriod = a * Math.Pow(bMinusV - c, b) * Math.Pow(ageInDays, n);

        return rotationalPeriod;
    }

    public static void Main()
    {
        double mass = 1.04;  // Example mass in solar masses
        double age = 4.6;                  // Age in million years

        double period = CalculateRotationalPeriod(mass, age);
        Console.WriteLine($"Estimated Rotational Period: {period:F2} days");
    }
}
