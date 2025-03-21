namespace Application.DTO.Response.Orders
{
    public class GetProductOrderdByMonthsResponseDTO
    {
        public string MonthName { get; set; }
        public decimal TotalAmount { get; set; }
        public string FormattedTotalAmount => TotalAmount.ToString("#,##0.00");
    }
}
