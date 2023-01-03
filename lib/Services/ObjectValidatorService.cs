using DuncansbyAPI.lib.Utils;
using System.Net;
using System.Reflection;

namespace WebshopAPI.lib.Services
{
    public class ObjectValidatorService<TObject> where TObject : class
    {
        TObject myObject;

        public ObjectValidatorService(TObject myObject)
        {
            this.myObject = myObject;
        }

        public void IsValid()
        {
            PropertyInfo[] props = myObject.GetType().GetProperties();
            foreach (PropertyInfo prop in props)
            {
                if (Attribute.IsDefined(prop, typeof(RequiredAttribute)))
                {
                    if (prop.GetValue(myObject) == null || string.IsNullOrEmpty(prop.GetValue(myObject).ToString()))
                    {
                        throw new MandatoryPropertyEmptyException($"{prop.Name}");
                    }
                }
                else if (Attribute.IsDefined(prop, typeof(EmailAddressAttribute)))
                {
                    string value = prop.GetValue(myObject).ToString();

                    if (!RegexUtilities.IsValidEmail(value)) {
                        throw new EmailNotValidException();
                    }
                }
            }
        }

        private bool _IsValidDomain(string email)
        {
            string domain = email.Split("@")[1];
            try
            {
                IPHostEntry entry = Dns.GetHostEntry(domain);
                if (entry != null)
                {
                    return true;
                }
            }
            catch
            { }
            return false;
        }
    }
}
