using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Core.Events;
using Core.SaveLoad;
using UnityEngine;
using Zenject;

namespace Core.Data
{
    public class PlayerDataModule : ISaveModel , ILoadModel
    {
        private SignalBus _signalBus;
        public GameData CurrentData { private set; get;}
        
        private readonly string _dataPath = Application.persistentDataPath + "/gamesave.save";

        public PlayerDataModule(SaveLoadModel _saveLoadModel,SignalBus signalBus)
        {
            _signalBus = signalBus;
            _saveLoadModel.AddLoadModel(this);
            _saveLoadModel.AddSaveModel(this);
            Configurate();
        }

        private void Configurate()
        {
            _signalBus.Subscribe<OnEnemyDestroySignal>(PointUpdate);
            _signalBus.Subscribe<ScoreUpdateSignal>(ScoreUpdate);
        }

        private void PointUpdate()
        {
            CurrentData.Point++;
            _signalBus.Fire<PlayerDataUpdateSignal>();
        }

        private void ScoreUpdate(ScoreUpdateSignal scoreUpdateSignal)
        {
            CurrentData.Score += scoreUpdateSignal.Score;
            _signalBus.Fire<PlayerDataUpdateSignal>();
        }
        

        #region SaveLoad
        public void Save()
        {
            var bf = new BinaryFormatter();
            var file = File.Create(_dataPath);
            bf.Serialize(file, CurrentData);
            file.Close();
        }
        
        public void Load()
        {
            if (!File.Exists(_dataPath))
            {
                return;
            }
            var bf = new BinaryFormatter();
            var file = File.Open(_dataPath, FileMode.Open);
            CurrentData = (GameData)bf.Deserialize(file);
            file.Close();
            
            _signalBus.Fire<PlayerDataUpdateSignal>();
        }
        #endregion
    }
}