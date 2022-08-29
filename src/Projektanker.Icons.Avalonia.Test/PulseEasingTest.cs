using FluentAssertions;
using Xunit;

namespace Projektanker.Icons.Avalonia.Test
{
    public class PulseEasingTest
    {
        [Theory]
        [InlineData(0.0, 0.000)]
        [InlineData(0.1, 0.000)]
        [InlineData(0.2, 0.125)]
        [InlineData(0.3, 0.250)]
        [InlineData(0.4, 0.375)]
        [InlineData(0.5, 0.500)]
        [InlineData(0.6, 0.500)]
        [InlineData(0.7, 0.625)]
        [InlineData(0.8, 0.750)]
        [InlineData(0.9, 0.875)]
        [InlineData(1.0, 1)]
        public void PulseEasing_Should_Return_Progress_In_Eight_Steps(double progress, double expected)
        {
            var pulseEasing = new PulseEasing();

            var result = pulseEasing.Ease(progress);
            result.Should().Be(expected);
        }
    }
}