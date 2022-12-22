using ArkGuide.Models;
using ArkGuide.Models.CreatureModels;
using ArkGuide.Models.DTOs;
using ArkGuide.Services.ResourceMapServices;
using NuGet.Common;
using SQLite;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ArkGuide.Database
{
    public class ArkGuideDatabase
    {
        static SQLiteAsyncConnection Database;

        public static readonly AsyncLazy<ArkGuideDatabase> Instance = new AsyncLazy<ArkGuideDatabase>(async () =>
        {
            var instance = new ArkGuideDatabase();
            await Database.CreateTableAsync<NotableLocationDTO>();
            await Database.CreateTableAsync<ResourceMapDTO>();

            await Database.CreateTableAsync<CreatureDTO>();
            await Database.CreateTableAsync<CanDamageDTO>();
            await Database.CreateTableAsync<CarryableByDTO>();
            await Database.CreateTableAsync<FoodRequiredDTO>();
            await Database.CreateTableAsync<ImmobilizedByDTO>();
            await Database.CreateTableAsync<KnockoutTamingGridDTO>();
            await Database.CreateTableAsync<SpawningMapDTO>();
            await Database.CreateTableAsync<BaseStatsDTO>();
            await Database.CreateTableAsync<SpawnCommandDTO>();

            await PopulateCreatureDatabase();
            await PopulateResourceMapDatabase();
            return instance;
        });


        private static async Task PopulateCreatureDatabase()
        {
            var count = await Database.Table<CreatureDTO>().CountAsync();
            if (count > 1) return;
            
            var assembly = IntrospectionExtensions.GetTypeInfo(typeof(DinoCollection)).Assembly;
            XmlSerializer serializer = new XmlSerializer(typeof(DinoCollection));
            DinoCollection dc = new DinoCollection();
            using (Stream reader = assembly.GetManifestResourceStream("ArkGuide.CreatureInfo.xml"))
            {
                try
                {
                    var dinoCollection = (DinoCollection)serializer.Deserialize(reader);

                    dc = dinoCollection;
                } catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
            var testList = new List<CreatureDTO>();

            foreach (var dino in dc.Dinos)
            {

                var newDino = ToDinoDTO(dino);
                var x = await Database.InsertAsync(newDino);
                testList.Add(newDino);
                try
                {
                    await Database.InsertAllAsync(dino.CanDamage.Select(nl => ToCanDamageDTO(nl, newDino.Id)));
                    await Database.InsertAllAsync(dino.ImmobilizedBy.Select(imob => ToImmobilizedByDTO(imob, newDino.Id)));
                    await Database.InsertAllAsync(dino.CarryableBy.Select(cryble => ToCarryableDTO(cryble, newDino.Id)));
                    await Database.InsertAllAsync(dino.SpawningMaps.Select(smaps => ToSpawningMapDTO(smaps, newDino.Id)));
                    await Database.InsertAllAsync(dino.SpawnCommands.Select(scs => ToSpawningCommandsDTO(scs, newDino.Id)));
                    await Database.InsertAsync(ToBaseStatsDTO(dino.BaseStats, newDino.Id));

                    foreach (var grid in dino.TamingGrids)
                    {
                        var newGrid = new KnockoutTamingGridDTO()
                        {
                            CreatureId = newDino.Id,
                            Level = grid.Level
                        };

                        await Database.InsertAsync(newGrid);
                        
                        for (int m = 0; m < grid.FoodRequired.Food.Count; m++)
                        {
                            await Database.InsertAsync(new FoodRequiredDTO() { KnockoutTamingGridId = newGrid.Id, Food = grid.FoodRequired.Food[m], FoodCount = grid.FoodRequired.FoodCount[m] });
                        }
                    }

                } catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                Console.WriteLine(testList);
            }
        }

        private static SpawnCommandDTO ToSpawningCommandsDTO(string scs, int id)
        {
            return new SpawnCommandDTO()
            {
                CreatureId = id,
                SpawnCommand = scs
            };
        }

        private static BaseStatsDTO ToBaseStatsDTO(BaseStats baseStats, int id)
        {
            return new BaseStatsDTO()
            {
                Health = baseStats.Health,
                Stamina = baseStats.Stamina,
                Oxygen = baseStats.Oxygen,
                MeleeDamage = baseStats.MeleeDamage,
                Food = baseStats.Food,
                MovementSpeed = baseStats.MovementSpeed,
                Torpidity = baseStats.Torpidity,
                Weight = baseStats.Weight,
                CreatureId = id
            };
        }

        private static object ToSpawningMapDTO(SpawningMap smaps, int id)
        {
            return new SpawningMapDTO()
            {
                CreatureId = id,
                MapImage = smaps.MapImage,
                OverlayImage = smaps.OverlayImage
            };
        }

        private static async Task<KnockoutTamingGridDTO> ToTamingGridDTO(KnockoutTamingGrid tgrids, int id)
        {
            var newGrid = new KnockoutTamingGridDTO()
            {
                CreatureId = id,
                Level = tgrids.Level
            };
            await Database.InsertAsync(newGrid);

            int i = 0;
            //for (int i = 0; id < tgrids.FoodRequired.FoodCount.Count; i++)
            foreach(var foodCount in tgrids.FoodRequired.Food)
            {
                var frDTO = new FoodRequiredDTO() { KnockoutTamingGridId = newGrid.Id, Food = tgrids.FoodRequired.Food[i], FoodCount = tgrids.FoodRequired.FoodCount[i] };
                await Database.InsertAsync(frDTO);
                i++;
            }
            return newGrid;
        }


        private static ImmobilizedByDTO ToImmobilizedByDTO(string imob, int id)
        {
            return new ImmobilizedByDTO() { CreatureId = id, Name = imob };
        }

        private static CarryableByDTO ToCarryableDTO(string cryble, int id)
        {
            return new CarryableByDTO() { CreatureId = id, Name = cryble };
        }

        private static CanDamageDTO ToCanDamageDTO(string nl, int id)
        {
            return new CanDamageDTO() { CreatureId = id, Name = nl };
        }

        private static CreatureDTO ToDinoDTO(Creature dino)
        {
            var newCritter = new CreatureDTO()
            {
                Name = dino.Name,
                Breedable = dino.Breedable,
                Diet = dino.Diet,
                Egg = dino.Egg,
                Group = dino.Group,
                ImageUrl = dino.ImageUrl,
                Rideable = dino.Rideable,
                Tameable = dino.Tameable,
                Temperament = dino.Temperament,
                TamingMethod = dino.TamingMethod,
                TorporImmune = dino.TorporImmune,
                PreferredFood = dino.PreferredFood
            };

            return newCritter;
        }

        private static async Task PopulateResourceMapDatabase()
        {
            var count = await Database.Table<NotableLocationDTO>().CountAsync();
            if (count < 1)
            {


                var assembly = IntrospectionExtensions.GetTypeInfo(typeof(ResourceMapCollection)).Assembly;
                XmlSerializer serializer = new XmlSerializer(typeof(ResourceMapCollection));
                ResourceMapCollection rmc = new ResourceMapCollection();
                using (Stream reader = assembly.GetManifestResourceStream("ArkGuide.ResourceMaps.xml"))
                {
                    var resourceMapCollection = (ResourceMapCollection)serializer.Deserialize(reader);

                    rmc = resourceMapCollection;
                }

                foreach(var map in rmc.Maps)
                {
                    var newMap = ToResourceMapDTO(map);
                    await Database.InsertAsync(newMap);
                    
                    await Database.InsertAllAsync(map.NotableLocations.Select(nl => ToNotableLocationDTO(nl, newMap.Id)));
                }
                
                
            }
        }

        private static NotableLocationDTO ToNotableLocationDTO(NotableLocation m, int mapId)
        {
            return new NotableLocationDTO()
            {
                LocationCategory = m.LocationName, 
                ResourceMapId = mapId,
                Lat = m.Lat,
                Long = m.Long
            };
        }

        private static ResourceMapDTO ToResourceMapDTO(ResourceMap map)
        {
            return new ResourceMapDTO()
            {
                MapName = map.MapName,
                MapImage = map.MapImage
            };
        }

        public ArkGuideDatabase()
        {
            Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        }

        public async Task<IEnumerable<Creature>> GetAllCreatures()
        {
            var allCreatures = await Database.Table<CreatureDTO>().ToListAsync();
            //var list = allCreatures.Select(async (c) => await ToCreature(c)).ToList();
            var list = new List<Creature>();
            foreach(var cr in allCreatures)
            {
                list.Add(await ToCreature(cr));
            }
            return list;
        }

        private async Task<Creature> ToCreature(CreatureDTO critter)
        {
            var newCreature =  new Creature()
            {
                Name = critter.Name,
                Diet = critter.Diet,
                Breedable = critter.Breedable,
                Group = critter.Group,
                Tameable = critter.Tameable,
                TamingMethod = critter.TamingMethod,
                Temperament = critter.Temperament,
                TorporImmune = critter.TorporImmune,
                Rideable = critter.Rideable,
                ImageUrl = critter.ImageUrl,
                PreferredFood = critter.PreferredFood
            };

            newCreature.SpawningMaps = await GetCreatureSpawnMaps(critter.Id);
            newCreature.SpawnCommands = await GetCreatureSpawnCommands(critter.Id);
            newCreature.BaseStats = await GetCreatureBaseStats(critter.Id);

            return newCreature;
        }

        private async Task<BaseStats> GetCreatureBaseStats(int id)
        {
            var stats = await Database.Table<BaseStatsDTO>().Where(sm => sm.CreatureId == id).FirstOrDefaultAsync();
            return ToBaseStats(stats);
        }

        private BaseStats ToBaseStats(BaseStatsDTO m)
        {
            return new BaseStats()
            {
                Health = m.Health,
                Stamina = m.Stamina,
                Oxygen = m.Oxygen,
                Food = m.Food,
                MeleeDamage = m.MeleeDamage,
                MovementSpeed = m.MovementSpeed,
                Torpidity = m.Torpidity,
                Weight = m.Weight
            };
        }

        private async Task<List<string>> GetCreatureSpawnCommands(int id)
        {
            var commands = await Database.Table<SpawnCommandDTO>().Where(sm => sm.CreatureId == id).ToListAsync();
            return commands.Select(m => m.SpawnCommand).ToList();
        }


        private async Task<List<SpawningMap>> GetCreatureSpawnMaps(int id)
        {
            var maps = await Database.Table<SpawningMapDTO>().Where(sm => sm.CreatureId == id).ToListAsync();
            return maps.Select(m => ToSpawnMap(m)).ToList();
        }

        private SpawningMap ToSpawnMap(SpawningMapDTO m)
        {
            return new SpawningMap()
            {
                MapImage = m.MapImage,
                OverlayImage = m.OverlayImage
            };
        }

        public async Task<ResourceMap> GetMapAsync(string mapName)
        {
            var maps = await Database.Table<ResourceMapDTO>().ToListAsync();
            ResourceMapDTO foundMap = await Database.Table<ResourceMapDTO>().Where(x => x.MapName == mapName).FirstOrDefaultAsync();
            return await ToResourceMap(foundMap);
        }

        private async Task<ResourceMap> ToResourceMap(ResourceMapDTO foundMap)
        {
            var newMap = new ResourceMap();
            if (foundMap.MapName.Contains("Genesis"))
            {

                var x = foundMap.MapName.Replace(" ", "_").Replace("-", "").Replace("__", "_") + ".jpg";
                
                newMap = new ResourceMap()
                {
                    //MapImage = foundMap.MapName.Replace(" ", "_").Replace("-","") + ".jpg",
                    MapImage = x,
                    MapName = foundMap.MapName
                };
            } else
            {

                newMap = new ResourceMap()
                {
                    MapImage = foundMap.MapName.Replace(" ", "_") + ".jpg",
                    MapName = foundMap.MapName
                };
            }

            var nl = await Database.Table<NotableLocationDTO>().Where(l => l.ResourceMapId == foundMap.Id).ToListAsync();
            newMap.NotableLocations = nl.Select(l => ToNotableLocation(l)).ToList();
            return newMap;
        }

        private NotableLocation ToNotableLocation(NotableLocationDTO nl)
        {
            return new NotableLocation()
            {
                LocationName = nl.LocationCategory,
                Lat = nl.Lat,
                Long = nl.Long
            };
        }

        public async Task<Creature> GetCreatureByName(string name)
        {
            var creature = await Database.Table<CreatureDTO>().Where(c => c.Name == name).FirstOrDefaultAsync();

            return await ToCreature(creature);
        }
    }
}
