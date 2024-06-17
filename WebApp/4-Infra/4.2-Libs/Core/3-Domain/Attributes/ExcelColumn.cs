namespace Core;

/// <summary>
/// Atributo que define o nome da coluna na exportação/importação.
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public class ExcelColumn : Attribute
{
    /// <summary>
    /// Obtém o nome da coluna.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Define a ordem da coluna na exportação
    /// </summary>
    public int Order { get; set; } = int.MaxValue;

    /// <summary>
    /// Define o tamanho da coluna.
    /// </summary>
    public uint Width { get; set; } = 20;

    /// <summary>
    /// Indica se o nome será usado na importação.
    /// </summary>
    public bool UseOnImport { get; set; } = true;

    /// <summary>
    /// Indica se o nome será usado na exportação.
    /// </summary>
    public bool UseOnExport { get; set; } = true;
}
