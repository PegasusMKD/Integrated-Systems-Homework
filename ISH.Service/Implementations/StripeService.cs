using DocumentFormat.OpenXml.Wordprocessing;
using ISH.Data.Authentication;
using ISH.Service.Dtos.Stripe;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Stripe;
using Stripe.Issuing;

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

        public async Task<StripeCustomerDto> AddStripeCustomerDtoAsync(string Email, string Name,string stripeToken, CancellationToken ct)
        {
            CustomerCreateOptions customerOptions = new()
            {
                Name = Name,
                Email = Email,
                Source = stripeToken
            };

            Customer createdCustomer = await _customerService.CreateAsync(customerOptions, null, ct);

            return new StripeCustomerDto(createdCustomer.Name, createdCustomer.Email, createdCustomer.Id);
        }




        public bool AddStripePaymentAsync(User? user, int orderPrice, AddStripeCard card, CancellationToken ct)
        {
            if (user == null)
                return false;

            TokenCreateOptions tokenOptions = new()
            {
                Card = new TokenCardOptions
                {
                    Name = user.UserName,
                    Number = card.CardNumber.ToString(),
                    ExpYear = card.ExpirationYear.ToString(),
                    ExpMonth = card.ExpirationMonth.ToString(),
                    Cvc = card.Cvc
                }
            };

            Token stripeToken = _tokenService.Create(tokenOptions, null);

            var getStripeCustomerDto = AddStripeCustomerDtoAsync(user.Email, card.Name, stripeToken.Id, ct).Result;

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

        public bool AddStripePaymentAsync(User? user, int orderPrice, string stripeEmail, string stripeToken, CancellationToken ct)
        {
            if (user == null)
                return false;

            var getStripeCustomerDto = AddStripeCustomerDtoAsync(stripeEmail, user.UserName, stripeToken, ct).Result;

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
