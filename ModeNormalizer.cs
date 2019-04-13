using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScaleGenerator
{
    public static class ModeNormalizer
    {
        public static IEnumerable<Chord> RemoveAllModes(IEnumerable<Chord> scalesWithAllModes)
        {
            HashSet<ulong> scalesRegarlessModes = new HashSet<ulong>();

            foreach (Chord scaleWithMode in scalesWithAllModes)
            {
                ulong scaleRegardlessMode = GetScaleRegardlessMode(scaleWithMode);

                if (!scalesRegarlessModes.Contains(scaleRegardlessMode))
                {
                    scalesRegarlessModes.Add(scaleRegardlessMode);

                    yield return scaleWithMode;
                }
            }
        }

        private static ulong GetScaleRegardlessMode(Chord scale)
        {
            IEnumerable<Chord> allModes = GetAllModes(scale).ToList();
            return allModes.Select(mode => GetModeHashCode(mode)).OrderBy(hashCode => hashCode).First();
        }

        private static ulong GetModeHashCode(Chord mode)
        {
            ulong hashCode = 0;
            ulong multiplicator = 1;

            foreach (int note in mode)
            {
                hashCode += ((ulong)note * multiplicator);
                multiplicator *= 12;
            }

            return hashCode;
        }

        private static IEnumerable<Chord> GetAllModes(Chord scale)
        {
            int noteIndex = 0;
            foreach (int note in scale)
            {
                Chord modeModulatedScale = scale.GetModeModulatedScale(noteIndex);
                yield return modeModulatedScale;
                ++noteIndex;
            }
        }
    }
}
