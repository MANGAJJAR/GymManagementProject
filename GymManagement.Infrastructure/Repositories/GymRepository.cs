using GymManagement.Core.Entities;
using GymManagement.Core.Interfaces;
using GymManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GymManagement.Infrastructure.Repositories
{
    public class GymRepository : IGymRepository
    {
        private readonly GymDbContext _context;

        public GymRepository(GymDbContext context)
        {
            _context = context;
        }

        // --- Members ---
        public async Task<IEnumerable<Member>> GetAllMembersAsync()
        {
            return await _context.Members
                .Include(m => m.Trainer)
                .Include(m => m.Plan)
                .ToListAsync();
        }

        public async Task<Member?> GetMemberByIdAsync(int id)
        {
            return await _context.Members
                .Include(m => m.Trainer)
                .Include(m => m.Plan)
                .Include(m => m.Payments)
                .FirstOrDefaultAsync(m => m.MemberId == id);
        }

        public async Task<Member> AddMemberAsync(Member member)
        {
            _context.Members.Add(member);
            await _context.SaveChangesAsync();
            return member;
        }

        public async Task UpdateMemberAsync(Member member)
        {
            _context.Members.Update(member);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteMemberAsync(int id)
        {
            var member = await _context.Members.FindAsync(id);
            if (member != null)
            {
                _context.Members.Remove(member);
                await _context.SaveChangesAsync();
            }
        }

        // --- Trainers ---
        public async Task<IEnumerable<Trainer>> GetAllTrainersAsync()
        {
            return await _context.Trainers.ToListAsync();
        }

        public async Task<Trainer?> GetTrainerByIdAsync(int id)
        {
            return await _context.Trainers.FindAsync(id);
        }

        public async Task<Trainer> AddTrainerAsync(Trainer trainer)
        {
            _context.Trainers.Add(trainer);
            await _context.SaveChangesAsync();
            return trainer;
        }

        public async Task AssignTrainerToMemberAsync(int memberId, int trainerId)
        {
            var member = await _context.Members.FindAsync(memberId);
            if (member != null)
            {
                member.TrainerId = trainerId;
                await _context.SaveChangesAsync();
            }
        }

        // --- Plans ---
        public async Task<IEnumerable<Plan>> GetAllPlansAsync()
        {
            return await _context.Plans.ToListAsync();
        }

        public async Task<Plan?> GetPlanByIdAsync(int id)
        {
            return await _context.Plans.FindAsync(id);
        }

        public async Task<Plan> AddPlanAsync(Plan plan)
        {
            _context.Plans.Add(plan);
            await _context.SaveChangesAsync();
            return plan;
        }

        // --- Payments ---
        public async Task<IEnumerable<Payment>> GetAllPaymentsAsync()
        {
            return await _context.Payments
                .Include(p => p.Member)
                .ToListAsync();
        }

        public async Task<IEnumerable<Payment>> GetPaymentsByMemberIdAsync(int memberId)
        {
            return await _context.Payments
                .Where(p => p.MemberId == memberId)
                .ToListAsync();
        }

        public async Task<Payment> RecordPaymentAsync(Payment payment)
        {
            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();
            return payment;
        }
    }
}
