using GymManagement.API.DTOs;

namespace GymManagement.API.Services
{
    public interface IGymService
    {
        // Members
        Task<IEnumerable<MemberDto>> GetAllMembersAsync();
        Task<MemberDto?> GetMemberByIdAsync(int id);
        Task<MemberDto> AddMemberAsync(CreateMemberDto dto);
        Task UpdateMemberAsync(UpdateMemberDto dto);
        Task DeleteMemberAsync(int id);

        // Trainers
        Task<IEnumerable<TrainerDto>> GetAllTrainersAsync();
        Task<TrainerDto> AddTrainerAsync(CreateTrainerDto dto);

        // Plans
        Task<IEnumerable<PlanDto>> GetAllPlansAsync();
        Task<PlanDto> AddPlanAsync(CreatePlanDto dto);

        // Payments
        Task<IEnumerable<PaymentDto>> GetAllPaymentsAsync();
        Task<PaymentDto> RecordPaymentAsync(CreatePaymentDto dto);
    }
}
