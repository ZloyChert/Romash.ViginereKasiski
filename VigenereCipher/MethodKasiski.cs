using System.Collections.Generic;
using System.Linq;

namespace VigenereCipher
{
    public class Pair
    {
        public int PeriodLength { get; set; }
        public int CountOfSubstrings { get; set; }
        public string Substring { get; set; }
    }

    public class MethodKasiski
    {
        public int GCD(int a, int b)
        {
            if (b == 0)
            {
                return a;
            }
            else
            {
                return GCD(b, a % b);
            }
        }

        public int FindKeyLength(string text)
        {
            List<Pair> repeatCount = new List<Pair>();
            foreach (var digramLength in Enumerable.Range(3, 70))
            {
                for (int i = 0; i < text.Length - digramLength; i++)
                {
                    string temp = text.Substring(i, digramLength);
                    for (int j = i + 1; j < text.Length - digramLength; j++)
                    {
                        string temp2 = text.Substring(j, digramLength);
                        if (temp == temp2)
                        {
                            if (repeatCount.Any(n => n.PeriodLength == j - i && n.Substring == temp2))
                                repeatCount.FirstOrDefault(n => n.PeriodLength == j - i).CountOfSubstrings++;
                            else
                                repeatCount.Add(new Pair {
                                    PeriodLength = j - i,
                                    CountOfSubstrings = 1,
                                    Substring = temp2
                                });
                        }
                    }
                }
            }
            List<Pair> gcds = new List<Pair>();
            for(int i = 0; i < repeatCount.Count(); i++)
                for(int j = i + 1; j < repeatCount.Count(); j++)
                {
                    var keyLength = GCD(repeatCount[i].PeriodLength, repeatCount[j].PeriodLength);
                    if (gcds.Any(n => n.PeriodLength == keyLength))
                        gcds.FirstOrDefault(n => n.PeriodLength == keyLength).CountOfSubstrings++;
                    else
                        gcds.Add(new Pair
                        {
                            PeriodLength = keyLength,
                            CountOfSubstrings = 1
                        });
                }
            var a = gcds.OrderByDescending(n => n.CountOfSubstrings).ToList();
            return gcds.Where(n => n.PeriodLength > 3).OrderByDescending(n => n.CountOfSubstrings).ToList()[0].PeriodLength;
        }
    }
}
