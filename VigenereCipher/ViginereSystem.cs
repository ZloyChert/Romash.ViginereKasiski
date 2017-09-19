using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VigenereCipher
{
    public class ViginereSystem
    {
        public ViginereSystem(string alphabet)
        {
            Separators = new char[] { ' ', '.', ',', ';', '(', ')', '\'', ':', '-' };
            Alphabet = alphabet;
        }

        public string[] LookupTable { get; set; }

        public string Alphabet { get; private set; }

        public char[] Separators { get; set; }

        private string keyWord;

        public string KeyWord {
            get
            {
                return keyWord;
            }
            set
            {
                keyWord = value;
                int keyWordLength = keyWord.Length;
                LookupTable = new string[keyWordLength];
                for (int i = 0; i < keyWordLength; i++)
                {
                    for(int j = 0; j < Alphabet.Length; j++)
                    {
                        LookupTable[i] += Alphabet[((j + Alphabet.IndexOf(keyWord[i])) % Alphabet.Length)];
                    }
                }
            }
        }

        public string Encode(string stringToEncode)
        {
            if(stringToEncode.Any(n => !Alphabet.Contains(n) && !Separators.Contains(n)))
            {
                throw new Exception("Finded symbol not from alphabet - ");
            }
            string s = string.Empty;
            for (int i = 0; i < stringToEncode.Length; i++)
            {
                if (Separators.Any(n => n == stringToEncode[i]))
                    s += stringToEncode[i];
                else
                    s += LookupTable[i % KeyWord.Length][Alphabet.IndexOf(stringToEncode[i])];
            }
            return s;
        }

        public IEnumerable<char> Decode(string stringToDecode)
        {
            if (stringToDecode.Any(n => !Alphabet.Contains(n) && !Separators.Contains(n)))
            {
                throw new Exception("Finded symbol not from alphabet");
            }
            string s = string.Empty;
            for (int i = 0; i < stringToDecode.Length; i++)
            {
                if (Separators.Any(n => n == stringToDecode[i]))
                    s += stringToDecode[i];
                else
                    s += Alphabet[LookupTable[i % KeyWord.Length].IndexOf(stringToDecode[i])];
            }
            return s;
        }
    }
}
