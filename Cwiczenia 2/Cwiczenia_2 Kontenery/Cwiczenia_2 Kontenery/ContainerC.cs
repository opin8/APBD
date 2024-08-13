namespace Cwiczenia_2_Kontenery;

public class ContainerC : Container
{



    public ContainerType type = ContainerType.C;
    public ContainerC(double loadWeightKg, double heightCm, double containerWeightKg, double depthInCm, double maxConWeight) 
        : base(loadWeightKg, heightCm, containerWeightKg, depthInCm, maxConWeight)
    {
        int idNumber = SerialCounter;
        
        serialNumber = "KON-" + type +"-"+ idNumber;

    }
}