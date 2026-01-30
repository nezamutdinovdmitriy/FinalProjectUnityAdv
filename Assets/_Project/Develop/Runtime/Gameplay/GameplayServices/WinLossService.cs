using Assets._Project.Develop.Runtime.Utilities.DataManagment;
using Assets._Project.Develop.Runtime.Utilities.DataManagment.DataProviders;

namespace Assets._Project.Develop.Runtime.Gameplay.GameplayServices
{
    public class WinLossService : IDataReader<PlayerData>, IDataWriter<PlayerData>
    {
        private int _totalWins;
        private int _totalLosses;

        private PlayerDataProvider _playerDataProvider;

        public WinLossService(PlayerDataProvider playerDataProvider)
        {
            _playerDataProvider = playerDataProvider;

            _playerDataProvider.RegisterWriter(this);
            _playerDataProvider.RegisterReader(this);
        }

        public int TotalWins => _totalWins;
        public int TotalLosses => _totalLosses;

        public void AddWins() => _totalWins++;
        public void AddLosses() => _totalLosses++;

        public void Reset()
        {
            _totalWins = 0;
            _totalLosses = 0;
        }

        public void ReadFrom(PlayerData data)
        {
            _totalWins = data.TotalWins;
            _totalLosses = data.TotalLosses;
        }

        public void WriteTo(PlayerData data)
        {
            data.TotalWins = _totalWins;
            data.TotalLosses = _totalLosses;
        }
    }
}