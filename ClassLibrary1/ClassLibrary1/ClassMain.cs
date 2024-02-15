using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.UI.Selection;
using Autodesk.Revit.ApplicationServices;
using System.Collections.ObjectModel;
using System.Xml.Linq;
using System.Data.Common;
using System.Security.Claims;
using Autodesk.Revit.DB.Structure;
using System.Security.Cryptography;

namespace ClassLibrary1
{
    [TransactionAttribute(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]

    public class ClassMain : IExternalCommand
    {    
        UIApplication uiapp;
        UIDocument uidoc;
        Autodesk.Revit.ApplicationServices.Application app;
        Document doc;

        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            uiapp = commandData.Application;
            uidoc = uiapp.ActiveUIDocument;
            app = uiapp.Application;
            doc = uidoc.Document;
            FormMain frm = new FormMain();
            var dlg = frm.ShowDialog();
            //chọn dầm, lây dầm dầu tiên để tính toán
            if (dlg == System.Windows.Forms.DialogResult.OK)
            {
                List<Element> lstElement = SelectCeiling();
                Element element = lstElement[0];
                Parameter b = element.LookupParameter("b");
                Parameter h = element.LookupParameter("h");
                Parameter l = element.LookupParameter("Length");
                Class_Bien.Beam = element;
                Class_Bien.B = b.AsDouble() * 304.8 / 1000;
                Class_Bien.H = h.AsDouble() * 304.8 / 1000;
                Class_Bien.L = l.AsDouble() * 304.8 / 1000;
                //truyền kích thước dầm vào forrm
                frm.kichthuoc(b.AsDouble()*304.8/1000, h.AsDouble() * 304.8 / 1000, l.AsDouble() * 304.8 / 1000);
                dlg = frm.ShowDialog();
                //Execute(commandData, ref message, elements);
            }
            if (dlg == System.Windows.Forms.DialogResult.Yes)
            {

                double b = Class_Bien.B * 1000 / 304.8;
                double h = Class_Bien.H * 1000 / 304.8;
                double l = Class_Bien.L * 1000 / 304.8;
                double a = 30 / 304.8;
                //lấy dầm được chọn
                Element elm = Class_Bien.Beam;
                //lấy tọa độ gốc
                LocationCurve beamLocation = elm.Location as LocationCurve;
                Curve curvebeam = beamLocation.Curve;
                Line linewall = curvebeam as Line;
                XYZ spoint = linewall.GetEndPoint(0);
                XYZ epoint = linewall.GetEndPoint(1);
                //đường line để vẽ thép
                List<List<Curve>> curves = new List<List<Curve>>();

                List<Curve> lstcurve = new List<Curve>();   

                // vẽ 2 thanh thép dưới
                XYZ p1bot = new XYZ(spoint.X - b/2+a,spoint.Y,spoint.Z -h+a );
                XYZ p2bot = new XYZ(epoint.X - b/2+a, epoint.Y, epoint.Z -h+ a );
                if(Math.Round(linewall.Direction.X) == 1)
                {
                    p1bot = new XYZ(spoint.X , spoint.Y - b / 2 + a, spoint.Z - h + a);
                    p2bot = new XYZ(epoint.X , epoint.Y - b / 2 +a, epoint.Z - h + a);
                }
                XYZ p3bot = new XYZ(spoint.X + b / 2 - a, spoint.Y, spoint.Z - h + a);
                XYZ p4bot = new XYZ(epoint.X + b / 2 - a, epoint.Y, epoint.Z - h + a);
                if (Math.Round(linewall.Direction.X) == 1)
                {
                    p3bot = new XYZ(spoint.X, spoint.Y + b / 2 - a, spoint.Z - h + a);
                    p4bot = new XYZ(epoint.X, epoint.Y + b / 2 - a, epoint.Z - h + a);
                }
                lstcurve.Add(Line.CreateBound(p1bot, p2bot));
                lstcurve.Add(Line.CreateBound(p3bot, p4bot));
                

                //vẽ thép trên
                XYZ p1top = new XYZ(spoint.X - b / 2 + a, spoint.Y, spoint.Z - a);
                XYZ p2top = new XYZ(epoint.X - b / 2 + a, epoint.Y, epoint.Z - a);
                if (Math.Round(linewall.Direction.X) == 1)
                {
                    p1top = new XYZ(spoint.X, spoint.Y - b / 2 + a, spoint.Z  - a);
                    p2top = new XYZ(epoint.X, epoint.Y - b / 2 + a, epoint.Z  - a);
                }
                double kc = (b - 2 * a) / (Class_Bien.nthepdoctren - 1) ;

                for (int i = 0; i < Class_Bien.nthepdoctren; i++)
                {
                    double x = kc * i;
                    XYZ top1 = new XYZ(p1top.X  + x, p1top.Y, p1top.Z - a);
                    XYZ top2 = new XYZ(p2top.X  + x, p2top.Y, p2top.Z - a);
                    if (Math.Round(linewall.Direction.X) == 1)
                    {
                       top1 = new XYZ(p1top.X, p1top.Y + x, p1top.Z - a);
                       top2 = new XYZ(p2top.X, p2top.Y + x, p2top.Z - a);
                    }
                    lstcurve.Add(Line.CreateBound(top1, top2));

                }
                foreach (Curve curve1 in lstcurve)
                {
                    List<Curve> lstcurvefake = new List<Curve>();
                    lstcurvefake.Add(curve1);
                    curves.Add(lstcurvefake);

                }

                foreach (List<Curve> vector in curves)
                {
                    using (Transaction transaction = new Transaction(doc, "Add Rebar to Column"))
                    {
                        //vẽ thép theo thư viện thép có sẵn trong revit

                        transaction.Start();
                        RebarBarType kieuthep = new FilteredElementCollector(doc).OfClass(typeof(RebarBarType)).Cast<RebarBarType>().First(x => x.Name == "22M");
                        //tạo thép
                        Rebar secondVerticalRebar = Rebar.CreateFromCurves(doc, RebarStyle.Standard, kieuthep, null, null, elm, XYZ.BasisZ, vector, RebarHookOrientation.Left, RebarHookOrientation.Right, true, true);

                        transaction.Commit();
                    }

                }
            }
            return Result.Succeeded;
        }
        List<Element> SelectCeiling()
        {
            MassSelectionFilter_Beam selectionFilter = new MassSelectionFilter_Beam();
            List<Element> selectedElement = new List<Element>();
            try
            {
                // Tạo một Selection object trống
                Selection selection = uidoc.Selection;
                selection.SetElementIds(new List<ElementId>());

                // Sử dụng PickObjects để chọn các đối tượng
                IList<Reference> references = uidoc.Selection.PickObjects(ObjectType.Element, selectionFilter, "Select by rectangle");

                // Lấy danh sách các đối tượng đã chọn
                List<ElementId> selectedElementIds = references.Select(r => r.ElementId).ToList();
                selection.SetElementIds(selectedElementIds);
                for (int ii = 0; ii < selectedElementIds.Count; ii++)
                {
                    selectedElement.Add(doc.GetElement(selectedElementIds[ii]));
                }
                return selectedElement;
            }
            catch (Autodesk.Revit.Exceptions.OperationCanceledException)
            {
                // Nếu người dùng hủy bỏ thao tác chọn, không cần xử lý gì
            }

            return selectedElement;

        }
        private List<List<Curve>> Getlstcurve(Element elm, ElementId id)
        {
            double a = Convert.ToDouble(30) / 304.8 ;
            List<List<Curve>> curves = new List<List<Curve>>();
            Element column = doc.GetElement(id);
            BoundingBoxXYZ bbox = column.get_BoundingBox(null);

            double b = Convert.ToDouble(Class_Bien.B) / 304.8 * 1000;
            double sothanhthep = Convert.ToDouble(Class_Bien.nthepdocduoi);
            double sokc = Math.Round(sothanhthep / 2 - 1);
            double kc = (b - 2 * a) / sokc;

            double bd = 0;
            for (int i = 0; i < sokc; i++)
            {
                List<Curve> c = new List<Curve>();
                XYZ startPoint = new XYZ(bbox.Min.X + a, bbox.Min.Y + a + bd, bbox.Min.Z); // Điểm bắt đầu
                XYZ endPoint = new XYZ(bbox.Min.X + a, bbox.Min.Y + a + bd, bbox.Max.Z);
                bd = bd + kc/**304.8*/;
                c.Add(Line.CreateBound(startPoint, endPoint));
                curves.Add(c);
            }

            return curves;
        }


    }

    public class MassSelectionFilter_Beam : ISelectionFilter
    {
        public bool AllowElement(Element element)
        {
            if (element.Category.Name.Contains("Structural Framing"))
            {
                return true;
            }
            return false;
        }

        public bool AllowReference(Reference reference, XYZ position)
        {
            return false;
        }
    }
}
