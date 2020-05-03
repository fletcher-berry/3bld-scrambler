namespace BldScramblerUi
{
    partial class TwistedCornerScrambler
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.TwistedCornersInput = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.FloatingLabel = new System.Windows.Forms.Label();
            this.FloatingInput = new System.Windows.Forms.ComboBox();
            this.CardinalityBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // TwistedCornersInput
            // 
            this.TwistedCornersInput.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TwistedCornersInput.Location = new System.Drawing.Point(312, 28);
            this.TwistedCornersInput.Name = "TwistedCornersInput";
            this.TwistedCornersInput.Size = new System.Drawing.Size(68, 24);
            this.TwistedCornersInput.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(38, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(241, 20);
            this.label1.TabIndex = 4;
            this.label1.Text = "Twisted corners outside buffer:";
            // 
            // FloatingLabel
            // 
            this.FloatingLabel.AutoSize = true;
            this.FloatingLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FloatingLabel.Location = new System.Drawing.Point(224, 106);
            this.FloatingLabel.Name = "FloatingLabel";
            this.FloatingLabel.Size = new System.Drawing.Size(73, 20);
            this.FloatingLabel.TabIndex = 6;
            this.FloatingLabel.Text = "Floating:";
            // 
            // FloatingInput
            // 
            this.FloatingInput.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.FloatingInput.FormattingEnabled = true;
            this.FloatingInput.Location = new System.Drawing.Point(311, 106);
            this.FloatingInput.Name = "FloatingInput";
            this.FloatingInput.Size = new System.Drawing.Size(108, 24);
            this.FloatingInput.TabIndex = 7;
            // 
            // CardinalityBox
            // 
            this.CardinalityBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CardinalityBox.FormattingEnabled = true;
            this.CardinalityBox.Location = new System.Drawing.Point(311, 68);
            this.CardinalityBox.Name = "CardinalityBox";
            this.CardinalityBox.Size = new System.Drawing.Size(103, 24);
            this.CardinalityBox.TabIndex = 8;
            this.CardinalityBox.SelectedValueChanged += new System.EventHandler(this.CardinalityBox_SelectedValueChanged);
            // 
            // TwistedCornerScrambler
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.CardinalityBox);
            this.Controls.Add(this.FloatingInput);
            this.Controls.Add(this.FloatingLabel);
            this.Controls.Add(this.TwistedCornersInput);
            this.Controls.Add(this.label1);
            this.Name = "TwistedCornerScrambler";
            this.Size = new System.Drawing.Size(444, 151);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TwistedCornersInput;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label FloatingLabel;
        private System.Windows.Forms.ComboBox FloatingInput;
        private System.Windows.Forms.ComboBox CardinalityBox;
    }
}
