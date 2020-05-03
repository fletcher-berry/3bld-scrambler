namespace BldScramblerUi
{
    partial class Form1
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
            this.label1 = new System.Windows.Forms.Label();
            this.ScrambleLabel = new System.Windows.Forms.Label();
            this.ScrambleButton = new System.Windows.Forms.Button();
            this.TypeSelector = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(37, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Scramble:";
            // 
            // ScrambleLabel
            // 
            this.ScrambleLabel.AutoSize = true;
            this.ScrambleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ScrambleLabel.Location = new System.Drawing.Point(128, 35);
            this.ScrambleLabel.Name = "ScrambleLabel";
            this.ScrambleLabel.Size = new System.Drawing.Size(336, 20);
            this.ScrambleLabel.TabIndex = 1;
            this.ScrambleLabel.Text = "R U R\' U\' R\' F R2 U\' R\' U\' R U R\' U\' R U R\'";
            // 
            // ScrambleButton
            // 
            this.ScrambleButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ScrambleButton.Location = new System.Drawing.Point(620, 74);
            this.ScrambleButton.Name = "ScrambleButton";
            this.ScrambleButton.Size = new System.Drawing.Size(122, 31);
            this.ScrambleButton.TabIndex = 2;
            this.ScrambleButton.Text = "Scramble!";
            this.ScrambleButton.UseVisualStyleBackColor = true;
            this.ScrambleButton.Click += new System.EventHandler(this.ScrambleButton_Click);
            // 
            // TypeSelector
            // 
            this.TypeSelector.FormattingEnabled = true;
            this.TypeSelector.Location = new System.Drawing.Point(37, 74);
            this.TypeSelector.Name = "TypeSelector";
            this.TypeSelector.Size = new System.Drawing.Size(167, 24);
            this.TypeSelector.TabIndex = 3;
            this.TypeSelector.SelectedValueChanged += new System.EventHandler(this.TypeSelector_SelectedValueChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(766, 203);
            this.Controls.Add(this.TypeSelector);
            this.Controls.Add(this.ScrambleButton);
            this.Controls.Add(this.ScrambleLabel);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Scrambler";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label ScrambleLabel;
        private System.Windows.Forms.Button ScrambleButton;
        private System.Windows.Forms.ComboBox TypeSelector;
    }
}

