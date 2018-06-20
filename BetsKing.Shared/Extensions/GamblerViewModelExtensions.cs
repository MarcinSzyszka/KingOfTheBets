using BetsKing.Shared.ViewModels.Gambler;
using System.Linq;

namespace BetsKing.Shared.Extensions
{
    public static class GamblerViewModelExtensions
    {
        public static bool IsAdmin(this GamblerViewModel model)
        {
            return model != null && model.Roles != null && model.Roles.Contains("Admin");
        }
    }
}
