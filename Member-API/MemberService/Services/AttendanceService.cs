using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MemberService.Models;
using MemberService.Models.DataTransferObjects;
using MemberService.Models.Interfaces;
using Microsoft.AspNetCore.Http;

namespace MemberService.Services
{
    public class AttendanceService : IReadService<AttendanceInfoDTO>, IUpdateService<AttendanceSummaryDTO>
    {
        private readonly ICrudRepository<Profile> profileRepository;

        public AttendanceService(ICrudRepository<Profile> profileRepository)
        {
            this.profileRepository = profileRepository;
        }

        public async Task<IEnumerable<AttendanceInfoDTO>> GetAllAsync(IQueryCollection q)
        {
            var profiles = await profileRepository.SelectAllAsync(q);
            return CreateAttendanceDTOs(profiles);
        }

        public async Task<AttendanceInfoDTO> GetByIdAsync(long id)
        {
            var profile = await profileRepository.SelectByIdAsync(id);
            return GenerateDTO(profile);
        }

        private static IEnumerable<AttendanceInfoDTO> CreateAttendanceDTOs(IEnumerable<Profile> profiles)
        {
            return profiles.Select(GenerateDTO);
        }

        private static AttendanceInfoDTO GenerateDTO(Profile profile)
        {
            return new AttendanceInfoDTO()
            {
                Id = profile.Id,
                Name = profile.Name,
                Late = profile.AttendanceInfo.Late,
                DayOff = profile.AttendanceInfo.DayOff,
                SickVerified = profile.AttendanceInfo.SickVerified,
                SickUnverified = profile.AttendanceInfo.SickUnverified,
                Class = profile.ClassApprentice.Name,
                Cohort = profile.CohortApprentice.Name
            };
        }

        public async Task UpdateAsync(long id, AttendanceSummaryDTO entity)
        {
            var profile = await profileRepository.SelectByIdAsync(id);
            if (profile != null)
            {
                profile = UpdateAttendanceInfo(profile, entity);
                await profileRepository.UpdateAsync(profile);
            }
        }

        private static Profile UpdateAttendanceInfo(Profile profile, AttendanceSummaryDTO entity)
        {
            profile.AttendanceInfo.DayOff = entity.DayOff;
            profile.AttendanceInfo.Late = entity.Late;
            profile.AttendanceInfo.SickVerified = entity.SickVerified;
            profile.AttendanceInfo.SickUnverified = entity.SickUnverified;
            return profile;
        }
    }
}
