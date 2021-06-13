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
            var result = Execute("jump");

        }
    }
}
