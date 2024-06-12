using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using лаба10;

namespace ClassLibrary1
{
    public class TestCollections
    {
        private List<ElectricGuitar> electricGuitars;
        private SortedDictionary<string, Piano> pianos;

        public TestCollections()
        {
            electricGuitars = new List<ElectricGuitar>();
            pianos = new SortedDictionary<string, Piano>();

            // Генерация элементов коллекций
            for (int i = 0; i < 1000; i++)
            {
                electricGuitars.Add(new ElectricGuitar($"Гитара {i}", "Производитель", 6, 2));
                pianos.Add($"Фортепиано {i}", new Piano(88, "Производитель", "Гранд", i));
            }
        }
        public void MeasureSearchTime()
        {
            // Измерение времени поиска элементов
            var stopwatch = new Stopwatch();

            // Поиск первого элемента
            stopwatch.Start();
            bool foundFirstGuitar = electricGuitars.Contains(electricGuitars[0]);
            stopwatch.Stop();
            Console.WriteLine($"Поиск первой гитары занял {stopwatch.ElapsedTicks} тиков. Элемент найден: {foundFirstGuitar}");

            // Поиск центрального элемента
            stopwatch.Restart();
            bool foundCentralPiano = pianos.ContainsKey($"Фортепиано {pianos.Count / 2}");
            stopwatch.Stop();
            Console.WriteLine($"Поиск центрального фортепиано занял {stopwatch.ElapsedTicks} тиков. Элемент найден: {foundCentralPiano}");

            // Поиск последнего элемента
            stopwatch.Restart();
            bool foundLastGuitar = electricGuitars.Contains(electricGuitars[electricGuitars.Count - 1]);
            stopwatch.Stop();
            Console.WriteLine($"Поиск последней гитары занял {stopwatch.ElapsedTicks} тиков. Элемент найден: {foundLastGuitar}");

            // Поиск элемента, не входящего в коллекцию
            stopwatch.Restart();
            bool foundMissingPiano = pianos.ContainsKey("Отсутствующее фортепиано");
            stopwatch.Stop();
            Console.WriteLine($"Поиск отсутствующего фортепиано занял {stopwatch.ElapsedTicks} тиков. Элемент найден: {foundMissingPiano}");
        }
    }
}
    
