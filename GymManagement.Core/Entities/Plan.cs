namespace GymManagement.Core.Entities
{
    public class Plan
    {
        public int PlanId { get; set; }
        public string PlanName { get; set; } = string.Empty;
        public int DurationMonths { get; set; }
        public decimal Price { get; set; }

        // Navigation property
        public ICollection<Member> Members { get; set; } = new List<Member>();
    }
}
