using System;

namespace Assets.LogicSystem.GameSave
{
    public class Chest
    {
        public static Chest CreateFromRuntime(ChestData data)
        {
            var result = new Chest
            {
                LastOpened = data.LastOpened,
                Name = data.name
            };
            return result;
        }

        public static void CreateFromSavedData(ref ChestData data, Chest chest)
        {
            data.LastOpened = chest.LastOpened;
        }

        public DateTime LastOpened { get; set; }
        public string Name { get; set; }
    }
}