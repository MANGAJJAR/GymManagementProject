namespace GymManagement.API.DTOs
{
    public class TrainerDto
    {
        public int TrainerId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Specialization { get; set; } = string.Empty;
    }

    public class CreateTrainerDto
    {
        public string Name { get; set; } = string.Empty;
        public string Specialization { get; set; } = string.Empty;
    }
}
