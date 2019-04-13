﻿using System;
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

            const int noteCountInScale = 5;
            const bool isEnableMultipleSameSizeChordOnSameRootNote = false;

            Chord minor = new Chord(0, 3, 7);
            Chord major = new Chord(0, 4, 7);
            Chord augmented = new Chord(0, 4, 8);
            Chord diminished = new Chord(0, 3, 6);

            //scaleGenerator.AddChord(minor, 2);
            scaleGenerator.AddChord(major, 2);
            //scaleGenerator.AddChord(diminished, 2);
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
