namespace Api.DTOs;

public record CreateTransactionDto(
    DateTime OccurredAt,
    string Type,
    string Category,
    decimal Amount
);

public record UpdateTransactionDto(
    DateTime OccurredAt,
    string Type,
    string Category,
    decimal Amount
);

public record TransactionResponseDto(
    Guid Id,
    DateTime OccurredAt,
    string Type,
    string Category,
    decimal Amount,
    DateTime CreatedAt,
    DateTime UpdatedAt
);