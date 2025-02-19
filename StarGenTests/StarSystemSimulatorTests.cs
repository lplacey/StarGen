using Microsoft.VisualStudio.TestTools.UnitTesting;
using StarGen;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace StarGen.Tests
{
    [TestClass()]
    public class StarSystemSimulatorTests
    {        
        [TestMethod]
        public void CalculateMassLuminosityRelation_PositiveTest()
        {
            var simulator = new StarSystemSimulator("TestStar", 1.0 * 1.989e30, 3.828e26, 1.0, 30000);
            double result = simulator.CalculateLuminosityRatioFromMass(1.0 * 1.989e30);
            Assert.IsTrue(result > 0, "Luminosity should be positive.");
        }

        [TestMethod]
        public void CalculateMassLuminosityRelation_NegativeTest()
        {
            var simulator = new StarSystemSimulator("TestStar", 1.0 * 1.989e30, 3.828e26, 1.0, 30000);
            double result = simulator.CalculateLuminosityRatioFromMass(-1.0 * 1.989e30);
            Assert.AreEqual(0, result, "Negative mass should return zero or be handled properly.");
        }

        [TestMethod]
        public void CalculateMassLuminosityRelation_ZeroMass()
        {
            var simulator = new StarSystemSimulator("TestStar", 1.0 * 1.989e30, 3.828e26, 1.0, 30000);
            double result = simulator.CalculateLuminosityRatioFromMass(0);
            Assert.AreEqual(0, result, "Zero mass should return zero luminosity.");
        }

        [TestMethod]
        public void CalculateMassLuminosityRelation_NegativeMass()
        {
            var simulator = new StarSystemSimulator("TestStar", 1.0 * 1.989e30, 3.828e26, 1.0, 30000);
            double result = simulator.CalculateLuminosityRatioFromMass(-5.0 * 1.989e30);
            Assert.AreEqual(0, result, "Negative mass should return zero luminosity.");
        }

        [TestMethod]
        public void CalculateMassLuminosityRelation_LowMass()
        {
            var simulator = new StarSystemSimulator("TestStar", 1.0 * 1.989e30, 3.828e26, 1.0, 30000);
            double massRatio = 0.3;
            double mass = 0.3 * 1.989e30;
            double expected = Math.Pow(massRatio, 2.3);            
            double result = simulator.CalculateLuminosityRatioFromMass(mass);
            Assert.AreEqual(expected, result, 1e24, "Low-mass stars should match expected luminosity.");
        }

        [TestMethod]
        public void CalculateMassLuminosityRelation_MediumMass()
        {
            var simulator = new StarSystemSimulator("TestStar", 1.0 * 1.989e30, 3.828e26, 1.0, 30000);
            double massRatio = 1.5;
            double mass = 1.5 * 1.989e30;
            double expected = Math.Pow(massRatio, 4.0);
            double result = simulator.CalculateLuminosityRatioFromMass(mass);
            Assert.AreEqual(expected, result, 1e24, "Medium-mass stars should match expected luminosity.");
        }

        [TestMethod]
        public void CalculateMassLuminosityRelation_HighMass()
        {
            var simulator = new StarSystemSimulator("TestStar", 1.0 * 1.989e30, 3.828e26, 1.0, 30000);
            double massRatio = 10.0;
            double mass = 10.0 * 1.989e30;            
            double expected = Math.Pow(massRatio, 3.5);            
            double result = simulator.CalculateLuminosityRatioFromMass(mass);
            Assert.AreEqual(expected, result, 1e28, "High-mass stars should match expected luminosity.");
        }

        [TestMethod]
        public void CalculateMassLuminosityRelation_VeryHighMass()
        {
            var simulator = new StarSystemSimulator("TestStar", 1.0 * 1.989e30, 3.828e26, 1.0, 30000);
            double massRatio = 30.0;
            double mass = 30.0 * 1.989e30;
            double expected = Math.Pow(massRatio, 3.0);            
            double result = simulator.CalculateLuminosityRatioFromMass(mass);
            Assert.AreEqual(expected, result, 1e30, "Very high-mass stars should match expected luminosity.");
        }

        [TestMethod]
        public void CalculateStarRadius_PositiveTest()
        {
            var simulator = new StarSystemSimulator("G", "G2");
            double result = StarSystemSimulator.CalculateStarRadius("G", "G2", 3.828e26);
            Assert.IsTrue(result > 0, "Star radius should be positive.");
        }

        [TestMethod]
        public void CalculateStarRadius_NegativeTest()
        {
            var simulator = new StarSystemSimulator("InvalidType", "InvalidSubclass");
            double result = StarSystemSimulator.CalculateStarRadius("InvalidType", "InvalidSubclass", 3.828e26);
            Assert.AreEqual(0, result, "Invalid star type should return zero radius.");
        }
       
        [TestMethod]
        public void CalculateHabitableZoneInner_PositiveTest()
        {
            var simulator = new StarSystemSimulator("G", "G2");
            simulator.StarLuminosity = 3.828e26;
            double result = simulator.CalculateHabitableZoneInner();
            Assert.IsTrue(result > 0, "Inner habitable zone should be positive.");
        }

        [TestMethod]
        public void CalculateHabitableZoneOuter_PositiveTest()
        {
            var simulator = new StarSystemSimulator("G", "G2");
            simulator.StarLuminosity = 3.828e26;
            double result = simulator.CalculateHabitableZoneOuter();
            Assert.IsTrue(result > 0, "Outer habitable zone should be positive.");
        }

        [TestMethod]
        public void CalculateStellarProperties_PositiveTest()
        {
            var (mass, luminosity, radius, temperature) = StarSystemSimulator.CalculateStellarProperties(1.989e30, 3.828e26, 6.955e8, 5778, 4.5e9);
            Assert.IsTrue(mass > 0 && luminosity > 0 && radius > 0 && temperature > 0, "All properties should be positive.");
        }

        [TestMethod]
        public void CalculateRadiusTest()
        {
            var simulator = new StarSystemSimulator("G", "G2");
            var radius = StarSystemSimulator.CalculateRadiusFromRadiusRatio(1.0);            
            Assert.IsTrue(radius > 0, "Outer habitable zone should be positive.");
        }

        // Unit Test for O-Type Stars (Supergiants)
        [TestMethod]
        public void Test_O_Type_Star()
        {
            double mass = 50.0;
            double expectedRadius = Math.Pow(mass, 1.0);            
            Assert.AreEqual(expectedRadius, StarSystemSimulator.CalculateRadiusRatio(mass));
        }

        // Unit Test for O-Type Main Sequence Stars
        [TestMethod]
        public void Test_O_MainSequence_Star()
        {
            double mass = 15.0;
            double expectedRadius = Math.Pow(mass, 0.9);            
            Assert.AreEqual(expectedRadius, StarSystemSimulator.CalculateRadiusRatio(mass));
        }

        // Unit Test for B-Type Stars
        [TestMethod]
        public void Test_B_Type_Star()
        {
            double mass = 5.0;
            double expectedRadius = Math.Pow(mass, 0.7);            
            Assert.AreEqual(expectedRadius, StarSystemSimulator.CalculateRadiusRatio(mass));
        }

        // Unit Test for A-Type Stars
        [TestMethod]
        public void Test_A_Type_Star()
        {
            double mass = 2.5;
            double expectedRadius = Math.Pow(mass, 0.7);            
            Assert.AreEqual(expectedRadius, StarSystemSimulator.CalculateRadiusRatio(mass));
        }

        // Unit Test for F-Type Stars
        [TestMethod]
        public void Test_F_Type_Star()
        {
            double mass = 1.5;
            double expectedRadius = Math.Pow(mass, 0.65);
            Assert.AreEqual(expectedRadius, StarSystemSimulator.CalculateRadiusRatio(mass));
        }

        // Unit Test for G-Type Stars (Sun-like)
        [TestMethod]
        public void Test_G_Type_Star()
        {
            double mass = 1.0;
            double expectedRadius = Math.Pow(mass, 0.6);            
            Assert.AreEqual(expectedRadius, StarSystemSimulator.CalculateRadiusRatio(mass));
        }

        // Unit Test for K-Type Stars
        [TestMethod]
        public void Test_K_Type_Star()
        {
            double mass = 0.6;
            double expectedRadius = Math.Pow(mass, 0.55);            
            Assert.AreEqual(expectedRadius, StarSystemSimulator.CalculateRadiusRatio(mass));
        }

        // Unit Test for M-Type Stars
        [TestMethod]
        public void Test_M_Type_Star()
        {
            double mass = 0.5;
            double expectedRadius = Math.Pow(mass, 0.55);            
            Assert.AreEqual(expectedRadius, StarSystemSimulator.CalculateRadiusRatio(mass));
        }

        // Unit Test for L-Type Brown Dwarfs
        [TestMethod]
        public void Test_L_Type_BrownDwarf()
        {
            double mass = 0.08;
            double expectedRadius = Math.Pow(mass, 0.4);            
            Assert.AreEqual(expectedRadius, StarSystemSimulator.CalculateRadiusRatio(mass));
        }

        // Unit Test for T-Type Brown Dwarfs
        [TestMethod]
        public void Test_T_Type_BrownDwarf()
        {
            double mass = 0.05;
            double expectedRadius = Math.Pow(mass, 0.3);            
            Assert.AreEqual(expectedRadius, StarSystemSimulator.CalculateRadiusRatio(mass));
        }

        // Unit Test for Y-Type Brown Dwarfs (coolest)
        [TestMethod]
        public void Test_Y_Type_BrownDwarf()
        {
            double mass = 0.02;
            double expectedRadius = Math.Pow(mass, 0.2);            
            Assert.AreEqual(expectedRadius, StarSystemSimulator.CalculateRadiusRatio(mass));
        }



    }
}

