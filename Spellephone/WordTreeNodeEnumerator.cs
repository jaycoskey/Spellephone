// Copyright 2011, by Jay Coskey
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Collections;

namespace Spellephone
{
    /// <summary>
    ///     An iterator through a Dictionary tree, following a specific path of digits.
    /// </summary>
    public class WordTreeNodeEnumerator : IEnumerator<WordTreeNode>
    {
        #region Lifecycle
        public WordTreeNodeEnumerator(WordTree tree, byte[] digits)
        {
            this.rootNode = tree.RootNode;
            this.curNode = rootNode;

            this.digits = digits;
            this.maxDepth = digits.Length;
        }
        #endregion Lifecycle

        #region Public non-enumerator fields / properties / methods
        public int CurrentDepth { get { return curDepth; } }

        public int MaxDepth { get { return maxDepth; } }

        public string Word
        {
            get { return Word; }
        }

        public int WordLength
        {
            get { return Word.Length; }
        }
        #endregion // Public non-enumerator fields / properties / methods

        #region Enumerator interface
        WordTreeNode IEnumerator<WordTreeNode>.Current
        {
            get { return curNode; }
        }

        object IEnumerator.Current
        {
            get { return (Object) curNode; }
        }

        public void Dispose()
        {
            this.curNode = null;
            this.digits = null;
        }

        public bool MoveNext()
        {
            if (curDepth == maxDepth) { return false; }
            WordTreeNode nextNode = curNode.GetChild(digits[curDepth]);
            if (nextNode == null) { return false; }
            curNode = nextNode;
            curDepth++;
            return true;
        }

        public void Reset()
        {
            curDepth = 0;
            curNode = rootNode;
        }
        #endregion // Enumerator interface

        #region Private fields
        private WordTreeNode rootNode;

        private int curDepth;
        private WordTreeNode curNode;
        private byte[] digits;
        private int maxDepth;
        #endregion // Private fields
    }
}