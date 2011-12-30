// Copyright 2011, by Jay Coskey
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Collections;

namespace Spellephone
{
    /// <summary>
    ///      Associates a series of digits with a WordTree, and supports the creation
    ///      of an iterator that traverses the WordTree, following WordTreeNodes that
    ///      correspond to the digits provided.
    /// </summary>
    class WordTreeNodeEnumerable : IEnumerable<WordTreeNode>
    {
        public WordTreeNodeEnumerable(WordTree tree, IEnumerable<byte> digits)
        {
            this.digits = digits.ToArray();
            this.tree = tree;
        }

        IEnumerator<WordTreeNode> IEnumerable<WordTreeNode>.GetEnumerator()
        {
            return new WordTreeNodeEnumerator(tree, digits);
        }

        IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return (System.Collections.IEnumerator) new WordTreeNodeEnumerator(tree, digits);
        }

        #region Private
        private byte[] digits;
        private WordTree tree;
        #endregion // Private
    }
}
