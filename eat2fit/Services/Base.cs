using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;

namespace eat2fit.Services
{
    public static class Base
    {
		public static string ToTitleCase(this string s) =>
			CultureInfo.InvariantCulture.TextInfo.ToTitleCase(s.ToLower());
	}
}
