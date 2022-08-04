using System.Collections.Generic;
using UnityEngine;

namespace Core.SaveLoad
{
    public class SaveLoadModel
    {
        private HashSet<ISaveModel> _saveModels = new();
        private HashSet<ILoadModel> _loadModels = new();

        public void AddSaveModel(ISaveModel saveModel) => _saveModels.Add(saveModel);
        public void AddLoadModel(ILoadModel saveModel) => _loadModels.Add(saveModel);

        public void Save()
        {
            Debug.Log("Start save process");
            foreach (var saveModel in _saveModels)
            {
                saveModel.Save();
            }
            Debug.Log("Finish save process");
        }
        
        public void Load()
        {
            Debug.Log("Start load process");
            foreach (var saveModel in _loadModels)
            {
                saveModel.Load();
            }
            Debug.Log("Finish load process");
        }
    }
}