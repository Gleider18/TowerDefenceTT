using Towers;

namespace Databases
{
    public interface ITowersDatabase
    {
        TowerVo GetTowerById(int id);
        TowerPlaceController GetTowerPlace();
    }
}