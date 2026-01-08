using Largest.Application.DTO_s;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Application.Exports
{
    public class PdfGenerator
    {
        public byte[] GenerateTransactionsPdf(List<TransactionDto> transactions)
        {
            // TODO IMPROVE PDF GENERATION WITH FORIGTN DETAILS, STYLES, ETC.
            return QuestPDF.Fluent.Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);
                    page.DefaultTextStyle(x => x.FontSize(12));

                    page.Header().Text("Transactions Export").SemiBold().FontSize(20);

                    page.Content().Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.ConstantColumn(50);
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                        });

                        table.Header(header =>
                        {
                            header.Cell().Text("ID");
                            header.Cell().Text("Description");
                            header.Cell().Text("Amount");
                            header.Cell().Text("Date");
                        });

                        foreach (var t in transactions)
                        {
                            table.Cell().Text(t.Id.ToString());
                            table.Cell().Text(t.Description);
                            table.Cell().Text(t.Amount.ToString("0.00"));
                            table.Cell().Text(t.Date.ToShortDateString());
                        }
                    });
                });
            }).GeneratePdf();
        }
    }
}
