using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarGen
{
    public abstract class StellarObject
    {
        protected const double GravitationalConstant = 6.674e-11; // m^3 kg^-1 s^-2
        protected const double SolarRotationSpeed = 2e-6;  // in m/s (2 km/s)
        protected const double SolarMass = 1.989e30;       // kg        
        protected const double RotationConstant = 2.6e-6;  // Empirical constant for mass-rotation relation
        protected const double SolarRadius = 6.955e8;      // meters
        protected const double SolarLuminosity = 3.828e26; // Watts
        protected const double SolarTemperature = 5778;    // Kelvin 
        // Stefan-Boltzmann constant (in W/m^2/K^4)
        protected const double StefanBoltzmannConstant = 5.67e-8;

        public string Name { get; set; }
        public double Mass { get; set; } // in kg
        public double MassRatio { get; set; } // in kg
        public double Radius { get; set; } // in meters
        public double RadiusRatio { get; set; } // in meters

        public double SurfaceTemperature { get; set; } // in Kelvin
        public double AxialTilt { get; set; } // in degrees
        public double RotationalSpeed { get; set; } // in m/s
        public double RotationalPeriod { get; set; }
        public double Luminosity { get; set; } // in Watts
        public double LuminosityRatio { get; set; } // in Watts
        public double OrbitalSpeed { get; set; } // in m/s (if applicable)
         
        protected StellarObject()
        {
            Name = "Test";
            Mass = 0;
            Radius = 0;
            SurfaceTemperature = 0;
            AxialTilt = 0;
            RotationalSpeed = 0;
            Luminosity = 0;            
        }
        protected StellarObject(string name, double mass, double radius, double surfaceTemp, double axialTilt, double rotationalSpeed, double luminosity, double albedo = 0)
        {
            Name = name;
            Mass = mass;
            Radius = radius;
            SurfaceTemperature = surfaceTemp;
            AxialTilt = axialTilt;
            RotationalSpeed = rotationalSpeed;
            Luminosity = luminosity;            
        }

        // Generic method to calculate gravitational acceleration on the object's surface
        public double CalculateSurfaceGravity()
        {
            if (Radius <= 0)
                throw new ArgumentException("Radius must be greater than zero.");

            return (GravitationalConstant * Mass) / Math.Pow(Radius, 2);
        }

        // General Stefan-Boltzmann law for black body radiation (for stars and planets)
        public double CalculateBlackBodyRadiation()
        {
            const double StefanBoltzmannConstant = 5.670374419e-8; // W·m⁻²·K⁻⁴
            return Math.Pow((Luminosity / (4 * Math.PI * Math.Pow(Radius, 2))) / StefanBoltzmannConstant, 0.25);
        }

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

        #region Radius Calculation Methods

        public double CalculateRadiusFromMass(double mass, double alpha = 0.8)
        {
            // Radius = Solar radius * (Mass / Solar mass) ^ alpha
            double radius = SolarRadius * Math.Pow(mass, alpha);
            return radius;
        }

        // Function to calculate radius based on the Luminosity-Radius relation using the Stefan-Boltzmann Law
        public double CalculateRadiusFromLuminosity(double luminosity, double temperature)
        {
            // Radius = sqrt(Luminosity / (4 * π * σ * T^4))
            double radius = Math.Sqrt(luminosity / (4 * Math.PI * StefanBoltzmannConstant * Math.Pow(temperature, 4)));
            return radius;
        }

        public double CalculateRadiusFromRadiusRatio(double StarRadiusRatio)
        {
            double starRadiusInMeters = StarRadiusRatio * SolarRadius; // 1.5 times the solar radius            

            return starRadiusInMeters;
        }

        public double CalculateRadiusRatio(double massRatio)
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
        public double CalculateRadiusFromLuminosityAndMass(double massInKg, double luminosityRatio, double temperature)
        {

            // Use the Stefan-Boltzmann law to calculate the radius
            // Radius = sqrt(L / (4 * pi * sigma * T^4))
            double radius = Math.Sqrt(luminosityRatio / (4 * Math.PI * StefanBoltzmannConstant * Math.Pow(temperature, 4)));

            return radius;
        }

        #endregion

        #region Rotational Calcuations

        public double CalculateInitialRotationalSpeed(double mass)
        {
            if (mass <= 0)
                return 0;

            double exponent = 0.6; // Empirical exponent for mass-rotation relation
            return SolarRotationSpeed * Math.Pow(mass / SolarMass, exponent);
        }

        public double CalculateRotationalPeriod(double mass)
        {
            return RotationConstant / Math.Sqrt(mass);
        }

        public double CalculateRotationalSpeed(double mass)
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

        public double CalculateRotationalSpeedDecay(double initialRotation, double initialAge, double finalAge)
        {
            if (initialRotation <= 0 || initialAge <= 0 || finalAge <= initialAge)
                return 0;

            return initialRotation * Math.Sqrt(initialAge / finalAge);
        }

        #endregion
    }
}
