namespace StarGen
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lblOutputMass = new Label();
            lblLuminosity = new Label();
            lblInnerHabitableZone = new Label();
            button1 = new Button();
            label7 = new Label();
            cboStarSubClass = new ComboBox();
            label9 = new Label();
            label10 = new Label();
            label11 = new Label();
            label12 = new Label();
            lblOuterHabitableZone = new Label();
            label13 = new Label();
            lblLuminosityRatio = new Label();
            label14 = new Label();
            lblRadius = new Label();
            label1 = new Label();
            lblCalculateLuminosityFromMassRatio = new Label();
            lblCalculateLuminosityRatioFromMass = new Label();
            label4 = new Label();
            lblCalculateLuminosityInWattsFromMass = new Label();
            label6 = new Label();
            lblCalculateLuminosityExponent = new Label();
            label15 = new Label();
            lblCalculateMassLuminosityRelation2 = new Label();
            label3 = new Label();
            label5 = new Label();
            label8 = new Label();
            SuspendLayout();
            // 
            // lblOutputMass
            // 
            lblOutputMass.AutoSize = true;
            lblOutputMass.Location = new Point(220, 157);
            lblOutputMass.Name = "lblOutputMass";
            lblOutputMass.Size = new Size(121, 25);
            lblOutputMass.TabIndex = 2;
            lblOutputMass.Text = "MassOfPlanet";
            // 
            // lblLuminosity
            // 
            lblLuminosity.AutoSize = true;
            lblLuminosity.Location = new Point(220, 193);
            lblLuminosity.Name = "lblLuminosity";
            lblLuminosity.Size = new Size(98, 25);
            lblLuminosity.TabIndex = 3;
            lblLuminosity.Text = "Luminosity";
            // 
            // lblInnerHabitableZone
            // 
            lblInnerHabitableZone.AutoSize = true;
            lblInnerHabitableZone.Location = new Point(220, 265);
            lblInnerHabitableZone.Name = "lblInnerHabitableZone";
            lblInnerHabitableZone.Size = new Size(168, 25);
            lblInnerHabitableZone.TabIndex = 4;
            lblInnerHabitableZone.Text = "InnerHabitableZone";
            // 
            // button1
            // 
            button1.Location = new Point(313, 805);
            button1.Name = "button1";
            button1.Size = new Size(126, 43);
            button1.TabIndex = 15;
            button1.Text = "GO";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(44, 76);
            label7.Name = "label7";
            label7.Size = new Size(146, 25);
            label7.TabIndex = 19;
            label7.Text = "StarAndSubClass";
            // 
            // cboStarSubClass
            // 
            cboStarSubClass.FormattingEnabled = true;
            cboStarSubClass.Location = new Point(228, 73);
            cboStarSubClass.Name = "cboStarSubClass";
            cboStarSubClass.Size = new Size(298, 33);
            cboStarSubClass.TabIndex = 18;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(161, 157);
            label9.Name = "label9";
            label9.Size = new Size(53, 25);
            label9.TabIndex = 22;
            label9.Text = "Mass";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(116, 193);
            label10.Name = "label10";
            label10.Size = new Size(98, 25);
            label10.TabIndex = 23;
            label10.Text = "Luminosity";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(41, 265);
            label11.Name = "label11";
            label11.Size = new Size(168, 25);
            label11.TabIndex = 24;
            label11.Text = "InnerHabitableZone";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(41, 302);
            label12.Name = "label12";
            label12.Size = new Size(173, 25);
            label12.TabIndex = 26;
            label12.Text = "OuterHabitableZone";
            // 
            // lblOuterHabitableZone
            // 
            lblOuterHabitableZone.AutoSize = true;
            lblOuterHabitableZone.Location = new Point(220, 302);
            lblOuterHabitableZone.Name = "lblOuterHabitableZone";
            lblOuterHabitableZone.Size = new Size(173, 25);
            lblOuterHabitableZone.TabIndex = 25;
            lblOuterHabitableZone.Text = "OuterHabitableZone";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(75, 231);
            label13.Name = "label13";
            label13.Size = new Size(139, 25);
            label13.TabIndex = 28;
            label13.Text = "LuminosityRatio";
            // 
            // lblLuminosityRatio
            // 
            lblLuminosityRatio.AutoSize = true;
            lblLuminosityRatio.Location = new Point(220, 231);
            lblLuminosityRatio.Name = "lblLuminosityRatio";
            lblLuminosityRatio.Size = new Size(139, 25);
            lblLuminosityRatio.TabIndex = 27;
            lblLuminosityRatio.Text = "LuminosityRatio";
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new Point(149, 122);
            label14.Name = "label14";
            label14.Size = new Size(65, 25);
            label14.TabIndex = 30;
            label14.Text = "Radius";
            // 
            // lblRadius
            // 
            lblRadius.AutoSize = true;
            lblRadius.Location = new Point(220, 122);
            lblRadius.Name = "lblRadius";
            lblRadius.Size = new Size(65, 25);
            lblRadius.TabIndex = 29;
            lblRadius.Text = "Radius";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 398);
            label1.Name = "label1";
            label1.Size = new Size(292, 25);
            label1.TabIndex = 31;
            label1.Text = "CalculateLuminosityFromMassRatio";
            // 
            // lblCalculateLuminosityFromMassRatio
            // 
            lblCalculateLuminosityFromMassRatio.AutoSize = true;
            lblCalculateLuminosityFromMassRatio.Location = new Point(371, 398);
            lblCalculateLuminosityFromMassRatio.Name = "lblCalculateLuminosityFromMassRatio";
            lblCalculateLuminosityFromMassRatio.Size = new Size(311, 25);
            lblCalculateLuminosityFromMassRatio.TabIndex = 32;
            lblCalculateLuminosityFromMassRatio.Text = "lblCalculateLuminosityFromMassRatio";
            // 
            // lblCalculateLuminosityRatioFromMass
            // 
            lblCalculateLuminosityRatioFromMass.AutoSize = true;
            lblCalculateLuminosityRatioFromMass.Location = new Point(371, 433);
            lblCalculateLuminosityRatioFromMass.Name = "lblCalculateLuminosityRatioFromMass";
            lblCalculateLuminosityRatioFromMass.Size = new Size(311, 25);
            lblCalculateLuminosityRatioFromMass.TabIndex = 34;
            lblCalculateLuminosityRatioFromMass.Text = "lblCalculateLuminosityRatioFromMass";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(12, 433);
            label4.Name = "label4";
            label4.Size = new Size(292, 25);
            label4.TabIndex = 33;
            label4.Text = "CalculateLuminosityRatioFromMass";
            // 
            // lblCalculateLuminosityInWattsFromMass
            // 
            lblCalculateLuminosityInWattsFromMass.AutoSize = true;
            lblCalculateLuminosityInWattsFromMass.Location = new Point(371, 467);
            lblCalculateLuminosityInWattsFromMass.Name = "lblCalculateLuminosityInWattsFromMass";
            lblCalculateLuminosityInWattsFromMass.Size = new Size(330, 25);
            lblCalculateLuminosityInWattsFromMass.TabIndex = 36;
            lblCalculateLuminosityInWattsFromMass.Text = "lblCalculateLuminosityInWattsFromMass";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(12, 467);
            label6.Name = "label6";
            label6.Size = new Size(311, 25);
            label6.TabIndex = 35;
            label6.Text = "CalculateLuminosityInWattsFromMass";
            // 
            // lblCalculateLuminosityExponent
            // 
            lblCalculateLuminosityExponent.AutoSize = true;
            lblCalculateLuminosityExponent.Location = new Point(371, 538);
            lblCalculateLuminosityExponent.Name = "lblCalculateLuminosityExponent";
            lblCalculateLuminosityExponent.Size = new Size(261, 25);
            lblCalculateLuminosityExponent.TabIndex = 38;
            lblCalculateLuminosityExponent.Text = "lblCalculateLuminosityExponent";
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Location = new Point(12, 538);
            label15.Name = "label15";
            label15.Size = new Size(333, 25);
            label15.TabIndex = 37;
            label15.Text = "CalculateLuminosityRatioFromMassRatio";
            // 
            // lblCalculateMassLuminosityRelation2
            // 
            lblCalculateMassLuminosityRelation2.AutoSize = true;
            lblCalculateMassLuminosityRelation2.Location = new Point(371, 575);
            lblCalculateMassLuminosityRelation2.Name = "lblCalculateMassLuminosityRelation2";
            lblCalculateMassLuminosityRelation2.Size = new Size(301, 25);
            lblCalculateMassLuminosityRelation2.TabIndex = 42;
            lblCalculateMassLuminosityRelation2.Text = "lblCalculateMassLuminosityRelation2";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 575);
            label3.Name = "label3";
            label3.Size = new Size(282, 25);
            label3.TabIndex = 41;
            label3.Text = "CalculateMassLuminosityRelation2";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(371, 502);
            label5.Name = "label5";
            label5.Size = new Size(352, 25);
            label5.TabIndex = 40;
            label5.Text = "lblCalculateLuminosityRatioFromMassRatio";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(12, 502);
            label8.Name = "label8";
            label8.Size = new Size(333, 25);
            label8.TabIndex = 39;
            label8.Text = "CalculateLuminosityRatioFromMassRatio";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(802, 1019);
            Controls.Add(lblCalculateMassLuminosityRelation2);
            Controls.Add(label3);
            Controls.Add(label5);
            Controls.Add(label8);
            Controls.Add(lblCalculateLuminosityExponent);
            Controls.Add(label15);
            Controls.Add(lblCalculateLuminosityInWattsFromMass);
            Controls.Add(label6);
            Controls.Add(lblCalculateLuminosityRatioFromMass);
            Controls.Add(label4);
            Controls.Add(lblCalculateLuminosityFromMassRatio);
            Controls.Add(label1);
            Controls.Add(label14);
            Controls.Add(lblRadius);
            Controls.Add(label13);
            Controls.Add(lblLuminosityRatio);
            Controls.Add(label12);
            Controls.Add(lblOuterHabitableZone);
            Controls.Add(label11);
            Controls.Add(label10);
            Controls.Add(label9);
            Controls.Add(label7);
            Controls.Add(cboStarSubClass);
            Controls.Add(button1);
            Controls.Add(lblInnerHabitableZone);
            Controls.Add(lblLuminosity);
            Controls.Add(lblOutputMass);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label lblOutputMass;
        private Label lblLuminosity;
        private Label lblInnerHabitableZone;
        private Button button1;
        private Label label7;
        private ComboBox cboStarSubClass;
        private Label label9;
        private Label label10;
        private Label label11;
        private Label label12;
        private Label lblOuterHabitableZone;
        private Label label13;
        private Label lblLuminosityRatio;
        private Label label14;
        private Label lblRadius;
        private Label label1;
        private Label lblCalculateLuminosityFromMassRatio;
        private Label lblCalculateLuminosityRatioFromMass;
        private Label label4;
        private Label lblCalculateLuminosityInWattsFromMass;
        private Label label6;
        private Label lblCalculateLuminosityExponent;
        private Label label15;
        private Label lblCalculateMassLuminosityRelation2;
        private Label label3;
        private Label label5;
        private Label label8;
    }
}
