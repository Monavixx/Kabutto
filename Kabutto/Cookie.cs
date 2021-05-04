using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kabutto
{
    public class Cookie
    {
        public string Name, Value;
        public string Domain = null, Path = null;
        public bool HttpOnly=false, Secure=false;
        public DateTime? Expires = null;
        public long? MaxAge = null;
        public SameSiteType SameSite = SameSiteType.Lax;

        public enum SameSiteType
        {
            Strict,
            Lax,
            None
        };

        public override string ToString()
        {
            string result = "Set-Cookie: " + Name + "=" + Value;
            
            if (Domain != null) result += "; Domain=" + Domain;
            if (Path != null) result += "; Path=" + Path;
            if (HttpOnly) result += "; HttpOnly";
            if (Secure) result += "; Secure";
            if (Expires != null) result += "; Expires=" + Expires.Value.ToString("r", CultureInfo.CreateSpecificCulture("en-US"));
            if (MaxAge != null) result += "; Max-Age=" + MaxAge.ToString();
            result += "; SameSite=" + SameSite.ToString();

            return result;
        }

    }
}
