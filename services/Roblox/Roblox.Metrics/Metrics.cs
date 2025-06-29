using InfluxDB.Client;
using InfluxDB.Client.Api.Domain;
using InfluxDB.Client.Core;
using InfluxDB.Client.Writes;
using Roblox.Logging;

namespace Roblox.Metrics;

public static class RobloxInfluxDb
{
    public static InfluxDBClient? client { get; set; }
    const string Bucket = "roblox-website-v2";
    const string Org = "Roblox";
    
    public static void Configure(string baseUrl, string websiteToken)
    {
        client = InfluxDBClientFactory.Create(baseUrl, websiteToken);
    }

    public static List<PointData> points { get; set; } = new();
    private static readonly Mutex PointsMutex = new();
    public static bool pointUploaderRunning { get; set; }

    public static void StartWriterTask()
    {

    }

    public static void WritePointInBackground(PointData point)
    {

    }
    
    public static async Task WritePointAsync(PointData point)
    {

    }
    
    public static async Task WriteMeasurement(Measurement data)
    {

    }
}