using DreamLuso.Domain.Interface;
using DreamLuso.Domain.Common;
using System.Transactions;
namespace DreamLuso.Domain.Model;

public class FinancialTransactions : AuditableEntity, IEntity<Guid>
{
    public Guid Id { get; set; }
    public Guid ContractId { get; set; }
    public Contract Contract { get; set; }
    public Guid ClientId { get; set; }
    public Client Client { get; set; }
    public Guid InvoiceId { get; set; }
    public Invoice Invoice { get; set; }
    public double Value { get; set; }
    public DateTime Date { get; set; }
    public string Description { get; set; }
    public double ReferenceId { get; set; }
    public string PaymentMethod { get; set; }
    public TransactionStatus TransactionStatus { get; set; }

    // Propriedades sugeridas
    // - Histórico de todas as transações associadas ao usuário
    public string TransactionHistory { get; set; }

    public bool IsBuyer { get; set; }
    public bool IsTenant { get; set; }
    public FinancialTransactions() { }

    public FinancialTransactions(
        Guid id,
        Guid contractId,         
        Contract contract,       
        Guid clientId,
        Guid invoiceId,         
        Invoice invoice,        
        double value,
        DateTime date,
        string description,
        double referenceId,
        string paymentMethod,
        TransactionStatus transactionStatus,
        string transactionHistory,
        bool isBuyer,
        bool isTenant
    )
    {
        Id = id;
        ContractId = contractId; 
        Contract = contract;      
        ClientId = clientId;
        InvoiceId = invoiceId;   
        Invoice = invoice;         
        Value = value;
        Date = date;
        Description = description;
        ReferenceId = referenceId;
        PaymentMethod = paymentMethod;
        TransactionStatus = transactionStatus;
        TransactionHistory = transactionHistory;
        IsBuyer = isBuyer;
        IsTenant = isTenant;
    }
}
public enum TransactionStatus
{
    Pending,
    Completed,
    Failed,
    Refunded
}