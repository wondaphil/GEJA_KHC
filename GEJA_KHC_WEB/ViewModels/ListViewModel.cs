using System.Collections.Generic;
using System.Linq;
using GEJA_KHC_WEB.Models;

namespace GEJA_KHC_WEB.ViewModels
{
    public static class ListViewModel
    {
        private static Geja_KHC_Entities db = new Geja_KHC_Entities();
        
        public static List<DropDownViewModel> GetGroupList()
        {
            var GroupList = (from grp in db.Group
                            select new DropDownViewModel
                            {
                                Text = grp.GroupName,
                                Value = grp.GroupId.ToString()
                            }).ToList();

            return GroupList;
        }

        public static List<DropDownViewModel> GetMemberNameList(int groupid)
        {
            var MemberNameList = (from mem in db.Member
                                  where mem.GroupId == groupid
                                  orderby mem.FullName
                                  select new DropDownViewModel { Value = mem.MemberId, Text = mem.FullName }).ToList();

            return MemberNameList;
        }
    }
}
