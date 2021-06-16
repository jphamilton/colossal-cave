using ColossalCave.Places;
using Xunit;

namespace Tests.VerbTests
{
    public class JumpTests : BaseTestFixture
    {
        [Fact]
        public void before_jump_handled()
        {
            Location = Room<EastBankOfFissure>();
            Execute("jump");
            Assert.Contains("You didn't make it.", ConsoleOut);
        }
    }
}
