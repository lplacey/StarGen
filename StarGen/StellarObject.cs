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

        public string Name { get; set; }
        public double Mass { get; set; } // in kg
        public double Radius { get; set; } // in meters
        public double SurfaceTemperature { get; set; } // in Kelvin
        public double AxialTilt { get; set; } // in degrees
        public double RotationalSpeed { get; set; } // in m/s
        public double Luminosity { get; set; } // in Watts
        public double OrbitalSpeed { get; set; } // in m/s (if applicable)
        public double Albedo { get; set; } // Reflectivity factor (0-1)
        public double Declination { get; set; } // Only relevant for planets with seasons

        private const double SolarRotationSpeed = 2e-6;
        private const double SolarMass = 1.989e30;

        protected StellarObject()
        {
            Name = "Test";
            Mass = 0;
            Radius = 0;
            SurfaceTemperature = 0;
            AxialTilt = 0;
            RotationalSpeed = 0;
            Luminosity = 0;
            Albedo = 0;
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
            Albedo = albedo;
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
            return 4 * Math.PI * Math.Pow(Radius, 2) * StefanBoltzmannConstant * Math.Pow(SurfaceTemperature, 4);
        }

        public double CalculateInitialRotationalSpeed(double mass)
        {
            if (mass <= 0)
                return 0;

            double exponent = 0.6; // Empirical exponent for mass-rotation relation
            return SolarRotationSpeed * Math.Pow(mass / SolarMass, exponent);
        }

        public double CalculateRotationalSpeedDecay(double initialRotation, double initialAge, double finalAge)
        {
            if (initialRotation <= 0 || initialAge <= 0 || finalAge <= initialAge)
                return 0;

            return initialRotation * Math.Sqrt(initialAge / finalAge);
        }
    }
}
