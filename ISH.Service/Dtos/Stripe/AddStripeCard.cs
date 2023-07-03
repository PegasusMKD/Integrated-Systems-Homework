namespace ISH.Service.Dtos.Stripe
{
    public record AddStripeCard(
        string Name,
        long CardNumber,
        int ExpirationYear,
        int ExpirationMonth,
        string Cvc);
}
