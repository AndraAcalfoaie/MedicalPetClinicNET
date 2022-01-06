namespace Services.Models.Patient
{
    public class PatientDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int Age { get; set; }
        public int ClientId { get; set; }
    }
}
