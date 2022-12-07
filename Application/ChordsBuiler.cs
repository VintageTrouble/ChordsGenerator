using Core.Entities;
using Core.Enums;

namespace Application;

public class ChordsBuiler
{
    private readonly char[] _noteSigns = new char[] { '#', 'b' };
    public Chord GetChord(Note note, Tone tone)
    {
        return new Chord(note.ToString(), note, tone);
    }

    public Chord ParseChord(string? chord)
    {
        if (string.IsNullOrWhiteSpace(chord) || chord.Length > 3)
            throw new FormatException("Invalid format of a chord.");

        if (!Enum.GetNames(typeof(Note)).Contains(chord[0].ToString()))
            throw new FormatException($"Can't find tonic {chord[0]} for chord.");

        return chord.Length == 1
            ? new Chord(chord, Enum.Parse<Note>(chord), Tone.Major)
            : ParseModifiedChord(chord);
    }

    private Chord ParseModifiedChord(string chord)
    {
        var hasSharp = !(chord[0] == 'E' || chord[0] == 'B');
        var maxLength = hasSharp ? 3 : 2;
        var sign = chord[chord.Length - (chord.Length == 3 ? 2 : 1)];

        if (!hasSharp && sign == _noteSigns[0])
            throw new FormatException("Sharp for E and B chords not exists.");

        var tone = ParseTone(chord, maxLength);

        if(chord.Length == 2)
            return new Chord(chord, Enum.Parse<Note>(chord[0].ToString()), tone);

        switch (sign)
        {
            case '#':
                return new Chord(chord, Enum.Parse<Note>(chord[0] + "Sharp"), tone);

            case 'b':
                var tonic = Enum.Parse<Note>(chord[0] + (hasSharp ? "Sharp" : "")) - 1;
                tonic = tonic < 0 ? (int)Note.B - tonic : tonic;

                return new Chord(chord, tonic, tone);

            default:
                throw new FormatException("Undeclared key sign.");
        }
    }

    private Tone ParseTone(string chord, int maxLength)
    {
        if (chord.Length == maxLength)
        {
            var sign = chord[maxLength - 1];
            if (sign == 'm')
                return Tone.Minor;

            if (_noteSigns.Contains(sign))
                return Tone.Major;

            throw new FormatException("Can't detect note sign.");
        }

        return Tone.Major;
    }
}
