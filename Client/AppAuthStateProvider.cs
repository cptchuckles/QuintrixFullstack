using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;

namespace QuintrixFullstack.Client
{
    public class AppAuthStateProvider : AuthenticationStateProvider
    {
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = "eyJhbGciOiJSUzUxMiIsInR5cCI6IkpXVCJ9.eyJ1c2VybmFtZSI6IkpvaG4gUHJpbmdsZXRvbiIsInJvbGUiOiJHaWdhY2hhZCIsImlhdCI6MTY1NTA3ODgwOH0.pzTdSuyK-BCVuADpNSKySVBTf7qMShoslvjBmGauj5Axm-SEH_ofE4ItqksxynP0xVWXlMKeJvRQR9JBWCQ57tPLtikl7ObJGVAf2rcC05uyNpbuVa6sjyukguamILrHExKNu6gS5WRO1lX6IznUrzYhn3xHmQnvPtA1slVStQ1Zpm7LzHI-GEglAPr-CuPG0QCBqUHhHptYudD2EiRvQCX7IoEZFS9Yn6T_5vRgFdEZNvgzGxHjrVxpEq4zXADSzZOJql6tzw9_efUBk28A62B9pJH3pAoDD-V446sib8q9zRQ81CeJhehOtwaXklac7uQLUXh2p_BKRC8k7RLH_g";

            // var identity = new ClaimsIdentity(ParseClaimsFromJwt(token));
            var identity = new ClaimsIdentity();

            var user = new ClaimsPrincipal(identity);
            var state = new AuthenticationState(user);

            NotifyAuthenticationStateChanged(Task.FromResult(state));

            return state;
        }

        // The following methods shamelessly stolen from SteveSandersonMS as per Patrick God
        private static IEnumerable<Claim>? ParseClaimsFromJwt(string jwt)
        {
            var body = jwt.Split(".")[1];
            var bodyJson = Base64UrlDecode(body);
            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(bodyJson);
            return keyValuePairs?.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()!));
        }

        private static byte[] Base64UrlDecode(string encoded)
        {
            switch (encoded.Length % 4)
            {
                case 2: encoded += "=="; break;
                case 3: encoded += "="; break;
            }
            return Convert.FromBase64String(encoded);
        }
    }
}