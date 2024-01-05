using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using GEJA_KHC_WEB_API.Models;

namespace GEJA_KHC_WEB_API.ViewModels
{
    public static class ListViewModel
    {
        private static Geja_KHC_Entities db = new Geja_KHC_Entities();
        
        public static List<DropDownListModel> GetGroupList()
        {
            var GroupList = (from grp in db.Group
                            select new DropDownListModel
                            {
                                Text = grp.GroupName,
                                Value = grp.GroupId.ToString()
                            }).ToList();

            return GroupList;
        }

        public static List<DropDownListModel> GetMemberNameList(int groupid)
        {
            var MemberNameList = (from mem in db.Member
                                  where mem.GroupId == groupid
                                  orderby mem.FullName
                                  select new DropDownListModel { Value = mem.MemberId, Text = mem.FullName }).ToList();

            return MemberNameList;
        }
    }
}
