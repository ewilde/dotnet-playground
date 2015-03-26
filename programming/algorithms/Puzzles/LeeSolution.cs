using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Problem79
{
    public class LeeSolution
    {
        static readonly Dictionary<int, CharacterStats> CharacterStatsDict = new Dictionary<int, CharacterStats>();
        
        static void Main(string[] args)
        {
            string line;

            string filename = args[0];
            filename = Path.GetFullPath(filename);
            var file = new StreamReader(filename);


            while ((line = file.ReadLine()) != null)
            {
                UpdateCharacterStatsList(line);
            }            


            int firstChar = CharacterStatsDict.Values.Single(cs => cs.Before.Count == 0).Id;
            int lastChar = CharacterStatsDict.Values.Single(cs => cs.After.Count == 0).Id;

            var resultList = new List<int>{firstChar};
            int current = firstChar, next;
            while ((next = FindNext(current, resultList)) != lastChar)
            {
                resultList.Add(next);
                current = next;
            }
            resultList.Add(lastChar);

            string result = string.Join("", resultList);

            Console.WriteLine("Result = {0}", result);
            Console.ReadLine();

            file.Close();
        }

        public static string Compute(string[] data)
        {
            foreach(var line in data)
            {
                UpdateCharacterStatsList(line);
            }            


            int firstChar = CharacterStatsDict.Values.Single(cs => cs.Before.Count == 0).Id;
            int lastChar = CharacterStatsDict.Values.Single(cs => cs.After.Count == 0).Id;

            var resultList = new List<int>{firstChar};
            int current = firstChar, next;
            while ((next = FindNext(current, resultList)) != lastChar)
            {
                resultList.Add(next);
                current = next;
            }
            resultList.Add(lastChar);

            string result = string.Join("", resultList);

            return result;
        }

        static int FindNext(int current, IEnumerable<int> resultList)
        {
            // Next character must satisfy the following:
            // 1.) Its 'before' collection must contain the last char we found AND
            // 2.) its 'Before' collection must be a subset of the current result list
            var potentialNext = CharacterStatsDict
                .Values
                .Where(cs =>
                    cs.Before.Contains(current) &&
                    !cs.Before.Except(resultList).Any())
                .ToList();
                
            if (potentialNext.Count != 1)
            {
                throw new ApplicationException("Cannot solve for one definite answer...");
            }

            int result = potentialNext[0].Id;

            return result;
        }

        static void UpdateCharacterStatsList(string line)
        {
            for (int i = 0; i < line.Length; i++)
            {
                UpdateCharacterStatsEntry(
                    int.Parse(line[i].ToString(CultureInfo.InvariantCulture)),
                    i > 0 ? int.Parse(line[i - 1].ToString(CultureInfo.InvariantCulture)) : default(int?),
                    i < line.Length - 1 ? int.Parse(line[i + 1].ToString(CultureInfo.InvariantCulture)) : default(int?)
                    );
            }            
        }

        static void UpdateCharacterStatsEntry(int id, int? before, int? after)
        {
            CharacterStats characterStats;
            if (!CharacterStatsDict.TryGetValue(id, out characterStats))
            {
                CharacterStatsDict[id] = characterStats = new CharacterStats {Id = id};
            }
            if (before != null && !characterStats.Before.Contains(before.Value))
                characterStats.Before.Add(before.Value);
            if (after != null && !characterStats.After.Contains(after.Value))
                characterStats.After.Add(after.Value);
        }
    }

    class CharacterStats
    {
        public int Id;
        public List<int> Before = new List<int>();
        public List<int> After = new List<int>();
    }


    

}