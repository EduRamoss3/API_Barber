namespace Barber.CartAPI.Model
{
    public class CartHeader : BaseEntity
    {
        public string UserId { get ; set; } 
        public string CouponCode { get; set; }
    }
}
