using Application.Models;

namespace Application.Interfaces;
using Application.Modeles;

public interface IConfiguration
{
    /// <summary>
    /// Имя com порта
    /// </summary>
    public string PortName { get; set; }
    /// <summary>
    /// Скорость порта
    /// </summary>
    public int BaudRate { get; set; }
    public List<OwnCounter> Counters { get; set; }
    
    

}