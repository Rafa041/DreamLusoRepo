using DreamLuso.Domain.Common;
using DreamLuso.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace DreamLuso.Domain.Model;


public class Invoice : AuditableEntity, IEntity<Guid>
{
    public Guid Id { get; set; }
    public Guid ContractId { get; set; }
    public Contract Contract { get; set; }
    public string InvoiceNumber { get; set; }
    public decimal Amount { get; set; }
    public DateTime IssueDate { get; set; }
    public DateTime DueDate { get; set; }
    public InvoiceStatus Status { get; set; }
    public Guid? TransactionId { get; set; }
    public FinancialTransactions Transaction { get; set; }

    protected Invoice()
    {
        Id = Guid.NewGuid();
    }

    public Invoice(
        Guid contractId,
        string invoiceNumber,
        decimal amount,
        DateTime issueDate,
        DateTime dueDate,
        InvoiceStatus status)
    {
        Id = Guid.NewGuid();
        ContractId = contractId;
        InvoiceNumber = invoiceNumber;
        Amount = amount;
        IssueDate = issueDate;
        DueDate = dueDate;
        Status = status;
    }

    public void SetTransaction(Guid transactionId)
    {
        TransactionId = transactionId;
        Status = InvoiceStatus.Paid;
    }
}

public enum InvoiceStatus
{
    Pending,
    Paid,
    Overdue,
    Cancelled
}
