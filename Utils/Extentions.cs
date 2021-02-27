using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace TransactonDiscovery.Utils
{
    public static class Extentions
    {

        public static string ToCSV(this Dictionary<string, double> data, List<string> colNames)
        {
            string csv = string.Join(",", colNames) + Environment.NewLine;
            csv += string.Join(Environment.NewLine, data.Select(d => $"{d.Key},{d.Value}"));
            return csv;
        }

        public static Stream ToStream(this string data)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(data);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }
    }
}
