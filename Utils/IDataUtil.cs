using System;
using System.Collections.Generic;

namespace dsi_coding_challenge.Utils
{
    public interface IDataUtil
    {
        Dictionary<string, List<GeoName>> GetData();
    }
}
