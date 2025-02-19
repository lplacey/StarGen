using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace StarGen
{
    public class StarSystemSimulator : StellarObject
    {
        //enum StarLum
        //{
        //        0,      //  Extremely Luminous SuperGiants
        //        1a,     //  Luminous SuperGiants
        //        1ab,    //  Luminous SuperGiants ( Intermediate )
        //        1b,     //  Less Luminous SuperGiants
        //        II,     //  Bright Giants
        //        III,    //  Giants
        //        IV,     //  SubGiants
        //        V,      //  Main Sequence
        //        VI,     //  Sub Dwarfs
        //        VII     //  White Dwarfs
        //}

        private const double SolarLuminosity = 3.828e26; // Watts
        private const double SolarMass = 1.989e30; // kg
        private const double SolarRadius = 6.955e8; // meters
        private const double SolarTemperature = 5778; // Kelvin        
        private static readonly Random random = new Random();
        private const double RotationConstant = 10;

        // Stefan-Boltzmann constant (in W/m^2/K^4)
        private const double StefanBoltzmannConstant = 5.67e-8;

        public string StarName { get; set; }
        public double StarMass { get; set; } // in kg
        public double StarMassRatio { get; set; }
        public double StarRadiusRatio { get; set; }

        public double StarRadiusInMeters { get; set; }
        public double StarRadiusInMeters2 { get; set; }
        public double StarRadiusInMeters3 { get; set; }
        public double StarLuminosity { get; set; } // in Watts
        //public double OrbitalSpeed { get; set; } // in m/s
        public double StarLuminosityRatio { get; set; }
        public double StarInnerHabitableZone { get; set; }
        public double StarOuterHabitableZone { get; set; }

        public static List<(string spectralClass, string subclass, string yerkesClass, double minRadius, double maxRadius, double minLum, double maxLum, double minMass, double maxMass, double minTemp, double maxTemp, string color, double occur, double prob, double minPeriod, double maxPeriod, string notes)> StarTypes = new List<(string, string, string, double, double, double, double, double, double, double, double, string, double, double, double, double, string)>();
        
        public StarSystemSimulator(string starName, double starMass, double starLuminosity, double planetSize, double orbitalSpeed)
        {
            StarName = starName;
            StarMass = starMass;
            StarLuminosity = starLuminosity;            
            OrbitalSpeed = orbitalSpeed;
        }

        public StarSystemSimulator(string SpectralClass, string subclass) : base()
        {
            StarName = "Test";
            LoadStarTypesFromCSV("Fully_Validated_Stellar_Classification_Data.csv");
            GenerateRandomStarProperties(SpectralClass, subclass);
            //StarMass = starMass;
            //StarLuminosity = starLuminosity;
            //PlanetSize = planetSize;
            //OrbitalSpeed = orbitalSpeed;
        }

        public static void LoadStarTypesFromCSV(string filePath)
        {
            try
            {
                using (var reader = new StreamReader(filePath))
                {

                    if (!reader.EndOfStream)
                        reader.ReadLine(); // Skip the first line (header)

                    //string headerLine = reader.ReadLine(); // Skip the header
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var values = line.Split(',');

                        if (values.Length < 11) continue;

                        StarTypes.Add(
                            (values[0].Trim(),
                             values[1].Trim(),
                             values[2].Trim(),
                             double.Parse(values[3], CultureInfo.InvariantCulture),
                             double.Parse(values[4], CultureInfo.InvariantCulture),
                             double.Parse(values[5], CultureInfo.InvariantCulture),
                             double.Parse(values[6], CultureInfo.InvariantCulture),
                             double.Parse(values[7], CultureInfo.InvariantCulture),
                             double.Parse(values[8], CultureInfo.InvariantCulture),
                             double.Parse(values[9], CultureInfo.InvariantCulture),
                             double.Parse(values[10], CultureInfo.InvariantCulture),
                             values[11].Trim(),
                             double.Parse(values[12], CultureInfo.InvariantCulture),
                             double.Parse(values[13], CultureInfo.InvariantCulture),
                             double.Parse(values[14], CultureInfo.InvariantCulture),
                             double.Parse(values[15], CultureInfo.InvariantCulture),
                             values.Length > 16 ? values[16].Trim() : "")
                        );
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        #region Temperature Calculations

        public static double CalculateSurfaceTemperature(double luminosity, double radius)
        {
            // Luminosity (in watts) and radius (in meters) are given as inputs
            // Temperature (in kelvins) is calculated using the Stefan-Boltzmann law

            // Ensure the radius is in meters (assuming input radius is in meters already)
            double temperature = Math.Pow(luminosity / (4 * Math.PI * Math.Pow(radius, 2) * StefanBoltzmannConstant), 0.25);

            return temperature;
        }

        public static double CalculateSurfaceTemperatureFromMassRatio(double massRatio, double luminosity)   // Uses MassRatio and luminosity
        {
            // Estimate the radius based on the luminosity (Luminosity-Radius relation)
            // R^2 = L / (4 * pi * sigma * T^4)
            // So, T = (L / (4 * pi * sigma * R^2)) ^ (1/4)
            // Here, we'll use an approximation for the radius for a typical main sequence star.
            double radius = SolarRadius * Math.Pow(massRatio, 0.8);  // Approximation for main-sequence stars

            // Now calculate the temperature using the Stefan-Boltzmann law
            double temperature = Math.Pow(luminosity / (4 * Math.PI * StefanBoltzmannConstant * Math.Pow(radius, 2)), 0.25);
            return temperature;
        }

        #endregion

        #region Luminosity Calculations

        public double CalculateLuminosityExponent(double massRatio)
        {
            if (massRatio <= 0)
                return 0;

            return massRatio < 0.43 ? 2.3 : (massRatio < 2.0 ? 4.0 : (massRatio < 20.0 ? 3.5 : 3.0));
        }

        public double CalculateLuminosityFromMassRatio(double massRatio)
        {
            if (massRatio <= 0)
                return 0;

            double exponent = CalculateLuminosityExponent(massRatio);            
            double luminosityRatio = Math.Pow(massRatio, exponent);
            return SolarLuminosity * luminosityRatio;
        }

        public double CalculateMassRatioFromMass(double massInKg)
        {
            if (massInKg <= 0)
                return 0;

            return massInKg / SolarMass;
        }

        public double CalculateLuminosityRatioFromMass(double massInKg)
        {
            if (massInKg <= 0)
                return 0;

            double massRatio = CalculateMassRatioFromMass(massInKg);
            double exponent = CalculateLuminosityExponent(massRatio);
            // Star Luminosity in Watts
            double starLuminosityRatio = Math.Pow(massRatio, exponent);
            
            return Math.Max(0, starLuminosityRatio);
        }

        public double CalculateLuminosityInWattsFromMass(double mass)
        {
            return SolarLuminosity * CalculateLuminosityRatioFromMass(mass);
        }

        public double CalculateLuminosityRatioFromMassRatio(double massRatio)
        {
            if (massRatio <= 0)
                return 0;

            return CalculateLuminosityFromMassRatio(massRatio) / SolarLuminosity;
        }

        #endregion

        #region Radius Calculations

        // Function to calculate radius based on the Mass-Radius relation
        public static double CalculateRadiusFromMass(double mass, double alpha = 0.8)
        {
            // Radius = Solar radius * (Mass / Solar mass) ^ alpha
            double radius = SolarRadius * Math.Pow(mass, alpha);
            return radius;
        }

        // Function to calculate radius based on the Luminosity-Radius relation using the Stefan-Boltzmann Law
        public static double CalculateRadiusFromLuminosity(double luminosity, double temperature)
        {
            // Radius = sqrt(Luminosity / (4 * π * σ * T^4))
            double radius = Math.Sqrt(luminosity / (4 * Math.PI * StefanBoltzmannConstant * Math.Pow(temperature, 4)));
            return radius;
        }

        public static double CalculateRadiusFromRadiusRatio(double StarRadiusRatio)
        {
            double starRadiusInMeters = StarRadiusRatio * SolarRadius; // 1.5 times the solar radius            
            
            return starRadiusInMeters;
        }

        public static double CalculateRadiusRatio(double massRatio)
        {
            /*
            * Dynamic Mass-Radius Algorithm:
            * - Uses smooth transition exponents based on empirical stellar models.
            * - Interpolates between main-sequence stars, brown dwarfs, and giant stars.
            */

            double exponent;

            if (massRatio >= 30)         // O-type supergiants
                exponent = 1.0;
            else if (massRatio >= 10)    // O-type main sequence
                exponent = 0.9;
            else if (massRatio >= 2)     // B/A-type stars
                exponent = 0.7;
            else if (massRatio >= 1.5)   // F-type stars
                exponent = 0.65;
            else if (massRatio >= 1)     // G-type (Sun-like)
                exponent = 0.6;
            else if (massRatio >= 0.5)   // K/M-type (cool main sequence)
                exponent = 0.55;
            else if (massRatio >= 0.08)  // L/T-type (brown dwarfs)
                exponent = 0.4;
            else if (massRatio >= 0.05)  // T/Y brown dwarfs
                exponent = 0.3;
            else                    // Substellar objects
                exponent = 0.2;

            double rad = Math.Pow(massRatio, exponent);
            // Calculate radius using mass-radius relationship
            return rad;
        }

        public static double CalculateStarRadius(string SpectralClass, string subclass, double luminosityRatio)
        {
            var starData = StarTypes.FirstOrDefault(s => s.spectralClass == SpectralClass && s.subclass == subclass);

            if (starData == default)
            {
                return 0;
            }

            double minRadius = starData.minRadius;
            double maxRadius = starData.maxRadius;
            double minLum = starData.minLum;
            double maxLum = starData.maxLum;

            // Prevent division by zero
            if (maxLum - minLum == 0)
                return minRadius; // If no luminosity range, return min radius

            // Interpolate the radius based on luminosity
            double radius = minRadius + ((luminosityRatio - minLum) / (maxLum - minLum)) * (maxRadius - minRadius);

            // Clamp radius within the range
            return Math.Max(minRadius, Math.Min(radius, maxRadius));
        }

        public static double CalculateRadiusFromLuminosityAndMass(double massInKg, double luminosityRatio, double temperature)
        {
           
            // Use the Stefan-Boltzmann law to calculate the radius
            // Radius = sqrt(L / (4 * pi * sigma * T^4))
            double radius = Math.Sqrt(luminosityRatio / (4 * Math.PI * StefanBoltzmannConstant * Math.Pow(temperature, 4)));

            return radius;
        }

        #endregion

        #region Rotational Calculations

        public static double CalculateRotationalPeriod(double mass)
        {
            return RotationConstant / Math.Sqrt(mass);
        }

        public static double CalculateRotationalSpeed(double mass)
        {
            // Calculate the radius (in Solar radii)
            double radius = CalculateRadiusRatio(mass);
            
            // Convert radius to meters (1 R⊙ = 6.96 x 10^8 m)
            double radiusInMeters = radius * 6.96e8;

            // Calculate the rotational period (in seconds)
            double rotationalPeriod = CalculateRotationalPeriod(mass) * 86400; // Convert days to seconds

            // Calculate the equatorial velocity (v_rot = 2 * pi * R / P)
            double rotationalSpeed = (2 * Math.PI * radiusInMeters) / rotationalPeriod;

            // Convert rotational speed to km/s
            rotationalSpeed /= 1000;

            return rotationalSpeed;  // in km/s
        }

        #endregion

        #region HabitableZone Calculations

        public double CalculateHabitableZoneInner()
        {
            return Math.Sqrt(StarLuminosityRatio) * 0.95; // AU
        }

        public double CalculateHabitableZoneOuter()
        {
            return Math.Sqrt(StarLuminosityRatio) * 1.37; // AU
        }

        #endregion

        #region AgeRelatedCalculations

        public static (double mass, double luminosity, double radius, double temperature) CalculateStellarProperties(
                double initialMass, double initialLuminosity, double initialRadius, double initialTemperature, double age)
        {
            // Determine the Main Sequence Lifetime (approximate)
            double mainSequenceLifetime = 10 * Math.Pow(initialMass / SolarMass, -2.5) * 1e9; // years

            double mass = initialMass;
            double luminosity = initialLuminosity;
            double radius = initialRadius;
            double temperature = initialTemperature;

            if (age <= mainSequenceLifetime)
            {
                // Main Sequence Phase
                mass = initialMass * (1 - 0.1 * (age / mainSequenceLifetime));
                luminosity = initialLuminosity * Math.Pow(mass / initialMass, 3.5);
                radius = initialRadius * Math.Pow(mass / initialMass, 0.8);
                temperature = initialTemperature * Math.Pow(mass / initialMass, 0.5);
            }
            else if (age <= mainSequenceLifetime + 1e9)
            {
                // Red Giant Phase (after hydrogen depletion)
                mass = initialMass * 0.8; // Loses more mass
                luminosity = initialLuminosity * 1000;
                radius = initialRadius * 100;
                temperature = 3500; // Red giants have cooler surfaces
            }
            else
            {
                // White Dwarf Phase (after shedding outer layers)
                mass = initialMass * 0.6; // Further mass loss
                luminosity = initialLuminosity * Math.Exp(-(age - (mainSequenceLifetime + 1e9)) / 1e9);
                radius = initialRadius * 0.01;
                temperature = 100000 * Math.Exp(-(age - (mainSequenceLifetime + 1e9)) / 1e9);
            }

            return (mass, luminosity, radius, temperature);
        }

        public static (double mass, double luminosity, double radius, double temperature, double rotationalPeriod) CalculateStellarProperties2(
        double initialMass, double initialLuminosity, double initialRadius, double initialTemperature, double age)
        {
            // Constants
            double SolarMass = 1.989e30;  // Solar mass in kg
            double mainSequenceLifetime = 10 * Math.Pow(initialMass / SolarMass, -2.5) * 1e9; // years

            // Initial properties
            double mass = initialMass;
            double luminosity = initialLuminosity;
            double radius = initialRadius;
            double temperature = initialTemperature;

            // Rotational Period (in days)
            double rotationalPeriod;

            if (age <= mainSequenceLifetime)
            {
                // Main Sequence Phase
                mass = initialMass * (1 - 0.1 * (age / mainSequenceLifetime));
                luminosity = initialLuminosity * Math.Pow(mass / initialMass, 3.5);
                radius = initialRadius * Math.Pow(mass / initialMass, 0.8);
                temperature = initialTemperature * Math.Pow(mass / initialMass, 0.5);

                // Main sequence stars have relatively short rotational periods.
                rotationalPeriod = 10 / Math.Pow(mass, 1.5); // Example empirical formula for rotational period
            }
            else if (age <= mainSequenceLifetime + 1e9)
            {
                // Red Giant Phase (after hydrogen depletion)
                mass = initialMass * 0.8; // Loses more mass
                luminosity = initialLuminosity * 1000;
                radius = initialRadius * 100;
                temperature = 3500; // Red giants have cooler surfaces

                // Red giants have longer rotational periods compared to main sequence stars.
                rotationalPeriod = 100; // Approximate for red giants (e.g., 100 days)
            }
            else
            {
                // White Dwarf Phase (after shedding outer layers)
                mass = initialMass * 0.6; // Further mass loss
                luminosity = initialLuminosity * Math.Exp(-(age - (mainSequenceLifetime + 1e9)) / 1e9);
                radius = initialRadius * 0.01;
                temperature = 100000 * Math.Exp(-(age - (mainSequenceLifetime + 1e9)) / 1e9);

                // White dwarfs have very slow rotation due to the loss of angular momentum.
                rotationalPeriod = 500; // Approximate for white dwarfs (e.g., 500 days)
            }

            return (mass, luminosity, radius, temperature, rotationalPeriod);
        }

        #endregion

        #region Axial Tilt

        // Function to generate a random axial tilt based on a Gaussian distribution
        public static double GenerateAxialTilt(double mean, double standardDeviation)
        {
            Random rand = new Random();

            // Using the Box-Muller transform to generate two independent standard normal variables
            double u1 = 1.0 - rand.NextDouble(); // Uniform random variable between 0 and 1
            double u2 = 1.0 - rand.NextDouble(); // Another uniform random variable

            // Applying the Box-Muller transform
            double z0 = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Cos(2.0 * Math.PI * u2);
            double z1 = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2);

            // We only need z0 (for generating one normally distributed value)
            double normalRandomValue = z0;

            // Scale and shift the normal distribution to get the desired tilt
            double tilt = mean + normalRandomValue * standardDeviation;

            // Return the axial tilt value, ensuring it's between 0° and 90° (if needed)
            return Math.Max(0, Math.Min(90, tilt));
        }

        #endregion

        public void GenerateRandomStarProperties(string SpectralClass, string Subclass)
        {
            foreach (var (spectralClass, subClass, yerk, minRad, maxRad, minLum, maxLum, minMass, maxMass, minTemp, maxTemp, color, occur, prob, minPeriod, maxPeriod, notes) in StarTypes)
            {
                if (spectralClass == SpectralClass && subClass == Subclass)
                {
                    StarMassRatio = (random.NextDouble() * (maxMass - minMass) + minMass);
                    StarMass = SolarMass * StarMassRatio;
                    Mass = SolarMass * StarMassRatio;
                    StarLuminosityRatio = CalculateLuminosityRatioFromMass(Mass);
                    StarLuminosity = CalculateLuminosityInWattsFromMass(Mass);
                    Luminosity = CalculateLuminosityInWattsFromMass(Mass);
                    StarRadiusRatio = CalculateRadiusRatio(StarMassRatio);
                    StarRadiusInMeters = CalculateRadiusFromRadiusRatio(StarRadiusRatio);
                    Radius = CalculateRadiusFromRadiusRatio(StarRadiusRatio);

                    // Function to calculate the rotational speed (in km/s) based on mass of the star
                    RotationalSpeed = CalculateRotationalSpeed(StarMassRatio);

                    // Function to calculate the rotational period (P) based on mass (in solar masses)
                    RotationalPeriod = CalculateRotationalPeriod(StarMassRatio);

                    // Generate a random axial tilt for a star
                    // GenerateAxialTilt(mean, standard deviation);
                    AxialTilt = GenerateAxialTilt(0.0, 5.0);  // Mean tilt = 0° (aligned with galactic plane)  // Standard deviation = 5°

                    double StarSurfaceTemp = CalculateSurfaceTemperatureFromMassRatio(StarMassRatio, StarLuminosity);    //temp of the sun in kelvin

                    SurfaceTemperature = CalculateSurfaceTemperature(StarLuminosity, StarRadiusInMeters);

                    StarInnerHabitableZone = CalculateHabitableZoneInner();
                    StarOuterHabitableZone = CalculateHabitableZoneOuter();

                    

                    return;
                }
            }
        }
    }
}

