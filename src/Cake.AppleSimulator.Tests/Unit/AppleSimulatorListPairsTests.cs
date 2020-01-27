using Cake.AppleSimulator.Tests.Fixtures;
using Cake.Core;
using Cake.Testing;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Cake.AppleSimulator.Tests.Unit
{
    public class AppleSimulatorListPairsTests
    {
        [Fact]
        public void Should_Find_AppleSimulator_If_Tool_Path_Not_Provided()
        {
            // Given
            var fixture = new AppleSimulatorListPairsFixture();

            // When
            var result = fixture.Run();

            // Then
            Assert.Equal("/Working/tools/simctl", result.Path.FullPath);
        }

        [Fact]
        public void Should_Return_Correct_Pair_Details()
        {
            // Given
            var fixture = new AppleSimulatorListPairsFixture();

            // When
            var result = fixture.Run();

            // Then
            fixture.ToolResult.Should().HaveCount(26);

            fixture.ToolResult.First().UDID.Should().Be("6B3FB034-93A6-4466-A59C-71C9F77CEDE1");
            fixture.ToolResult.First().State.Should().Be("(unavailable)");

            fixture.ToolResult.First().Watch.UDID.Should().Be("36A3FE85-19E8-4349-A348-FB2A1C81F1E0");
            fixture.ToolResult.First().Watch.State.Should().Be("Shutdown");
            fixture.ToolResult.First().Watch.Name.Should().Be("Apple Watch Series 2 - 38mm");

            fixture.ToolResult.First().Phone.UDID.Should().Be("208EDBD6-FA1E-4682-A2C0-6DD4A619B4C1");
            fixture.ToolResult.First().Phone.State.Should().Be("Booted");
            fixture.ToolResult.First().Phone.Name.Should().Be("iPhone 7");
        }

        [Fact]
        public void Should_Set_Working_Directory()
        {
            // Given
            var fixture = new AppleSimulatorListPairsFixture();

            // When
            var result = fixture.Run();

            // Then
            Assert.Equal("/Working", result.Process.WorkingDirectory.FullPath);
        }

        [Fact]
        public void Should_Throw_If_AppleSimulator_Was_Not_Found()
        {
            // Given
            var fixture = new AppleSimulatorListPairsFixture();
            fixture.GivenDefaultToolDoNotExist();

            // When
            fixture.Invoking(x => x.Run())

            // Then
                .Should().Throw<CakeException>()
                .WithMessage("AppleSimulator: Could not locate executable.");
        }

        [Fact]
        public void Should_Throw_If_Has_A_Non_Zero_Exit_Code()
        {
            // Given
            var fixture = new AppleSimulatorListPairsFixture();
            fixture.GivenProcessExitsWithCode(1);

            // When
            fixture.Invoking(x => x.Run())

            // Then
                .Should().Throw<CakeException>()
                .WithMessage("AppleSimulator: Process returned an error (exit code 1).");
        }

        [Fact]
        public void Should_Throw_If_Process_Was_Not_Started()
        {
            // Given
            var fixture = new AppleSimulatorListPairsFixture();
            fixture.GivenProcessCannotStart();

            // When
            fixture.Invoking(x => x.Run())

            // Then
                .Should().Throw<CakeException>()
                .WithMessage("AppleSimulator: Process was not started.");
        }

        [Theory]
        [InlineData("/Applications/Xcode.app/Contents/Developer/usr/bin/simctl", "/Applications/Xcode.app/Contents/Developer/usr/bin/simctl")]
        public void Should_Use_AppleSimulator_Runner_From_Tool_Path_If_Provided(string toolPath, string expected)
        {
            // Given
            var fixture = new AppleSimulatorListPairsFixture { Settings = { ToolPath = toolPath } };
            fixture.GivenSettingsToolPathExist();

            // When
            var result = fixture.Run();

            // Then
            result.Path.FullPath.Should().Be(expected);
        }
    }
}