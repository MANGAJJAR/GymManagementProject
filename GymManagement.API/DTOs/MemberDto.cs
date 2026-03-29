namespace GymManagement.API.DTOs
{
    public class MemberDto
    {
        public int MemberId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }
        public string Phone { get; set; } = string.Empty;
        public DateTime JoinDate { get; set; }
        public string? TrainerName { get; set; }
        public string? PlanName { get; set; }
    }

    public class CreateMemberDto
    {
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }
        public string Phone { get; set; } = string.Empty;
        public int? TrainerId { get; set; }
        public int PlanId { get; set; }
    }

    public class UpdateMemberDto : CreateMemberDto
    {
        public int MemberId { get; set; }
    }
}
