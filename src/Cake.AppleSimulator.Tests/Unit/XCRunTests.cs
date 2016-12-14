using Cake.AppleSimulator.Tests.Fixtures;
using Cake.Core;
using Cake.Testing;
using FluentAssertions;
using Xunit;

namespace Cake.AppleSimulator.Tests.Unit
{
    public class XCRunTests
    {
        [Fact]
        public void Should_Find_AppleSimulator_If_Tool_Path_Not_Provided()
        {
            // Given
            var fixture = new XCRunFindSimCtlFixture();

            // When
            var result = fixture.Run();

            // Then
            Assert.Equal("/Working/tools/xcrun", result.Path.FullPath);
        }

        [Fact]
        public void Should_Throw_If_Has_A_Non_Zero_Exit_Code()
        {
            // Given
            var fixture = new XCRunFindSimCtlFixture();
            fixture.GivenProcessExitsWithCode(72);

            // When
            fixture.Invoking(x => x.Run())
                // Then
                .ShouldThrow<CakeException>()
                .WithMessage("XcodeRun: Process returned an error (exit code 72).");
        }

        [Fact]
        public void Should_Throw_If_Does_Not_Exist()
        {
            // Given
            var fixture = new XCRunFindSimCtlFixture();
            fixture.GivenSettingsToolPathExist();

            // When
            fixture.Invoking(x => x.Run())
               // Then
               .ShouldNotThrow<CakeException>();
        }

        [Theory]
        [InlineData("/usr/local/bin/xcrun", "/usr/local/bin/xcrun")]
        public void Should_Use_XCRun_Runner_From_Tool_Path_If_Provided(string toolPath, string expected)
        {
            // Given
            var fixture = new XCRunFindSimCtlFixture { Settings = { ToolPath = toolPath } };
            fixture.GivenSettingsToolPathExist();

            // When
            var result = fixture.Run();

            // Then
            result.Path.FullPath.Should().Be(expected);
        }
    }
}