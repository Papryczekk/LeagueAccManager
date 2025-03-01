using System.Collections.Generic;
using System.Drawing;

namespace LeagueAccManager
{
    public class Regions
    {
        public static List<string> GetRegions()
        {
            return new List<string>
            {
                "EUW",
                "EUNE",
                "NA",
                "BR",
                "LAN",
                "LAS",
                "OCE",
                "KR",
                "JP",
                "TR",
                "RU"
            };
        }
    }
}