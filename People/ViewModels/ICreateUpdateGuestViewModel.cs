using HotelSystem.Infrastructure.MVVM;

namespace Guests.ViewModels
{
    public interface ICreateUpdateGuestViewModel : IViewModel
    {
        GuestViewModel Model { get; set; }
    }
}
