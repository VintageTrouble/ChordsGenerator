using Application;

var cb = new ChordsBuiler();

while (true)
{
    Console.Write("Enter a chord: ");
    var chordText = Console.ReadLine();

    try
    {
        var chord = cb.ParseChord(chordText);

        Console.WriteLine(chord);
    }
    catch (FormatException ex)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"Error: {ex.Message}");
        Console.ResetColor();
    }
    finally
    {
        Console.WriteLine("Press enter to continue...");

        Console.ReadKey(true);
    }
}
