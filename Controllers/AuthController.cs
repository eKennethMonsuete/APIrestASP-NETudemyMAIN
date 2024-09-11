using APIrestASP_NETudemy.Business;
using APIrestASP_NETudemy.Data.VO;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIrestASP_NETudemy.Controllers
{
    [ApiVersion("1")]
    [Route("api/[controller]/v{version:apiVersion}")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private ILoginBusiness _loginBusiness;

        public AuthController(ILoginBusiness loginBusiness )
        {
            _loginBusiness = loginBusiness;
        }


        [HttpPost]
        [Route("signin")]
        public IActionResult Signin([FromBody] UserVO user)
        {
            //Console.WriteLine("user AUTH");
            if (user == null) return BadRequest("User invalido null");
            var token = _loginBusiness.ValidateCredentials(user);
            if (token == null) return Unauthorized("chegou aqui algo nao autorizado");

            return Ok(token);

        }

        [HttpPost]
        [Route("refresh")]
        public IActionResult Refresh([FromBody] TokenVO tokenVo)
        {
            if (tokenVo is null) return BadRequest("Ivalid client request");
            var token = _loginBusiness.ValidateCredentials(tokenVo);
            if (token == null) return BadRequest("Ivalid client request");
            return Ok(token);
        }

        [HttpGet]
        [Route("revoke")]
        [Authorize("Bearer")]
        public IActionResult Revoke()
        {
            var username = User.Identity.Name;
            var result = _loginBusiness.RevokeToken(username);

            if (!result) return BadRequest("Ivalid client request");
            return NoContent();
        }
    }
}
