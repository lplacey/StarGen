using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
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
        public string StarName { get; set; }
        public double StarMass { get; set; } // in kg

        public double StarRadius { get; set; }
        public double StarLuminosity { get; set; } // in Watts
        //public double OrbitalSpeed { get; set; } // in m/s
        public double LuminosityRatio { get; set; }
        public double InnerHabitableZone { get; set; }
        public double OuterHabitableZone { get; set; }

        public static List<(string spectralClass, string subclass, string yerkesClass, double minRadius, double maxRadius, double minLum, double maxLum, double minMass, double maxMass, double minTemp, double maxTemp, string color, double occur, double prob, string notes)> StarTypes = new List<(string, string, string, double, double, double, double, double, double, double, double, string, double, double, string)>();

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
                             values.Length > 14 ? values[14].Trim() : "")
                        );
                    }
                }
            }
            catch (Exception ex)
            {
                
            }
        }

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

        public double CalculateMassLuminosityRelation2(double mass)
        {
            if (mass <= 0)
                return 0;

            double exponent = CalculateLuminosityExponent(mass);
            double luminosity = SolarLuminosity * Math.Pow(mass / SolarMass, exponent);

            return Math.Max(0, luminosity);

        }

        public double CalculateLuminosityExponent(double mass)
        {
            if (mass <= 0)
                return 0;

            return mass < 0.43 ? 2.3 : (mass < 2.0 ? 4.0 : (mass < 20.0 ? 3.5 : 3.0));
        }

        public double CalculateLuminosityFromMassRatio(double massRatio)
        {
            if (massRatio <= 0)
                return 0;

            double exponent = CalculateLuminosityExponent(massRatio);
            return SolarLuminosity * Math.Pow(massRatio, exponent);
        }

        public double CalculateLuminosityRatioFromMass(double massInKg)
        {
            if (massInKg <= 0)
                return 0;

            double exponent = massInKg < (0.43 * SolarMass) ? 2.3 : (massInKg < (2.0 * SolarMass) ? 4.0 : (massInKg < (20.0 * SolarMass) ? 3.5 : 3.0));

            // Solar Luminosity in Watts
            double luminosity = SolarLuminosity * Math.Pow(massInKg, exponent);
            
            return Math.Max(0, luminosity);
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

        public double CalculateRadius(double mass)
        {
            /*
            * Dynamic Mass-Radius Algorithm:
            * - Uses smooth transition exponents based on empirical stellar models.
            * - Interpolates between main-sequence stars, brown dwarfs, and giant stars.
            */

            double exponent;

            if (mass >= 30)         // O-type supergiants
                exponent = 1.0;
            else if (mass >= 10)    // O-type main sequence
                exponent = 0.9;
            else if (mass >= 2)     // B/A-type stars
                exponent = 0.7;
            else if (mass >= 1.5)   // F-type stars
                exponent = 0.65;
            else if (mass >= 1)     // G-type (Sun-like)
                exponent = 0.6;
            else if (mass >= 0.5)   // K/M-type (cool main sequence)
                exponent = 0.55;
            else if (mass >= 0.08)  // L/T-type (brown dwarfs)
                exponent = 0.4;
            else if (mass >= 0.05)  // T/Y brown dwarfs
                exponent = 0.3;
            else                    // Substellar objects
                exponent = 0.2;

            double rad = Math.Pow(mass, exponent);
            // Calculate radius using mass-radius relationship
            return rad;
        }

        public double CalculateStarRadius(string SpectralClass, string subclass, double luminosity)
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
            double radius = minRadius + ((luminosity - minLum) / (maxLum - minLum)) * (maxRadius - minRadius);

            // Clamp radius within the range
            return Math.Max(minRadius, Math.Min(radius, maxRadius));
        }

        public void GenerateRandomStarProperties(string SpectralClass, string Subclass)
        {
            foreach (var (spectralClass, subClass, yerk, minRad, maxRad, minLum, maxLum, minMass, maxMass, minTemp, maxTemp, color, occur, prob, notes) in StarTypes)
            {
                if (spectralClass == SpectralClass && subClass == Subclass)
                {
                    StarMass = SolarMass * (random.NextDouble() * (maxMass - minMass) + minMass);

                    //StarLuminosity = SolarLuminosity * (random.NextDouble() * (maxLum - minLum) + minLum);
                    StarLuminosity = CalculateLuminosityRatioFromMass(StarMass);

                    StarRadius = CalculateStarRadius(SpectralClass, Subclass, StarLuminosity);

                    LuminosityRatio = StarLuminosity / SolarLuminosity;

                    InnerHabitableZone = CalculateHabitableZoneInner();
                    OuterHabitableZone = CalculateHabitableZoneOuter();
                    return;
                }
            }            
        }

        public string GetStarType()
        {
            foreach (var (spectralClass, subClass, _, minLum, maxLum, _, _, _, _, _, _, _, _, _, _) in StarTypes)
            {
                LuminosityRatio = StarLuminosity / SolarLuminosity;
                //if (LuminosityRatio >= minLum && LuminosityRatio <= maxLum)
                //{
                //    return $"{spectralClass} ({subClass})";
                //}
                if (LuminosityRatio >= minLum)
                {
                    string temp = "Reached";
                    if (LuminosityRatio <= maxLum)
                    {
                        return $"{spectralClass} ({subClass})";
                    }
                }
            }
            return "Unknown";
        }

        public double CalculateHabitableZoneInner()
        {
            return Math.Sqrt(LuminosityRatio) * 0.95; // AU
        }

        public double CalculateHabitableZoneOuter()
        {
            return Math.Sqrt(LuminosityRatio) * 1.37; // AU
        }

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
    }
}

