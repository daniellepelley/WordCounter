using System.Collections;
using System.Collections.Generic;

namespace WordCounter
{
    public class DictionaryWordCounter : IWordCounter
    {
        private readonly Dictionary<string, int> _results = new Dictionary<string, int>();

        public Dictionary<string, int> Results
        {
            get { return _results; }
        }

        public void AddWord(string word)
        {
            if (Results.ContainsKey(word))
            {
                Results[word]++;
            }
            else
            {
                Results.Add(word, 1);
            }
        }
    }

    public class HashsetWordCounter : IWordCounter
    {
        private readonly Hashtable _results = new Hashtable();

        public Hashtable Results
        {
            get { return _results; }
        }

        public void AddWord(string word)
        {
            Results[word] = 0;

            if (Results.ContainsKey(word))
            {
                Results[word] = 0;
            }
            else
            {
                Results[word] = (int)Results[word] + 1;
            }
        }
    }
}