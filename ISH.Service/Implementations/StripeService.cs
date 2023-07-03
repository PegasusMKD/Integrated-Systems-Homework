using ISH.Data.Authentication;
using ISH.Service.Dtos.Stripe;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Stripe;

namespace ISH.Service.Implementations
{
    public class StripeService : IStripeService
    {
        private readonly ChargeService _chargeService;
        private readonly CustomerService _customerService;
        private readonly TokenService _tokenService;

        public StripeService(
            ChargeService chargeService,
            CustomerService customerService,
            TokenService tokenService)
        {
            _chargeService = chargeService;
            _customerService = customerService;
            _tokenService = tokenService;
        }

        public async Task<StripeCustomerDto> AddStripeCustomerDtoAsync(string Email, string Name, AddStripeCard card, CancellationToken ct)
        {
            TokenCreateOptions tokenOptions = new()
            {
                Card = new TokenCardOptions
                {
                    Name = Name,
                    Number = card.CardNumber.ToString(),
                    ExpYear = card.ExpirationYear.ToString(),
                    ExpMonth = card.ExpirationMonth.ToString(),
                    Cvc = card.Cvc
                }
            };

            Token stripeToken = await _tokenService.CreateAsync(tokenOptions, null, ct);

            CustomerCreateOptions customerOptions = new()
            {
                Name = Name,
                Email = Email,
                Source = stripeToken.Id
            };

            Customer createdCustomer = await _customerService.CreateAsync(customerOptions, null, ct);

            return new StripeCustomerDto(createdCustomer.Name, createdCustomer.Email, createdCustomer.Id);
        }


        public bool AddStripePaymentAsync(User? user, int orderPrice, AddStripeCard card, CancellationToken ct)
        {
            if (user == null)
                return false;

            var getStripeCustomerDto = AddStripeCustomerDtoAsync(user.Email, user.UserName, card, ct).Result;

            ChargeCreateOptions paymentOptions = new()
            {
                Customer = getStripeCustomerDto.CustomerId,
                ReceiptEmail = getStripeCustomerDto.Email,
                Description = "Ordering a ticket",
                Currency = "USD",
                Amount = Convert.ToInt32(orderPrice * 100) // Convert dollars to cents
            };

            var createdPayment = _chargeService.CreateAsync(paymentOptions, null, ct).Result;
            
            return createdPayment != null;
        }

    }
}
