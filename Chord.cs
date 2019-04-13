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
            return ChordDescriptor.GetChordDescription(this.notes);
        }

        public IEnumerator<int> GetEnumerator()
        {
            return this.notes.ToList().GetEnumerator();
        }

        public Chord GetKeyModulatedScale(int offset)
        {
            int[] newNotes = new int[this.notes.Length];

            for (int index = 0; index < this.notes.Length; ++index)
            {
                newNotes[index] = this.Modulo(this.notes[index] - offset, 12);
            }

            return new Chord(newNotes);
        }

        public Chord GetModeModulatedScale(int offset)
        {
            int[] newNotes = new int[this.notes.Length];

            for (int index = 0; index < this.notes.Length; ++index)
            {
                int newIndex = (index + offset);

                newIndex = Modulo(newIndex, this.notes.Length);

                newNotes[index] = this.notes[newIndex];
            }

            int key = newNotes.First();
            for (int noteIndex = 0; noteIndex < newNotes.Length; ++noteIndex)
            {
                newNotes[noteIndex] = Modulo(newNotes[noteIndex] - key, 12);
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
