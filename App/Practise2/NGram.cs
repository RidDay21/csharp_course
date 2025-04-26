using System;
using System.Collections.Generic;
using System.Text;

namespace App.Practise2
{
    public class NGram
    {
        public static Dictionary<string, string> CreateNGramStatistic(string text)
        {
            var bigramStats = new Dictionary<string, Dictionary<string, int>>();
            var trigramStats = new Dictionary<string, Dictionary<string, int>>();
            
            string[] sentences = text.Split(new[] {'.', '?', '!'}, StringSplitOptions.RemoveEmptyEntries);
            
            foreach (string sentence in sentences)
            {
                var words = sentence.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                if (words.Length >= 2)
                {
                    ProcessBigrams(words, bigramStats);
                    ProcessTrigrams(words, trigramStats);
                }
            }
            
            var result = new Dictionary<string, string>();
            CreateDict(bigramStats, result);
            CreateDict(trigramStats, result);
            
            return result;
        }

        private static void ProcessBigrams(string[] words, Dictionary<string, Dictionary<string, int>> bigramStats)
        {
            for (int i = 0; i < words.Length - 1; i++)
            {
                string currentWord = words[i];
                string nextWord = words[i + 1];

                if (!bigramStats.ContainsKey(currentWord))
                {
                    bigramStats[currentWord] = new Dictionary<string, int> { { nextWord, 1 } };
                }
                else if (bigramStats[currentWord].ContainsKey(nextWord))
                {
                    bigramStats[currentWord][nextWord]++;
                }
                else
                {
                    bigramStats[currentWord].Add(nextWord, 1);
                }
            }
        }

        private static void ProcessTrigrams(string[] words, Dictionary<string, Dictionary<string, int>> trigramStats)
        {
            for (int i = 0; i < words.Length - 2; i++)
            {
                string trigramKey = $"{words[i]} {words[i + 1]}";
                string continuation = words[i + 2];

                if (!trigramStats.ContainsKey(trigramKey))
                {
                    trigramStats[trigramKey] = new Dictionary<string, int> { { continuation, 1 } };
                }
                else if (trigramStats[trigramKey].ContainsKey(continuation))
                {
                    trigramStats[trigramKey][continuation]++;
                }
                else
                {
                    trigramStats[trigramKey].Add(continuation, 1);
                }
            }
        }

        private static void CreateDict(
            Dictionary<string, Dictionary<string, int>> subDict,
            Dictionary<string, string> resultDict)
        {
            foreach (var kvp in subDict)
            {
                KeyValuePair<string, int> mostFrequent = default;
                foreach (var continuation in kvp.Value)
                {
                    if (continuation.Value > mostFrequent.Value ||
                        (continuation.Value == mostFrequent.Value && 
                         string.CompareOrdinal(continuation.Key, mostFrequent.Key) < 0))
                    {
                        mostFrequent = continuation;
                    }
                }
                resultDict.Add(kvp.Key, mostFrequent.Key);
            }
        }
    }
}