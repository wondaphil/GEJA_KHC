using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GEJA_KHC_WEB.ViewModels
{
    public enum StatusMessageId
    {
        AddDataSuccess,
        UpdateDataSuccess,
        DeleteDataSuccess,
        AddDataError,
        UpdateDataError,
        DeleteDataError,
        DuplicateDataError,
        ForeignKeyConstraintError,
        Error
    }

    public static class StatusMessageViewModel
    {
        public static string GetMessage(StatusMessageId? statusId)
        {
            return
                statusId == StatusMessageId.AddDataSuccess ? "መረጃው በትክክል ተመዝግቧል!"
                : statusId == StatusMessageId.AddDataError ? "መረጃውን መመዝገብ አልተቻለም!"
                : statusId == StatusMessageId.UpdateDataSuccess ? "መረጃው በትክክል ተቀይሯል!"
                : statusId == StatusMessageId.UpdateDataError ? "መረጃውን መቀየር አልተቻለም!"
                : statusId == StatusMessageId.DeleteDataSuccess ? "መረጃው ተሰርዟል!"
                : statusId == StatusMessageId.DeleteDataError ? "መረጃውን መሰረዝ አልተቻለም!"
                : statusId == StatusMessageId.DuplicateDataError ? "በመረጃ ተመሳሳይነት ምክንያት አልተሳካም!"
                : statusId == StatusMessageId.ForeignKeyConstraintError ? "መረጃው ከአባል መረጃ ጋር ስለተሳሰረ መሰረዝ አልተቻለም!"
                : statusId == StatusMessageId.Error ? "በሆነ ችግር ምክንያት አልተሳካም!"
                : statusId == null ? ""
                : "";
        }
    }
}