// Copyright 2011, by Jay Coskey
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Diagnostics;

namespace Spellephone
{
    public partial class MainForm : Form
    {
        #region Lifecycle
        public MainForm()
        {
            InitializeComponent();
            
        }
        #endregion // Lifecycle

        #region Public fields / properties / methods
        public static WordTree Tree;
        #endregion // Public fields / properties / methods

        #region Private fields / methods
        /// <summary>
        ///     When true, a list of the words loaded from a word file are written to the Output window .		
        /// </summary>
        private const bool doDumpLoadedWords_Debug = false;
        /// <summary>
        ///     When true, the contents of the WordTree are written to the Output window.
        /// </summary>
        private const bool doDumpTree_Debug = false;
        /// <summary>
        ///     When true, a small test file of words is used instead of a full-sized word list
        /// </summary>
        private const bool doLoadTestWords_Debug = false;
        #endregion // Private fields / methods

        #region Private methods
        private void addTooltips()
        {
            ToolTip phoneNumberToolTip = new ToolTip();
            phoneNumberToolTip.SetToolTip(phoneNumberLabel, phoneNumberLabel.AccessibleDescription);

            ToolTip maxDigitsToolTip = new ToolTip();
            maxDigitsToolTip.SetToolTip(maxDigitsLabel, maxDigitsLabel.AccessibleDescription);

            ToolTip cacheUsageToolTip = new ToolTip();
            cacheUsageToolTip.SetToolTip(cacheUsageLabel, cacheUsageLabel.AccessibleDescription);
        }
        private void displayWords()
        {
            string phoneString = phoneNumberTextBox.Text;
            WordTree tree = MainForm.Tree;
            IEnumerable<string> words = tree.GetAlphanumericWords(
                phoneString,
                (int) maxDigitsUpDown.Value,
                cacheUsageCheckBox.Checked);
            bool didFindWords = words.Count() > 0;
            if (didFindWords)
            {
                string digitsStr = null;
                switch ((int) maxDigitsUpDown.Value) {
                    case 0:
                        digitsStr = "no digits";
                        break;
                    case 1:
                        digitsStr = "up to one digit";
                        break;
                    default:
                        digitsStr = "up to " + ((int) maxDigitsUpDown.Value).ToString() + " digits";
                        break;
                }
                wordsLabel.Text = "Words spelled in "
                    + "\"" + phoneString + "\""
                    + " with " + digitsStr + ":";
            }
            else
            {
                wordsLabel.Text = "Words spelled:";
            }
            wordsTextBox.Text = (words.Count() == 0)
                ? "No words found"
                : String.Join(Environment.NewLine, words);
        }

        private void mainForm_Load(object sender, EventArgs e)
        {
            addTooltips();
            wordsTextBox.Text = "Loading dictionary....";
            string fileName = doLoadTestWords_Debug
                ? "../../Resources/testWords.txt"
                : "../../Resources/mobyWords.txt";
            try
            {
                fileName = String.Format(fileName);
                Tree = new WordTree(fileName);
                // Debug.Print(Tree.ToString());
            }
            catch (Exception ex)
            {
                wordsTextBox.Text = "Could not read file: "
                    + fileName + ".  "
                    + ex.Message + Environment.NewLine;
            }
            wordsTextBox.Text = String.Format("Loaded {0:d} words.{1:s}",
                Tree.WordCount(int.MaxValue),
                Environment.NewLine);
            if (doDumpLoadedWords_Debug)
            {
                #pragma warning disable 0162
                Debug.WriteLine(String.Join(Environment.NewLine, Tree.GetAllWords()));
                #pragma warning restore 0162
            }
            if (doDumpTree_Debug)
            {
                #pragma warning disable 0162
                Debug.WriteLine(Tree.ToString());
                #pragma warning restore 0162
            }
            this.ActiveControl = phoneNumberTextBox;
        }

        private void phoneNumberButton_Click(object sender, EventArgs e)
        {
            displayWords();
        }

        private void phoneNumberTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            phoneNumberButton.Enabled = (phoneNumberTextBox.Text != String.Empty);
            if (e.KeyChar == '\r')
            {
                displayWords();
            }
        }
        #endregion // Private methods
    }
}