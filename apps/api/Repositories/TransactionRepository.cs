using Api.Data;
using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Repositories;

public interface ITransactionRepository
{
    Task<List<Transaction>> GetAllAsync();
    Task<Transaction?> GetByIdAsync(Guid id);
    Task<Transaction> CreateAsync(Transaction transaction);
    Task<Transaction?> UpdateAsync(Transaction transaction);
    Task<bool> DeleteAsync(Guid id);
}

public class TransactionRepository : ITransactionRepository
{
    private readonly AppDbContext _context;

    public TransactionRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Transaction>> GetAllAsync() =>
        await _context.Transactions.OrderByDescending(t => t.OccurredAt).ToListAsync();

    public async Task<Transaction?> GetByIdAsync(Guid id) =>
        await _context.Transactions.FindAsync(id);

    public async Task<Transaction> CreateAsync(Transaction transaction)
    {
        _context.Transactions.Add(transaction);
        await _context.SaveChangesAsync();
        return transaction;
    }

    public async Task<Transaction?> UpdateAsync(Transaction transaction)
    {
        var existing = await _context.Transactions.FindAsync(transaction.Id);
        if (existing is null) return null;

        existing.OccurredAt = transaction.OccurredAt;
        existing.Type = transaction.Type;
        existing.Category = transaction.Category;
        existing.Amount = transaction.Amount;
        existing.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return existing;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var transaction = await _context.Transactions.FindAsync(id);
        if (transaction is null) return false;

        _context.Transactions.Remove(transaction);
        await _context.SaveChangesAsync();
        return true;
    }
}