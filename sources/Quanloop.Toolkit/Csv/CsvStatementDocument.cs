using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;

namespace DustInTheWind.Quanloop.Toolkit.Csv;

internal class CsvStatementDocument
{
	private readonly CsvReader csvReader;

	public CsvStatementDocument(TextReader textReader)
	{
		if (textReader == null) throw new ArgumentNullException(nameof(textReader));

		CsvConfiguration csvConfiguration = new(CultureInfo.InvariantCulture)
		{
			HasHeaderRecord = true,
			IgnoreBlankLines = true,
			TrimOptions = TrimOptions.Trim,
			PrepareHeaderForMatch = args => args.Header.Trim()
		};

		csvReader = new CsvReader(textReader, csvConfiguration);
		csvReader.Context.RegisterClassMap(new TransactionRecordMap());
	}

	public IAsyncEnumerable<TransactionRecord> ReadTransactions(CancellationToken cancellationToken = default)
	{
		return csvReader.GetRecordsAsync<TransactionRecord>(cancellationToken);
	}
}