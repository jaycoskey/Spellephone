// Copyright 2011, by Jay Coskey
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Diagnostics;
using System.IO;
using System.Threading;

namespace Spellephone
{
    /// <summary>
    ///     A wrapper for the root node of a WordTree, which is represented as a WordTreeNode.
    /// </summary>
    public class WordTree : IDisposable
    {
        #region Types
        public struct Bounds
        {
            public Bounds(int begin, int end)
            {
                Begin = begin;
                End = end;
            }
            public int Begin;
            public int End;
            public override string ToString()
            {
                return String.Format("[{0:d}, {1:d}]", Begin, End);
            }
        }
        #endregion // Types

        #region Lifecycle
        static WordTree()
        {
            LetterToDigit = new Dictionary<char, byte>();
            foreach (byte digit in DigitToLetters.Keys)
            {
                foreach (char c in DigitToLetters[digit])
                {
                    LetterToDigit[c] = digit;
                }
            }
        }

        public WordTree()
        {
            rootNode = new WordTreeNode();
        }

        public WordTree(string fileName, int maxWords = int.MaxValue)
            : this()
        {
            LoadWordFile(fileName, maxWords);
        }
        #endregion // Lifecycle

        #region Public static fields/properties
        public static readonly Dictionary<Byte, List<char>> DigitToLetters
            = new Dictionary<Byte, List<char>>
                {
                    { 1, new List<char> {} },
                    { 2, new List<char> {'a', 'b', 'c'} },
                    { 3, new List<char> {'d', 'e', 'f'} },
                    { 4, new List<char> {'g', 'h', 'i'} },
                    { 5, new List<char> {'j', 'k', 'l'} },
                    { 6, new List<char> {'m', 'n', 'o'} },
                    { 7, new List<char> {'p', 'r', 's'} },
                    { 8, new List<char> {'t', 'u', 'v'} },
                    { 9, new List<char> {'w', 'x', 'y'} }
                };
        public static readonly Dictionary<char, Byte> LetterToDigit;
        public static readonly byte NoDigit = 255;
        #endregion // Public static fields/properties

        #region Public static methods
        public static byte[] GetDigits(string phoneNumberString)
        {
            var digits = phoneNumberString
                .Where(c => Char.IsDigit(c))
                .Select(c => byte.Parse(new String(c, 1)))
                .ToArray();
            return digits;
        }
        #endregion // Public static methods

        #region Public instance fields/properties/methods
        public void AddWord(string word)
        {
            rootNode.AddWord(word);
        }

        void IDisposable.Dispose()
        {
            ((IDisposable) RootNode).Dispose();
        }

        public IEnumerable<string> GetAllWords()
        {
            Stack<WordTreeNode> nodes = new Stack<WordTreeNode>();  // Depth first
            nodes.Push(RootNode);
            while (nodes.Count() > 0) {
                WordTreeNode node = nodes.Pop();
                foreach (string word in node.GetWords()) {
                    yield return word;
                }
                foreach (WordTreeNode child in node.GetChildren()) {
                    nodes.Push(child);
                }
            }
        }

        public IEnumerable<string> GetAlphanumericWords(
            string phoneString,
            int maxDigitsInResult = int.MaxValue,
            bool doUseResultsCache = false)
        {
            byte[] digits = WordTree.GetDigits(phoneString);
            Dictionary<WordTree.Bounds, List<string>> alphaWordsDict;
            getAlphaWordsDict(digits, out alphaWordsDict);

            // Debug.Write(alphaWordsDict.ToString_Debug());
            Dictionary<int, List<string>> resultsCache = null;
            if (doUseResultsCache)
            {
                resultsCache = new Dictionary<int, List<string>>();
            }
            List<string> words = getAlphanumericWordsImpl(alphaWordsDict, digits, 0, ref resultsCache);
            words = words.Distinct().ToList();  // Prevent duplicates, such as "ago" and "a" + "go".
            words = words.Where(word => word.Count(c => Char.IsDigit(c)) <= maxDigitsInResult).ToList();
            return words;
        }

