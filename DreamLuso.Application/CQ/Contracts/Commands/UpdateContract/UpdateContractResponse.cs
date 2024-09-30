﻿namespace DreamLuso.Application.CQ.Contracts.Commands.UpdateContract;

public class UpdateContractResponse
{
    public Guid ContractId { get; set; }
    public Guid PropertyId { get; set; }
    public Guid ClientId { get; set; }
    public Guid RealStateAgentId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public double Value { get; set; }
    public double AdditionalFees { get; set; }
    public string PaymentFrequency { get; set; }
    public bool RenewalOption { get; set; }
    public string TermsAndConditions { get; set; }
    public string TerminationClauses { get; set; }
}
