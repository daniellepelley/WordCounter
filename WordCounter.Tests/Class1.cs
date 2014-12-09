using System;
using System.Collections.Generic;
using System.Diagnostics;
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


        public void Test2()
        {
            

        }


    }
}
