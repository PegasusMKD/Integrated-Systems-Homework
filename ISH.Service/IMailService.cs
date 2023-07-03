using ISH.Service.Dtos;
using Org.BouncyCastle.Asn1.Pkcs;

namespace ISH.Service
{
    public interface IMailService
    {
        bool SendMail(MailDataDto mailData);
}
}
