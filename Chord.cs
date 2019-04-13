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
        private int[] notes;

        private int last;

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
            foreach (int note in this.notes)
            {
                if (!isFirst)
                {
                    stringBuilder.Append(", ");
                }
                stringBuilder.Append(note);
                isFirst = false;
            }

            return stringBuilder.ToString();
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
