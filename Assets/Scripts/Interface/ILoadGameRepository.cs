using Cysharp.Threading.Tasks;
using Jing.Setting;

namespace Jing.Game
{
    public interface ILoadGameRepository
    {
        UniTask Load();
        T GetItem<T>(int id) where T : Setting_ItemBase;
        T[] GetAllItems<T>() where T : Setting_ItemBase;
    }
}