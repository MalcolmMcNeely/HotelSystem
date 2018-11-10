using Guests.ViewModels;
using Prism.Events;

namespace Guests.Events
{
    internal class GuestUpdatedEvent : PubSubEvent<GuestViewModel>
    {
    }
}
