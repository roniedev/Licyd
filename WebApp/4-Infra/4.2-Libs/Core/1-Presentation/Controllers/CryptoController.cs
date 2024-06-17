using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Core;

[ApiController]
[EnableCors("CorsPolicy")]
[Route("api/[controller]")]
public class CryptoController : ControllerBase
{
    [HttpGet("Encrypt")]
    public IActionResult Encrypt(string str) 
    {
        return Ok(CryptoHelper.Encrypt(str));
    }

    [HttpGet("Decrypt")]
    public IActionResult Decrypt(string str)
    {
        return Ok(CryptoHelper.Decrypt(str));
    }
}
