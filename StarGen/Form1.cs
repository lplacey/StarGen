using System.Security.Cryptography.X509Certificates;
using StarGen;

namespace StarGen
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            StarSystemSimulator.LoadStarTypesFromCSV("Harvard-Yerkes_Star_Classification_with_Full_Probabilities.csv");

            // Preserve original file order
            var orderedStarTypes = StarSystemSimulator.StarTypes.ToList();

            // Reorder subclasses but keep types in original order
            var reorderedStarTypes = orderedStarTypes
                .GroupBy(s => new { s.spectralClass, s.color }) // Keep original type order
                .SelectMany(g => g.OrderByDescending(s => s.subclass)) // Sort subclasses in descending order
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
            //myStar.GenerateRandomStarProperties(starType, starSubclass);
            lblOutputMass.Text = myStar.StarMass.ToString();
            lblLuminosity.Text = myStar.StarLuminosity.ToString();
            lblInnerHabitableZone.Text = myStar.InnerHabitableZone.ToString();
            lblOuterHabitableZone.Text = myStar.OuterHabitableZone.ToString();
            lblLuminosityRatio.Text = myStar.LuminosityRatio.ToString();
            lblRadius.Text = myStar.StarRadius.ToString();
            //myStar = StarSystemSimulator("TestStart", , double starLuminosity, double planetSize, double orbitalSpeed)
        }
    }
}
