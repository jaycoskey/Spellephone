// Copyright 2011, by Jay Coskey
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Diagnostics;
using System.IO;

namespace Spellephone
{
    /// <summary>
    ///     A representation of a node of a WordTree.
    ///     Operations that should only be performed on the root node of the tree
    ///         are implemented in the class WordTree, which is a wrapper
    ///         around the root DicitonaryTreeNode.
    /// </summary>
    public class WordTreeNode : IDisposable
    {
        #region Lifecycle
        public WordTreeNode()
        {
            children = new Dictionary<Byte, WordTreeNode>();
            words = new List<string>();
        }
        #endregion // Lifecycle

        #region Operators
        public WordTreeNode this [byte b] {
            get { return children[b]; }
        }
        #endregion // Operators

        #region Public methods
        public void AddWord(string origWord)
        {
            char firstChar = origWord[0];
            byte firstDigit;
            if (! WordTree.LetterToDigit.TryGetValue(firstChar, out firstDigit)) { return; }
            if (! children.ContainsKey(firstDigit)) { children[firstDigit] = new WordTreeNode(); }
            children[firstDigit].addWordImpl(origWord, origWord.Substring(1));
        }

        void IDisposable.Dispose()
        {
            foreach (WordTreeNode child in children.Values) {
                ((IDisposable) child).Dispose();
            }
            words = null;
            children = null;
        }

        public int FullWordCount(int maxDepth, int curDepth)
        {
            int result = NodeWordCount
                + children.Values
                    .Select(wtn => wtn.FullWordCount(maxDepth, curDepth + 1))
                    .Sum();
            return result;
        }

        public WordTreeNode GetChild(byte digit)
        {
            WordTreeNode result;
            return (children.TryGetValue(digit, out result)) ? result : null;
        }

        public IEnumerable<WordTreeNode> GetChildren()
        {
            foreach (WordTreeNode child in children.Values)
            {
                yield return child;
            }
        }

        public List<string> GetWords()
        {
            return new List<string>(words);
        }

        public int NodeWordCount
        {
            get { return words.Count; }
        }

        public string ToString(
            IEnumerable<byte> digits,
            int maxDepth = int.MaxValue,
            int curDepth = 0)
        {
            string indentStr = String.Empty; // new String(' ', 2 * curDepth);
            string nodeIdStr = String.Format("{0,-15:s}",
                "[" + String.Join("", digits.Select(d => d.ToString())) + "]");
            string wordsStr = "\tWord list = "
                + String.Join(", ", words)
                + Environment.NewLine;
            StringBuilder sb = new StringBuilder();
            sb.Append(indentStr + nodeIdStr + wordsStr);
            if (curDepth < maxDepth) {
                foreach (byte b in children.Keys)
                {
                    sb.Append(children[b].ToString(
                        digits.Append(b), maxDepth, curDepth + 1));
                }
            }
            return sb.ToString();
        }
        #endregion // Public methods

        #region Private methods
        private void addWordImpl(string origWord, string partialWord)
        {
            if (partialWord == String.Empty)
            {
                this.words.Add(origWord);
                return;
            }
            char nextChar = partialWord[0];
            byte nextDigit;
            if (! WordTree.LetterToDigit.TryGetValue(nextChar, out nextDigit)) { return; }
            if (! children.ContainsKey(nextDigit)) { children[nextDigit] = new WordTreeNode(); }
            children[nextDigit].addWordImpl(origWord, partialWord.Substring(1));
        }
        #endregion // Private methods

        #region Private fields/properties
        private Dictionary<Byte, WordTreeNode> children;
        private List<string> words;
        #endregion // Private fields/properties
    }
}