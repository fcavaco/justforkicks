using kata.LawnMower.Models;
using Microsoft.Extensions.Configuration;

namespace kata.LawnMower.Infrastructure
{
    public interface ISettings
    {
        ISize GardenSize { get; }
        IPosition DevicePosition { get; }
    }
    public class InitialSettings:ISettings
    {
        private IConfiguration _configuration;

        public InitialSettings(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public ISize GardenSize { get; }
        public IPosition DevicePosition { get; }
    }
}
