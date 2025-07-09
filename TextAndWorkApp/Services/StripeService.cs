using Microsoft.Extensions.Options;
using Stripe;

namespace TextAndWorkApp.Services;

public class StripeService(IOptions<StripeSettings> stripeSettings)
{
    private readonly StripeSettings _stripeSettings = stripeSettings.Value;

    public async Task<string> CreateCustomerAsync(string email, string name)
    {
        var customerOptions = new CustomerCreateOptions
        {
            Email = email,
            Name = name
        };
        
        var customerService = new CustomerService();
        var customer = await customerService.CreateAsync(customerOptions);
        
        return customer.Id;
    }
    
    public async Task<string> CreateSetupIntentAsync(string customerId)
    {
        var options = new SetupIntentCreateOptions
        {
            Customer = customerId,
        };
            
        var service = new SetupIntentService();
        var intent = await service.CreateAsync(options);
            
        return intent.ClientSecret;
    }
    
    public async Task<List<PaymentMethod>> GetPaymentMethodsAsync(string customerId)
    {
        var options = new PaymentMethodListOptions
        {
            Customer = customerId,
            Type = "card",
        };
            
        var service = new PaymentMethodService();
        var paymentMethods = await service.ListAsync(options);
            
        return paymentMethods.Data;
    }
    
    public async Task<PaymentMethod> GetPaymentMethodAsync(string setupIntentId)
    {
        var service = new SetupIntentService();
        var setupIntent = await service.GetAsync(setupIntentId);
        
        var paymentMethodService = new PaymentMethodService();
        var paymentMethod = await paymentMethodService.GetAsync(setupIntent.PaymentMethodId);

        return paymentMethod;
    }
    
    public async Task<PaymentMethod> AttachPaymentMethodToCustomerAsync(string paymentMethodId, string customerId)
    {
        var options = new PaymentMethodAttachOptions
        {
            Customer = customerId,
        };
            
        var service = new PaymentMethodService();
        return await service.AttachAsync(paymentMethodId, options);
    }

    public async Task<PaymentMethod> DetachPaymentMethodAsync(string paymentMethodId)
    {
        var service = new PaymentMethodService();
        return await service.DetachAsync(paymentMethodId);
    }


}