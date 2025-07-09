
    window.initializeStripe = async function(publicKey) {
        console.log("Initializing Stripe with public key:", publicKey);
        window.stripe = Stripe(publicKey);
        window.elements = stripe.elements();

        var cardElement= elements.create('card', {
            style: {
                base: {
                    color: '#32325d',
                    fontFamily: '"Helvetica Neue", Helvetica, sans-serif',
                    fontSmoothing: 'antialiased',
                    fontSize: '16px',
                    '::placeholder': {
                        color: '#aab7c4'
                    }
                },
                invalid: {
                    color: '#fa755a',
                    iconColor: '#fa755a'
                }
            }
        });
        cardElement.mount('#card-element');

        cardElement.on('change', function(event) {
            var displayError = document.getElementById('card-errors');
            if (event.error) {
                displayError.textContent = event.error.message;
            } else {
                displayError.textContent = '';
            }
        });
    };

    window.confirmCardSetup = async function(clientSecret) {
        console.log("Confirming card setup...");
        const result = await stripe.confirmCardSetup(clientSecret, {
            payment_method: {
                card: window.elements.getElement('card')
            }
        });

        if (result.error) {
            var errorElement = document.getElementById('card-errors');
            errorElement.textContent = result.error.message;
            return { isSuccess: false };
        } else {
            // Card setup successful
            console.log(result.setupIntent);
            return result.setupIntent.id;
        }
    };
    
    window.resetStripeElement = function() {
        cardElement.clear();
        document.getElementById('card-errors').textContent = '';
    }


