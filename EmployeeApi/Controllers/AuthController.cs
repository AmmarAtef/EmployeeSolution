using Microsoft.IdentityModel.Tokens;
using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using System.Web.Http.Cors;

namespace EmployeeApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class AuthController : ApiController
    {

        string webUrl = System.Configuration.ConfigurationManager.AppSettings["SiteUrl"];
        string listTitle = System.Configuration.ConfigurationManager.AppSettings["listName"];
        [HttpGet]
        [Authorize]
        [Route("ok")]
        public IHttpActionResult Authenticated() => Ok("Authenticated");

        [HttpGet]
        [Route("notok")]
        public IHttpActionResult NotAuthenticated() => Unauthorized();


        [HttpGet]
        [AllowAnonymous]
        [Route("api/Auth/Authenticate")]
        public IHttpActionResult Authenticate(string userLoginName, string password)
        {
            try
            {
                using (var ctx = new ClientContext(webUrl))
                {
                    // Get Permission For the User
                    ClientResult<BasePermissions> permissions = ctx.Web.Lists.GetByTitle(listTitle).GetUserEffectivePermissions(userLoginName);
                    ctx.ExecuteQuery();
                  //  ctx.Credentials = new NetworkCredential(userLoginName, password);
                  //  ctx.ExecuteQuery();
                    // check if the permission has view items
                    Boolean hasPermission = permissions.Value.Has(PermissionKind.AddListItems);
                    string role = string.Empty;
                    if (hasPermission)
                        role = "FullControl";
                    else
                        role = "Read";
                    // if credentials are valid

                    var token = CreateToken(userLoginName, role);
                    //return the token
                    return Ok(token);
                }
            }
            catch (Exception ex)
            {
                return ResponseMessage(new HttpResponseMessage(HttpStatusCode.Unauthorized));
            }
        }


        private string CreateToken(string userLoginName, string role)
        {

            DateTime issuedAt = DateTime.UtcNow;

            DateTime expires = DateTime.UtcNow.AddDays(7);

            //https://stackoverflow.com/questions/42974472/jwt-is-issuing-the-same-token
            var tokenHandler = new JwtSecurityTokenHandler();


            var claimsIdentity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, userLoginName),
                new Claim(ClaimTypes.Role, role)
            });

            const string secrectKey = "EmployeeSharePointPortal";
            var securityKey = new SymmetricSecurityKey(System.Text.Encoding.Default.GetBytes(secrectKey));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);


            //Create the jwt (JSON Web Token)
            //Replace the issuer and audience with your URL 
            var token =
                (JwtSecurityToken)
                tokenHandler.CreateJwtSecurityToken(
                    issuer: webUrl,
                    audience: webUrl,
                    subject: claimsIdentity,
                    notBefore: issuedAt,
                    expires: expires,
                    signingCredentials: signingCredentials);

            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;
        }
    }

}
