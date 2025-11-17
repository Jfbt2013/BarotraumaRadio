using System;
using System.Collections.Generic;
using System.Text;

namespace BarotraumaRadio
{
    public record ClientRadioConfig(int LastPlayedIndex, float Volume, bool ServerSync, bool? IsPlaying = false);
    public record ServerRadioConfig(string LastPlayedUrl, bool? IsPlaying = false);
}
