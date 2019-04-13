using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScaleGenerator
{
    public static class KeyNormalizer
    {
        public static IEnumerable<Chord> GetMostDiatonicModes(IEnumerable<Chord> scales)
        {
            foreach (Chord scale in scales)
            {
                IEnumerable<Chord> allKeys = GetAllKeys(scale).ToList();
                yield return allKeys.OrderByDescending(key => key.DiatonicCloseness).First();
            }
        }

        private static IEnumerable<Chord> GetAllKeys(Chord scale)
        {
            for (int offset = 0; offset < 12; ++offset)
            {
                Chord modulateChord = scale.GetKeyModulatedScale(offset);
                yield return modulateChord;
            }
        }
    }
}
