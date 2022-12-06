using System;
using Core.Enums;
using Core.Extensions;

namespace Core.Entities;

public class Chord
{
    public string Name { get; set; }
    public IEnumerable<Note> Notes { get; set; }

    private Chord(string name)
    {
        Name = name;
    }

    public Chord(string name, IEnumerable<Note> notes)
        : this(name)
    {
        Notes = notes;
    }

    public Chord(string name, Note tonic)
        : this(name)
    {
        var notes = new List<Note>
        {
            tonic,
            tonic.AddInterval(Interval.M3),
            tonic.AddInterval(Interval.M3, Interval.m3)
        };

        Notes = notes;
    }
}
