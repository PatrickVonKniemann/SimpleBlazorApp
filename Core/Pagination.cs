namespace Core;

public class Pagination
{
    public int ItemsPerPage { get; set; } = 10;
    public int Page { get; set; } = 1;
    public string FilterValue { get; set; } = "";
    public QuerySort QuerySort { get; set; } = new();
}

public class QuerySort
{
    public string SortParam { get; set; } = "Id";
    public string SortOrder { get; set; } = "asc";
}
