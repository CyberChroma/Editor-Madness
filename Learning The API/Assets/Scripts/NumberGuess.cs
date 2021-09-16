// Enum to store a number between 1 and 10
public enum Number
{
    ChooseANumber,
    One,
    Two,
    Three,
    Four,
    Five,
    Six,
    Seven,
    Eight,
    Nine,
    Ten
}

[System.Serializable]
public class NumberGuess
{
    // Getting the number enum
    public Number number;

    public NumberGuess()
    {
        // Setting the default enum value
        number = Number.ChooseANumber;
    }
}
