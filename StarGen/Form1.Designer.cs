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
            button1.Location = new Point(183, 379);
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
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(678, 566);
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
    }
}
