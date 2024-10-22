using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using backend.Models;
using backend.context;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace backend.Controllers
{
    public class LoginModel
    {
        [Required]
        public string usuario_name { get; set; }

        [Required]
        public string contrasena { get; set; }
    }

    [ApiController]
    [Route("api/[controller]")]
    public class usersController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly string _jwtSecret = "ASuperSecureSecretKey1234567890!";

        public usersController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] usuario usuario)
        {
            var existingUser = await _context.usuario
                .FirstOrDefaultAsync(u => u.correo == usuario.correo);
            if (existingUser != null)
            {
                return Conflict(new { message = "El correo ya está registrado." });
            }

            usuario.contrasena = BCrypt.Net.BCrypt.HashPassword(usuario.contrasena);

            await _context.usuario.AddAsync(usuario);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUserById), new { id_usuario = usuario.id_usuario }, usuario);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel login)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var foundUser = await _context.usuario.FirstOrDefaultAsync(u => u.usuario_name == login.usuario_name);
            if (foundUser == null || !BCrypt.Net.BCrypt.Verify(login.contrasena, foundUser.contrasena))
            {
                return Unauthorized();
            }

            var token = GenerateJwtToken(foundUser);

            return Ok(new { token, userId = foundUser.id_usuario });
        }


        [HttpGet("{id_usuario}")]
        public async Task<IActionResult> GetUserById(int id_usuario)
        {
            var usuario = await _context.usuario.FindAsync(id_usuario);
            if (usuario == null) return NotFound();

            return Ok(usuario);
        }

        [HttpPut("{id_usuario}")]
        public async Task<IActionResult> UpdateUser(int id_usuario, [FromBody] UpdateUsuarioDto updatedUsuario)
        {
            var usuario = await _context.usuario.FindAsync(id_usuario);
            if (usuario == null) return NotFound();

            usuario.cedula = updatedUsuario.cedula ?? usuario.cedula; 
            usuario.nombre = updatedUsuario.nombre ?? usuario.nombre; 
            usuario.apellido = updatedUsuario.apellido ?? usuario.apellido; 
            usuario.estatura = updatedUsuario.estatura ?? usuario.estatura; 
            usuario.estado_civil = updatedUsuario.estado_civil ?? usuario.estado_civil; 
            usuario.fecha_nacimiento = updatedUsuario.fecha_nacimiento ?? usuario.fecha_nacimiento; 

            _context.Entry(usuario).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        public class UpdateUsuarioDto
        {
            public long? cedula { get; set; }
            public string? nombre { get; set; }
            public string? apellido { get; set; }
            public decimal? estatura { get; set; }
            public string? estado_civil { get; set; }
            public DateTime? fecha_nacimiento { get; set; }
        }




        [HttpDelete("{id_usuario}")]
        public async Task<IActionResult> DeleteUser(int id_usuario)
        {
            var usuario = await _context.usuario.FindAsync(id_usuario);
            if (usuario == null) return NotFound();

            _context.usuario.Remove(usuario);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private string GenerateJwtToken(usuario usuario)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, usuario.usuario_name), 
                new Claim(JwtRegisteredClaimNames.Jti, usuario.id_usuario.ToString()),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSecret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: null,
                audience: null,
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
