using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScaleGenerator
{
    public class ScaleGenerator
    {
        private Dictionary<Chord, int> chords = new Dictionary<Chord, int>();

        public void Reset()
        {
            this.chords.Clear();
        }

        public void AddChord(Chord chord, int count)
        {
            chords.Add(chord, count);
        }

        public IEnumerable<Chord> GenerateScales(int noteCountInScale)
        {
            IEnumerable<Chord> scalesWithAllModes = this.GenerateScales(noteCountInScale, true).OrderByDescending(scale => scale.Stability).ThenByDescending(scale => scale.Brightness).ToList();
            IEnumerable<Chord> scalesWithoutModes = ModeNormalizer.RemoveAllModes(scalesWithAllModes).ToList();
            IEnumerable<Chord> scalesWithKeyCloserToDiatonicModes = KeyNormalizer.GetMostDiatonicModes(scalesWithoutModes).ToList();

            return scalesWithKeyCloserToDiatonicModes;
        }

        private IEnumerable<Chord> GenerateScales(int noteCountInScale, bool isApplyChordCountFilter)
        {
            if (isApplyChordCountFilter)
            {
                IEnumerable<Chord> scalesRegardlessChords = this.GenerateScales(noteCountInScale, false).ToList();

                foreach (Chord scale in scalesRegardlessChords)
                {
                    if (this.IsMatchChordCount(scale))
                    {
                        yield return scale;
                    }
                }
            }
            else
            {
                if (noteCountInScale == 1)
                {
                    yield return new Chord(0);
                }
                else
                {
                    IEnumerable<Chord> smallerScales = this.GenerateScales(noteCountInScale - 1, false);

                    foreach (Chord smallerScale in smallerScales)
                    {
                        IEnumerable<Chord> newScales = this.BuildScalesFromSmallerScale(smallerScale);

                        foreach (Chord scale in newScales)
                        {
                            yield return scale;
                        }
                    }
                }
            }
        }

        private bool IsMatchChordCount(Chord scale)
        {
            foreach (KeyValuePair<Chord, int> chordCount in this.chords)
            {
                Chord chord = chordCount.Key;
                int count = chordCount.Value;

                if (this.GetChordCount(scale, chord) < count)
                {
                    return false;
                }
            }

            return true;
        }

        private int GetChordCount(Chord scale, Chord chord)
        {
            int count = 0;
            foreach (int notePosition in scale)
            {
                if (this.IsMatchChordAt(scale, chord, notePosition))
                {
                    ++count;
                }
            }
            return count;
        }

        private bool IsMatchChordAt(Chord scale, Chord chordToMatch, int notePosition)
        {
            Chord scaleWithOffset = scale.GetKeyModulatedScaleNormalizedToZero(notePosition);
            return scaleWithOffset.ContainsChord(chordToMatch);
        }

        private IEnumerable<Chord> BuildScalesFromSmallerScale(Chord smallerScale)
        {
            for (int notePosition = smallerScale.Last + 1; notePosition < 12; ++notePosition)
            {
                Chord newScale = new Chord(smallerScale, notePosition);
                yield return newScale;
            }
        }
    }
}
