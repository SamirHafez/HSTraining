using HST.Core.Models;
using HST.Core.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.Storage;

namespace HST.Services
{
    public class StorageService : IStorageService
    {
        readonly StorageFolder LocalFolder;

        public StorageService()
        {
            LocalFolder = ApplicationData.Current.LocalFolder;
        }

        public async Task<IList<HSTWorkout>> GetWorkoutsAsync()
        {
            StorageFile file = await LocalFolder.CreateFileAsync("workouts.json", CreationCollisionOption.OpenIfExists);

            string content = await FileIO.ReadTextAsync(file);

            return JsonConvert.DeserializeObject<IList<HSTWorkout>>(content) ?? Enumerable.Empty<HSTWorkout>().ToList();
        }

        public async Task InsertWorkoutAsync(HSTWorkout workout)
        {
            StorageFile file = await LocalFolder.CreateFileAsync("workouts.json", CreationCollisionOption.OpenIfExists);

            var workouts = await GetWorkoutsAsync();

            workouts.Add(workout);

            string content = JsonConvert.SerializeObject(workouts);

            await FileIO.WriteTextAsync(file, content);
        }
    }
}