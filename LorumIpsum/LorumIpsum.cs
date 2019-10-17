using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using DevTest;
using System.Diagnostics;

namespace LorumIpsum
{
    public class Word
    {
        public string Value;
        public int Frequency;
        public int Length;

        public Word(string _value)
        {
            Value = _value;
            Frequency = 1;
            Length = _value.Length;
        }
    }

    public class Character
    {
        public char Value;
        public int Frequency;

        public Character(char _value)
        {
            Value = _value;
            Frequency = 1;
        }
    }

    public class LpStream : IDisposable
    {
        protected LorumIpsumStream stream;
        protected StreamReader reader;
        public int Characters { get; private set; }
        public int Words { get; private set;  }
        public string LastReadText { get; private set;  }
        public string Text { get; private set; }

        public List<Word> Largest5
        {
            get
            {
                if (WordStack == null || WordStack.Count <= 0) return null;
                return WordStack.OrderByDescending(o => o.Length).Take(5).ToList();
            }
        }

        public List<Word> Smallest5
        {
            get
            {
                if (WordStack == null || WordStack.Count <= 0) return null;
                return WordStack.OrderBy(o => o.Length).Take(5).ToList();
            }
        }

        public List<Word> FrequentlyUsed10
        {
            get
            {
                if (WordStack == null || WordStack.Count <= 0) return null;
                return WordStack.OrderByDescending(o => o.Frequency).Take(10).ToList();
            }
        }

        public List<Character> AllCharacters
        {
            get
            {
                if (CharacterStack == null || CharacterStack.Count <= 0) return null;
                return CharacterStack.OrderByDescending(o => o.Frequency).ToList();
            }
        }

        List<Word> WordStack { get; set; }
        List<Character> CharacterStack { get; set; }

        bool disposed = false;

        public LpStream()
        {
            stream = new LorumIpsumStream();
            reader = new StreamReader(stream, Encoding.Unicode);

            Characters = 0;
            Words = 0;
            WordStack = new List<Word>();
            CharacterStack = new List<Character>();
        }

        public void ReadText(int words)
        {
            StringBuilder st = new StringBuilder(Text);
            StringBuilder sb = new StringBuilder();
            StringBuilder sw = new StringBuilder();

            int count = 0;

            while (count < words)
            {
                char[] chr = new char[1];
                reader.Read(chr, 0, 1);
                Characters++;

                st.Append(chr[0].ToString());
                sb.Append(chr[0].ToString());

                AddToCharacterStack(chr[0]);

                if (chr[0] == ' ')
                {
                    Words++;
                    count++;

                    AddToWordStack(sw.ToString());
                    sw.Clear();

                } else
                {
                    if (chr[0] != '.') sw.Append(chr[0].ToString());
                }
            }

            LastReadText = sb.ToString();
            Text = st.ToString();
        }

        public async Task ReadTextAsync(int words)
        {
            StringBuilder st = new StringBuilder(Text);
            StringBuilder sb = new StringBuilder();
            StringBuilder sw = new StringBuilder();

            int count = 0;

            while (count < words)
            {
                char[] chr = new char[1];
                await reader.ReadAsync(chr, 0, 1);
                Characters++;

                st.Append(chr[0].ToString());
                sb.Append(chr[0].ToString());

                AddToCharacterStack(chr[0]);

                if (chr[0] == ' ')
                {
                    Words++;
                    count++;

                    AddToWordStack(sw.ToString());
                    sw.Clear();

                } else
                {
                    if (chr[0] != '.') sw.Append(chr[0].ToString());
                }
            }

            LastReadText = sb.ToString();
            Text = st.ToString();
        }

        void AddToCharacterStack(char chr)
        {
            Character character = null;
            if (CharacterStack.Count > 0) character = CharacterStack.Find(x => x.Value == chr);
            if (character == null)
            {
                CharacterStack.Add(new Character(chr));
            }
            else
            {
                character.Frequency++;
            }
        }

        void AddToWordStack(string wrd)
        {
            Word word = null;
            if (WordStack.Count > 0) word = WordStack.Find(x => x.Value == wrd);
            if (word == null)
            {
                WordStack.Add(new Word(wrd));
            }
            else
            {
                word.Frequency++;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                // close our reader
                reader.Close();
            }

            disposed = true;
        }

        ~LpStream()
        {
            Dispose(false);
        }
    }

}
