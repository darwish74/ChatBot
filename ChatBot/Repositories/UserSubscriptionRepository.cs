using ChatBot.Data;
using ChatBot.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatBot.Repositories
{
    public class UserSubscriptionRepository
    {
        private readonly ApplicationDbContext _context;

        public UserSubscriptionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<UserSubscription>> GetUserSubscriptionsAsync(int userId)
        {
            return await _context.UserSubscriptions
                .Include(us => us.Plan)
                .Where(us => us.UserId == userId)
                .ToListAsync();
        }

        public async Task<UserSubscription> GetActiveSubscriptionAsync(int userId)
        {
            return await _context.UserSubscriptions
                .Include(us => us.Plan)
                .Where(us => us.UserId == userId && us.Status == "active")
                .OrderByDescending(us => us.StartDate)
                .FirstOrDefaultAsync();
        }

        public async Task<UserSubscription> SubscribeUserAsync(UserSubscription subscription)
        {
            _context.UserSubscriptions.Add(subscription);
            await _context.SaveChangesAsync();
            return subscription;
        }

        public async Task<bool> CancelSubscriptionAsync(int subscriptionId)
        {
            var subscription = await _context.UserSubscriptions.FindAsync(subscriptionId);
            if (subscription == null) return false;

            subscription.Status = "canceled";
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
