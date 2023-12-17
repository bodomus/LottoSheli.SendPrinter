using LiteDB;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LottoSheli.SendPrinter.Settings.Adapters
{
    public abstract class BaseAdapter<T>
    {
        protected abstract string _fileName { get; }
        protected virtual string _settingsDir => Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Settings");
        protected virtual string _settingsFile => Path.Combine(_settingsDir, $"{_fileName}.json");

        protected readonly SettingsStore _settingsStore;
        protected readonly SettingsHelper _settingsHelper;

        protected T _currentSettings { get; set; }
        

        public BaseAdapter() 
        { 
            _settingsStore = SettingsStore.Instance;
            _settingsHelper = new SettingsHelper();
            Load();
        }

        public virtual void Load() 
        {
            _currentSettings = _settingsStore.HasSettings(_fileName) 
                ? _settingsStore.GetSettings<T>(_fileName) 
                : _settingsHelper.LoadJson<T>(_settingsFile);
        }

        public virtual void Save() 
        {
            _settingsStore.SaveSettings<T>(_currentSettings, _fileName);
        }

        public virtual void Reset() 
        {
            _settingsStore.ClearSettings(_fileName);
            Load();
        }
    }
}