        public void LoadWordFile(string fileName, int maxWords = int.MaxValue)
        {
            int numWords = 0;
            foreach (string word in File.ReadLines(fileName))
            {
                AddWord(word);
                if (++numWords >= maxWords) { break; }
            }
        }

        public WordTreeNode RootNode
        {
            get { return rootNode; }
        }

        public string ToString(int maxDepth = int.MaxValue)
        {
            return RootNode.ToString(Enumerable.Empty<byte>(), maxDepth, 0);
        }

        public int WordCount(int maxDepth = int.MaxValue)
        {
            return rootNode.FullWordCount(maxDepth, 0);
        }
        #endregion // Public instance fields/properties/methods

        #region Private instance methods
        /// <param name="wordsByLength">
        ///     Dictionary<Bounds, List<string>> = List of dictionary words that exactly match digitString[i..j].
        /// </param>
        private List<string> getAlphanumericWordsImpl(
            Dictionary<WordTree.Bounds, List<string>> alphaWordsDict,
            byte[] digits,
            int begin,
            ref Dictionary<int, List<string>> resultsCache)
        {
            if (resultsCache != null && resultsCache.ContainsKey(begin))
            {
                return resultsCache[begin];
            }
            int numDigits = digits.Length;
            List<string> result = new List<string>();

            for (int end = begin; end < numDigits; end++)
            {
                // Add words in digits[i : j], then move to remainder. 
                Bounds bounds = new Bounds(begin, end);
                if (!alphaWordsDict.ContainsKey(bounds))
                {
                    continue;
                }
                foreach (string word in alphaWordsDict[bounds])
                {
                    if (end + 1 == numDigits)
                    {
                        // Debug.WriteLine(String.Format("Found word ending result: \"{0:s}\"", word));
                        result.Add(word);
                    }
                    else
                    {
                        //Debug.WriteLine(String.Format(
                        //    "Found word not ending result: \"{0:s}\"", word));
                        List<string> words2 = getAlphanumericWordsImpl(
                            alphaWordsDict,
                            digits,
                            begin + word.Length,
                            ref resultsCache);
                        foreach (string word2 in words2)
                        {
                            //Debug.WriteLine(String.Format(
                            //    "Found complement: \"{0:s}\" => \"{1:s}\"", word, word2));
                            result.Add(word + word2);
                        }
                    }
                }
            }

            // Add a digit next, then alphanumeric words after that.
            if (begin + 1 < numDigits)
            {
                foreach (string subWord in
                    getAlphanumericWordsImpl(alphaWordsDict, digits, begin + 1, ref resultsCache))
                {
                    //Debug.WriteLine(String.Format(
                    //    "Found digit {0:d}'s complementary result: \"{1:s}\"", digits[begin], subWord));
                    result.Add(digits[begin].ToString() + subWord);
                }
            }
            else if (begin + 1 == numDigits)
            {
                //Debug.WriteLine(String.Format(
                //    "Result completed by digit {0:d}", digits[begin]));
                result.Add(digits[begin].ToString());
            }
            //Debug.WriteLine(String.Format(
            //    "numDigits={0:d}, begin={1:d}, words={2:s}",
            //    numDigits, begin, String.Join(", ", result)));
            if (resultsCache != null) {
                resultsCache[begin] = result;
            }
            return result;
        }

        private void getAlphaWordsDict(IEnumerable<byte> digits,
            out Dictionary<WordTree.Bounds, List<string>> alphaWordsDict
            )
        {
            alphaWordsDict = new Dictionary<WordTree.Bounds, List<string>>();
            for (int i = 0; i < digits.Count(); i++)
            {
                WordTreeNodeEnumerable digitNodes;
                digitNodes = new WordTreeNodeEnumerable(this, digits.Skip(i).ToArray());
                int length = 0;
                foreach (WordTreeNode node in digitNodes)
                {
                    Bounds bounds = new Bounds(i, i + length++);
                    IEnumerable<string> words = node.GetWords();
                    if (words.Count() > 0)
                    {
                        alphaWordsDict[bounds] = new List<string>(node.GetWords());
                    }
                }
            }
        }
        #endregion // Private instance methods

        #region Private instance fields/properties
        private WordTreeNode rootNode;
        #endregion // Private instance fields/properties
    }
}