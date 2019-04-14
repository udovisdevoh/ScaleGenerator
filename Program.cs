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

            const int noteCountInScale = 6;
            const bool isEnableMultipleSameSizeChordOnSameRootNote = false;

            Chord minor = new Chord(0, 3, 7);
            Chord major = new Chord(0, 4, 7);
            Chord augmented = new Chord(0, 4, 8);
            Chord diminished = new Chord(0, 3, 6);
            Chord powerChord = new Chord(0, 7);
            Chord fourth = new Chord(0, 5);

            scaleGenerator.AddChord(minor, 2);
            //scaleGenerator.AddChord(major, 1);
            scaleGenerator.AddChord(powerChord, 4);
            //scaleGenerator.AddChord(fourth, 5);
            //scaleGenerator.AddChord(diminished, 1);
            //scaleGenerator.AddChord(augmented, 1);

            IEnumerable<Chord> scales = scaleGenerator.GenerateScales(noteCountInScale, isEnableMultipleSameSizeChordOnSameRootNote);
            scaleGenerator.Reset();

            foreach (Chord scale in scales)
            {
                Console.WriteLine(scale);
            }

            Console.ReadLine();
        }
    }
}
