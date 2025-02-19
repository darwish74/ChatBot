using ChatBot.Data;
using ChatBot.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatBot.Repositories
{
    public class MessageRepository
    {
        private readonly ApplicationDbContext _context;

        public MessageRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Message>> GetAllMessagesAsync()
        {
            return await _context.Messages
                .Where(m => !m.IsDeleted)
                .OrderBy(m => m.CreatedAt)
                .ToListAsync();
        }

        public async Task<Message> AddMessageAsync(Message message)
        {
            _context.Messages.Add(message);
            await _context.SaveChangesAsync();
            return message;
        }

        public async Task<Message> UpdateMessageAsync(int messageId, string newContent)
        {
            var message = await _context.Messages.FindAsync(messageId);
            if (message == null || message.IsDeleted) return null;

            message.Content = newContent;
            message.EditedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return message;
        }

        public async Task<bool> DeleteMessageAsync(int messageId)
        {
            var message = await _context.Messages.FindAsync(messageId);
            if (message == null) return false;

            message.IsDeleted = true;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
