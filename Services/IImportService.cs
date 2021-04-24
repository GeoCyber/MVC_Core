using FixedModules.Models;
using FixedModules.ViewModels.CSVViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;

namespace FixedModules.Services
{
	public interface IImportService
	{
		FixedDataRegisterImportViewModel GetFixedAssetRegisterDataFromCSV(IFormFile file);
	}

	public class ImportService : IImportService
	{
		private readonly IWebHostEnvironment _env;
		public ImportService(IWebHostEnvironment env)
		{
			_env = env;
		}

		public string SaveFile(IFormFile file) {

			var pathandname = Path.Combine("Fixed_AssetAttachments", Guid.NewGuid().ToString() + file.FileName);
			var path = Path.Combine(_env.WebRootPath, pathandname);
			using (var stream1 = new FileStream(path, FileMode.CreateNew))
			{
				file.CopyTo(stream1);
			}

			return path;
		}
		public FixedDataRegisterImportViewModel GetFixedAssetRegisterDataFromCSV(IFormFile file)
		{
			try
			{
				string path = SaveFile(file);
				
				var data = new List<Fixed_Asset_Register>();
				var msgs = new List<string>();

				using (var reader = new StreamReader(path))
				{
					int linenumber = 1;
					while (!reader.EndOfStream)
					{
						var values = reader.ReadLine().Split(',');
						if (linenumber != 1)
						{
							try
							{
								data.Add(new Fixed_Asset_Register()
								{
									AssetCode = values[0].ToString(),
									AssetName = values[1].ToString(),
									AssetType = Convert.ToInt32(values[2].ToLower() == "null" ? "0" : values[2]),
									AssetCategory = Convert.ToInt32(values[3].ToLower() == "null" ? "0" : values[3]),
									AssetModel = Convert.ToInt32(values[4].ToLower() == "null" ? "0" : values[4]),
									AssetBrand = Convert.ToInt32(values[5].ToLower() == "null" ? "0" : values[5]),
									Remarks = values[6],
									SupplierID = Convert.ToInt32(values[7].ToLower() == "null" ? "0" : values[7]),
									PurchaseDate = DateTime.TryParse(values[8], out DateTime Temp) ? Convert.ToDateTime(values[8]) : DateTime.Now,
									InvoiceNumber = values[9],
									TotalUnit = values[10],
									UnitPrice = values[11],
									CapitalizeAmount = values[12],
									DepreciationStartDate = DateTime.TryParse(values[13], out DateTime Temp1) ? Convert.ToDateTime(values[13]) : DateTime.Now,
									ResidualAmount = values[14],
									NetBookValue = values[15],
									UtilizeLife = values[16],
									DepreciationpercentagePer = values[17],
									DepreciationEndDate = DateTime.TryParse(values[18], out DateTime Temp2) ? Convert.ToDateTime(values[18]) : DateTime.Now,
									FixedAssetAccountID = Convert.ToInt32(values[19].ToLower() == "null" ? "0" : values[19]),
									PL_DepreciationAccount = Convert.ToInt32(values[20].ToLower() == "null" ? "0" : values[20]),
									PS_DepreciationAccount = Convert.ToInt32(values[21].ToLower() == "null" ? "0" : values[21]),
									DisposalGainAccount = Convert.ToInt32(values[22].ToLower() == "null" ? "0" : values[22]),
									DisposalLossAccount = Convert.ToInt32(values[23].ToLower() == "null" ? "0" : values[23]),
									writeOfAccount = Convert.ToInt32(values[24].ToLower() == "null" ? "0" : values[24]),
									Adjustment_CapitalizeAmount = values[25],
									Adjustment_ResidualAmount = values[26],
									Adjustment_UtilizeLifeInMonths = values[27],
									Attachment = values[28],
									AssetSubCode = values[29],
									RegistrationNumber = values[30],
									SerialNumber = values[31],
									Department = Convert.ToInt32(values[32].ToLower() == "null" ? "0" : values[32]),
									Location = Convert.ToInt32(values[33].ToLower() == "null" ? "0" : values[33]),
									Asset_UnitPrice = values[34],
									AllocationValue = values[35],
									Status = Convert.ToBoolean(values[36]),
								});
							}
							catch
							{
								msgs.Add($"data of row number {linenumber - 1} is in bad manner !");
							}


						}
						linenumber++;
					}
				}

				File.Delete(path);
				return new FixedDataRegisterImportViewModel() { Data = data, Messages = msgs };
			}
			catch (Exception)
			{

				throw;
			}



		}
	}
}
