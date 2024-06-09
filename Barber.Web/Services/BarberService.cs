using Barber.Web.Models;
using Barber.Web.Services.Interfaces;
using Barber.Web.Utils;

namespace Barber.Web.Services
{
    public class BarberService : IBarber
    {
        private readonly HttpClient _client;
        public const string BasePath = "api/v1/barber";

        public Task AddNewBarberAsync(BarberVO barberVO)
        {
            throw new NotImplementedException();
        }

        public Task RemoveBarberByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<BarberVO>> GetBarbersAsync()
        {
            var response = await _client.GetAsync(BasePath);
            return await response.ReadContentAs<IEnumerable<BarberVO>>();
        }

        public Task SetDisponibilityAsync(BarberVO barberVO, bool disponibility)
        {
            throw new NotImplementedException();
        }
    }
}
