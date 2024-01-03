using DecryptionAPI.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using DecryptionAPI.Repository.Encryption;
using DecryptionAPI.Repository.Decryption;
using DecryptionAPI.Contracts;

namespace DecryptionAPI.Controllers
{
    [AllowAnonymous]
    [Route("api/dec")]
    [EnableCors("MyPolicy")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly InputDataDto _dto;
        private readonly DecryptionRepo _dec;
        private readonly ILoggerService _logger;
        public HomeController(InputDataDto dto, DecryptionRepo dec, ILoggerService logger)
        {
            _dto = dto;
            _dec = dec;
            _logger = logger;
        }

        [HttpPost("hextobase", Name = "hextobase")]
        public async Task<IActionResult> hextobase([FromBody] InputDataDto _dto)
        {
            string data = _dto.indata;

            //hex to base64 conversion
            var base64data =  await _dec.FromHexToBase64(data);

           

            if (base64data == null)
            {
                 _logger.LogError($"Decryption failed!!");
                return NotFound();
            }
            else
            {
                _logger.LogError($"Decryption Success!!");
                return Ok(JsonConvert.SerializeObject(base64data));
            }

        }

    }
}

