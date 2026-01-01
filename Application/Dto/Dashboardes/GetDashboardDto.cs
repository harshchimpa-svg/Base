namespace Application.Dto.Dashboardes;

public class GetDashboardDto
{
    public int TotalAmount { get; set; }
    public int Salery { get; set; } 
    public int ChairCount { get; set; }


    public double AvgRoomsPerHouse { get; set; }
    public double AvgChairsPerRoom { get; set; }
    public double AvgBedsPerRoom { get; set; }
    public double AvgMembersPerHouse { get; set; }
}

