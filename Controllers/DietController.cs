using Microsoft.AspNetCore.Mvc;
namespace WeatherApp.Controllers;

[ApiController]
[Route("[controller]")]
public class DietController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "סלט ירוק", "דג סלמון", "חזה עוף", "שיבולת שועל", "תפוח אדמה אפוי"
    };

    // המערך מוגרל פעם אחת בלבד בעליית השרת ונשמר קבוע,
    // כדי שכל הבקשות יעבדו על אותם נתונים — בלי זה החיפוש חסר משמעות.
    private static readonly FoodItem[] Items = GenerateItems();

    private static FoodItem[] GenerateItems()
    {
        var startDate = DateOnly.FromDateTime(DateTime.Now);

        return Enumerable.Range(1, 5).Select(index => new FoodItem
        {
            Date = startDate.AddDays(index),
            Calories = Random.Shared.Next(100, 500),
            FoodName = Summaries[Random.Shared.Next(Summaries.Length)],
            ProteinGrams = Random.Shared.Next(5, 40)
        })
        .ToArray();
    }

    private readonly ILogger<DietController> _logger;

    public DietController(ILogger<DietController> logger)
    {
        _logger = logger;
    }

    // GET /Diet
    [HttpGet(Name = "GetDietData")]
    public IEnumerable<FoodItem> Get()
    {
        return Items;
    }

    // GET /Diet/GetDietDataByDate/2026-10-15
    // מחזיר 404 אם אין מוצר בתאריך המבוקש.
    [HttpGet("GetDietDataByDate/{date}", Name = "GetDietDataByDate")]
    public ActionResult<FoodItem> GetDietDataByDate(DateOnly date)
    {
        var foodItem = Items.FirstOrDefault(item => item.Date == date);

        if (foodItem is null)
            return NotFound($"לא נמצא מוצר בתאריך {date:yyyy-MM-dd}");

        return Ok(foodItem);
    }
}
