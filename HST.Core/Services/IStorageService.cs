using HST.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HST.Core.Services
{
    public interface IStorageService
    {
        Task InsertWorkoutAsync(HSTWorkout workout);
        Task<IList<HSTWorkout>> GetWorkoutsAsync();
    }
}
