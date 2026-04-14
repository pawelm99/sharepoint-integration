namespace SharePoint.Integration.SharePoint.Models
{
    public class OperationResult(bool success, string? error = null)
    {
        public bool Success { get; } = success;
        public string? Error { get; set; } = error;
    }
}
