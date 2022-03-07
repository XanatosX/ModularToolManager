using ModularToolManager2.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModularToolManager2.ViewModels
{
    public class CultureInfoViewModel : ViewModelBase
    {
        private CultureInfoModel CultureInfoModel;

        public CultureInfo Culture => CultureInfoModel.Culture;

        public string DisplayName => CultureInfoModel.DisplayName;

        public CultureInfoViewModel(CultureInfoModel cultureInfoModel)
        {
            CultureInfoModel = cultureInfoModel;
        }


    }
}
