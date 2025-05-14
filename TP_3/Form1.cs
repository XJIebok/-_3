using System;
using System.Collections.Generic;
using System.IO;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TP_3.m;

namespace TP_3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
        }
        private void button_test_Click(object sender, EventArgs e)
        {
            string filePath = "";
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                string pathNow = Directory.GetCurrentDirectory();
                pathNow = pathNow.Remove(pathNow.Length - 29);
                openFileDialog.InitialDirectory = pathNow;
                
                openFileDialog.Filter = "Excel Files|*.xls;*xlsx;*.xlsm";
                //openFileDialog.FilterIndex = 2;
                //openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    filePath = openFileDialog.FileName;
                }
            }
            StatisticalData inflationData = ExcelDB.FindClassByName("InflationData");
            
            List<StatisticalData> sd = ExcelDB.GetDataFromExcel(inflationData, filePath);
            //MessageBox.Show(String.Join(" -> ", sd));
            
            // Вывод данных на таблицу в форме
            DataTable dataTable = new DataTable();

            foreach (Specifications specification in sd[0].StatisticData)
                dataTable.Columns.Add(specification.GetType().Name);
            foreach (StatisticalData data in sd)
            {
                int i = data.StatisticData.Count;
                string[] arr = { };
                Array.Resize(ref arr, i);
                for(int s = 0; s < data.StatisticData.Count; s++)
                {
                    arr[s] = data.StatisticData[s].ToString();
                }
                dataTable.Rows.Add(arr);
            }
            dataGridView1.DataSource = dataTable;
        }
    }
}
