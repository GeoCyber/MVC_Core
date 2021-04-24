using FixedAssets.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FixedAssets.ViewModels
{
    public class FixedModuleViewModel
    {
        public List<FixedAssetWriteOff> fixedAssetWriteOff { get; set; }
        public List<FixedAssetTransfer> fixedAssetTransfer { get; set; }
        public List<FixedAssetDisposal> fixedAssetDisposal { get; set; }
    }
}
