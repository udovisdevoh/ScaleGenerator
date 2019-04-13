using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScaleGenerator
{
    public class Program
    {
        static void Main(string[] args)
        {
            ScaleGenerator scaleGenerator = new ScaleGenerator();

            const int noteCountInScale = 7;

            Chord minor = new Chord(0, 3, 7);
            Chord major = new Chord(0, 4, 7);
            Chord augmented = new Chord(0, 4, 8);
            Chord diminished = new Chord(0, 3, 6);

            scaleGenerator.AddChord(minor, 3);
            scaleGenerator.AddChord(major, 3);
            scaleGenerator.AddChord(diminished, 1);
            //scaleGenerator.AddChord(augmented, 1);

            IEnumerable<Chord> scales = scaleGenerator.GenerateScales(noteCountInScale).OrderBy(scale => scale.Stability).ThenBy(scale => scale.Brightness).ToList();
            scaleGenerator.Reset();

            foreach (Chord scale in scales)
            {
                Console.WriteLine(scale);
            }
        }
    }
}
