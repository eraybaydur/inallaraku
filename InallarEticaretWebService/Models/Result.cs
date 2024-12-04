using System.Net;

namespace InallarEticaretWebService.Models
{
    public class Result<T> where T: class
    {
        public bool IsSuccess { get; set; }
        public string? Message { get; set; } = string.Empty;
        public T? Data { get; set; } 
        public HttpStatusCode StatusCode { get; set; }
        public int RowCount { get; set; } = 0;
    }
}
