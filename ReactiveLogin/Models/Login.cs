using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltraDBSimulator.Models
{
    public class Login
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public async Task<bool> DoLogin()
        {
            var validData = new Dictionary<string, string>()
            {
                {"john", "wayne"},
                {"robert", "deniro"},
                {"meryl", "streep"},
                {"julia", "roberts"},
                {"richard", "gere"},
                {"drew", "barrymore"}
            };
            if (string.IsNullOrWhiteSpace(UserName) || string.IsNullOrWhiteSpace(Password))
                return false;
            var userName = UserName.ToLowerInvariant();
            await Task.Delay(5000);
            return validData.ContainsKey(userName) &&
                validData[userName] == Password.ToLowerInvariant();
        }
    }
}
