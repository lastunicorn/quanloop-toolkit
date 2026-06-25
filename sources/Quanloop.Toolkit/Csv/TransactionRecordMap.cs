using CsvHelper.Configuration;

namespace DustInTheWind.Quanloop.Toolkit.Csv;

internal sealed class TransactionRecordMap : ClassMap<TransactionRecord>
{
	public TransactionRecordMap()
	{
		Map(x => x.Date)
			.Name("Date");

		Map(x => x.Counterpart)
			.Name("Sender/Receiver");

		Map(x => x.Account)
			.Name("Account");

		Map(x => x.BicSwift)
			.Name("BIC/SWIFT");

		Map(x => x.Description)
			.Name("Description");

		Map(x => x.Amount)
			.Name("Amount")
			.Default(0m);

		Map(x => x.Balance)
			.Name("Balance");
	}
}