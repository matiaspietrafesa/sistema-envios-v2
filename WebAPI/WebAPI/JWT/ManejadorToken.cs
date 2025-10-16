﻿using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Compartido.DTOs.Usuarios;

namespace WebAPI.JWT
{
    public class ManejadorToken
    {
        public static string GenerarToken(UsuarioLogueadoDTO usu)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            //clave secreta, generalmente se incluye en el archivo de configuración 
            //Debe ser un vector de bytes  
            byte[] clave = Encoding.ASCII.GetBytes("ZWRpw6fDo28gZW0gY29tcHV0YWRvcmE=");
            //Se incluye un claim para el email 
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] {
                    new Claim(ClaimTypes.Email, usu.Email),
                    new Claim(ClaimTypes.Role, usu.Rol)
                }),
                Expires = DateTime.UtcNow.AddMonths(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(clave),
                SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
