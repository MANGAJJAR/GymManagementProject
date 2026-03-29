namespace GymManagement.Core.Entities
{
    public class Member
    {
        public int MemberId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }
        public string Phone { get; set; } = string.Empty;
        public DateTime JoinDate { get; set; } = DateTime.UtcNow;

        // Foreign Keys
        public int? TrainerId { get; set; }
        public int PlanId { get; set; }

        // Navigation properties
        public Trainer? Trainer { get; set; }
        public Plan? Plan { get; set; }
        public ICollection<Payment> Payments { get; set; } = new List<Payment>();
    }
}
