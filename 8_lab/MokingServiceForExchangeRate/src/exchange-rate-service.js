const mbHelper = require('./mountebank-helper');
const settings = require('./settings');

function addService() {
    const response = { 
        USD: "68.4487",
        EUR: "72.6226",
        JPY: "51.6594",
        GPB: "82.3438",
        KZT: "14.8167"
    }

    const stubs = [
        {
            predicates: [{
                equals: {
                    method: "GET",
                    "path": "/exchange-rates"
                }
            }],
            responses: [
                {
                    is: {
                        statusCode: 200,
                        headers: {
                            "Content-Type": "application/json"
                        },
                        body: JSON.stringify(response)
                    }
                }
            ] 
        }
    ];

    const imposter = {
        port: settings.exchange_rate_service_port,
        protocol: 'http',
        stubs: stubs
    };
    return mbHelper.postImposter(imposter);
}

module.exports = { addService };