using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sahika.Helper.Services
{
    public interface IEmailService
    {
       void SendMail(string userName, string email, string subjectText, string callBackUrl);
    }
}
