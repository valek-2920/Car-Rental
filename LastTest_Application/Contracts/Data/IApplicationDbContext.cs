using LastTest.Domain.Models.DataModels;
using Microsoft.EntityFrameworkCore;

namespace LastTest.Application.Contracts.Data
{
    public interface IApplicationDbContext
    {
        public DbSet<Car> Cars { get; set; }

        public DbSet<ReservationStatus> Statuses { get; set; }
    }
}
