using System;
using System.Collections.Generic;
using System.Text;

namespace TP_3.m
{
    public abstract class Specifications
    { // Абстракный класс для всех типов получаемых данных для анализа
        public abstract Object Get();
        public abstract void Set(Object s);
    }
    public class Date : Specifications // Дата 
    {
        private DateTime date;
        public Date() { }
        public override Object Get()
        {
            return date;
        }
        public override void Set (Object date)
        {
            this.date = Convert.ToDateTime(date.ToString());
        }
    }
    public class Inflation : Specifications // Данные по инфляции
    {
        private float inflation;
        public Inflation() { }
        public override object Get()
        {
            return inflation;
        }
        public override void Set(object inflation)
        {
            this.inflation = float.Parse(inflation.ToString());
        }
    }
}
