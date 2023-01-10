using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab_4
{
    public class Program
    {
        private static bool _isRight = true;

        private static void Main(string[] args)
        {
            var grammatic = new List<Grammar>
            {
                new Grammar("S", "B"),
                new Grammar("B", "A"),
                new Grammar("A", "T"),
                new Grammar("A", "AZT"),
                new Grammar("T", "U"),
                new Grammar("T", "TMU"),
                new Grammar("U", "H"),
                new Grammar("H", "R"),
                new Grammar("H", "(A)"),
                new Grammar("R", "I"),
                new Grammar("I", "D"),
                new Grammar("I", "ID"),
                new Grammar("Z", "+"),
                new Grammar("M", "/"),
                new Grammar("D", "d"),
            };

            grammatic.Reverse();
            var rules = grammatic.GroupBy(rule => rule.Key).ToDictionary(rule => rule.Key, rule => rule.ToList());
            foreach (var key in rules)
            {
                var total = new List<char>();
                AppendData(key.Key, rules, total);
                var leftOrRightSign = _isRight ? "R" : "L";
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine();
                Console.Write($"{leftOrRightSign}({key.Key}) ----> ");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write($"{{{string.Join(", ", total.Distinct())}}}");


            }
            Console.ReadLine();
        }

        private static void AppendData(string key, IReadOnlyDictionary<string, List<Grammar>> rules, ICollection<char> result)
        {
            if (!rules.ContainsKey(key)) return;

            var values = rules[key];
            foreach (var value in values)
            {
                var newKey = _isRight ? value.Value.Last() : value.Value.First();
                if (newKey.ToString().Equals(key))
                {
                    result.Add(newKey);
                    continue;
                }
                result.Add(newKey);
                AppendData(newKey.ToString(), rules, result);
            }
        }
    }

    public class Grammar
    {
        public Grammar(string key, string value)
        {
            Key = key;
            Value = value;
        }

        public string Key { get; }

        public string Value { get; }
    }
}
