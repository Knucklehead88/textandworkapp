window.googlePayInterop = {
    renderButton: function (elementId, paymentRequest, dotNetHelper) {
        if (!window.google || !window.google.payments) {
            console.error('Google Pay JS library not loaded.');
            return;
        }

        const client = new google.payments.api.PaymentsClient({ environment: 'TEST' });
        const button = client.createButton({
            onClick: function () {
                client.loadPaymentData(paymentRequest)
                    .then(function (paymentData) {
                        dotNetHelper.invokeMethodAsync('OnGooglePaySuccess', paymentData);
                    })
                    .catch(function (err) {
                        dotNetHelper.invokeMethodAsync('OnGooglePayFailure', err.toString());
                    });
            }
        });
        const el = document.getElementById(elementId);
        if (el) {
            el.innerHTML = '';
            el.appendChild(button);
        }
    }
};
