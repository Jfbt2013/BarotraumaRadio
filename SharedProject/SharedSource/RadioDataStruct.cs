using Barotrauma;

namespace BarotraumaRadio
{
    public class RadioDataStruct : INetSerializableStruct
    {
        [NetworkSerialize]
        public int? RadioID;
        [NetworkSerialize]
        public string? StringParamValue;
        [NetworkSerialize]
        public bool? BooleanParamValue;

        public RadioDataStruct(int radioID, string? stringParamValue = null)
        {
            RadioID = radioID;
            StringParamValue = stringParamValue;
        }

        public RadioDataStruct(int radioID, string? stringParamValue = null, bool? booleanParamValue = null)
        {
            RadioID = radioID;
            StringParamValue = stringParamValue;
            BooleanParamValue = booleanParamValue;
        }

        public RadioDataStruct()
        {
        }
    };
}
