using System;
using System.Collections.Generic;
using System.Text;

namespace TP_3.m
{
    public abstract class StatisticalData // Общий вид данных
    {
        public List<Specifications> StatisticData { get; set; }
        public abstract void CreateStatistic(); // Создание списка данных для анализа
        public override string ToString()
        {
            string data = "";
            foreach (Specifications specification in StatisticData)
                data += specification.Get().ToString() + " ";
            return data;
        }
    }
    public class InflationData : StatisticalData // Данные по инфляции
    {
        public override void CreateStatistic()
        {
            StatisticData = new List<Specifications>
            {
                new Date(), // Дата получения данных
                new Inflation() // Значение инфляции на эту дату
            };
        }
    }
}
