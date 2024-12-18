using System.Diagnostics.CodeAnalysis;

namespace DotBlogs.Models;

public class BaseResponseModel
{
    [ExcludeFromCodeCoverage]
    public partial class Responses<T>
    {
        public int StatusCode { set; get; }
        public string? Message { set; get; }
        public List<T>? Data { set; get; }
        public Meta? Meta { get; set; }
    }

    public partial class Responses
    {
        public int StatusCode { set; get; }
        public string? Message { set; get; }
    }

    [ExcludeFromCodeCoverage]
    public partial class Response<T>
    {
        public int StatusCode { set; get; }
        public string? Message { set; get; }
        public T? Data { set; get; }
    }

    [ExcludeFromCodeCoverage]
    public partial class Response
    {
        public int StatusCode { set; get; }
        public string? Message { set; get; }
    }
    
    [ExcludeFromCodeCoverage]
    public partial class ResponseValidator
    {
        public bool IsValid { set; get; }
        public string? Message { set; get; }
    }

    [ExcludeFromCodeCoverage]
    public partial class Meta
    {
        public int TotalPages { get; set; }
        public int TotalRecords { get; set; }
    }
}