using Amazon;
using Amazon.SimpleSystemsManagement;
using Amazon.SimpleSystemsManagement.Model;
using System.Threading.Tasks;

namespace Transferzor.Services
{
    public class AwsParameterStoreClient
    {
        private readonly RegionEndpoint _region;

        public AwsParameterStoreClient(
            RegionEndpoint region)
        {
            _region = region;
        }

        public async Task<string> GetValueAsync(string parameter)
        {
            var ssmClient = new AmazonSimpleSystemsManagementClient(_region);

            var response = await ssmClient.GetParameterAsync(new GetParameterRequest
            {
                Name = parameter,
                WithDecryption = true
            });

            return response.Parameter.Value;
        }
        
        public string GetValue(string parameter)
        {
            var ssmClient = new AmazonSimpleSystemsManagementClient(_region);

            var response = ssmClient.GetParameterAsync(new GetParameterRequest
            {
                Name = parameter,
                WithDecryption = true
            }).Result;

            return response.Parameter.Value;
        }
    }
}
