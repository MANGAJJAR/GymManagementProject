using GymManagement.API.DTOs;
using GymManagement.Core.Entities;
using GymManagement.Core.Interfaces;

namespace GymManagement.API.Services
{
    public class GymService : IGymService
    {
        private readonly IGymRepository _repository;

        public GymService(IGymRepository repository)
        {
            _repository = repository;
        }

        // --- Members ---
        public async Task<IEnumerable<MemberDto>> GetAllMembersAsync()
        {
            var members = await _repository.GetAllMembersAsync();
            return members.Select(m => new MemberDto
            {
                MemberId = m.MemberId,
                Name = m.Name,
                Age = m.Age,
                Phone = m.Phone,
                JoinDate = m.JoinDate,
                TrainerName = m.Trainer?.Name,
                PlanName = m.Plan?.PlanName
            });
        }

        public async Task<MemberDto?> GetMemberByIdAsync(int id)
        {
            var m = await _repository.GetMemberByIdAsync(id);
            if (m == null) return null;

            return new MemberDto
            {
                MemberId = m.MemberId,
                Name = m.Name,
                Age = m.Age,
                Phone = m.Phone,
                JoinDate = m.JoinDate,
                TrainerName = m.Trainer?.Name,
                PlanName = m.Plan?.PlanName
            };
        }

        public async Task<MemberDto> AddMemberAsync(CreateMemberDto dto)
        {
            var member = new Member
            {
                Name = dto.Name,
                Age = dto.Age,
                Phone = dto.Phone,
                TrainerId = dto.TrainerId,
                PlanId = dto.PlanId,
                JoinDate = DateTime.UtcNow
            };

            var added = await _repository.AddMemberAsync(member);
            return new MemberDto { MemberId = added.MemberId, Name = added.Name };
        }

        public async Task UpdateMemberAsync(UpdateMemberDto dto)
        {
            var member = await _repository.GetMemberByIdAsync(dto.MemberId);
            if (member != null)
            {
                member.Name = dto.Name;
                member.Age = dto.Age;
                member.Phone = dto.Phone;
                member.TrainerId = dto.TrainerId;
                member.PlanId = dto.PlanId;
                await _repository.UpdateMemberAsync(member);
            }
        }

        public async Task DeleteMemberAsync(int id)
        {
            await _repository.DeleteMemberAsync(id);
        }

        // --- Trainers ---
        public async Task<IEnumerable<TrainerDto>> GetAllTrainersAsync()
        {
            var trainers = await _repository.GetAllTrainersAsync();
            return trainers.Select(t => new TrainerDto
            {
                TrainerId = t.TrainerId,
                Name = t.Name,
                Specialization = t.Specialization
            });
        }

        public async Task<TrainerDto> AddTrainerAsync(CreateTrainerDto dto)
        {
            var trainer = new Trainer { Name = dto.Name, Specialization = dto.Specialization };
            var added = await _repository.AddTrainerAsync(trainer);
            return new TrainerDto { TrainerId = added.TrainerId, Name = added.Name, Specialization = added.Specialization };
        }

        // --- Plans ---
        public async Task<IEnumerable<PlanDto>> GetAllPlansAsync()
        {
            var plans = await _repository.GetAllPlansAsync();
            return plans.Select(p => new PlanDto
            {
                PlanId = p.PlanId,
                PlanName = p.PlanName,
                DurationMonths = p.DurationMonths,
                Price = p.Price
            });
        }

        public async Task<PlanDto> AddPlanAsync(CreatePlanDto dto)
        {
            var plan = new Plan { PlanName = dto.PlanName, DurationMonths = dto.DurationMonths, Price = dto.Price };
            var added = await _repository.AddPlanAsync(plan);
            return new PlanDto { PlanId = added.PlanId, PlanName = added.PlanName, DurationMonths = added.DurationMonths, Price = added.Price };
        }

        // --- Payments ---
        public async Task<IEnumerable<PaymentDto>> GetAllPaymentsAsync()
        {
            var payments = await _repository.GetAllPaymentsAsync();
            return payments.Select(p => new PaymentDto
            {
                PaymentId = p.PaymentId,
                MemberId = p.MemberId,
                MemberName = p.Member?.Name,
                Amount = p.Amount,
                PaymentDate = p.PaymentDate,
                Status = p.Status
            });
        }

        public async Task<PaymentDto> RecordPaymentAsync(CreatePaymentDto dto)
        {
            var payment = new Payment
            {
                MemberId = dto.MemberId,
                Amount = dto.Amount,
                Status = dto.Status,
                PaymentDate = DateTime.UtcNow
            };
            var added = await _repository.RecordPaymentAsync(payment);
            return new PaymentDto { PaymentId = added.PaymentId, Amount = added.Amount, Status = added.Status };
        }
    }
}
