namespace WeatherApp;

public class FoodItem
{
    public DateOnly Date { get; set; }

    public int Calories { get; set; }

    public string? FoodName { get; set; }

    public int ProteinGrams { get; set; }

    public string? Summary => Calories > 300 ? "עשיר בקלוריות" : "קל ומומלץ";
}
