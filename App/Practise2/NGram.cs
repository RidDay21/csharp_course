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
            var sentences = text.Split(new[] {'.', '?', '!'}, StringSplitOptions.RemoveEmptyEntries);
            
            foreach (string sentence in sentences)
            {
                var words = sentence.Split(new[] {' ',',', '-', ':', ';'}, StringSplitOptions.RemoveEmptyEntries);
                if (words.Length >= 2)
                {
                    SetNGramStats(words, bigramStats, trigramStats);
                }
            }
            
            var result = new Dictionary<string, string>();
            CreateDict(bigramStats, result);
            CreateDict(trigramStats, result);
            
            return result;
        }

        enum NGramType
        {
            Bigram,
            Trigram
        }

        private static void SetNGramStats(string[] words,
            Dictionary<string, Dictionary<string, int>> bigramStats,
            Dictionary<string, Dictionary<string, int>> trigramStats)
        {
            for (var i = 0; i < words.Length - 1; i++)
            {
                var bigramModel = new NGramModel(words, i, NGramType.Bigram);
                var trigramModel = new NGramModel(words, i, NGramType.Trigram);
                UpdateStatsWithNewNGram(bigramStats, bigramModel);
                if (trigramModel.NGramKey != String.Empty)
                {
                    UpdateStatsWithNewNGram(trigramStats, trigramModel);
                }
            }
        }

        private static void UpdateStatsWithNewNGram(Dictionary<string, Dictionary<string, int>> stats, NGramModel model)
        {
            if (!stats.ContainsKey(model.NGramKey))
            {
                stats[model.NGramKey] = new Dictionary<string, int> {
                {
                    model.NGramValue, 1
                } };
            }
            else if (stats[model.NGramKey]
                     .ContainsKey(model.NGramValue))
            {
                stats[model.NGramKey][model.NGramValue]++;
            }
            else
            {
                stats[model.NGramKey].Add(model.NGramValue, 1);
            }
        }

        private struct NGramModel
        {
            public string NGramKey { get; init; }
            public string NGramValue { get; init; }
            

            public NGramModel(string[] words, int currentWordIndex, NGramType type)
            {
                NGramKey = String.Empty;
                NGramValue = String.Empty;
                switch (type)
                {
                    case NGramType.Bigram:
                        NGramKey = words[currentWordIndex];
                        NGramValue = words[currentWordIndex + 1];
                        break;
                    case NGramType.Trigram:
                        if (currentWordIndex + 2 < words.Length)
                        {
                            NGramKey = $"{words[currentWordIndex]} {words[currentWordIndex + 1]}";
                            NGramValue = words[currentWordIndex + 2];
                        }
                        else
                            NGramKey = NGramValue = String.Empty;
                        break;
                    default:
                        break;
                }
            }
        }

        private static void CreateDict(
            Dictionary<string, Dictionary<string, int>> dictionaryWithNGramStats,
            Dictionary<string, string> resultDict)
        {
            foreach (var oneNGramPair in dictionaryWithNGramStats)
            {
                KeyValuePair<string, int> mostFrequent = default;
                foreach (var continuation in oneNGramPair.Value)
                {
                    if (continuation.Value > mostFrequent.Value ||
                        (continuation.Value == mostFrequent.Value && 
                         string.CompareOrdinal(continuation.Key, mostFrequent.Key) < 0))
                    {
                        mostFrequent = continuation;
                    }
                }
                resultDict.Add(oneNGramPair.Key, mostFrequent.Key);
            }
        }
    }
}