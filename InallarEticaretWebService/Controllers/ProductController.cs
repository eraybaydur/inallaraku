using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using NotthomePortalApi.Models;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{

    private readonly IWebHostEnvironment _environment;

    public ProductController(IWebHostEnvironment environment)
    {
        _environment = environment;
    }

    [HttpGet("allProducts")]
    public IActionResult GetProducts(string? brand, string? akuAh, string? ccaEn, string? kutuTabanSekli, string? kutupBasi, string? kutuTipi, string? kutupBasiYerlesimDuzeni)
    {
        var products = new Dictionary<int, Product>();
        var baseUrl = $"{Request.Scheme}://{Request.Host}{Request.PathBase}";

        string ConvertToXml(string? values)
        {
            if (string.IsNullOrEmpty(values)) return null;
            var xmlValues = values.Split(',')
                .Select(v => $"<value>{v.Trim()}</value>")
                .ToList();
            return $"<root>{string.Join("", xmlValues)}</root>";
        }

        using (var connection = new SqlConnection(Settings.ConnectionString))
        {
            connection.Open();

            var command = new SqlCommand(@"
            DECLARE @brandXML XML = CAST(@brand AS XML)
            DECLARE @akuAhXML XML = CAST(@akuAh AS XML)
            DECLARE @ccaEnXML XML = CAST(@ccaEn AS XML)
            DECLARE @kutuTabanSekliXML XML = CAST(@kutuTabanSekli AS XML)
            DECLARE @kutupBasiXML XML = CAST(@kutupBasi AS XML)
            DECLARE @kutuTipiXML XML = CAST(@kutuTipi AS XML)
            DECLARE @kutupBasiYerlesimDuzeniXML XML = CAST(@kutupBasiYerlesimDuzeni AS XML)

            SELECT 
                ITEMS.LOGICALREF,
                ITEMS.NAME,
                PRCLIST.PRICE,
                IMAGES.ImagePath,
                ITEMS.KEYWORD1,
                ITEMS.KEYWORD2 
            FROM LG_219_ITEMS ITEMS
            LEFT JOIN LG_219_PRCLIST PRCLIST ON PRCLIST.CARDREF = ITEMS.LOGICALREF
            LEFT JOIN PY_ETICARET_PRODUCT_IMAGES IMAGES ON IMAGES.ProductId = ITEMS.LOGICALREF 
            LEFT JOIN LG_XT300_219 XT300 ON XT300.PARLOGREF = ITEMS.LOGICALREF 
            WHERE ITEMS.ACTIVE = 0 
                AND ITEMS.SPECODE = 'PAR' 
                AND PRICE > 0
                AND (@brand IS NULL OR ITEMS.KEYWORD2 IN (
                    SELECT T.c.value('.', 'nvarchar(50)') 
                    FROM @brandXML.nodes('/root/value') T(c)
                ))
                AND (@akuAh IS NULL OR XT300.AKUAH IN (
                    SELECT T.c.value('.', 'int') 
                    FROM @akuAhXML.nodes('/root/value') T(c)
                ))
                AND (@ccaEn IS NULL OR XT300.AKUCCAEN IN (
                    SELECT T.c.value('.', 'nvarchar(50)') 
                    FROM @ccaEnXML.nodes('/root/value') T(c)
                ))
                AND (@kutuTabanSekli IS NULL OR XT300.AKUKUTUTABANSEKLI IN (
                    SELECT T.c.value('.', 'nvarchar(50)') 
                    FROM @kutuTabanSekliXML.nodes('/root/value') T(c)
                ))
                AND (@kutupBasi IS NULL OR XT300.AKUKUTUPBASI IN (
                    SELECT T.c.value('.', 'nvarchar(50)') 
                    FROM @kutupBasiXML.nodes('/root/value') T(c)
                ))
                AND (@kutuTipi IS NULL OR XT300.AKUKUTUTIPI IN (
                    SELECT T.c.value('.', 'nvarchar(50)') 
                    FROM @kutuTipiXML.nodes('/root/value') T(c)
                ))
                AND (@kutupBasiYerlesimDuzeni IS NULL OR XT300.AKUKUTUPBASIYERLESIMDUZENI IN (
                    SELECT T.c.value('.', 'nvarchar(50)') 
                    FROM @kutupBasiYerlesimDuzeniXML.nodes('/root/value') T(c)
                ))", connection);

            // Parametreleri XML formatında ekle
            command.Parameters.AddWithValue("@brand", (object)ConvertToXml(brand) ?? DBNull.Value);
            command.Parameters.AddWithValue("@akuAh", (object)ConvertToXml(akuAh) ?? DBNull.Value);
            command.Parameters.AddWithValue("@ccaEn", (object)ConvertToXml(ccaEn) ?? DBNull.Value);
            command.Parameters.AddWithValue("@kutuTabanSekli", (object)ConvertToXml(kutuTabanSekli) ?? DBNull.Value);
            command.Parameters.AddWithValue("@kutupBasi", (object)ConvertToXml(kutupBasi) ?? DBNull.Value);
            command.Parameters.AddWithValue("@kutuTipi", (object)ConvertToXml(kutuTipi) ?? DBNull.Value);
            command.Parameters.AddWithValue("@kutupBasiYerlesimDuzeni", (object)ConvertToXml(kutupBasiYerlesimDuzeni) ?? DBNull.Value);

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var logicalRef = reader.GetInt32(0);
                    if (!products.ContainsKey(logicalRef))
                    {
                        products[logicalRef] = new Product
                        {
                            LogicalRef = logicalRef,
                            Name = reader.GetString(1),
                            Price = Convert.ToDecimal(reader.GetDouble(2)),
                            Keyword1 = reader.GetString(4),
                            Keyword2 = reader.GetString(5),
                            ImagePaths = new List<string>()
                        };
                    }

                    if (!reader.IsDBNull(3))
                    {
                        var imagePath = reader.GetString(3);
                        var fullImageUrl = Path.Combine(baseUrl, "productImages", Path.GetFileName(imagePath));
                        products[logicalRef].ImagePaths.Add(fullImageUrl);
                    }
                }
            }
        }

        return Ok(products.Values);
    }

    [HttpGet("search")]
    public IActionResult SearchProducts(string q)
    {
        if (string.IsNullOrWhiteSpace(q) || q.Length < 3)
        {
            return BadRequest("Arama terimi en az 3 karakter olmalıdır.");
        }

        var products = new Dictionary<int, Product>(); // LogicalRef'e göre ürünleri tutacak

        using (var connection = new SqlConnection(Settings.ConnectionString))
        {
            var baseUrl = $"{Request.Scheme}://{Request.Host}{Request.PathBase}";
            connection.Open();

            var command = new SqlCommand(@"
        SELECT 
            ITEMS.LOGICALREF,
            ITEMS.NAME,
            PRCLIST.PRICE,
            IMAGES.ImagePath
        FROM LG_219_ITEMS ITEMS
        LEFT JOIN LG_219_PRCLIST PRCLIST ON PRCLIST.CARDREF = ITEMS.LOGICALREF
        LEFT JOIN PY_ETICARET_PRODUCT_IMAGES IMAGES ON IMAGES.ProductId = ITEMS.LOGICALREF 
        WHERE ITEMS.ACTIVE = 0 
          AND ITEMS.SPECODE = 'PAR' 
          AND PRICE > 0
          AND ITEMS.NAME LIKE @SearchTerm", connection);

            command.Parameters.AddWithValue("@SearchTerm", $"%{q}%");

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var logicalRef = reader.GetInt32(0);

                    // Eğer ürün daha önce eklenmemişse ekle
                    if (!products.ContainsKey(logicalRef))
                    {
                        products[logicalRef] = new Product
                        {
                            LogicalRef = logicalRef,
                            Name = reader.GetString(1),
                            Price = Convert.ToDecimal(reader.GetDouble(2)),
                            ImagePaths = new List<string>()
                        };
                    }

                    // Resim varsa ekle
                    if (!reader.IsDBNull(3))
                    {
                        var imagePath = reader.GetString(3);
                        var fullImageUrl = Path.Combine(baseUrl, "productImages", Path.GetFileName(imagePath));
                        products[logicalRef].ImagePaths.Add(fullImageUrl);
                    }
                }
            }
        }

        return Ok(products.Values); // Dictionary'nin değerlerini liste olarak dön
    }

    [HttpGet("product")]
    public async Task<IActionResult> GetProduct(int itemRef)
    {
        try
        {
            var baseUrl = $"{Request.Scheme}://{Request.Host}{Request.PathBase}";
            string query = $@"SELECT 
                         ITEMS.LOGICALREF,
                         ITEMS.NAME,
                         PRCLIST.PRICE,
                         IMAGES.ImagePath,
                         ITEMS.KEYWORD1,
                         ITEMS.KEYWORD2
                        FROM {Settings.ProsesController.ITEMS} ITEMS
                        LEFT JOIN {Settings.ProsesController.PRCLIST} PRCLIST ON ITEMS.LOGICALREF = PRCLIST.CARDREF
                        LEFT JOIN PY_ETICARET_PRODUCT_IMAGES IMAGES ON ITEMS.LOGICALREF = IMAGES.ProductId
                        WHERE ITEMS.LOGICALREF = @ITEMREF";

            using var connection = new SqlConnection(Settings.ConnectionString);
            await connection.OpenAsync();

            using var cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@ITEMREF", itemRef);

            using SqlDataReader reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                var product = new Product
                {
                    LogicalRef = reader.GetInt32(0),
                    Name = reader.IsDBNull(1) ? null : reader.GetString(1),
                    Price = reader.IsDBNull(2) ? (decimal)0 : Convert.ToDecimal(reader.GetDouble(2)),
                    Keyword1 = reader.IsDBNull(4) ? null : reader.GetString(4),
                    Keyword2 = reader.IsDBNull(5) ? null : reader.GetString(5),
                    ImagePaths = new List<string>()
                };

                do
                {
                    if (!reader.IsDBNull(3))
                    {
                        var imagePath = reader.GetString(3);
                        var fullImageUrl = Path.Combine(baseUrl, "productImages", Path.GetFileName(imagePath));
                        product.ImagePaths.Add(fullImageUrl);
                    }
                } while (await reader.ReadAsync());

                return Ok(product);
            }
            else
            {
                return NotFound("Product not found.");
            }
        }
        catch (SqlException sqlEx)
        {
            return StatusCode(500, "Database error occurred.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, "An unexpected error occurred.");
        }
    }

    [HttpPost("upload-images")]
    public async Task<IActionResult> UploadImages([FromForm] int urunId, [FromForm] List<IFormFile> files)
    {
        if (files == null || !files.Any())
            return BadRequest("Dosya yüklenmedi.");

        try
        {
            List<string> uploadedImagePaths = new List<string>();

            foreach (var file in files)
            {
                // Dosya adını benzersiz yapmak için GUID kullanıyoruz
                string fileName = $"{Guid.NewGuid()}_{file.FileName}";
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "productImages", fileName);

                // Dosya yolu yoksa oluştur
                Directory.CreateDirectory(Path.GetDirectoryName(filePath));

                // Dosyayı kaydet
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    // Open the original image file
                    using (var image = Image.Load(file.OpenReadStream()))
                    {
                        // Save the compressed and resized image
                        image.Save(stream, new JpegEncoder
                        {
                            Quality = 60 // Set your desired image quality (0-100)
                        });
                    }
                }

                string imagePath = $"/productImages/{fileName}";
                uploadedImagePaths.Add(imagePath);
            }

            // Veritabanına yeni resim yollarını ekle
            using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
            {
                await connection.OpenAsync();
                string query = "INSERT INTO PY_ETICARET_PRODUCT_IMAGES (ProductId, ImagePath) VALUES (@ProductId, @ImagePath)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    foreach (var imagePath in uploadedImagePaths)
                    {
                        command.Parameters.Clear();
                        command.Parameters.AddWithValue("@ProductId", urunId);
                        command.Parameters.AddWithValue("@ImagePath", imagePath);
                        await command.ExecuteNonQueryAsync();
                    }
                }
            }

            return Ok(new { filepaths = uploadedImagePaths });
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Bir hata oluştu: {ex.Message}");
        }
    }

    [HttpDelete("delete-image")]
    public async Task<IActionResult> DeleteImage([FromQuery] string imagePath)
    {
        try
        {
            // URL'den gelen path'i düzelt
            imagePath = imagePath.Replace("https://localhost:7021", "").Replace('\\', '/');

            // Dosyayı sil
            string fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", imagePath.TrimStart('/'));
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }

            // Veritabanından resim kaydını sil
            using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
            {
                await connection.OpenAsync();
                string query = "DELETE FROM PY_ETICARET_PRODUCT_IMAGES WHERE ImagePath = @ImagePath";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ImagePath", imagePath);
                    int affectedRows = await command.ExecuteNonQueryAsync();
                    if (affectedRows == 0)
                        return NotFound("Resim bulunamadı.");
                }
            }

            return Ok("Resim başarıyla silindi.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Bir hata oluştu: {ex.Message}");
        }
    }

}
public class Product
{
    public int LogicalRef { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string? Keyword1 { get; set; }
    public string? Keyword2 { get; set; }
    public List<string> ImagePaths { get; set; }
}