using System.ComponentModel.DataAnnotations.Schema;

namespace Core
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBaseEntity
    {
        /// <summary>
        /// 
        /// </summary>
        [Column(Order = 0)]
        long Id { get; set; }
    }
}
