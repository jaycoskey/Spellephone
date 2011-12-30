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
        /// 
        /// </summary>
        private const bool doDumpLoadedWords_Debug = false;
        private const bool doDumpTree_Debug = false;
        private const bool doLoadTestWords_Debug = false;
        private void displayWords()
        {
            string phoneString = phoneNumberTextBox.Text;
            WordTree tree = MainForm.Tree;
            IEnumerable<string> words = tree.GetAlphanumericWords(
                phoneString,
                (int) maxDigitsUpDown.Value,
                cacheUsageCheckBox.Checked);
            bool didFindWords = words.Count() > 0;
            wordsLabel.Text = didFindWords
                ? ("Words spelled in \"" + phoneString + "\":")
                : "Words spelled:";
            wordsTextBox.Text = (words.Count() == 0)
                ? "No words found"
                : String.Join(Environment.NewLine, words);
        }
        private void mainForm_Load(object sender, EventArgs e)
        {
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
                Debug.WriteLine(String.Join(Environment.NewLine, Tree.GetAllWords()));
            }
            if (doDumpTree_Debug)
            {
                Debug.WriteLine(Tree.ToString());
            }
        }
        private void phoneNumberButton_Click(object sender, EventArgs e)
        {
            displayWords();
        }
        private void phoneNumberTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                displayWords();
            }
        }
        #endregion // Private methods
    }
}