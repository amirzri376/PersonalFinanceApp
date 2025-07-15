using Xunit;
using PersonalFinanceApp.Models;
using System.Collections.Generic;
using System;

namespace PersonalFinanceApp.Tests
{
    public class LoggerTests
    {
        private class FakeClock : IClock
        {
            public DateTime Now => new DateTime(2024, 1, 1, 12, 0, 0);
        }

        private class FakeWriter : ILogWriter
        {
            public List<string> Lines { get; } = new();

            public void WriteLine(string message)
            {
                Lines.Add(message);
            }
        }

        [Fact]
        public void Log_WritesFormattedMessageToWriter()
        {
            // Arrange
            var clock = new FakeClock();
            var writer = new FakeWriter();
            var logger = new Logger(clock, writer);

            // Act
            logger.Log("Test message");

            // Assert
            Assert.Single(writer.Lines);
            Assert.Equal("[2024-01-01 12:00:00] Test message", writer.Lines[0]);
        }
    }
}
