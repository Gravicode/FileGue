using FileGue.Helpers;

namespace FileGue.Data
{
    public class AppState
    {

        public event Action<string> OnProfileChange;

        public event Action<string, GeoLocation> OnLocationChange;


        public void RefreshProfile(string username)
        {
            ProfileStateChanged(username);
        }


        public void SelectLocation(string username, GeoLocation loc)
        {
            LocationStateChanged(username, loc);
        }


        private void ProfileStateChanged(string username) => OnProfileChange?.Invoke(username);

        private void LocationStateChanged(string username, GeoLocation loc) => OnLocationChange?.Invoke(username, loc);

    }
}
