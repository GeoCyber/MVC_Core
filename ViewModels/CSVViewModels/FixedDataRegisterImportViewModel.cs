using FixedModules.Models;
using System.Collections.Generic;

namespace FixedModules.ViewModels.CSVViewModels
{
	public class FixedDataRegisterImportViewModel
	{
		public List<string> Messages{ get; set; }
		public List<Fixed_Asset_Register> Data{ get; set; }
	}
}
