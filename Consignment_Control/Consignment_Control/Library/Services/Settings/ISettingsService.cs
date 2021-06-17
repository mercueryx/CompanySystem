using Consignment_Control.Library.Data.SQLDomain.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Consignment_Control.Library.Services.Settings
{
    public partial interface  ISettingsService
    {
        IList<ConsigneeDetailsData> GetConsigneeList(string com);

        ConsigneeDetailsData GetSpecificConsigneeData(int id);



        IList<ConsigneeDetailsData> GetSameConsignee_Ah_code(string com, string ah_code);

        void AddNewConsignee(ConsigneeDetailsData Consignee_entity);


        void EditConsignee(string com, ConsigneeDetailsData Consignee_entity);
    }
}