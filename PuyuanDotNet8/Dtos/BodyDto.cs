namespace PuyuanDotNet8.Dtos
{
    public class BodyDto
    {
        public double systolic {  get; set; }
        public double diastolic { get; set; }
        public int pulse {  get; set; }
        public DateTime? recorded_at { get; set; }

    }
    public class WeightDto
    {
        public double weight { get; set; }
        public double body_fat { get; set; }
        public double bmi { get; set; }
        public DateTime recorded_at { set; get; }
    }
    public class BloodSugarDto
    {
        public int sugar { get; set; }
        public int timeperiod { get; set; }
        public DateTime recorded_at { get; set; }
    }

    public class HbA1cDto
    { 
        public double alc { get; set; }
        public DateTime recorded_at { get; set; }
    }
    public class HbA1cDelete
    {
        public List<int> ids {  get; set; }    
    }

}
