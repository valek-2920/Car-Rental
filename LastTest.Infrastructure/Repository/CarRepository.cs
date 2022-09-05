using LastTest.Application.Contracts.Data;
using LastTest.DataAccess.Repository;
using LastTest.Domain.Models.DataModels;
using LastTest.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LastTest.Infrastructure.Repository
{
    public class CarRepository : Repository<Car>, IRepository<Car>
    {

        public CarRepository(ApplicationDbContext context)
            : base(context)
        {

        }

    }
}
