using System.Globalization;
using DustInTheWind.ConsoleTools.Controls;
using DustInTheWind.ConsoleTools.Controls.Tables;

namespace DustInTheWind.Quanloop.Toolkit.Demo;

internal static class Program
{
	public static async Task Main(string[] args)
	{
		const string fileName = "statement.csv";

		try
		{
			StatementDocument document = await StatementDocument.LoadFromFileAsync(fileName);
			Display(document);
		}
		catch (DocumentLoadException ex)
		{
			await Console.Error.WriteLineAsync($"Failed to read '{fileName}': {ex}");
			Environment.ExitCode = 1;
		}
		catch (Exception ex)
		{
			await Console.Error.WriteLineAsync($"Unexpected error: {ex}");
			Environment.ExitCode = 1;
		}
	}

	private static void Display(StatementDocument document)
	{
		DataGrid dataGrid = new()
		{
			Title = "Transactions",
			BorderTemplate = BorderTemplate.PlusMinusBorderTemplate,
			Footer = new[]
			{
				$"Count: {document.Count}",
				$"Starting Balance: {document.StartingBalance}",
				$"Ending Balance: {document.EndingBalance}"
			}
		};

		dataGrid.Columns.Add("Date");
		dataGrid.Columns.Add("Sender/Receiver");
		dataGrid.Columns.Add("Account");
		dataGrid.Columns.Add("BIC/SWIFT");
		dataGrid.Columns.Add("Description");
		dataGrid.Columns.Add("Amount", HorizontalAlignment.Right);
		dataGrid.Columns.Add("Balance", HorizontalAlignment.Right);

		foreach (TransactionRecord transaction in document)
		{
			dataGrid.Rows.Add(
				transaction.Date.ToString("yyyy-MM-dd"),
				transaction.Counterpart,
				transaction.Account,
				transaction.BicSwift,
				transaction.Description,
				transaction.Amount.ToString(CultureInfo.CurrentCulture),
				transaction.Balance.ToString(CultureInfo.CurrentCulture));
		}

		dataGrid.Display();
	}
}