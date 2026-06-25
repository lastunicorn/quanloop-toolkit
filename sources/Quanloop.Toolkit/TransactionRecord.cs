namespace DustInTheWind.Quanloop.Toolkit;

/// <summary>
/// Represents a transaction.
/// </summary>
public record class TransactionRecord
{
	public DateOnly Date { get; set; }

	public string Counterpart { get; set; }

	public string Account { get; set; }

	public string BicSwift { get; set; }
	
	public string Description { get; set; }

	public decimal Amount { get; set; }
	
	public decimal Balance { get; set; }
}