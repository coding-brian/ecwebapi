using System.ComponentModel.DataAnnotations.Schema;

namespace EcWebapi.Database.Table
{
    public class Entity
    {
        [Column("id")]
        public Guid Id { get; set; }

        [Column("entity_status")]
        public bool EntityStatus { get; set; }

        [Column("creation_time")]
        public DateTime? CreationTime { get; set; }

        [Column("create_by")]
        public Guid? CreateBy { get; set; }

        [Column("modification_time")]
        public DateTime? ModificationTime { get; set; }

        [Column("modify_by")]
        public Guid? ModifyBy { get; set; }

        [Column("deletion_time")]
        public DateTime? DeletionTime { get; set; }

        [Column("delete_by")]
        public Guid? DeleteBy { get; set; }
    }
}