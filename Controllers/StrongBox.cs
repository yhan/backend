using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace backend.Controllers
{
    public static class StrongBox
    {
        public static SecurityKey PassPhrase = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("This is the pass phrase"));
    }
}