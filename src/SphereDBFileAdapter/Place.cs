namespace Linq2SphereDBProvider
{
    public class Place
    {
        // Properties. 
        public string Name { get; private set; }
        public string State { get; private set; }
        public PlaceType PlaceType { get; private set; }

        // Constructor. 
        internal Place(string name,
                        string state,
                      SphereDBQueryProvider.TerraServerReference.PlaceType placeType)
        {
            Name = name;
            State = state;
            PlaceType = (PlaceType)placeType;
        }
    }
}