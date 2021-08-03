using System;
using FluentAssertions;
using FluentAssertions.Common;
using Xunit;

namespace zbw.Auftragsverwaltung.Core.Test
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            true.IsSameOrEqualTo(true);
        }
    }
}
