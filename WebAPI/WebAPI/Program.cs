
using LogicaAccesoDatos;
using LogicaAccesoDatos.Repositorios;
using LogicaAplicacion.ImplementacionCasosUso.Envios;
using LogicaAplicacion.ImplementacionCasosUso.Usuarios;

using LogicaAplicacion.InterfacesCasosUso.Envios;
using LogicaAplicacion.InterfacesCasosUso.Usuarios;
using LogicaNegocio.InterfacesRepositorios;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

            builder.Services.AddScoped<IObtenerPorNroTracking, ObtenerPorNroTracking>();
            builder.Services.AddScoped<IRepositorioEnvios, RepositorioEnvios>();

            builder.Services.AddScoped<IRepositorioUsuarios, RepositorioUsuarios>();

            builder.Services.AddScoped<ILogin, Login>();
            builder.Services.AddScoped<IListarEnviosDeCliente, ListarEnviosDeCliente>();
            builder.Services.AddScoped<IObtenerComentarios, ObtenerComentarios>();
            builder.Services.AddScoped<IListarEnviosPorFecha, ListarEnviosPorFecha>();
            builder.Services.AddScoped<IListarEnviosPorComentario, ListarEnviosPorComentario>();
            builder.Services.AddScoped<ICambioContrasenia, CambioContrasenia>();

            // conexion a la base
            string cadenaConexion = builder.Configuration.GetConnectionString("cadenaConexion");
            builder.Services.AddDbContext<Contexto>(option => option.UseSqlServer(cadenaConexion));
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(opt => opt.IncludeXmlComments("WebAPI.xml"));

            // jwt
            var claveSecreta = "ZWRpw6fDo28gZW0gY29tcHV0YWRvcmE=";
            builder.Services.AddAuthentication(aut => {
                aut.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                aut.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(aut => {
                aut.RequireHttpsMetadata = false;
                aut.SaveToken = true;
                aut.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(claveSecreta)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
