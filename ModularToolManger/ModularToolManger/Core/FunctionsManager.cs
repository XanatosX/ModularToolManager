using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using JSONSettings;

namespace ModularToolManger.Core
{
    public class FunctionsManager
    {
        private List<Function> _allFunctions;
        private List<Function> _availablefunctions;
        private List<string> _allowedTypes;
        public List<Function> Functions
        {
            get
            {
                return _availablefunctions;
            }
        }

        private string _saveFile;
        private int _nextSortingSequence;
        public int NextSortingSequence => _nextSortingSequence;

        public FunctionsManager(string SaveFile, List<string> allowedTypes)
        {
            _saveFile = SaveFile;
            _allowedTypes = allowedTypes;
            _allFunctions = new List<Function>();
            _availablefunctions = new List<Function>();
            _nextSortingSequence = 0;
        }

        public void Load()
        {
            if (!File.Exists(_saveFile))
                return;
            using (StreamReader writer = new StreamReader(_saveFile))
            {
                FunctionsRoot data = JsonConvert.DeserializeObject<FunctionsRoot>(writer.ReadToEnd());
                _allFunctions = data.Functions;
                foreach (Function currentFunction in data.Functions)
                {
                    if (_allowedTypes.Contains(currentFunction.Type))
                    {
                        _availablefunctions.Add(currentFunction);
                    }
                       
                }
            }


        }

        public void Save()
        {
            FunctionsRoot root = new FunctionsRoot();
            root.Functions = _allFunctions;
            string dicretory = new FileInfo(_saveFile).DirectoryName;
            if (!Directory.Exists(dicretory))
                Directory.CreateDirectory(dicretory);

            using (StreamWriter writer = new StreamWriter(_saveFile))
            {
                string sData = JsonConvert.SerializeObject(root, Formatting.Indented);
                writer.Write(sData);
            }
        }

        public void AddNewFunction(Function F)
        {
            _availablefunctions.Add(F);
            _allFunctions.Add(F);
        }

        public void DeleteFunction(Function F)
        {
            _availablefunctions.Remove(F);
            _allFunctions.Remove(F);
            Save();
        }
    }




}
