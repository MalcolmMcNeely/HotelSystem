using HotelSystem.Data.StaticData;

namespace HotelSystem.Business
{
    public static class StaticData
    {
        public static void EnsureGuestsAreInDatabase()
        {
            var staticData = new StaticGuestData();
            staticData.EnsureGuestsAreInDatabase();
        }
    }
}
