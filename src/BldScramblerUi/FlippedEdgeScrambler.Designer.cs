namespace BldScramblerUi
{
    partial class FlippedEdgeScrambler
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
            this.FlippedEdgesInput = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.CardinalityBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // FlippedEdgesInput
            // 
            this.FlippedEdgesInput.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FlippedEdgesInput.Location = new System.Drawing.Point(296, 32);
            this.FlippedEdgesInput.Name = "FlippedEdgesInput";
            this.FlippedEdgesInput.Size = new System.Drawing.Size(68, 24);
            this.FlippedEdgesInput.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(32, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(225, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Flipped edges outside buffer:";
            // 
            // CardinalityBox
            // 
            this.CardinalityBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CardinalityBox.FormattingEnabled = true;
            this.CardinalityBox.Location = new System.Drawing.Point(260, 76);
            this.CardinalityBox.Name = "CardinalityBox";
            this.CardinalityBox.Size = new System.Drawing.Size(103, 24);
            this.CardinalityBox.TabIndex = 4;
            // 
            // FlippedEdgeScrambler
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.CardinalityBox);
            this.Controls.Add(this.FlippedEdgesInput);
            this.Controls.Add(this.label1);
            this.Name = "FlippedEdgeScrambler";
            this.Size = new System.Drawing.Size(407, 122);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox FlippedEdgesInput;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox CardinalityBox;
    }
}
