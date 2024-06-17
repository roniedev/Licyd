using Microsoft.AspNetCore.Http;

namespace Core;

/// <summary>
/// 
/// </summary>
public class AuthUser : IAuthUser
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="httpContextAccessor"></param>
    public AuthUser(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    /// <summary>
    /// 
    /// </summary>
    public string UserName
    {
        get
        {
            try
            {
                return _httpContextAccessor.HttpContext.User.Claims.ToList()[1].Value;
            }
            catch (Exception)
            {
                return "Sistema";
            }
        }
    }

    public string Matricula
    {
        get
        {
            try
            {
                return _httpContextAccessor.HttpContext!.User.Claims?.First(clain => clain.Type == "Matricula").Value;
            }
            catch (Exception)
            {
                return "Sistema";
            }
        }
    }
}
