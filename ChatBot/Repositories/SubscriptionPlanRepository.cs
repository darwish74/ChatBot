using ChatBot.Data;
using ChatBot.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChatBot.Repositories
{
    public class SubscriptionPlanRepository
    {
        private readonly ApplicationDbContext _context;

        public SubscriptionPlanRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<SubscriptionPlan>> GetAllPlansAsync()
        {
            return await _context.SubscriptionPlans.ToListAsync();
        }

        public async Task<SubscriptionPlan> GetPlanByIdAsync(int planId)
        {
            return await _context.SubscriptionPlans.FindAsync(planId);
        }

        public async Task<SubscriptionPlan> AddPlanAsync(SubscriptionPlan plan)
        {
            _context.SubscriptionPlans.Add(plan);
            await _context.SaveChangesAsync();
            return plan;
        }

        public async Task<bool> DeletePlanAsync(int planId)
        {
            var plan = await _context.SubscriptionPlans.FindAsync(planId);
            if (plan == null) return false;

            _context.SubscriptionPlans.Remove(plan);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
