namespace Cwiczenia_2_Kontenery;

public class ContainerG : Container
{


    public ContainerType type = ContainerType.G;
    
    public ContainerG(double loadWeightKg, double heightCm, double containerWeightKg, double depthInCm, double maxConWeight) : 
        base(loadWeightKg, heightCm, containerWeightKg, depthInCm, maxConWeight)
    {
        int idNumber = SerialCounter;
        
        serialNumber = "KON-" + type +"-"+ idNumber;
    }
}