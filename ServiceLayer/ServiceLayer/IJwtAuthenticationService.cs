using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.ServiceLayer
{
    public interface IJwtAuthenticationService
    {
       
        string GenerateToken(string secret, List<Claim> claims);


        bool IsTokenValid(string key, string issuer, string token);
        string BuildMessage(string stringToSplit, int chunkSize);

        public string Decrypt(string cipherText);

        public string Encrypt(string clearText);
        
    }
}
