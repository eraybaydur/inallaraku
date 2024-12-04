using InallarEticaretWebService.Dtos.User;
using InallarEticaretWebService.Helpers;
using InallarEticaretWebService.Models;
using InallarEticaretWebService.Models.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NotthomePortalApi;
using NotthomePortalApi.Models;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace InallarEticaretWebService.Controllers
{

    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IEmailSender _emailSender;
        public UsersController(IConfiguration configuration, IEmailSender emailSender)
        {
            _configuration = configuration;
            _emailSender = emailSender;
        }

        public class ReturnObject
        {
            public bool Durum { get; set; }
            public string? Mesaj { get; set; }
            public string? LoginTuru { get; set; }
            public User? User { get; set; }
        }
        private string GenerateJwtToken(UserDto user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.NameIdentifier, user.Email)
        }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        [HttpPost("register")]
        [Consumes("application/json")]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new Result<UserDto>
                {
                    IsSuccess = false,
                    Message = "Geçersiz kullanıcı verisi.",
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                });
            }

            try
            {
                using var connection = new SqlConnection(Settings.ConnectionString);
                await connection.OpenAsync();

                string checkEmailQuery = "SELECT COUNT(*) FROM PY_ETICARET_USERS WHERE EMAIL = @EMAIL";
                using var checkCmd = new SqlCommand(checkEmailQuery, connection);
                checkCmd.Parameters.AddWithValue("@EMAIL", user.Email);
                int existingUserCount = (int)await checkCmd.ExecuteScalarAsync();

                if (existingUserCount > 0)
                {
                    return Conflict(new Result<UserDto>
                    {
                        IsSuccess = false,
                        Message = "Bu e-posta adresi zaten kayıtlı.",
                        StatusCode = System.Net.HttpStatusCode.Conflict
                    });
                }

                // Mevcut kayıt işlemi
                string registerQuery = @"INSERT INTO PY_ETICARET_USERS (EMAIL, PHONENUMBER, PASSWORD, REGISTIRATIONDATE)
                                 VALUES (@EMAIL, @PHONENUMBER, @PASSWORD, @REGISTIRATIONDATE)";

                using var cmd = new SqlCommand(registerQuery, connection);
                cmd.Parameters.AddWithValue("@EMAIL", user.Email);
                cmd.Parameters.AddWithValue("@PHONENUMBER", user.PhoneNumber);
                cmd.Parameters.AddWithValue("@PASSWORD", user.Password);
                cmd.Parameters.AddWithValue("@REGISTIRATIONDATE", DateTime.Now);

                int affectedRows = await cmd.ExecuteNonQueryAsync();

                if (affectedRows > 0)
                {
                    var result = new Result<UserDto>
                    {
                        Data = new UserDto { Email = user.Email, PhoneNumber = user.PhoneNumber, RegistirationDate = DateTime.Now },
                        IsSuccess = true,
                        Message = "Kayıt başarıyla oluşturuldu.",
                        StatusCode = System.Net.HttpStatusCode.OK
                    };
                    return Ok(result);
                }
                else
                {
                    throw new Exception("Kayıt eklenemedi.");
                }
            }
            catch (SqlException sqlEx)
            {
                return StatusCode(500, new Result<UserDto>
                {
                    IsSuccess = false,
                    Message = "Veritabanı işlemi sırasında bir hata oluştu.",
                    StatusCode = System.Net.HttpStatusCode.InternalServerError
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Result<UserDto>
                {
                    IsSuccess = false,
                    Message = "Beklenmeyen bir hata oluştu. Lütfen daha sonra tekrar deneyin.",
                    StatusCode = System.Net.HttpStatusCode.InternalServerError
                });
            }
        }

        [HttpPost("login")]
        [Consumes("application/json")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new Result<LoginResponseDto>
                {
                    IsSuccess = false,
                    Message = "Geçersiz giriş bilgileri.",
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                });
            }

            try
            {
                string loginQuery = @"SELECT * FROM PY_ETICARET_USERS 
                              WHERE EMAIL = @EMAIL";

                using var connection = new SqlConnection(Settings.ConnectionString);
                await connection.OpenAsync();

                using var cmd = new SqlCommand(loginQuery, connection);
                cmd.Parameters.AddWithValue("@EMAIL", loginModel.Email);

                using var reader = await cmd.ExecuteReaderAsync();

                if (!await reader.ReadAsync())
                {
                    return Ok(new Result<LoginResponseDto>
                    {
                        IsSuccess = false,
                        Message = "Bu e-posta adresiyle kayıtlı kullanıcı bulunamadı.",
                        StatusCode = System.Net.HttpStatusCode.NotFound
                    });
                }

                var storedHash = reader["PASSWORD"].ToString();
                if (BCrypt.Net.BCrypt.Verify(loginModel.Password, storedHash))
                {
                    var user = new UserDto
                    {
                        LogicalRef = Convert.ToInt32(reader["LOGICALREF"]),
                        Email = reader["EMAIL"].ToString(),
                        PhoneNumber = reader["PHONENUMBER"].ToString(),
                        RegistirationDate = Convert.ToDateTime(reader["REGISTIRATIONDATE"])
                    };

                    var token = GenerateJwtToken(user);

                    var result = new Result<LoginResponseDto>
                    {
                        Data = new LoginResponseDto { User = user, Token = token },
                        IsSuccess = true,
                        Message = "Giriş başarılı.",
                        StatusCode = System.Net.HttpStatusCode.OK
                    };
                    return Ok(result);
                }


                return Ok(new Result<LoginResponseDto>
                {
                    IsSuccess = false,
                    Message = "Geçersiz e-posta veya şifre.",
                    StatusCode = System.Net.HttpStatusCode.Unauthorized
                });
            }
            catch (SqlException sqlEx)
            {
                return Ok(new Result<LoginResponseDto>
                {
                    IsSuccess = false,
                    Message = "Veritabanı işlemi sırasında bir hata oluştu.",
                    StatusCode = System.Net.HttpStatusCode.InternalServerError
                });
            }
            catch (Exception ex)
            {
                return Ok( new Result<LoginResponseDto>
                {
                    IsSuccess = false,
                    Message = "Beklenmeyen bir hata oluştu. Lütfen daha sonra tekrar deneyin.",
                    StatusCode = System.Net.HttpStatusCode.InternalServerError
                });
            }
        }


        [HttpPost("SifremiUnuttum")]
        public async Task<ActionResult<ReturnObject>> SifremiUnuttum(string email)
        {
            try
            {

                string connectionString = Settings.ConnectionString;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT * FROM PY_WEB_USERS WHERE EMAIL = @Email";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Email", email.ToUpper());

                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {

                            if (reader.Read())
                            {

                                string resetLink = GenerateResetLink(email);
                                await _emailSender.SendEmailAsync(email, "Şifre sıfırlama isteği", resetLink);

                                return new ReturnObject()
                                {
                                    Durum = true,
                                    User = new User()
                                    {
                                        LogicalRef = Convert.ToInt32(reader["LOGICALREF"]),
                                        ResetToken = Convert.IsDBNull(reader["RESETTOKEN"]) ? "" : (string)reader["RESETTOKEN"]
                                    },
                                    Mesaj = "Şifre sıfırlama e-postası başarıyla gönderilmiştir."
                                };
                            }
                            else
                            {
                                // Eğer sorguya ait veri yoksa, hata döndür
                                return new ReturnObject()
                                {
                                    Durum = false,
                                    Mesaj = "Böyle bir kullanıcı bulunamadı."
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Hata oluştu: {ex.Message}");
            }
        }

        [HttpPut("SifreGuncelle")]
        public IActionResult SifreGuncelle(string token, string newPassword)
        {
            try
            {
                string connectionString = Settings.ConnectionString;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT * FROM PY_ETICARET_USERS WHERE RESETTOKEN = @RESETTOKEN";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@RESETTOKEN", token);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                // Kullanıcı bulundu, şifreyi güncelle
                                reader.Read();
                                int userId = Convert.ToInt32(reader["LOGICALREF"]);
                                UpdatePassword(userId, newPassword);

                                return Ok("Şifre başarıyla güncellendi.");
                            }
                            else
                            {
                                // Kullanıcı bulunamazsa, hata döndür
                                return NotFound("Belirtilen kullanıcı bulunamadı.");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Hata oluştu: {ex.Message}");
            }
        }

        private void UpdatePassword(int userId, string newPassword)
        {

            string connectionString = Settings.ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string updateQuery = "UPDATE PY_ETICARET_USERS SET SIFRE = @NewPassword WHERE LOGICALREF = @UserId";
                using (SqlCommand updateCommand = new SqlCommand(updateQuery, connection))
                {
                    updateCommand.Parameters.AddWithValue("@NewPassword", newPassword);
                    updateCommand.Parameters.AddWithValue("@UserId", userId);

                    updateCommand.ExecuteNonQuery();
                }
            }
        }

        private string GenerateResetLink(string userEmail)
        {
            string resetToken = Guid.NewGuid().ToString();

            string connectionString = Settings.ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string updateQuery = $@"UPDATE PY_WEB_USERS SET RESETTOKEN = '{resetToken}' WHERE EMAIL = @EMAIL";
                using (SqlCommand updateCommand = new SqlCommand(updateQuery, connection))
                {
                    updateCommand.Parameters.AddWithValue("@EMAIL", userEmail);
                    updateCommand.ExecuteNonQuery();
                }
            }

            string resetLink = $"bayiportali.nott.com.tr/sifre-sifirla?token={resetToken}";

            return resetLink;
        }


    }
}
