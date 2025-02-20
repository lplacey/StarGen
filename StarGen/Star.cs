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
 
        private static readonly Random random = new Random();      

        //public string StarName { get; set; }
        //public double StarMass { get; set; } // in kg
        //public double StarMassRatio { get; set; }
        //public double StarRadiusRatio { get; set; }
        //public double StarRadiusInMeters { get; set; }
        //public double StarRadiusInMeters2 { get; set; }
        //public double StarRadiusInMeters3 { get; set; }
        //public double StarLuminosity { get; set; } // in Watts
        //public double OrbitalSpeed { get; set; } // in m/s
        //public double StarLuminosityRatio { get; set; }
        public double StarInnerHabitableZone { get; set; }
        public double StarOuterHabitableZone { get; set; }

        public static List<(string spectralClass, string subclass, string yerkesClass, double minRadius, double maxRadius, double minLum, double maxLum, double minMass, double maxMass, double minTemp, double maxTemp, string color, double occur, double prob, double minPeriod, double maxPeriod, string notes)> StarTypes = new List<(string, string, string, double, double, double, double, double, double, double, double, string, double, double, double, double, string)>();
        
        public StarSystemSimulator(string starName, double starMass, double starLuminosity, double planetSize, double orbitalSpeed)
        {
            Name = starName;
            Mass = starMass;
            Luminosity = starLuminosity;            
            OrbitalSpeed = orbitalSpeed;
        }

        public StarSystemSimulator(string SpectralClass, string subclass) : base()
        {
            Name = "Test";
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

        #region HabitableZone Calculations

        public double CalculateHabitableZoneInner()
        {
            return Math.Sqrt(LuminosityRatio) * 0.95; // AU
        }

        public double CalculateHabitableZoneOuter()
        {
            return Math.Sqrt(LuminosityRatio) * 1.37; // AU
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

        

        public void GenerateRandomStarProperties(string SpectralClass, string Subclass)
        {
            foreach (var (spectralClass, subClass, yerk, minRad, maxRad, minLum, maxLum, minMass, maxMass, minTemp, maxTemp, color, occur, prob, minPeriod, maxPeriod, notes) in StarTypes)
            {
                if (spectralClass == SpectralClass && subClass == Subclass)
                {
                    MassRatio = (random.NextDouble() * (maxMass - minMass) + minMass);
                    //StarMass = SolarMass * StarMassRatio;
                    Mass = SolarMass * MassRatio;
                    LuminosityRatio = CalculateLuminosityRatioFromMass(Mass);
                    //StarLuminosity = CalculateLuminosityInWattsFromMass(Mass);
                    Luminosity = CalculateLuminosityInWattsFromMass(Mass);
                    RadiusRatio = CalculateRadiusRatio(MassRatio);
                    //StarRadiusInMeters = CalculateRadiusFromRadiusRatio(StarRadiusRatio);
                    Radius = CalculateRadiusFromRadiusRatio(RadiusRatio);

                    // Function to calculate the rotational speed (in km/s) based on mass of the star
                    RotationalSpeed = CalculateRotationalSpeed(MassRatio);

                    // Function to calculate the rotational period (P) based on mass (in solar masses)
                    RotationalPeriod = CalculateRotationalPeriod(MassRatio);

                    // Generate a random axial tilt for a star
                    // GenerateAxialTilt(mean, standard deviation);
                    AxialTilt = GenerateAxialTilt(0.0, 5.0);  // Mean tilt = 0° (aligned with galactic plane)  // Standard deviation = 5°

                    double StarSurfaceTemp = CalculateSurfaceTemperatureFromMassRatio(MassRatio, Luminosity);    //temp of the sun in kelvin

                    SurfaceTemperature = CalculateSurfaceTemperature(Luminosity, Radius);

                    StarInnerHabitableZone = CalculateHabitableZoneInner();
                    StarOuterHabitableZone = CalculateHabitableZoneOuter();

                    

                    return;
                }
            }
        }
    }
}

