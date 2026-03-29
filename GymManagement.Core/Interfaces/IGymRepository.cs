using GymManagement.Core.Entities;

namespace GymManagement.Core.Interfaces
{
    public interface IGymRepository
    {
        // Members
        Task<IEnumerable<Member>> GetAllMembersAsync();
        Task<Member?> GetMemberByIdAsync(int id);
        Task<Member> AddMemberAsync(Member member);
        Task UpdateMemberAsync(Member member);
        Task DeleteMemberAsync(int id);

        // Trainers
        Task<IEnumerable<Trainer>> GetAllTrainersAsync();
        Task<Trainer?> GetTrainerByIdAsync(int id);
        Task<Trainer> AddTrainerAsync(Trainer trainer);
        Task AssignTrainerToMemberAsync(int memberId, int trainerId);

        // Plans
        Task<IEnumerable<Plan>> GetAllPlansAsync();
        Task<Plan?> GetPlanByIdAsync(int id);
        Task<Plan> AddPlanAsync(Plan plan);

        // Payments
        Task<IEnumerable<Payment>> GetAllPaymentsAsync();
        Task<IEnumerable<Payment>> GetPaymentsByMemberIdAsync(int memberId);
        Task<Payment> RecordPaymentAsync(Payment payment);
    }
}
