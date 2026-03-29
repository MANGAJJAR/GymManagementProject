namespace GymManagement.Core.Entities
{
    public class Payment
    {
        public int PaymentId { get; set; }
        public int MemberId { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; } = DateTime.UtcNow;
        public string Status { get; set; } = string.Empty;

        // Navigation property
        public Member? Member { get; set; }
    }
}
