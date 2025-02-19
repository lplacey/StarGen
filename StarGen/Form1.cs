using System.Security.Cryptography.X509Certificates;
using StarGen;

namespace StarGen
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            StarSystemSimulator.LoadStarTypesFromCSV("Fully_Validated_Stellar_Classification_Data.csv");

            // Preserve original file order
            var orderedStarTypes = StarSystemSimulator.StarTypes.ToList();
            
            var reorderedStarTypes = orderedStarTypes
                .GroupBy(s => new { s.spectralClass, s.color }) // Keep original type order
                .SelectMany(g => g.OrderBy(s => s.subclass)) // Sort subclasses in ascending order
                .ToList();

            cboStarSubClass.DataSource = reorderedStarTypes
                .Select(s => $"{s.spectralClass} - {s.color} - {s.subclass}")
                .Distinct().ToList();

            var starTypesList = reorderedStarTypes
                .Select(s => new { FullDisplay = $"{s.spectralClass} - {s.color} ({s.subclass}) - {s.yerkesClass}" })
                .ToList();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Extract Type and Subclass from the selected item in cboStarSubClass
            string selectedText = cboStarSubClass.SelectedItem.ToString();
            string[] parts = selectedText.Split(" - ");

            if (parts.Length < 3)
            {
                MessageBox.Show("Invalid selection format. Please select a valid star subclass.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string starType = parts[0].Trim();
            string starSubclass = parts[2].Trim();  // Subclass is the 3rd element

            StarSystemSimulator myStar = new StarSystemSimulator(starType, starSubclass);
            
            lblOutputMass.Text = myStar.StarMass.ToString();
            lblLuminosity.Text = myStar.StarLuminosity.ToString();
            lblInnerHabitableZone.Text = myStar.StarInnerHabitableZone.ToString();
            lblOuterHabitableZone.Text = myStar.StarOuterHabitableZone.ToString();
            lblLuminosityRatio.Text = myStar.StarLuminosityRatio.ToString();
            lblRadiusRatio.Text = myStar.StarRadiusRatio.ToString();
            lblMassRatio.Text = myStar.StarMassRatio.ToString();
            lblSurfaceTemp.Text = myStar.SurfaceTemperature.ToString();
            lblRadiusInMeters.Text = myStar.StarRadiusInMeters.ToString();

            string temp1 = myStar.RotationalSpeed.ToString();
            string temp2 = myStar.Declination.ToString();
            string temp3 = myStar.Albedo.ToString();
            string temp4 = myStar.OrbitalSpeed.ToString();
            string temp5 = myStar.AxialTilt.ToString();

           





        }
    }
}
