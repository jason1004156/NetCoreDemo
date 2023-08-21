using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Demo.DataModel
{
	public class Security
	{
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public string Id { get; set; }

        [Column(Order = 1)]
        public string Code { get; set; }

        [Column(Order = 2)]
        public string Name { get; set; }
	}
}

