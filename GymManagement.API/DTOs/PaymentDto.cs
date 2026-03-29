namespace GymManagement.API.DTOs
{
    public class PaymentDto
    {
        public int PaymentId { get; set; }
        public int MemberId { get; set; }
        public string? MemberName { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string Status { get; set; } = string.Empty;
    }

    public class CreatePaymentDto
    {
        public int MemberId { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}
