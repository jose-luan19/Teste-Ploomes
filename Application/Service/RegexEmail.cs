using System.Text.RegularExpressions;

namespace Application.Service
{
    public static class RegexEmail
    {
        public static void validEmail(string email)
        {
            Regex rg = new Regex(@"^(?("")("".+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$");

            if (!rg.IsMatch(email))
            {
                throw new ArgumentException("Email ["+email+"] invalido");
            }
        }
    }
}
