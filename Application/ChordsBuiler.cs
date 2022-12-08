using Core.Entities;
using Core.Enums;

namespace Application;

public class ChordsBuiler
{
    private readonly char[] _noteSigns = new char[] { '#', 'b', 'm' };
    public Chord GetChord(Note note, Tone tone)
    {
        return new Chord(note.ToString(), note, tone);
    }

    public Chord[] GetChordsByTonic(string? tonic)
    {
        var coreNote = Enum.Parse<Note>(ValidateChord(tonic));
        var note = ParseTonic(tonic!, coreNote);

        return new Chord[]
        {
            new Chord(tonic!, note, Tone.Major),
            new Chord(tonic! + "m", note, Tone.Minor),
        };
    }

    public Chord ParseChord(string? chord)
    {
        var coreNote = Enum.Parse<Note>(ValidateChord(chord));
        var tonic = ParseTonic(chord!, coreNote);
        var tone = Tone.Major;

        if (chord!.Length > 1)
            tone = ParseTone(chord);

        return new Chord(chord, tonic, tone);
    }

    private string ValidateChord(string? chord)
    {
        if (string.IsNullOrWhiteSpace(chord) || chord.Length > 3)
            throw new FormatException("Invalid format of a chord.");

        if (chord[0] == 'H')
            chord = chord.Replace('H', 'B');

        var coreNote = chord[0].ToString().ToUpper();

        if (!Enum.GetNames(typeof(Note)).Contains(coreNote))
            throw new FormatException($"Can't find tonic {chord[0]} for chord.");

        foreach (var sign in chord.Substring(1))
        {
            if (!_noteSigns.Contains(sign))
                throw new FormatException($"Can't handle sign {sign} in chord.");
        }

        return coreNote;
    }

    private Note ParseTonic(string chord, Note coreNote)
    {
        if (chord.Contains('#'))
        {
            if(coreNote == Note.B || coreNote == Note.E)
                throw new FormatException($"Note {coreNote} can't be sharped");

            return coreNote + 1;
        }

        if (chord.Contains('b'))
        {
            if(coreNote == Note.C || coreNote == Note.F)
                throw new FormatException($"Note {coreNote} can't be flated");

            return coreNote - 1;
        }

        return coreNote;
    }

    private Tone ParseTone(string chord)
    {
        if (_noteSigns.Contains(chord.Last()))
            return Tone.Major;

        if (chord.Last() == 'm')
            return Tone.Minor;

        throw new FormatException("Can't detect chord tone.");
    }    
}
