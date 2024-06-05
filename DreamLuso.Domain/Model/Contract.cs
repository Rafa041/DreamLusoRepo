﻿using DreamLuso.Domain.Common;
using DreamLuso.Domain.Interface;
namespace DreamLuso.Domain.Model;

public class Contract : AuditableEntity, IEntity<Guid>
{
    public Guid Id { get; set; }
    public Guid PropertyId { get; set; }
    public Property Property { get; set; }
    public Guid UserId { get; set; } // Valid is not RealStateAgent
    public User User { get; set; }
    public Guid RealStateAgentId { get; set; }
    public RealStateAgent RealStateAgent { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public double Value { get; set; }

    // Propriedades sugeridas
    // - Termos e condições do contrato
    public string TermsAndConditions { get; set; }
    // - Taxas adicionais associadas ao contrato
    public double AdditionalFees { get; set; }
    // - Frequência de pagamento (mensal, anual, etc.)
    public string PaymentFrequency { get; set; }
    // - Opção de renovação do contrato
    public bool RenewalOption { get; set; }
    // - Cláusulas de rescisão do contrato
    public string TerminationClauses { get; set; }

    private Contract()
    {
        Id = Guid.NewGuid();
    }
    public Contract(
        Guid id,
        Guid propertyId,
        Guid userId,
        DateTime startDate,
        DateTime endDate,
        double value,
        string termsAndConditions,
        double additionalFees,
        string paymentFrequency,
        bool renewalOption,
        string terminationClauses)
    {
        Id = id;
        PropertyId = propertyId;
        UserId = userId;
        StartDate = startDate;
        EndDate = endDate;
        Value = value;
        TermsAndConditions = termsAndConditions;
        AdditionalFees = additionalFees;
        PaymentFrequency = paymentFrequency;
        RenewalOption = renewalOption;
        TerminationClauses = terminationClauses;

    }
}

