using HotelSystem.Infrastructure.WPF.MVVM;

namespace Guests.ViewModels
{
    public interface ICreateUpdateGuestViewModel : IViewModel
    {
        GuestViewModel Model { get; set; }
    }
}
