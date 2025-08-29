// GameDate.cs

[System.Serializable] // Para que se pueda ver en el Inspector si es necesario
public class GameDate
{
    public int Day { get; private set; }
    public int Month { get; private set; }
    public int Year { get; private set; }

    // Constructor para una fecha inicial
    public GameDate(int day, int month, int year)
    {
        Day = day;
        Month = month;
        Year = year;
    }

    // Método para avanzar un día
    public void AdvanceDay()
    {
        Day++;
        // Lógica simple para meses de 30 días. ¡Podrías hacerlo más complejo!
        if (Day > 30)
        {
            Day = 1;
            Month++;
            if (Month > 12)
            {
                Month = 1;
                Year++;
            }
        }
    }

    // Un método útil para mostrar la fecha como texto
    public override string ToString()
    {
        return $"Día {Day}, Mes {Month}";
    }
}