// Copyright 2011, by Jay Coskey
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Spellephone
{
    public static class ExtentionMethods
    {
        public static IEnumerable<T> Append<T>(this IEnumerable<T> items, T last) {
            foreach (T item in items) { yield return item; }
            yield return last;
            yield break;
        }

        public static string ToString_Debug(
            this Dictionary<WordTree.Bounds, List<string>> alphaWordsDict)
        {
            StringBuilder sb = new StringBuilder();
            foreach (WordTree.Bounds bs in alphaWordsDict.Keys)
            {
                sb.Append(String.Format("{0:s} => {1:s}",
                    bs, String.Join(", ", alphaWordsDict[bs])));
                sb.Append(Environment.NewLine);
            }
            return sb.ToString();
        }
    }
}