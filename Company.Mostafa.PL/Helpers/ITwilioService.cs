using Company.Mostafa.DAL.Models;
using Twilio.Rest.Api.V2010.Account;


namespace Company.Mostafa.PL.Helpers
{
    public interface ITwilioService
    {
        public MessageResource SendSms(Sms sms);

    }
}
