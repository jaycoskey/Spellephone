Spellephone
Copyright 2011, by Jay Coskey

Summary: This program lets the user enter a phone number, and the program finds words that can be
  spelled by the letters corresponding to the sequential digits in the sequence.

Overview:
  This program first loads a word list (or "dictionary") of words into a trie, or prefix tree.
  It then allows the user to enter a phone number, and it finds combinations of dictionary words
  (and digits as filler) that can be "spelled" with a telephone keypad by pressing those digits.
  The user actually enters:
    (1) A telephone number, consisting of digits, and possibly other characters.
	      - The non-digit characters are ignored.
    (2) The maximum number of digits that will be allowed in the words spelled by the digits.
    (3) Whether or not the main algorithm uses a cache to eliminate possible duplications of computations.
  The program then displays the search results in the main control window, or displays a message in that same
  window saying that no results were found.

Implementation notes
  * The dictionary words are read into a trie, or prefix tree.  For each word, the corresponding series
    of digits is computed.  A series of WordTreeNodes is then added to the trie, each pointing to the next
	according to the next digit in the sequence.  Each WordTreeNode contains a list of words that are spelled
	by letters corresponding to the digits that lead from the root down to the given node.
  * The root WordTreeNode is wrapped in a WordTree class instance that has methods such as LoadWordFile(),
    which are not appropriate to have as a method in WordTreeNode.
  * Rather than repeatedly traverse the trie to find words corresponding to a given sequence of digits,
    the program first gathers (in a Dictionary called alphaWordsDict) a collection of all the words in the
	tree that can be spelled by contiguous subsequences of its digits.  This is then used to form the list
	of all alphanumeric sequences, which are composed of words with digits used as padding where necessary.
  * The user can choose through the UI to enable caching for the main algorithm that identifies alphanumeric
    sequences in the phone number entered.  Consider the phone number 246-4686 which, among other results,
	spells "ago-into".  Without caching, the algorithm would find the words "ago" and "a" + "go", and end up
	twice processing the suffix "4686" to see what words can be found there.  If digits are allowed as fill,
	then the last two digits, "86", are visited multiple times, after finding
	    ago+in, a+go+in, 2+go+in, a+46+in, ago+46, etc.
	With caching, the duplicate visits are handled quickly by cache lookups.

Testing notes
  * Three const boolean variables are set to false, but can be set to true in order to enable functionality
    that might be helpful in debugging.
      - doDumpLoadedWords_Debug.	Writes to the Output window a list of all words loaded from a word file.
      - doDumpTree_Debug.			Writes to the Output window the contents of the WordTree.
      - doLoadTestWords_Debug.		Loads a small file of test words, instead of a full-sized word list.
  * There are several other debugging statements in the code that are currently commented out.  For example,
    there is one that writes to the Output window an indexed list of the words in the tree that use a 
	subset of the digits in the phone number entered.