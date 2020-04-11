using Amazon;
using System;
using Transferzor.Services;
using Xunit;

namespace Transferzor.Tests
{
    public class AwsParameterStoreClientTests
    {
        [Fact]
        public async void GetValue_Works()
        {
            var client = new AwsParameterStoreClient(RegionEndpoint.EUCentral1);
            var result = await client.GetValueAsync("Transferzor-DB");

            Assert.NotNull(result);
        }
    }
}
