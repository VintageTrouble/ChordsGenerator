using System;
using System.Text;

using Core.Enums;
using Core.Extensions;

namespace Core.Entities;

public class Chord
{
    public string Name { get; set; }
    public List<Note> Notes { get; set; } = null!;
    public Note Tonic { get; set; }
    public Tone Tone { get; set; }

    private Chord(string name)
    {
        Name = name;
    }

    public Chord(string name, Note tonic, Tone tone)
        : this(name)
    {
        var notes = new List<Note>
        {
            tonic,
            tonic.AddInterval(tone == Tone.Major ? Interval.M3 : Interval.m3),
            tonic.AddInterval(Interval.M3, Interval.m3)
        };

        Notes = notes;
        Tonic = tonic;
        Tone = tone;
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.Append($"Chord: {Name}. Notes: ");

        foreach (var item in Notes)
        {
            sb.Append($"{item.ToString().Replace("Sharp", "#")}, ");
        }
        

        return sb.ToString().Trim(", ".ToArray());
    }
}
