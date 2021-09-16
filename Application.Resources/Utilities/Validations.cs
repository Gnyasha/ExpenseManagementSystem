using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace Application.Resources.Utilities
{
    /// <summary>
    /// Checks for various validations
    /// </summary>
    public class Validations
    {

        /// <summary>
        /// Returns true if input is a valid email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public bool IsValidEmail(string email)
        {
            try
            {
                var mail = new MailAddress(email);
                return mail.Address == email;
            }
            catch
            {
                return false;
            }
        }

    }
}
