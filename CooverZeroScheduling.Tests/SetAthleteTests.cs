using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoverZeroScheduling;
using Xunit;

namespace CooverZeroScheduling.Tests
{
    public class SetAthleteTests
    {

        [Theory]
        [InlineData("sp_viewEditCornerByID", true)]
        [InlineData("sp_viewEditSafetyByID", false)]
        [InlineData("sp_viewEditCornerByID", false)]
        [InlineData("sp_viewEditSafetyByID", true)]

        public void SetSp_AthleteIDShouldSetSp(string expected, bool isCorner)
        {
            // Act
            string actual = Schedule.SetSp_AthleteID(isCorner);

            // Asert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("cornerDiscipline", true)]
        [InlineData("cornerDiscipline", false)]
        [InlineData("safetyDiscipline", false)]
        [InlineData("safetyDiscipline", true)]

        public void SetAthleteDisciplineSouldSetDiscipline(string expected, bool isCorner)
        {
            // Act
            string actual = Schedule.SetAthleteDiscipline(isCorner);

            // Asert
            Assert.Equal(expected, actual);
        }
    }
}
