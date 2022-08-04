using Core.Data;
using Zenject;

namespace Core.UI.Counters
{
    public class ScoreCounter : Counter
    {
        [Inject] private PlayerDataModule _playerDataModule;

        public override void UpdateValue()
        {
            SetValue(_playerDataModule.CurrentData.Score);
        }
    }
}