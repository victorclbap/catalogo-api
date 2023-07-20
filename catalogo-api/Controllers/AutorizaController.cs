using catalogo_api.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace catalogo_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutorizaController : ControllerBase
    {
        public readonly UserManager<IdentityUser> _userManager;
        public readonly SignInManager<IdentityUser> _signInManager;
        public readonly IConfiguration _configuration;

        public AutorizaController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;  // fornece api para grenciar o usuário
            _signInManager = signInManager; // fornece api para o login do usuário
            _configuration = configuration; // necessário para ler as informações do arquivo appsettings.json
        }


        // verifica se a api está atendendo

        [HttpGet]

        public ActionResult<string> Get()
        {
            return "AutorizaController :: Acessado em : " +
                DateTime.Now.ToLongDateString();
        }

        // cria um novo usuário

        [HttpPost("register")]
        public async Task<ActionResult> RegisterUser([FromBody] UsuarioDTO model)
        {
            // cria uma instância do usuário do identity

            var user = new IdentityUser
            {
                UserName = model.Email,
                Email = model.Email,
                EmailConfirmed = true,
            };

            var result = await _userManager.CreateAsync(user, model.Password); // cria o usuário
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            await _signInManager.SignInAsync(user, false); // se for feito com sucesso, loga o usuário
            return Ok(GeraToken(model));
        }


        //verifica as credenciais de um usuário existente

        [HttpPost("login")]

        public async Task<ActionResult> Login([FromBody] UsuarioDTO userInfo)
        {

            var result = await _signInManager.PasswordSignInAsync(userInfo.Email, userInfo.Password,
                isPersistent: false, lockoutOnFailure: false); // realiza o login

            //lockoutOnFailure = false significa que se tentar mais de 3 vezes logar não bloqueia

            if (result.Succeeded)
            {
                return Ok(GeraToken(userInfo));
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Login Inválido ...");
                return BadRequest(ModelState);
            }
        }

        // System.IdentityModel.Tokens,Jwt -> pacote para adicionar token
        private UsuarioToken GeraToken(UsuarioDTO userInfo)
        {
            //define declarações do usuário -> não é obrigatório mas aumenta a segurança
            var claims = new[]
            {
                 new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.UniqueName, userInfo.Email),
                 new Claim("meuPet", "pipoca"),
                 new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
             };

            //gera uma chave com base em um algoritmo simetrico 
            // pega a chave do appsettings e gera uma chave privada
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:key"]));

            //gera a assinatura digital do token usando o algoritmo Hmac e a chave privada
            var credenciais = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            //Tempo de expiracão do token. -> appsettings
            var expiracao = _configuration["TokenConfiguration:ExpireHours"];
            var expiration = DateTime.UtcNow.AddHours(double.Parse(expiracao));

            // classe que representa um token JWT e gera o token
            JwtSecurityToken token = new JwtSecurityToken(
              issuer: _configuration["TokenConfiguration:Issuer"],
              audience: _configuration["TokenConfiguration:Audience"],
              claims: claims,
              expires: expiration,
              signingCredentials: credenciais);

            //retorna os dados com o token e informacoes
            return new UsuarioToken()
            {
                Authenticated = true,
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration,
                Message = "Token JWT OK"
            };

        }
    };
}