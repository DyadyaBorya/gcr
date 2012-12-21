using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GCR.Core.Entities;

namespace GCR.Core.Services
{
    public interface IMemberService
    {
        IQueryable<Member> FetchAll();
        IQueryable<Member> FetchActive();
        Member GetById(int id);
        void SaveMember(Member product);
        void DeleteMember(Member product);
        void DeleteOrphanPhotos(Func<string, bool> validationFunc);
        string GetPhotoUploadPath();
    }
}
