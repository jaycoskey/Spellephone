// Copyright 2011, by Jay Coskey
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Collections;

namespace Spellephone
{
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
