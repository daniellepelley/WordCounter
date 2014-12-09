using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Security.Cryptography.X509Certificates;

namespace WordCounter
{
    public class WordStreamer
    {
        public char[] GetNonLetters()
        {
            return Enumerable.Range(0, 255)
                .Select(x => (char) x)
                .Where(x => !char.IsLetter(x))
                .ToArray();
        }

        public IObservable<string> CreateObservable(string file)
        {
            var nonLetters = GetNonLetters();

            return Observable.Create<string>(o =>
            {
                using (var reader = new StreamReader(file))
                {
                    while (!reader.EndOfStream)
                    {
                        var readLine = reader.ReadLine();

                        if (readLine == null)
                        {
                            continue;
                        }
                        foreach (
                            var word in
                                readLine.Split(nonLetters, StringSplitOptions.RemoveEmptyEntries))
                        {
                            o.OnNext(word);
                        }
                    }
                }

                o.OnCompleted();
                return Disposable.Empty;
            });
        }
    }

    public interface IWordCounter
    {
        void AddWord(string word);
    }

    public class TrieNodeWordCounter : IWordCounter
    {
        private readonly TrieNode _node = new TrieNode(null, (char)10);

        public void AddWord(string word)
        {
            _node.AddWord(word);
        }
    }

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





}