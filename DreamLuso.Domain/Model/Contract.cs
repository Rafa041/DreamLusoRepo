using DreamLuso.Domain.Common;
using DreamLuso.Domain.Interface;
using System.Transactions;
namespace DreamLuso.Domain.Model;

public class Contract : AuditableEntity, IEntity<Guid>
{
    public Guid Id { get; set; }
    public Guid PropertyId { get; set; }
    public Property Property { get; set; }
    public Guid ClientId { get; set; } // Valid is not RealStateAgent
    public Client Client { get; set; }
    public Guid RealStateAgentId { get; set; }
    public RealStateAgent RealStateAgent { get; set; }
    public ContractType ContractType { get; set; }
    public ContractStatus Status { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public double Value { get; set; }
    // Propriedades sugeridas
    // - Termos e condições do contrato
    public string TermsAndConditions { get; set; }
    // - Taxas adicionais associadas ao contrato
    public double AdditionalFees { get; set; }// tem que ser removido e mandado para FinancialTransaction 
    // - Frequência de pagamento (mensal, anual, etc.)
    public string PaymentFrequency { get; set; }
    // - Opção de renovação do contrato
    public bool RenewalOption { get; set; }
    // - Cláusulas de rescisão do contrato
    public string TerminationClauses { get; set; }

    public DateTime SignatureDate { get; set; }
    public decimal? SecurityDeposit { get; set; }
    public string? InsuranceDetails { get; set; }
    public string? Notes { get; set; }
    public string DocumentPath { get; set; }
    public ICollection<Invoice> Invoices { get; set; }
    public ICollection<FinancialTransactions> Transactions { get; set; }

    public Contract() { }

    public Contract(
        Guid id,
        Guid propertyId,
        Property property,
        Guid clientId,
        Client client,
        Guid realStateAgentId,
        RealStateAgent realStateAgent,
        DateTime startDate,
        DateTime endDate,
        double value,
        string termsAndConditions,
        string paymentFrequency,
        bool renewalOption,
        string terminationClauses,
        ContractStatus status,
        DateTime signatureDate,
        decimal? securityDeposit,
        string? insuranceDetails,
        string? notes,
        string documentPath,
        ICollection<FinancialTransactions> transactions
    )
    {
        Id = id;
        PropertyId = propertyId;
        Property = property;
        ClientId = clientId;
        Client = client;
        RealStateAgentId = realStateAgentId;
        RealStateAgent = realStateAgent;
        StartDate = startDate;
        EndDate = endDate;
        Value = value;
        TermsAndConditions = termsAndConditions;
        PaymentFrequency = paymentFrequency;
        RenewalOption = renewalOption;
        TerminationClauses = terminationClauses;
        Status = status;
        SignatureDate = signatureDate;
        SecurityDeposit = securityDeposit;
        InsuranceDetails = insuranceDetails;
        Notes = notes;
        DocumentPath = documentPath;
        Transactions = transactions;
    }
}


public enum ContractType
{
    Sale,
    Rent,
}

public enum ContractStatus
{
    Draft,
    Pending,
    Active,
    Completed,
    Terminated,
    Expired
}
