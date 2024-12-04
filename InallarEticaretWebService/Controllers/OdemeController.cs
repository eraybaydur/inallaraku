using InallarEticaretWebService.Dtos;
using InallarEticaretWebService.Dtos.TP_WMD_UCD;
using InallarEticaretWebService.Helpers;
using InallarEticaretWebService.Models.Odeme;
using InallarEticaretWebService.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using TurkPosWSTEST;

namespace InallarEticaretWebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OdemeController : ControllerBase
    {

        private readonly ITurkPosWSTESTAsyncRepository TurkPosWSTESTAsyncRepository;

        public OdemeController(ITurkPosWSTESTAsyncRepository turkPosWSTESTAsyncRepository)
        {
            this.TurkPosWSTESTAsyncRepository = TurkPosWSTESTAsyncRepository;
        }

        [HttpPost]
        public async Task<IActionResult> PosOdemeAsync([FromForm] PosOdemeViewModel model) // Satış - 3D Pay / NS sayfasından gönderilen formu işler.
        {
            ST_TP_Islem_Odeme result = await TurkPosWSTESTAsyncRepository.PosOdemeAsync(model.G, model.GUID, model.KK_Sahibi, model.KK_No, model.KK_SK_Ay, model.KK_SK_Yil, model.KK_CVC, model.KK_Sahibi_GSM, model.Hata_URL, model.Basarili_URL, model.Siparis_ID, model.Siparis_Aciklama, model.Taksit, model.Islem_Tutar, model.Toplam_Tutar, model.Islem_Hash, model.Islem_Guvenlik_Tip, model.Islem_ID, model.IPAdr, model.Ref_URL, model.Data1, model.Data2, model.Data3, model.Data4, model.Data5, model.Data6, model.Data7, model.Data8, model.Data9, model.Data10);
            Regex urlRegex = new Regex("^https?:\\/\\/(?:www\\.)?[-a-zA-Z0-9@:%._\\+~#=]{1,256}\\.[a-zA-Z0-9()]{1,6}\\b(?:[-a-zA-Z0-9()@:%_\\+.~#?&\\/=]*)$");
            if (model.Islem_Guvenlik_Tip == "NS" || !urlRegex.IsMatch(result.UCD_URL)) return BadRequest() ;
            else
            {
                string jsonG = System.Text.Json.JsonSerializer.Serialize(new PosOdemeCookieModel() { ST_TP_Islem_Odeme = result });
                Response.Cookies.Append("PosOdemeCookie", jsonG, new CookieOptions() { Expires = DateTime.Now.AddMinutes(3) });
                return Redirect(result.UCD_URL);
            }
        }

        [HttpPost("odeme")]
        public async Task<IActionResult> TPWMDUCDAsync([FromBody] TP_WMD_UCDRequestDTO dto)
        {
            try
            {
                dto.G = new()
                {
                    CLIENT_CODE = "10738",
                    CLIENT_PASSWORD = "test",
                    CLIENT_USERNAME = "test",
                };

                // Set IP Address
                dto.IPAdr = "127.0.0.1";


                // Calculate Hash
                var shaDto = new SHA2B64RequestDTO()
                {
                    ClientCode = "10738",
                    GUID = "0c13d406-873b-403b-9c09-a5766840d98c",
                    Taksit = dto.Taksit.ToString(),
                    IslemTutar = dto.Islem_Tutar,
                    SiparisId = dto.Siparis_ID,
                    ToplamTutar = dto.Toplam_Tutar
                };

                dto.Islem_Hash = await SHA2B64Async(shaDto);

                // Construct SOAP envelope
                var soapEnvelope = $@"<?xml version=""1.0"" encoding=""utf-8""?>
                        <soap:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" 
                                       xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" 
                                       xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">
                            <soap:Body>
                                <TP_WMD_UCD xmlns=""https://turkpos.com.tr/"">
                                    <G>
                                        <CLIENT_CODE>{dto.G.CLIENT_CODE}</CLIENT_CODE>
                                        <CLIENT_USERNAME>{dto.G.CLIENT_USERNAME}</CLIENT_USERNAME>
                                        <CLIENT_PASSWORD>{dto.G.CLIENT_PASSWORD}</CLIENT_PASSWORD>
                                    </G>
                                    <GUID>0c13d406-873b-403b-9c09-a5766840d98c</GUID>
                                    <KK_Sahibi>{dto.KK_Sahibi}</KK_Sahibi>
                                    <KK_No>{dto.KK_No}</KK_No>
                                    <KK_SK_Ay>{dto.KK_SK_Ay}</KK_SK_Ay>
                                    <KK_SK_Yil>{dto.KK_SK_Yil}</KK_SK_Yil>
                                    <KK_CVC>{dto.KK_CVC}</KK_CVC>
                                    <KK_Sahibi_GSM>{dto.KK_Sahibi_GSM}</KK_Sahibi_GSM>
                                    <Hata_URL>{dto.Hata_URL}</Hata_URL>
                                    <Basarili_URL>{dto.Basarili_URL}</Basarili_URL>
                                    <Siparis_ID>{dto.Siparis_ID}</Siparis_ID>
                                    <Siparis_Aciklama>{dto.Siparis_Aciklama}</Siparis_Aciklama>
                                    <Taksit>{dto.Taksit}</Taksit>
                                    <Islem_Tutar>{dto.Islem_Tutar}</Islem_Tutar>
                                    <Toplam_Tutar>{dto.Toplam_Tutar}</Toplam_Tutar>
                                    <Islem_Hash>{dto.Islem_Hash}</Islem_Hash>
                                    <Islem_Guvenlik_Tip>{dto.Islem_Guvenlik_Tip}</Islem_Guvenlik_Tip>
                                    <Islem_ID>{dto.Islem_ID}</Islem_ID>
                                    <IPAdr>{dto.IPAdr}</IPAdr>
                                    <Ref_URL>{dto.Ref_URL}</Ref_URL>
                                    <Data1>{dto.Data1}</Data1>
                                    <Data2>{dto.Data2}</Data2>
                                    <Data3>{dto.Data3}</Data3>
                                    <Data4>{dto.Data4}</Data4>
                                    <Data5>{dto.Data5}</Data5>
                                </TP_WMD_UCD>
                            </soap:Body>
                        </soap:Envelope>";

                using (var client = new HttpClient())
                {
                    var content = new StringContent(soapEnvelope, Encoding.UTF8, "text/xml");
                    client.DefaultRequestHeaders.Add("SOAPAction", "https://turkpos.com.tr/TP_WMD_UCD");

                    var response = await client.PostAsync("https://test-dmz.param.com.tr/turkpos.ws/service_turkpos_test.asmx", content);

                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = await response.Content.ReadAsStringAsync();
                        return Ok(responseContent);
                    }
                    else
                    {
                        return StatusCode((int)response.StatusCode,
                            $"Error: {response.StatusCode} - {response.ReasonPhrase}");
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("[action]")]
        public IActionResult ModelOnayla([FromForm] TP_WMD_PayPostDTO sonuc3DSecure)
        {
            return RedirectHelper.PostRedirect("/TPWMDPay", sonuc3DSecure);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> TPWMDPayAsync([FromBody] TP_WMD_PayRequestDTO dto)
        {
            using TurkPosWSTESTSoapClient client = new(TurkPosWSTESTSoapClient.EndpointConfiguration.TurkPos_x0020_WS_x0020_TESTSoap12);
            return Ok(await client.TP_WMD_PayAsync(dto.G, dto.GUID, dto.UCD_MD, dto.Islem_GUID, dto.Siparis_ID));
        }

        private async Task<string> SHA2B64Async(SHA2B64RequestDTO dto)
        {
            string hashString = string.Concat(dto.GetType()
            .GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Select(prop => prop.GetValue(dto)?.ToString() ?? string.Empty));

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            SHA1 hasher = SHA1.Create();
            string hashedString = Convert.ToBase64String(hasher.ComputeHash(Encoding.GetEncoding("ISO-8859-9").GetBytes(hashString)));
            return hashedString;
        }
    }
}
