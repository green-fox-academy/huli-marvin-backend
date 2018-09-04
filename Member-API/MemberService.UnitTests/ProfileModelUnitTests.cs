using FluentAssertions;
using MemberService.Factories;
using MemberService.Models;
using Xunit;

namespace MemberService.UnitTests
{
    public class ProfileModelUnitTests
    {
        [Fact]
        public void GetTeamApprenticeId_WithValidId_ShouldReturnId()
        {
            var newProfile = (Profile)ModelFactory.Creator<Profile>();
            var testTeam = (Team)ModelFactory.Creator<Team>();
            newProfile.TeamApprentice = testTeam;

            newProfile.GetTeamApprenticeId(newProfile.TeamApprentice)
                .ShouldBeEquivalentTo(testTeam.Id);
        }

        [Fact]
        public void GetTeamApprenticeId_WithNull_ShouldReturnNull()
        {
            var varProfile = (Profile)ModelFactory.Creator<Profile>();
            varProfile.TeamApprentice = null;

            varProfile.GetTeamApprenticeId(varProfile.TeamApprentice).Should().BeNull();
        }

        [Fact]
        public void GetClassApprenticeId_WithValidId_ShouldReturnId()
        {
            var newProfile = (Profile)ModelFactory.Creator<Profile>();
            var testClass = (Class)ModelFactory.Creator<Class>();
            newProfile.ClassApprentice = testClass;

            newProfile.GetClassApprenticeId(newProfile.ClassApprentice)
                .ShouldBeEquivalentTo(testClass.Id);
        }

        [Fact]
        public void GetClassApprenticeId_WithNull_ShouldReturnNull()
        {
            var newProfile = (Profile)ModelFactory.Creator<Profile>();
            newProfile.ClassApprentice = null;

            newProfile.GetClassApprenticeId(newProfile.ClassApprentice).Should().BeNull();
        }
    }
}
