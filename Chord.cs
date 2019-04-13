using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScaleGenerator
{
    public class Chord : IEnumerable<int>
    {
        #region Members
        private int[] notes;

        private int last;
        #endregion

        #region Constructors
        public Chord(params int[] newNotes)
        {
            if (newNotes.Length < 1)
            {
                throw new ArgumentException("Needs at leat one note");
            }

            for (int index = 0; index < newNotes.Length; ++index)
            {
                newNotes[index] = this.Modulo(newNotes[index], 12);
            }

            List<int> newUniqueSortedNotes = newNotes.Distinct().OrderBy(note => note).ToList();

            int firstValue = newUniqueSortedNotes[0];

            for (int index = 0; index < newUniqueSortedNotes.Count; ++index)
            {
                newUniqueSortedNotes[index] = newUniqueSortedNotes[index] - firstValue;
            }

            this.notes = newUniqueSortedNotes.ToArray();
            this.last = notes[this.notes.Length - 1];
        }

        public Chord(Chord smallerScale, int notePosition)
        {
            List<int> newNotes = new List<int>(smallerScale);
            newNotes.Add(notePosition);

            this.notes = newNotes.ToArray();
            this.last = notes[this.notes.Length - 1];
        }
        #endregion

        #region Properties
        public int Brightness
        {
            get
            {
                return this.notes.Sum();
            }
        }

        public int Stability
        {
            get
            {
                int stability = 0;

                if (this.notes.Contains(7))
                {
                    stability+=7;
                }
                if (this.notes.Contains(5))
                {
                    stability+=5;
                }

                return stability;
            }
        }
        #endregion

        private int Modulo(int value, int maxValue)
        {
            while (value >= maxValue)
            {
                value -= maxValue;
            }
            while (value < 0)
            {
                value += maxValue;
            }
            return value;
        }

        public int Last
        {
            get { return this.last; }
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            bool isFirst = true;

            for (int noteIndex = 0; noteIndex < this.notes.Length; ++noteIndex)
            {
                int previousNote = noteIndex > 0 ? this.notes[noteIndex - 1] : -1;
                int currentNote = this.notes[noteIndex];
                int nextNote = noteIndex < this.notes.Length - 1 ? this.notes[noteIndex + 1] : -1;

                if (!isFirst)
                {
                    stringBuilder.Append(", ");
                }
                stringBuilder.Append(this.GetNoteDescription(previousNote, currentNote, nextNote));
                isFirst = false;
            }

            return stringBuilder.ToString();
        }

        private string GetNoteDescription(int previousNote, int currentNote, int nextNote)
        {
            if (currentNote == 0)
            {
                return "C";
            }
            else if (currentNote == 2)
            {
                return "D";
            }
            else if (currentNote == 4)
            {
                return "E";
            }
            else if (currentNote == 5)
            {
                return "F";
            }
            else if (currentNote == 7)
            {
                return "G";
            }
            else if (currentNote == 9)
            {
                return "A";
            }
            else if (currentNote == 11)
            {
                return "B";
            }
            else if (currentNote == 1)
            {
                if (previousNote == 0)
                {
                    return "Db";
                }
                else
                {
                    return "C#";
                }
            }
            else if (currentNote == 3)
            {
                if (previousNote == 2 || previousNote == 1)
                {
                    return "Eb";
                }
                else
                {
                    return "D#";
                }
            }
            else if (currentNote == 6)
            {
                if (previousNote == 5)
                {
                    return "Gb";
                }
                else
                {
                    return "F#";
                }
            }
            else if (currentNote == 8)
            {
                if (previousNote == 7 || previousNote == 6)
                {
                    return "Ab";
                }
                else
                {
                    return "G#";
                }
            }
            else if (currentNote == 10)
            {
                if (previousNote == 9 || previousNote == 8)
                {
                    return "Bb";
                }
                else
                {
                    return "A#";
                }
            }
            else
            {
                return currentNote.ToString();
            }
        }

        public IEnumerator<int> GetEnumerator()
        {
            return this.notes.ToList().GetEnumerator();
        }

        public Chord GetModulatedScale(int offset)
        {
            int[] newNotes = new int[this.notes.Length];

            for (int index = 0; index < this.notes.Length; ++index)
            {
                newNotes[index] = this.Modulo(this.notes[index] - offset, 12);
            }

            return new Chord(newNotes);
        }

        public bool ContainsChord(Chord chord)
        {
            foreach (int note in chord)
            {
                if (!this.notes.Contains(note))
                {
                    return false;
                }
            }
            return true;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.notes.GetEnumerator();
        }
    }
}
