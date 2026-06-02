using Api.DTOs;
using Api.Models;
using Api.Repositories;

namespace Api.Services;

public interface ITransactionService
{
    Task<List<TransactionResponseDto>> GetAllAsync();
    Task<TransactionResponseDto?> GetByIdAsync(Guid id);
    Task<TransactionResponseDto> CreateAsync(CreateTransactionDto dto);
    Task<TransactionResponseDto?> UpdateAsync(Guid id, UpdateTransactionDto dto);
    Task<bool> DeleteAsync(Guid id);
}

public class TransactionService : ITransactionService
{
    private readonly ITransactionRepository _repository;

    public TransactionService(ITransactionRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<TransactionResponseDto>> GetAllAsync()
    {
        var transactions = await _repository.GetAllAsync();
        return transactions.Select(MapToDto).ToList();
    }

    public async Task<TransactionResponseDto?> GetByIdAsync(Guid id)
    {
        var transaction = await _repository.GetByIdAsync(id);
        return transaction is null ? null : MapToDto(transaction);
    }

    public async Task<TransactionResponseDto> CreateAsync(CreateTransactionDto dto)
    {
        if (!Enum.TryParse<TransactionType>(dto.Type, out var type))
            throw new ArgumentException($"Invalid transaction type: {dto.Type}");

        var transaction = new Transaction
        {
            OccurredAt = dto.OccurredAt,
            Type = type,
            Category = dto.Category,
            Amount = dto.Amount
        };

        var created = await _repository.CreateAsync(transaction);
        return MapToDto(created);
    }

    public async Task<TransactionResponseDto?> UpdateAsync(Guid id, UpdateTransactionDto dto)
    {
        if (!Enum.TryParse<TransactionType>(dto.Type, out var type))
            throw new ArgumentException($"Invalid transaction type: {dto.Type}");

        var transaction = new Transaction
        {
            Id = id,
            OccurredAt = dto.OccurredAt,
            Type = type,
            Category = dto.Category,
            Amount = dto.Amount
        };

        var updated = await _repository.UpdateAsync(transaction);
        return updated is null ? null : MapToDto(updated);
    }

    public async Task<bool> DeleteAsync(Guid id) =>
        await _repository.DeleteAsync(id);

    private static TransactionResponseDto MapToDto(Transaction t) => new(
        t.Id,
        t.OccurredAt,
        t.Type.ToString(),
        t.Category,
        t.Amount,
        t.CreatedAt,
        t.UpdatedAt
    );
}