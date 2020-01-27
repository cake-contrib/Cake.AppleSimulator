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
    public class AppleSimulatorListDeviceTypesTests
    {
        [Fact]
        public void Should_Find_AppleSimulator_If_Tool_Path_Not_Provided()
        {
            // Given
            var fixture = new AppleSimulatorListDeviceTypesFixture();

            // When
            var result = fixture.Run();

            // Then
            Assert.Equal("/Working/tools/simctl", result.Path.FullPath);
        }

        [Fact]
        public void Should_Return_Correct_DeviceType_Details()
        {
            // Given
            var fixture = new AppleSimulatorListDeviceTypesFixture();

            // When
            var result = fixture.Run();

            // Then
            fixture.ToolResult.Should().HaveCount(15);

            fixture.ToolResult.First().Name.Should().Be("iPhone 4s"); // correct order

            fixture.ToolResult.Last().Name.Should().Be("Apple Watch - 42mm");
            fixture.ToolResult.Last().Identifier.Should().Be("com.apple.CoreSimulator.SimDeviceType.Apple-Watch-42mm");
        }

        [Fact]
        public void Should_Set_Working_Directory()
        {
            // Given
            var fixture = new AppleSimulatorListDeviceTypesFixture();

            // When
            var result = fixture.Run();

            // Then
            Assert.Equal("/Working", result.Process.WorkingDirectory.FullPath);
        }

        [Fact]
        public void Should_Throw_If_AppleSimulator_Was_Not_Found()
        {
            // Given
            var fixture = new AppleSimulatorListDeviceTypesFixture();
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
            var fixture = new AppleSimulatorListDeviceTypesFixture();
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
            var fixture = new AppleSimulatorListDeviceTypesFixture();
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
            var fixture = new AppleSimulatorListDeviceTypesFixture { Settings = { ToolPath = toolPath } };
            fixture.GivenSettingsToolPathExist();

            // When
            var result = fixture.Run();

            // Then
            result.Path.FullPath.Should().Be(expected);
        }
    }
}