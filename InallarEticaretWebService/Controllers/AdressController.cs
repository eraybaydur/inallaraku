using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NotthomePortalApi.Models;

[ApiController]
[Route("api/[controller]")]
public class AddressController : ControllerBase
{


    [HttpPost]
    [HttpPut("{id}")]
    public IActionResult AddOrUpdateAddress(int? id, [FromBody] UserAddress model)
    {
        try
        {
            using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
            {
                connection.Open();

                string query;
                if (id.HasValue)
                {
                    // Update existing address
                    query = @"UPDATE PY_ETICARET_USER_ADRESSES 
                          SET Email = @Email, Baslik = @Baslik, AdSoyad = @AdSoyad, 
                              Il = @Il, Ilce = @Ilce, Adres = @Adres, TelefonNumarasi = @TelefonNumarasi, TCVNNumarasi = @TCVN 
                          WHERE Id = @Id;
                          SELECT @Id;";
                }
                else
                {
                    // Insert new address
                    query = @"INSERT INTO PY_ETICARET_USER_ADRESSES 
                          (Email, Baslik, AdSoyad, Il, Ilce, Adres, TelefonNumarasi, TCVNNumarasi)
                          VALUES (@Email, @Baslik, @AdSoyad, @Il,@Ilce, @Adres, @TelefonNumarasi, @TCVN);
                          SELECT SCOPE_IDENTITY();";
                }

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", model.Email);
                    command.Parameters.AddWithValue("@Baslik", model.Baslik);
                    command.Parameters.AddWithValue("@AdSoyad", model.AdSoyad);
                    command.Parameters.AddWithValue("@Il", model.Il);
                    command.Parameters.AddWithValue("@Ilce", model.Ilce);
                    command.Parameters.AddWithValue("@Adres", model.Adres);
                    command.Parameters.AddWithValue("@TelefonNumarasi", model.TelefonNumarasi);
                    command.Parameters.AddWithValue("@TCVN", model.TCVNNumarasi);

                    if (id.HasValue)
                    {
                        command.Parameters.AddWithValue("@Id", id.Value);
                    }

                    int addressId = Convert.ToInt32(command.ExecuteScalar());

                    string message = id.HasValue ? "Adres başarıyla güncellendi." : "Adres başarıyla eklendi.";
                    return Ok(new { Id = addressId, Message = message });
                }
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Bir hata oluştu: {ex.Message}");
        }
    }

    [HttpGet("GetAddressesByEmail")]
    public IActionResult GetAddressesByEmail(string email)
    {
        if (string.IsNullOrEmpty(email))
        {
            return BadRequest("Email address is required.");
        }

        var addresses = new List<Dictionary<string, object>>();

        try
        {
            using (var connection = new SqlConnection(Settings.ConnectionString))
            {
                connection.Open();
                var command = new SqlCommand(
                    "SELECT * FROM PY_ETICARET_USER_ADRESSES WHERE Email = @Email",
                    connection);
                command.Parameters.AddWithValue("@Email", email);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var address = new Dictionary<string, object>();
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            address[reader.GetName(i)] = reader[i];
                        }
                        addresses.Add(address);
                    }
                }
            }

            return Ok(addresses);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while fetching addresses: {ex.Message}");
        }
    }

    [HttpGet("TumSehirler")]
    public async Task<IActionResult> TumSehirler()
    {
        try
        {
            string query = @"SELECT UPPER(NAME) SEHIR FROM L_CITY WHERE COUNTRY = 178 AND NAME IS NOT NULL";
            string sqlDataSource = Settings.ConnectionString;

            using (SqlConnection conn = new SqlConnection(sqlDataSource))
            {
                await conn.OpenAsync();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    var sehirler = new List<string>();
                    while (await reader.ReadAsync())
                    {
                        sehirler.Add(reader["SEHIR"].ToString());
                    }
                    return Ok(sehirler);
                }
            }
        }
        catch (Exception ex)
        {
            return BadRequest("Hata : " + ex.Message);
        }
    }

    [HttpGet("IlcelerBySehir")]
    public async Task<IActionResult> IlcelerBySehir(string sehir)
    {
        try
        {
            string query = @"SELECT UPPER(NAME) ILCE FROM L_TOWN 
                WHERE CTYREF = (SELECT LOGICALREF FROM L_CITY WHERE NAME = @Sehir) AND NAME IS NOT NULL";
            string sqlDataSource = Settings.ConnectionString;

            using (SqlConnection conn = new SqlConnection(sqlDataSource))
            {
                await conn.OpenAsync();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Sehir", sehir);
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        var ilceler = new List<string>();
                        while (await reader.ReadAsync())
                        {
                            ilceler.Add(reader["ILCE"].ToString());
                        }
                        return Ok(ilceler);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            return BadRequest("Hata : " + ex.Message);
        }
    }

    [HttpDelete("remove/{id}")]
    public async Task<ActionResult> RemoveAddress(int id)
    {
        if (id <= 0)
        {
            return BadRequest("Geçersiz adres ID'si.");
        }

        try
        {
            const string deleteCommand = @"DELETE FROM PY_ETICARET_USER_ADRESSES WHERE ID = @ID";

            using var connection = new SqlConnection(Settings.ConnectionString);
            await connection.OpenAsync();

            using var cmd = new SqlCommand(deleteCommand, connection);
            cmd.Parameters.AddWithValue("@ID", id);

            int rowsAffected = await cmd.ExecuteNonQueryAsync();


            if (rowsAffected == 0)
            {
                return NotFound("Belirtilen ID'ye sahip adres bulunamadı.");
            }

            return NoContent(); 
        }
        catch (SqlException ex)
        {
            // Log the exception details here
            return StatusCode(500, "Veritabanı işlemi sırasında bir hata oluştu.");
        }
        catch (Exception ex)
        {
            // Log the exception details here
            return StatusCode(500, "Beklenmeyen bir hata oluştu.");
        }
    }

}

[Table("PY_ETICARET_USER_ADRESSES")]
public class UserAddress
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ID { get; set; }

    [Required]
    [EmailAddress]
    [StringLength(255)]
    public string Email { get; set; }

    [Required]
    [StringLength(100)]
    public string Baslik { get; set; }

    [Required]
    [StringLength(100)]
    public string AdSoyad { get; set; }

    [Required]
    [StringLength(50)]
    public string Il { get; set; }

    [Required]
    [StringLength(50)]
    public string Ilce { get; set; }

    [Required]
    public string Adres { get; set; }

    [Required]
    [StringLength(20)]
    public string TelefonNumarasi { get; set; }

    [Required]
    [StringLength(11)]
    public string TCVNNumarasi { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTime OlusturulmaTarihi { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime GuncellenmeTarihi { get; set; }
}