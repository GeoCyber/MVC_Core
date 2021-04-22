using FixedModules.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FixedModules.ViewModels
{
    public class FixedAssetViewModel
    {

        public List<Fixed_Asset_Register> Fixed_Asset_Registers  { get; set; }

        //public Fixed_Asset_Register Fixed_Asset_Register { get; set; }
        public List<SelectListItem> static_list { get; set; }
        public List<MasterAssetCategory> asset_Category { get; set; }
        public List<MasterAssetModel> asset_model  { get; set; }
        public List<MasterAssetBrand> asset_brand  { get; set; }
        public List<SelectListItem> asset_type { get; set; }
    }
}
