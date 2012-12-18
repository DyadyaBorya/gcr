using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GCR.Core;
using GCR.Core.Entities;
using GCR.Core.Repositories;
using GCR.Core.Services;

namespace GCR.Business.Services
{
    public class MemberService : IMemberService
    {
        private IMemberRepository memberRepository;
        private IPhotoService photoService;
        private string uploadPath;

        public MemberService(IMemberRepository repo, IPhotoService service)
        {
            memberRepository = repo;
            photoService = service;
            uploadPath = Configuration.UploadPath + "Photos/Members";
            photoService.Initialize(uploadPath);

        }

        public IEnumerable<Member> FetchAll()
        {
            return memberRepository.Query.
                OrderByDescending(a => a.IsActive).
                ThenBy(a => a.LastName).
                ThenBy(a=> a.FirstName);
        }

        public IEnumerable<Member> FetchActive()
        {
            return this.FetchAll().Where(a=>a.IsActive);
        }

        public Member GetById(int id)
        {
            return memberRepository.Query.SingleOrDefault(a => a.MemberId == id);
        }

        public void SaveMember(Member member)
        {
            if (member.MemberId == 0)
            {
                memberRepository.Create(member);
            }
            else
            {
                memberRepository.Update(member);
            }

            memberRepository.SaveChanges();
        }

        public void DeleteMember(Member member)
        {
            using (var scope = new TransactionScope())
            {
                memberRepository.Delete(member);
                memberRepository.SaveChanges();
                photoService.DeletePhoto(member.Photo);
                scope.Complete();
            }

        }

        public void DeleteOrphanPhotos(Func<string, bool> validationFunc)
        {
            photoService.DeleteOrphanPhotos(validationFunc);
        }

        public string GetPhotoUploadPath()
        {
            return uploadPath;
        }
    }
}
