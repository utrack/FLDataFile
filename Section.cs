using System;
using System.Collections.Generic;
using System.Linq;

namespace FLDataFile
{
    public class Section
    {
        public List<Setting> Settings = new List<Setting>();
        private readonly Dictionary<string, Setting> _setDictionary = new Dictionary<string, Setting>(); 
        public string Name { get; set; }

        public Section(string name)
        {
            Name = name;
        }

        public Section(string name, string bufBytes)
        {
            Name = name;

            var sets = bufBytes.Split(new []{'\n'},StringSplitOptions.RemoveEmptyEntries);

            foreach (var str in sets.Select(set => set.Split('=')).Where(str => str[0].Trim()[0] != ';'))
            {
                Settings.Add(new Setting(str[1],str[0].Trim()));
            }
        }


        /// <summary>
        /// Returns first setting with this name. Speeds up consequentive calls if used.
        /// </summary>
        /// <param name="name">Name of the setting.</param>
        /// <returns>Setting class.</returns>
        public Setting GetFirstOf(string name)
        {
            if (name == null) return null;
            if (!_setDictionary.ContainsKey(name))
                _setDictionary[name] = Settings.FirstOrDefault(a => a.Name == name);

            return _setDictionary[name];
        }


        /// <summary>
        /// Try to get first setting with the name defined and store it in setting value.
        /// </summary>
        /// <param name="name">Name of the setting</param>
        /// <param name="setting">Where to store the setting.</param>
        /// <returns>True if succeeds, otherwise false.</returns>
        public bool TryGetFirstOf(string name, out Setting setting)
        {
            setting = GetFirstOf(name);
            return setting != null;
        }

        /// <summary>
        /// Returns any setting found with the names provided. At least two names needed, obviously.
        /// </summary>
        /// <param name="setting1"></param>
        /// <param name="setting2"></param>
        /// <param name="setting3"></param>
        /// <param name="setting4"></param>
        /// <returns>First setting found, or null if none found.</returns>
        public Setting GetAnySetting(string setting1, string setting2,string setting3 = null, string setting4 = null)
        {
            var gf1 = GetFirstOf(setting1);
            if (gf1 != null) return gf1;
            var gf2 = GetFirstOf(setting2);
            if (gf2 != null) return gf2;
            var gf3 = GetFirstOf(setting3);
            if (gf3 != null) return gf3;
            var gf4 = GetFirstOf(setting4);
            if (gf4 != null) return gf4;
            return null;
        }

        /// <summary>
        /// Returns all settings matching the name specified.
        /// </summary>
        /// <param name="name">Setting name</param>
        /// <returns></returns>
        public IEnumerable<Setting> GetSettings(string name)
        {
            return Settings.Where(a => a.Name == name);
        } 
    }



}
