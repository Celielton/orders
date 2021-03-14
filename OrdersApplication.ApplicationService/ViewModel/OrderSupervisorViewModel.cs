namespace OrdersApplication.ApplicationService.ViewModel
{
    public class OrderSupervisorViewModel
    {
        public int OrderId { get; set; }
        public void IncrementOrderId()
        {
            OrderId += 1;
        }
    }
}
