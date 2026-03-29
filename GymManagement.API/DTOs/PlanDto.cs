namespace GymManagement.API.DTOs
{
    public class PlanDto
    {
        public int PlanId { get; set; }
        public string PlanName { get; set; } = string.Empty;
        public int DurationMonths { get; set; }
        public decimal Price { get; set; }
    }

    public class CreatePlanDto
    {
        public string PlanName { get; set; } = string.Empty;
        public int DurationMonths { get; set; }
        public decimal Price { get; set; }
    }
}
