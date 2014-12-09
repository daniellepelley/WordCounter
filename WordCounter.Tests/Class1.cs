using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reactive.Linq;
using System.Threading;
using NUnit.Framework;

namespace WordCounter.Tests
{
    public class Class1
    {
        private readonly Dictionary<string, int> _results = new Dictionary<string, int>();
        
        [Test]
        public void Test1()
        {   
            var timer = new Stopwatch();
            timer.Start();
            var counter = new DictionaryWordCounter();
            var wordStreamer = new WordStreamer();
            var collection = wordStreamer.CreateObservable("UKWiki.txt").Subscribe(counter.AddWord);

            
            Assert.AreEqual(100, timer.ElapsedMilliseconds);
        }

        [Test]
        public void TrieNodeWordCounterSpeedTest()
        {
            var result = GetValue(new TrieNodeWordCounter());
            Assert.IsFalse(true, result.ToString(CultureInfo.InvariantCulture));
        }

        [Test]
        public void DictionaryCounterSpeedTest()
        {
            var result = GetValue(new DictionaryWordCounter());
            Assert.IsFalse(true, result.ToString(CultureInfo.InvariantCulture));
        }

        [Test]
        public void HashTableCounterSpeedTest()
        {
            var result = GetValue(new HashsetWordCounter());
            Assert.IsFalse(true, result.ToString(CultureInfo.InvariantCulture));
        }

        private static double GetValue(IWordCounter wordCounter)
        {
            var results = new List<long>();

            for (var i = 0; i < 100; i++)
            {
                var timer = new Stopwatch();
                timer.Start();

                var wordStreamer = new WordStreamer();
                wordStreamer.BruteForce("UKWiki.txt", wordCounter);

                results.Add(timer.ElapsedMilliseconds);
            }

            return results.Average();
        }

        [Test]
        public void Test3()
        {
            List<long> results = new List<long>();

            for (int i = 0; i < 100; i++)
            {
                var timer = new Stopwatch();
                timer.Start();
                var wordStreamer = new WordStreamer();
                wordStreamer.GetWordFrequency("UKWiki.txt");

                results.Add(timer.ElapsedMilliseconds);
            }

            Assert.AreEqual(100, results.Average());            

        }


        


    }
}
