using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace VigenereCipher
{
    class Program
    {
        static void Main(string[] args)
        {
             string textToEncrypt = File.ReadAllText("input.txt");

            string alphabet = "abcdefghijklmnopqrstuvwxyz";
            ViginereSystem vs = new ViginereSystem(alphabet);
            vs.KeyWord = "abcabdabgabcklonmltreploertyqwertyuio";
            //Console.WriteLine(vs.Decode(vs.Encode(s.ToLower())));

            string chipherText = vs.Encode(textToEncrypt.ToLower());
            MethodKasiski mk = new MethodKasiski();
            var keyLengthByKasiski = mk.FindKeyLength(chipherText);

            List<int> offsets = new List<int>();
            FrequentAnaliser fa = new FrequentAnaliser();
            for (int i = 0; i < keyLengthByKasiski; i++)
            {
                var ns = GetSymbolsWithPeriod(chipherText, i, keyLengthByKasiski);
                offsets.Add(fa.GetChipherOffset(ns));
            }

            string keyword = "";
            for (int i = 0; i < offsets.Count; i++)
                keyword += alphabet[offsets[i]];
            Console.WriteLine(keyword);
            Console.Read();
        }

        public static string GetSymbolsWithPeriod(string s, int start, int period)
        {
            StringBuilder sb = new StringBuilder();
            for(int i = start; i < s.Length; i += period)
            {
                sb.Append(s[i]);
            }
            return sb.ToString();
        }
    }
}
