using DreamLuso.Domain.Interface;
using DreamLuso.Domain.Common;
namespace DreamLuso.Domain.Model;

public class FinancialTransactions : AuditableEntity, IEntity<Guid>
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }
    public Guid PropertyId { get; set; }
    public Property Property { get; set; }
    public Guid RealStateAgentId { get; set; }
    public RealStateAgent RealStateAgent { get; set; }
    public double Value { get; set; }
    public DateTime Date { get; set; }
    public string Description { get; set; }
    public double ReferenceId { get; set; }
    public string PaymentMethod { get; set; }
    public string TransactionStatus { get; set; }

    // Propriedades sugeridas
    // - Histórico de todas as transações associadas ao usuário
    public string TransactionHistory { get; set; }

    private FinancialTransactions()
    {
        Id = Guid.NewGuid();
    }
    public FinancialTransactions(
        Guid id,
        Guid userId,
        double value,
        DateTime date,
        string description,
        double referenceId,
        string paymentMethod,
        string transactionStatus,
        string transactionHistory)
    {
        Id = id;
        UserId = userId;
        Value = value;
        Date = date;
        Description = description;
        ReferenceId = referenceId;
        PaymentMethod = paymentMethod;
        TransactionStatus = transactionStatus;
        TransactionHistory = transactionHistory;

    }
}
