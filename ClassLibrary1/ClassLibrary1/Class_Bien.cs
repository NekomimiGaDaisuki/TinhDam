using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;

namespace ClassLibrary1
{
    internal class Class_Bien
    {
        public static Element Beam ;
        public static double B = 0;
        public static double H = 0;
        public static double L = 0;
        public static double N_goi = 0;
        public static double Q_goi = 0;
        public static double M_goi = 0;
        public static double N_nhip = 0;
        public static double Q_nhip = 0;
        public static double M_nhip = 0;
        public static double N_goi2 = 0;
        public static double Q_goi2 = 0;
        public static double M_goi2 = 0;

        public static double Rbt = 0;
        public static double Rb = 0;
        public static double Rs = 0;
        public static List<double> lstAsGoi = new List<double>();  
        public static List<double> lstAsNhip = new List<double>();
        public static List<double> lstAsGoi2 = new List<double>();

        public static double nthepdocduoi = 2;
        public static double nthepdoctren = 2;


    }
}
