using BetsKing.Shared.ViewModels.Gambler;

namespace BetsKing.Shared.ViewModels
{
    public class StaticClientInfoViewModel
    {
        public GamblerViewModel Gambler { get; set; } = new GamblerViewModel();

        public void SetGamblerData(GamblerViewModel gambler)
        {
            Gambler.Id = gambler.Id;
            Gambler.DisplayName = gambler.DisplayName;
            Gambler.UserName = gambler.UserName;
            Gambler.Roles = gambler.Roles;
            Gambler.IsAdmin = gambler.IsAdmin;
        }
    }
}
