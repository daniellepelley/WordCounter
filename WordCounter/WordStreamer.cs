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

        public void BruteForce(string file, IWordCounter counter)
        {
            var nonLetters = GetNonLetters();

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
                        counter.AddWord(word);
                    }
                }
            }
        }

        public Dictionary<string, int> GetWordFrequency(string file)
        {
            var nonLetters = GetNonLetters();

            return File.ReadLines(file)
                       .SelectMany(x => x.Split(nonLetters))
                       .Where(x => x != string.Empty)
                       .GroupBy(x => x)
                       .ToDictionary(x => x.Key, x => x.Count());
        }
    }
}