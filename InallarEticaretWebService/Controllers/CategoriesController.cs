using InallarEticaretWebService.Models;
using Microsoft.AspNetCore.Mvc;
using NotthomePortalApi.Models;
using System.Data;
using System.Data.SqlClient;

namespace InallarEticaretWebService.Controllers
{
    [Route("api/kategoriler")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        [HttpGet()]
        [Produces("application/json")]
        public async Task<Result<List<Category>>> Kategoriler()
        {
            try
            {
                string kategori = string.Empty;
                string query = $@"SELECT 
                            DISTINCT(KEYWORD1)
                            FROM  {Settings.ProsesController.ITEMS} ITEMS
                            WHERE
                            ACTIVE = 0 AND ITEMS.KEYWORD1 <> ''";

                using var connection = new SqlConnection(Settings.ConnectionString);
                await connection.OpenAsync();

                List<Category> kategoriler = new List<Category>();
                using SqlCommand kategoriCommand = new SqlCommand(query, connection);
                using SqlDataReader kategoriReader = await kategoriCommand.ExecuteReaderAsync();

                DataTable dataTable = new DataTable();
                dataTable.Load(kategoriReader);
                kategoriReader.Close();

                foreach (DataRow kategoriRow in dataTable.Rows)
                {
                    kategori = kategoriRow["KEYWORD1"].ToString() ?? string.Empty;
                    string altKategoriQuery = @$"
                            SELECT DISTINCT(KEYWORD2) FROM {Settings.ProsesController.ITEMS} 
                            WHERE ACTIVE = 0 AND
                            KEYWORD1 <> '' AND KEYWORD1 LIKE '%{kategori}%'";
                    List<SubCategory> altKategoriler = new List<SubCategory>(); 

                    using SqlCommand altKategorilerCommand = new SqlCommand(altKategoriQuery,connection);
                    altKategorilerCommand.Parameters.AddWithValue("@KEYWORD1", kategoriRow["KEYWORD1"]);
                    SqlDataReader altKategoriReader = altKategorilerCommand.ExecuteReader();

                    DataTable dataTable2 = new DataTable();
                    dataTable2.Load(altKategoriReader);
                    altKategoriReader.Close();

                    foreach(DataRow altKategoriRow in dataTable2.Rows)
                    {

                        SubCategory altKategori = new SubCategory()
                        {
                            Name = altKategoriRow["KEYWORD2"].ToString() ?? string.Empty,
                        };
                        altKategoriler.Add(altKategori);
                    }

                    Category category = new Category()
                    {
                        Name= kategoriRow["KEYWORD1"].ToString() ?? string.Empty,
                        SubCategories = altKategoriler
                    };

                    kategoriler.Add(category);

                }

                return new Result<List<Category>>
                {
                    IsSuccess = true,
                    Message = "Kategoriler başarıyla getirildi.",
                    Data = kategoriler,
                    StatusCode = System.Net.HttpStatusCode.OK,
                    RowCount = kategoriler.Count
                };

            }
            catch(Exception ex)
            {
                return new Result<List<Category>>
                {
                    IsSuccess = false,
                    Message = $"Kategoriler getirilirken bir hata oluştu. Hata: {ex.Message}",
                    Data = null,
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    RowCount = 0
                };
            }
        }
    }
}
