using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreLayerKit.Entities
{
    /// <summary>
    /// 所有实体的基类，包含主键和创建时间。
    /// </summary>
    public abstract class BaseEntity
    {
        /// <summary>
        /// 主键标识。
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 创建时间。
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
