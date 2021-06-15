using komatsu.api.Model.Request;

namespace komatsu.api.Interface
{
    public interface IEmailServiceRepo
    {
        bool SendEmail(EmailData emailData);
    }
}
