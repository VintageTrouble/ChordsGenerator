using Core.Entities;
using Core.Enums;

namespace Application;

public class ChordsBuiler
{
    public Chord GetChord(Note note)
    {
        return new Chord(note.ToString(), note);
    }
}
