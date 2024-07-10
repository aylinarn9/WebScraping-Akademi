namespace webscraping.Models
{
    public interface IDatabaseSetting
    {
        string CollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
