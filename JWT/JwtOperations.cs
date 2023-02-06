using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JWT
{
    public class JwtOperations
    {
        UserManager<IdentityUser> _UserManager;
        public JwtOperations(UserManager<IdentityUser> _UserManager)
        {
            this._UserManager = _UserManager;
        }
        public string GenToken(IdentityUser model, Models.JWT jwt)
        {
            var role=string.Empty;
            var jwtSecurityToken = createJWTToken(model,jwt,out role);

            string con = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken) + ";/" + role;
            
            return (con);
        }

        private JwtSecurityToken createJWTToken(IdentityUser user , Models.JWT jwt,out string rolee)
        {
            var SymmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key));
            var SigningCredentials = new SigningCredentials(SymmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            
            var roles = _UserManager.GetRolesAsync(user).Result;
            if (roles.Contains("Teacher"))
            {
                rolee = "Teacher";
            }
            else
            {
                rolee = "User";
            }
            var roleclaim = new List<Claim>();
            foreach(var role in roles)
            {
                roleclaim.Add(new Claim("roles", role));
            }

            var jwtsecuritytoken = new JwtSecurityToken
            (
                issuer: jwt.Issuer,
                audience: jwt.Audience,
                claims:roleclaim,
                expires: DateTime.Now.AddDays(jwt.DurationInDays),
                signingCredentials: SigningCredentials
            );

            return jwtsecuritytoken;
        }
    }
}
