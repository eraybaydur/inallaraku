using InallarEticaretWebService.Models;
using Microsoft.AspNetCore.Mvc;
using NotthomePortalApi;
using NotthomePortalApi.Models;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class CartController : ControllerBase
{


    [HttpPost("addToCart")]
    public async Task<IActionResult> AddToCart([FromBody] AddToCartRequest request)
    {
        try
        {
            using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
            {
                await connection.OpenAsync();

                string query = @"
                    IF EXISTS (SELECT 1 FROM PY_ETICARET_KULLANICI_SEPET WHERE UserEmail = @UserEmail AND ProductId = @ProductId)
                        UPDATE PY_ETICARET_KULLANICI_SEPET SET Quantity = Quantity + 1 WHERE UserEmail = @UserEmail AND ProductId = @ProductId
                    ELSE
                        INSERT INTO PY_ETICARET_KULLANICI_SEPET (UserEmail, ProductId, Quantity) VALUES (@UserEmail, @ProductId, 1)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserEmail", request.UserEmail);
                    command.Parameters.AddWithValue("@ProductId", request.ProductId);

                    await command.ExecuteNonQueryAsync();
                }
            }

            return Ok(new { message = "Ürün sepete eklendi." });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Bir hata oluştu.", error = ex.Message });
        }
    }

    [HttpGet("GetCartItems")]
    public ActionResult<IEnumerable<SepettekiUrun>> GetCartItems(string email)
    {
        if (string.IsNullOrEmpty(email))
        {
            return BadRequest("E-posta adresi gereklidir.");
        }

        var baseUrl = $"{Request.Scheme}://{Request.Host}{Request.PathBase}";

        var cartItems = new List<SepettekiUrun>();

        using (var connection = new SqlConnection(Settings.ConnectionString))
        {
            connection.Open();

            var query = $@"
                SELECT ITEMS.LOGICALREF, Items.NAME, Sepet.ProductId, Sepet.Quantity, Sepet.UserEmail, PRCLIST.PRICE, IMAGES.ImagePath 
                FROM PY_ETICARET_KULLANICI_SEPET Sepet
                INNER JOIN {Settings.ProsesController.ITEMS} Items ON Sepet.ProductId = Items.LOGICALREF
                LEFT JOIN PY_ETICARET_PRODUCT_IMAGES IMAGES ON IMAGES.ProductId = Items.LOGICALREF 
                LEFT JOIN LG_219_PRCLIST PRCLIST ON PRCLIST.CARDREF = ITEMS.LOGICALREF
                WHERE Sepet.UserEmail = @Email";

            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Email", email);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var cartItem = new SepettekiUrun
                        {
                            LogicalRef = Convert.IsDBNull(reader["LOGICALREF"]) ? 0 : Convert.ToInt32(reader["LOGICALREF"]),
                            Adet = Convert.IsDBNull(reader["Quantity"]) ? 0 : Convert.ToInt32(reader["Quantity"]),
                            UrunAdi = Convert.IsDBNull(reader["NAME"]) ? string.Empty : reader["NAME"].ToString(),
                            BirimFiyat = Convert.IsDBNull(reader["PRICE"]) ? 0 : Convert.ToDouble(reader["PRICE"]),
                            ImagePaths = new List<string>()
                        };

                        if (!reader.IsDBNull(reader.GetOrdinal("ImagePath")))
                        {
                            var imagePath = reader["ImagePath"].ToString();
                            var fullImageUrl = Path.Combine(baseUrl, "productImages", Path.GetFileName(imagePath));
                            cartItem.ImagePaths.Add(fullImageUrl);
                        }

                        cartItems.Add(cartItem);
                    }
                }
            }
        }

        if (cartItems.Count == 0)
        {
            return NotFound("Kullanıcının sepetinde ürün bulunamadı.");
        }

        return Ok(cartItems);
    }

    [HttpPut("UpdateCartItem")]
    public IActionResult UpdateCartItem([FromBody] UpdateCartItemRequest request)
    {
        try
        {
            using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
            {
                connection.Open();

                string query = @"
                    UPDATE PY_ETICARET_KULLANICI_SEPET 
                    SET Quantity = @Quantity 
                    WHERE UserEmail = @Email AND ProductId = @LogicalRef";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@Email", SqlDbType.NVarChar).Value = request.Email;
                    command.Parameters.Add("@LogicalRef", SqlDbType.Int).Value = request.LogicalRef;
                    command.Parameters.Add("@Quantity", SqlDbType.Int).Value = request.Quantity;

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        return Ok(new { Message = "Sepet öğesi başarıyla güncellendi." });
                    }
                    else
                    {
                        return NotFound(new { Message = "Güncellenecek sepet öğesi bulunamadı." });
                    }
                }
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = "Sepet öğesi güncellenirken bir hata oluştu.", Error = ex.Message });
        }
    }


    [HttpDelete("RemoveCartItem")]
    public IActionResult RemoveCartItem([FromBody] RemoveCartItemRequest request)
    {
        try
        {
            using (SqlConnection connection = new SqlConnection(Settings.ConnectionString))
            {
                connection.Open();

                string query = @"
                    DELETE FROM PY_ETICARET_KULLANICI_SEPET 
                    WHERE UserEmail = @Email AND ProductId = @LogicalRef";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@Email", SqlDbType.NVarChar).Value = request.Email;
                    command.Parameters.Add("@LogicalRef", SqlDbType.Int).Value = request.LogicalRef;

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        return Ok(new { Message = "Sepet öğesi başarıyla silindi." });
                    }
                    else
                    {
                        return NotFound(new { Message = "Silinecek sepet öğesi bulunamadı." });
                    }
                }
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = "Sepet öğesi silinirken bir hata oluştu.", Error = ex.Message });
        }
    }
}


public class UpdateCartItemRequest
{
    public string Email { get; set; }
    public int LogicalRef { get; set; }
    public int Quantity { get; set; }
}

public class AddToCartRequest
{
    public string UserEmail { get; set; }
    public int ProductId { get; set; }
}

public class RemoveCartItemRequest
{
    public string Email { get; set; }
    public int LogicalRef { get; set; }
}

