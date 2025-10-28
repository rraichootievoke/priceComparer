namespace PriceCompare.Core.Contracts
{
 public class DealerModel
 {
 public int Id { get; set; }
 public string? OracleDealerId { get; set; }
 public string? CompanyName { get; set; }
 public override string ToString() => CompanyName ?? OracleDealerId ?? Id.ToString();
 }
}
