namespace Domain.Product;
public class Category : AuditableWithBaseEntity<int>
{
    /// <summary>
    /// Gets or sets the name
    /// </summary>
    public string Name { get; set; } = String.Empty;    

    /// <summary>
    /// Gets or sets the description
    /// </summary>
    public string Description { get; set; } = String.Empty ;

    /// <summary>
    /// Gets or sets the display order
    /// </summary>
    public int DisplayOrder { get; set; }
}
