using Core.Data;
using Zenject;

namespace Core.UI.Counters
{
    public class PointCounter : Counter
    {
        [Inject] private PlayerDataModule _playerDataModule;

        public override void UpdateValue()
        {
            SetValue(_playerDataModule.CurrentData.Point);
        }
    }
}