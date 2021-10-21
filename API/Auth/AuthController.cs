using API.Security.DTO;
using API.Security.Service;
using Auth.API.Auth.DTO;
using AutoMapper;
using BLL;
using DAL.Model;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Auth
{
    public class AuthController : ControllerBase
    {
        private readonly IUserAuthorizationService _authService;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public AuthController(ILogger<AuthController> logger, IUserAuthorizationService authService)
        {
            _authService = authService;
            _logger = logger;

            MapperConfiguration configMapper = new MapperConfiguration(m =>
            {
                m.CreateMap<TokenResponse, UserTokenDTO>();
                m.CreateMap<User, UserInfoTokenDTO>();
            });

            _mapper = configMapper.CreateMapper();
        }

        [ProducesResponseType(typeof(UserInfoTokenDTO), 200)]
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Get([FromBody] UserLoginDTO userLoginDto)
        {
            try
            {
                TokenResponse userToken = await _authService.LoginAsync(userLoginDto.UserName, userLoginDto.Password);

                User user = await _authService.GetUserAsync(userLoginDto.UserName);

                UserInfoTokenDTO userInfoDto = _mapper.Map<User, UserInfoTokenDTO>(user);

                userInfoDto.TokenResponse = _mapper.Map<TokenResponse, UserTokenDTO>(userToken);

                return new OkObjectResult(userInfoDto);
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(e);
            }

        }

        [ProducesResponseType(200)]
        [HttpGet]
        [Route("check")]
        [Authorize]
        public ActionResult CheckAuthRole()
        {
            return Ok();
        }

       
        [ProducesResponseType(200)]
        [HttpGet]
        [Route("check-with-role")]
        [AuthorizedByRole("Admin")]
        public ActionResult CheckAuthOnly()
        {
            return Ok();
        }


    }
}
