namespace PuyuanDotNet8.Dtos
{
    public class DailyDietDto
    {
        public string? description { get; set; }
        public int? meal { get; set; }
        public string? tag { get; set; }
        public string? image { get; set; }
        public double? lat { get; set; }
        public double? lng { get; set; }
        public DateTime recorded_at { get; set; }
    }

    public class DairyDelete
    {
        public List<int>? blood_sugar { get; set; }
        public List<int>? blood_pressure { get; set; }
        public List<int>? diary_diets { get; set; }
        public List<int>? weights { get; set; }
    }
}
