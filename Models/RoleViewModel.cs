using FixedModules.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FixedModules.Models
{
	public class RoleViewModel
	{
		public string Id { get; set; }

		public string Name { get; set; }

		public bool Selected { get; set; }

        public List<int> permission { get; set; }
    }
}
