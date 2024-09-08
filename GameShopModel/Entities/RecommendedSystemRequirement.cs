using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameShopModel.Entities;

public class RecommendedSystemRequirement
{
    public int Id { get; set; }
    public required string OS { get; set; }
    public required string Processor { get; set; }
    public required string RandomAccessMemory { get; set; }
    public required string VideoCard { get; set; }
    public required string DirectX { get; set; }
    public required string DiskSpace { get; set; }
    public required string? SoundCard { get; set; }
    public required string? Network { get; set; }
    public required string? Additionally { get; set; }
}
