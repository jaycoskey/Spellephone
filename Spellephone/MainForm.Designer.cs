using System.Windows.Forms;

namespace Spellephone
{
    partial class MainForm
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
            this.wordsTextBox = new System.Windows.Forms.TextBox();
            this.phoneNumberLabel = new System.Windows.Forms.Label();
            this.phoneNumberTextBox = new System.Windows.Forms.TextBox();
            this.wordsLabel = new System.Windows.Forms.Label();
            this.phoneNumberButton = new System.Windows.Forms.Button();
            this.spacerTextBox = new System.Windows.Forms.TextBox();
            this.maxDigitsLabel = new System.Windows.Forms.Label();
            this.maxDigitsUpDown = new System.Windows.Forms.NumericUpDown();
            this.cacheUsageLabel = new System.Windows.Forms.Label();
            this.cacheUsageCheckBox = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.maxDigitsUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // wordsTextBox
            // 
            this.wordsTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.wordsTextBox.Location = new System.Drawing.Point(0, 130);
            this.wordsTextBox.Multiline = true;
            this.wordsTextBox.Name = "wordsTextBox";
            this.wordsTextBox.ReadOnly = true;
            this.wordsTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.wordsTextBox.Size = new System.Drawing.Size(384, 329);
            this.wordsTextBox.TabIndex = 0;
            // 
            // phoneNumberLabel
            // 
            this.phoneNumberLabel.AutoSize = true;
            this.phoneNumberLabel.Location = new System.Drawing.Point(12, 12);
            this.phoneNumberLabel.Name = "phoneNumberLabel";
            this.phoneNumberLabel.Size = new System.Drawing.Size(79, 13);
            this.phoneNumberLabel.TabIndex = 1;
            this.phoneNumberLabel.Text = "Phone number:";
            // 
            // phoneNumberTextBox
            // 
            this.phoneNumberTextBox.AcceptsReturn = true;
            this.phoneNumberTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.phoneNumberTextBox.Location = new System.Drawing.Point(142, 9);
            this.phoneNumberTextBox.Name = "phoneNumberTextBox";
            this.phoneNumberTextBox.Size = new System.Drawing.Size(152, 20);
            this.phoneNumberTextBox.TabIndex = 2;
            this.phoneNumberTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.phoneNumberTextBox_KeyPress);
            // 
            // wordsLabel
            // 
            this.wordsLabel.AutoSize = true;
            this.wordsLabel.Location = new System.Drawing.Point(12, 114);
            this.wordsLabel.Name = "wordsLabel";
            this.wordsLabel.Size = new System.Drawing.Size(77, 13);
            this.wordsLabel.TabIndex = 4;
            this.wordsLabel.Text = "Words spelled:";
            // 
            // phoneNumberButton
            // 
            this.phoneNumberButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.phoneNumberButton.Location = new System.Drawing.Point(300, 7);
            this.phoneNumberButton.Name = "phoneNumberButton";
            this.phoneNumberButton.Size = new System.Drawing.Size(75, 23);
            this.phoneNumberButton.TabIndex = 5;
            this.phoneNumberButton.Text = "Find words";
            this.phoneNumberButton.UseVisualStyleBackColor = true;
            this.phoneNumberButton.Click += new System.EventHandler(this.phoneNumberButton_Click);
            // 
            // spacerTextBox
            // 
            this.spacerTextBox.Location = new System.Drawing.Point(12, 91);
            this.spacerTextBox.Name = "spacerTextBox";
            this.spacerTextBox.Size = new System.Drawing.Size(363, 20);
            this.spacerTextBox.TabIndex = 6;
            this.spacerTextBox.Visible = false;
            // 
            // maxDigitsLabel
            // 
            this.maxDigitsLabel.AutoSize = true;
            this.maxDigitsLabel.Location = new System.Drawing.Point(12, 38);
            this.maxDigitsLabel.Name = "maxDigitsLabel";
            this.maxDigitsLabel.Size = new System.Drawing.Size(121, 13);
            this.maxDigitsLabel.TabIndex = 7;
            this.maxDigitsLabel.Text = "Max digit count in result:";
            // 
            // maxDigitsUpDown
            // 
            this.maxDigitsUpDown.Location = new System.Drawing.Point(142, 36);
            this.maxDigitsUpDown.Name = "maxDigitsUpDown";
            this.maxDigitsUpDown.Size = new System.Drawing.Size(152, 20);
            this.maxDigitsUpDown.TabIndex = 8;
            this.maxDigitsUpDown.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // cacheUsageLabel
            // 
            this.cacheUsageLabel.AutoSize = true;
            this.cacheUsageLabel.Location = new System.Drawing.Point(12, 65);
            this.cacheUsageLabel.Name = "cacheUsageLabel";
            this.cacheUsageLabel.Size = new System.Drawing.Size(121, 13);
            this.cacheUsageLabel.TabIndex = 9;
            this.cacheUsageLabel.Text = "Algorighm cache usage:";
            // 
            // cacheUsageCheckBox
            // 
            this.cacheUsageCheckBox.AutoSize = true;
            this.cacheUsageCheckBox.Location = new System.Drawing.Point(142, 65);
            this.cacheUsageCheckBox.Name = "cacheUsageCheckBox";
            this.cacheUsageCheckBox.Size = new System.Drawing.Size(78, 17);
            this.cacheUsageCheckBox.TabIndex = 10;
            this.cacheUsageCheckBox.Text = "Use cache";
            this.cacheUsageCheckBox.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 398);
            this.Controls.Add(this.cacheUsageCheckBox);
            this.Controls.Add(this.cacheUsageLabel);
            this.Controls.Add(this.maxDigitsUpDown);
            this.Controls.Add(this.maxDigitsLabel);
            this.Controls.Add(this.spacerTextBox);
            this.Controls.Add(this.phoneNumberButton);
            this.Controls.Add(this.wordsLabel);
            this.Controls.Add(this.phoneNumberTextBox);
            this.Controls.Add(this.phoneNumberLabel);
            this.Controls.Add(this.wordsTextBox);
            this.Name = "MainForm";
            this.Text = "Phone number speller";
            this.Load += new System.EventHandler(this.mainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.maxDigitsUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private System.Windows.Forms.TextBox wordsTextBox;
        private System.Windows.Forms.Label phoneNumberLabel;
        private System.Windows.Forms.TextBox phoneNumberTextBox;
        private System.Windows.Forms.Label wordsLabel;
        private System.Windows.Forms.Button phoneNumberButton;
        private TextBox spacerTextBox;
        private Label maxDigitsLabel;
        private NumericUpDown maxDigitsUpDown;
        private Label cacheUsageLabel;
        private CheckBox cacheUsageCheckBox;
    }
}