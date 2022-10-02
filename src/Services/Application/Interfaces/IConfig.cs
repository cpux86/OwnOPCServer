using Application.Models;

namespace Application.Interfaces;
using Application.Modeles;

public interface IConfig
{
    /// <summary>
    /// Имя com порта
    /// </summary>
    public string Port { get; set; }
    /// <summary>
    /// Скорость порта
    /// </summary>
    public int BaudRate { get; set; }
    public List<OwnCounter> Counters { get; set; }
    
    

}