using ISH.Data.Authentication;
using ISH.Service.Dtos.Stripe;

namespace ISH.Service
{
    public interface IStripeService
    {
        bool AddStripePaymentAsync(User? user, int orderPrice, AddStripeCard card, CancellationToken ct);
        bool AddStripePaymentAsync(User? user, int orderPrice, string stripeEmail, string stripeToken, CancellationToken ct);
    }
}
