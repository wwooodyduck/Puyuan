namespace PuyuanDotNet8.Dtos
{
    public class DrugDto
    {
        public bool type { get; set; }
    }
    public class DrugUploadDto
    {
        public bool Type { get; set; }
        public string Name { get; set; }
        public DateTime recorded_at { get; set; }
    }

    public class DrugDeleteDto
    {
        public List<int> ids { get; set; }
    }
}
