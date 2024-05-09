using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace PuyuanDotNet8.Dtos
{
    public class UsersetDto
    {
        public string name { get; set; }

        [DefaultValue("2003-08-11")]
        public string birthday { get; set; }

        [DefaultValue("0")]
        public double height { get; set; }

        [DefaultValue("0")]
        public string weight { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public bool gender { get; set; }
        public string fcm_id { get; set; }
        public string address { get; set; }
    }
    public class UserDefaultDto
    {
        public int? suger_delta_max { get; set; }
        public int? suger_delta_min { get; set; }
        public int? suger_morning_max { get; set; }
        public int? suger_morning_min { get; set; }
        public int? suger_evening_max { get; set; }
        public int? suger_evening_min { get; set; }
        public int? suger_before_max { get; set; }
        public int? suger_before_min { get; set; }
        public int? suger_after_max { get; set; }
        public int? suger_after_min { get; set; }
        public int? systolic_max { get; set; }
        public int? systolic_min { get; set; }
        public int? diastolic_max { get; set; }
        public int? diastolic_min { get; set; }
        public int? pulse_max { get; set; }
        public int? pulse_min { get; set; }
        public int? weight_max { get; set; }
        public int? weight_min { get; set; }
        public int? bmi_max { get; set; }
        public int? bmi_min { get; set; }
        public int? body_fat_max { get; set; }
        public int? body_fat_min { get; set; }
    }
    public class SettingDto
    {
        public bool after_recording { get; set; }
        public bool no_recording_for_a_day { get; set; }
        public bool over_max_or_under_min {  get; set; }
        public bool after_meal { get; set; }
        public bool unit_of_sugar { get; set; }
        public bool unit_of_weight { get; set; }
        public bool unit_of_height { get; set; }
    }
    public class BadgeUpdateDto
    {
        public int badge { get; set; }
    }
    public class LastRecordDto
    {
        public List<int?> diets { get; set; }
    }
}
