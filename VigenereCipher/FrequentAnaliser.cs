using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VigenereCipher
{
    public class FrequentAnaliser
    {
        public List<(char Symbol, double Frequence)> AlphabetStatistics { get; set; }
        public string Alphabet { get; set; } = "abcdefghijklmnopqrstuvwxyz";
        public char[] Separators { get; set; } = new char[] { ' ', '.', ',', ';', '(', ')', '\'', ':', '-' };
        private List<(char Symbol, double Frequence)> textStatistics { get; set; } = new List<(char Symbol, double Frequence)>();

        public FrequentAnaliser()
        {
            #region Statistics
            AlphabetStatistics = new List<(char Symbol, double Frequence)>()
            {
                ('a', 0.08167),
                ('b', 0.01492),
                ('c', 0.02782),
                ('d', 0.04253),
                ('e', 0.12702),
                ('f', 0.0228),
                ('g', 0.02015),
                ('h', 0.06094),
                ('i', 0.06966),
                ('j', 0.00153),
                ('k', 0.00772),
                ('l', 0.04025),
                ('m', 0.02406),
                ('n', 0.06749),
                ('o', 0.07507),
                ('p', 0.01929),
                ('q', 0.00095),
                ('r', 0.05987),
                ('s', 0.06327),
                ('t', 0.09056),
                ('u', 0.02758),
                ('v', 0.00978),
                ('w', 0.0236),
                ('z', 0.0015),
                ('y', 0.01974),
                ('z', 0.00074)
            };
            #endregion
        }

        private void CountTextStatistics(string text)
        {
            textStatistics = new List<(char Symbol, double Frequence)>();
            int symbolsCount = text.Where(n => !Separators.Any(v => v == n)).Count();
            foreach (var symb in Alphabet)
            {
                textStatistics.Add((symb, (double)text.Where(n => n == symb).Count() / symbolsCount));
            }
        }

        private double CountCharackteristicForOffset(int offset)
        {
            double charackteristic = 0;
            for(int i = 0; i < Alphabet.Length; i++)
                charackteristic += AlphabetStatistics[i].Frequence * textStatistics[(i + offset) % Alphabet.Length].Frequence;
            return charackteristic;
        }

        private int ReturnAllCharackteristics()
        {
            double max = CountCharackteristicForOffset(0);
            int index = 0;
            for (int i = 1; i < Alphabet.Length; i++)
            {
                if(max < CountCharackteristicForOffset(i))
                {
                    max = CountCharackteristicForOffset(i);
                    index = i;
                }
            }
            return index;
        }

        public int GetChipherOffset(string text)
        {
            CountTextStatistics(text);
            return ReturnAllCharackteristics();
        }
    }
}
