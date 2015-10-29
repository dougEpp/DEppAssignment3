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
            this.btnCheck = new System.Windows.Forms.Button();
            this.btnSolve = new System.Windows.Forms.Button();
            this.btnScramble = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.dlgSave = new System.Windows.Forms.SaveFileDialog();
            this.btnLoad = new System.Windows.Forms.Button();
            this.dlgOpen = new System.Windows.Forms.OpenFileDialog();
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
            // btnSolve
            // 
            this.btnSolve.Location = new System.Drawing.Point(467, 154);
            this.btnSolve.Name = "btnSolve";
            this.btnSolve.Size = new System.Drawing.Size(75, 23);
            this.btnSolve.TabIndex = 1;
            this.btnSolve.Text = "Solve";
            this.btnSolve.UseVisualStyleBackColor = true;
            this.btnSolve.Click += new System.EventHandler(this.Solve_Click);
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
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(467, 288);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // dlgSave
            // 
            this.dlgSave.FileName = "Game";
            this.dlgSave.Filter = "Game File|*.gam|Text Files|*.txt";
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(467, 317);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(75, 23);
            this.btnLoad.TabIndex = 4;
            this.btnLoad.Text = "Load";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // dlgOpen
            // 
            this.dlgOpen.FileName = "Game";
            this.dlgOpen.Filter = "Game Files|*.gam|Text Files|*.txt";
            // 
            // FifteenPuzzle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(621, 556);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnScramble);
            this.Controls.Add(this.btnSolve);
            this.Controls.Add(this.btnCheck);
            this.KeyPreview = true;
            this.Name = "FifteenPuzzle";
            this.Text = "Form1";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FifteenPuzzle_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCheck;
        private System.Windows.Forms.Button btnSolve;
        private System.Windows.Forms.Button btnScramble;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.SaveFileDialog dlgSave;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.OpenFileDialog dlgOpen;
    }
}

