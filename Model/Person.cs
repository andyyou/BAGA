using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Person
    {

        public int PersonId { get; set; }
        // [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [ConcurrencyCheck] // 更新，刪除，都會多確認SocialSecurityNumber是否一致。
        public int SocialSecurityNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }
        public Address Address { get; set; }
        public PersonalInfo Info { get; set; }
        public List<Lodging> PrimaryContactFor { get; set; }
        public List<Lodging> SecondaryContactFor { get; set; }
        [Required]
        public PersonPhoto Photo { get; set; }
        public Person()
        {
            Address = new Address();
            Info = new PersonalInfo { Weight = new Measurement(), Height = new Measurement() };
            Photo = new PersonPhoto { Photo = new Byte[] { 0 } };
        }
    }
}
