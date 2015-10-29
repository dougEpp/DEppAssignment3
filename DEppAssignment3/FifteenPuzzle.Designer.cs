namespace DEppAssignment3
{
    partial class FifteenPuzzle
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btnCheck = new System.Windows.Forms.Button();
            this.Solve = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.btnScramble = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnCheck
            // 
            this.btnCheck.Location = new System.Drawing.Point(467, 125);
            this.btnCheck.Name = "btnCheck";
            this.btnCheck.Size = new System.Drawing.Size(75, 23);
            this.btnCheck.TabIndex = 0;
            this.btnCheck.Text = "Check";
            this.btnCheck.UseVisualStyleBackColor = true;
            this.btnCheck.Click += new System.EventHandler(this.btnCheck_Click);
            // 
            // Solve
            // 
            this.Solve.Location = new System.Drawing.Point(467, 154);
            this.Solve.Name = "Solve";
            this.Solve.Size = new System.Drawing.Size(75, 23);
            this.Solve.TabIndex = 1;
            this.Solve.Text = "btnSolve";
            this.Solve.UseVisualStyleBackColor = true;
            this.Solve.Click += new System.EventHandler(this.Solve_Click);
            // 
            // btnScramble
            // 
            this.btnScramble.Location = new System.Drawing.Point(467, 96);
            this.btnScramble.Name = "btnScramble";
            this.btnScramble.Size = new System.Drawing.Size(75, 23);
            this.btnScramble.TabIndex = 2;
            this.btnScramble.Text = "Scramble";
            this.btnScramble.UseVisualStyleBackColor = true;
            this.btnScramble.Click += new System.EventHandler(this.btnScramble_Click);
            // 
            // FifteenPuzzle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(621, 556);
            this.Controls.Add(this.btnScramble);
            this.Controls.Add(this.Solve);
            this.Controls.Add(this.btnCheck);
            this.KeyPreview = true;
            this.Name = "FifteenPuzzle";
            this.Text = "Form1";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FifteenPuzzle_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCheck;
        private System.Windows.Forms.Button Solve;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button btnScramble;
    }
}

