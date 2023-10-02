using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ProjectCrud.Models
{
    public class Client
    {
            [Key]
            public int ClientId { get; set; }

            [Required]
            public string NameClient { get; set; }
            public string LastnameClient { get; set; }
            public long DNIClient { get; set; }
            public string AdressClient { get; set; }
            public long Phone { get; set; }
            public Status status { get; set; }


    }
        public enum Status
        {
            Activo,

            Inactivo,

            Bloqueado
        }
}

