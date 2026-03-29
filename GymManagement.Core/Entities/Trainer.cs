namespace GymManagement.Core.Entities
{
    public class Trainer
    {
        public int TrainerId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Specialization { get; set; } = string.Empty;

        // Navigation property
        public ICollection<Member> Members { get; set; } = new List<Member>();
    }
}
