using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScaleGenerator
{
    public static class ChordDescriptor
    {
        public static string GetChordDescription(int[] notes)
        {
            StringBuilder stringBuilder = new StringBuilder();

            bool isFirst = true;

            for (int noteIndex = 0; noteIndex < notes.Length; ++noteIndex)
            {
                int previousNote = noteIndex > 0 ? notes[noteIndex - 1] : -1;
                int currentNote = notes[noteIndex];
                int nextNote = noteIndex < notes.Length - 1 ? notes[noteIndex + 1] : -1;

                if (!isFirst)
                {
                    stringBuilder.Append(", ");
                }
                stringBuilder.Append(GetNoteDescription(previousNote, currentNote, nextNote));
                isFirst = false;
            }

            return stringBuilder.ToString();
        }

        private static string GetNoteDescription(int previousNote, int currentNote, int nextNote)
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
    }
}
