using Company.Mostafa.PL.Settings;
using Microsoft.Extensions.Options;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

using Company.Mostafa.DAL.Models;

namespace Company.Mostafa.PL.Helpers
{
    public class TwilioServicee(IOptions<TwilioSetting> _options) : ITwilioService
    {
        public MessageResource SendSms(Sms sms)
        {
            // Initialize connections
            TwilioClient.Init(_options.Value.AccountSID, _options.Value.AuthToken);

            // bulid message
            var message = MessageResource.Create(
                body : sms.Body,
                to : sms.To,
                from : _options.Value.PhoneNumber
             );

            // return message
            return message;

        }

        public MessageResource SendSms(Twilio.TwiML.Voice.Sms sms)
        {
            throw new NotImplementedException();
        }
    }
}
