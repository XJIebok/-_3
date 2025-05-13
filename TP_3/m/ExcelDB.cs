using System;
using System.Collections.Generic;
using System.Text;
using ClosedXML.Excel;
//using System.Windows.Forms;

namespace TP_3.m
{
    class ExcelDB
    {
        static public List<StatisticalData> GetDataFromExcel(StatisticalData data, string path)
        {
            List<StatisticalData> statisticalDatas = new List<StatisticalData>();

            data.CreateStatistic(); // На случай, если ранее не создавалось

            using (var workbook = new XLWorkbook(path)) // Открыть Excel файл
            {
                // Выбор листа в соответствии с названием класса
                var worksheet = workbook.Worksheet(data.GetType().Name);
                // Размеры листа
                var range = worksheet.RangeUsed();
                int rowCount = range.RowCount(); // Количество строк в выбранном листе
                int columnCount = range.ColumnCount();

                Dictionary<string, int> columns = new Dictionary<string, int>();
                foreach (Specifications specification in data.StatisticData)
                {
                    int dataColumn = 0;
                    for (int column = 1; column < columnCount; column++)
                    {
                        if (specification.GetType().Name == worksheet.Cell(1, column).Value.ToString())
                        {
                            dataColumn = column;
                            break;
                        }
                    }
                    if (dataColumn != 0)
                        columns.Add(specification.GetType().Name, dataColumn);
                }
                for (int row = 2; row < rowCount; row++)
                {
                    StatisticalData _data = FindClassByName(data.GetType().Name);
                    _data.CreateStatistic();

                    foreach (Specifications specification in _data.StatisticData)
                    {
                        //MessageBox.Show(worksheet.Cell(row, columns[specification.GetType().Name]).Value.ToString());
                        specification.Set(worksheet.Cell(row, columns[specification.GetType().Name]).Value);
                    }
                    statisticalDatas.Add(_data);
                }
            }

            return statisticalDatas;
        }
        public static StatisticalData FindClassByName(string type) // Поиск класса по названию
        {
            switch (type) // Дополнять при увеличении кол-ва классов
            {
                case "InflationData":
                    return new InflationData();
            }
            return null;
        }
    }
}
