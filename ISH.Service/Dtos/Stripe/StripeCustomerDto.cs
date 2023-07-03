namespace ISH.Service.Dtos.Stripe
{
    public record StripeCustomerDto(
        string Name,
        string Email,
        string CustomerId);
}
