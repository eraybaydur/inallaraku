using Microsoft.AspNetCore.Mvc;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using NotthomePortalApi.Models;

[ApiController]
[Route("api/[controller]")]
public class BannerController : ControllerBase
{
    private readonly string _connectionString;

    public BannerController(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    [HttpPost]
    public async Task<IActionResult> AddBanners([FromBody] List<BannerData> banners)
    {
        try
        {
            using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
            {
                await connection.OpenAsync();

                foreach (var banner in banners)
                {
                    string query = "INSERT INTO PY_ETICARET_BANNERS (ImagePath, Url) VALUES (@ImagePath, @Url)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ImagePath", banner.Image);
                        command.Parameters.AddWithValue("@Url", banner.Url);
                        await command.ExecuteNonQueryAsync();
                    }
                }
            }

            return Ok(new { message = "Bannerlar başarıyla eklendi." });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Bannerlar eklenirken bir hata oluştu.", error = ex.Message });
        }
    }

    [HttpDelete("{filename}")]
    public async Task<IActionResult> DeleteBanner(string filename)
    {
        try
        {
            using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
            {
                await connection.OpenAsync();

                string query = "DELETE FROM PY_ETICARET_BANNERS WHERE ImagePath LIKE @Filename";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Filename", $"%{filename}");
                    int affectedRows = await command.ExecuteNonQueryAsync();

                    if (affectedRows > 0)
                    {
                        return Ok(new { message = "Banner başarıyla silindi." });
                    }
                    else
                    {
                        return NotFound(new { message = "Banner bulunamadı." });
                    }
                }
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Banner silinirken bir hata oluştu.", error = ex.Message });
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetBanners()
    {
        List<BannerData> banners = new List<BannerData>();

        try
        {
            using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT ImagePath, Url FROM PY_ETICARET_BANNERS";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            banners.Add(new BannerData
                            {
                                Image = reader["ImagePath"].ToString(),
                                Url = reader["Url"].ToString()
                            });
                        }
                    }
                }
            }

            return Ok(banners);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Bannerlar alınırken bir hata oluştu.", error = ex.Message });
        }
    }
}

public class BannerData
{
    public string Image { get; set; }
    public string Url { get; set; }
}