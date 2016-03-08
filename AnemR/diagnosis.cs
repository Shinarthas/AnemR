using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnemR
{
    public class Diagnosis
    {
        public string Title;
        public Dictionary<string, double[,]> Params = new Dictionary<string, double[,]>();

        public int[] getResult(string[] names,double[] vals, bool sex)
        {
            int index;
            if (sex) index = 1;
            else index = 0;
            int[] res=new int[names.Length];
            // 0- не соответствует
            // 1- соответсвтвует
            // 2- ни то ни то, скорее всего нет такого ключа
            for (int i = 0; i < res.Length; i++)
            {
                res[i] = 2;
            }
            string key;
            double l;
            double r;
            double val;
            for (int i = 0; i < names.Length; i++)
            {
                key = names[i];
                if (Params.ContainsKey(key))
                {
                    l = Params[key][index, 0];
                    r = Params[key][index, 1];
                    val = vals[i];
                    if (val >= l && val <= r)
                        res[i] = 1;
                    else
                        res[i] = 0;
                }
                else
                {
                    res[i] = 2;
                }
            }
            return res;
        }
    }

    public static class template
    {
        public static List<Diagnosis> templates = new List<Diagnosis>();

         static template()
        {
            Diagnosis Norma = new Diagnosis();
             Norma.Title = "Norma";
             Norma.Params["RBC"]=new double[,]{{4,5},{3.5,4.7}};
             Norma.Params["MCV"] = new double[,] { { 75, 100 }, { 75, 100 } };
             Norma.Params["PLT"] = new double[,] { { 180, 320 }, { 180, 320 } };
             Norma.Params["WBC"] = new double[,] { { 4, 9 }, { 4, 9 } };
             Norma.Params["RET"] = new double[,] { { 0.24, 1.7 }, { 0.12, 2.05 } };
             Norma.Params["HGB"] = new double[,] { { 130, 170 }, { 120, 150 } };
             Norma.Params["MCH"] = new double[,] { { 27, 33 }, { 27, 33 } };
             Norma.Params["Hct"] = new double[,] { { 42, 50 }, { 38, 47 } };
             Norma.Params["CP"] = new double[,] { { 0.85, 1.05 }, { 0.85, 1.05 } };
             Norma.Params["COE"] = new double[,] { { 3, 10 }, { 5, 15 } };
             templates.Add(Norma);

             Diagnosis GDA = new Diagnosis();
             GDA.Title = "Железодефицитная анемия (ЖДА)";
             GDA.Params["RBC"] = new double[,] { { 0, 3.9999999 }, { 0, 3.4999999 } };
             GDA.Params["MCV"] = new double[,] { { 0, 69.9999999 }, { 0, 69.999999 } };
             GDA.Params["PLT"] = new double[,] { { 320.00000001, 10000 }, { 320.000001, 10000 } };
             GDA.Params["WBC"] = new double[,] { { 9.0000001, 10000 }, { 9.0000001, 10000 } };
             GDA.Params["RET"] = new double[,] { { 0, 0.239999999999 }, { 0, 0.119999999999 } };
             GDA.Params["HGB"] = new double[,] { { 0, 119.9999999 }, { 0, 109.999999999 } };
             GDA.Params["MCH"] = new double[,] { { 0, 23.999999999 }, { 0, 23.9999999999 } };
             GDA.Params["Hct"] = new double[,] { { 0, 39.99999999 }, { 0, 34.9999999999 } };
             GDA.Params["CP"] = new double[,] { { 0, 0.84999999999 }, { 0, 0.8499999999 } };
             GDA.Params["COE"] = new double[,] { { 15.00000001, 10000 }, { 20.0000001, 20000 } };
             templates.Add(GDA);

             Diagnosis SERP = new Diagnosis();
             SERP.Title = "Серповидноклеточная анемия";
             SERP.Params["RBC"] = new double[,] { { 0, 3.999999999 }, { 0, 3.49999999 } };
             SERP.Params["MCV"] = new double[,] { { 0, 69.99999999 }, { 0, 69.999999 } };
             SERP.Params["PLT"] = new double[,] { { 0, 179.9999999 }, { 0, 179.999999 } };
             SERP.Params["WBC"] = new double[,] { { 9.0000001, 10000 }, { 9.00000001, 10000 } };
             SERP.Params["RET"] = new double[,] { { 1.7000000001, 110 }, { 2.050000000001, 110 } };
             SERP.Params["HGB"] = new double[,] { { 0, 129.999999999 }, { 0, 119.999999999 } };
             SERP.Params["MCH"] = new double[,] { { 0, 23.999999999 }, { 0, 23.999999999 } };
             SERP.Params["Hct"] = new double[,] { { 0, 41.999999999 }, { 0, 41.9999999 } };
             SERP.Params["CP"] = new double[,] { { 0, 0.8499999999 }, { 0, 0.8499999999 } };
             SERP.Params["COE"] = new double[,] { { 10.0000001, 10000 }, { 15.00000001, 20000 } };
             templates.Add(SERP);

             Diagnosis B12 = new Diagnosis();
             B12.Title = "В12 дефицитная анемия";
             B12.Params["RBC"] = new double[,] { { 0, 3.999999999 }, { 0, 3.499999999 } };
             B12.Params["MCV"] = new double[,] { { 110.0000001, 100000 }, { 110.0000001, 100000 } };
             B12.Params["PLT"] = new double[,] { { 0, 179.99999999 }, { 0, 179.99999999 } };
             B12.Params["WBC"] = new double[,] { { 0, 3.99999999 }, { 0, 3.99999999 } };
             B12.Params["RET"] = new double[,] { { 0, 0.23999999999 }, { 0, 0.1199999999 } };
             B12.Params["HGB"] = new double[,] { { 0, 129.99999999 }, { 0, 129.99999999 } };
             B12.Params["MCH"] = new double[,] { { 400.000001, 40000 }, { 400.000001, 40000 } };
             B12.Params["Hct"] = new double[,] { { 42.0000001, 100 }, { 42.0000001, 100 } };
             B12.Params["CP"] = new double[,] { { 1.100000001, 1000 }, {1.1000001, 1000 } };
             B12.Params["COE"] = new double[,] { { 15.0000001, 10000 }, { 20.000001, 20000 } };
             templates.Add(B12);

             Diagnosis tela = new Diagnosis();
             tela.Title = "Талассемия";
             tela.Params["RBC"] = new double[,] { { 0, 3.999999999 }, { 0, 3.49999999999 } };
             tela.Params["MCV"] = new double[,] { { 0, 69.99999999 }, { 0, 69.999999999 } };
             tela.Params["PLT"] = new double[,] { { 0, 179.9999999 }, { 0, 179.9999999 } };
             tela.Params["WBC"] = new double[,] { { 0, 3.99999999 }, { 0, 3.99999999 } };
             tela.Params["RET"] = new double[,] { { 2.500000001, 3.99999999 }, { 3.00000001, 4.999999999 } };
             tela.Params["HGB"] = new double[,] { { 0, 129.9999999 }, { 0, 119.9999999 } };
             tela.Params["MCH"] = new double[,] { { 0, 299.99999999 }, { 0, 299.99999999 } };
             tela.Params["Hct"] = new double[,] { { 0, 419.999999999 }, { 0, 379.99999999 } };
             tela.Params["CP"] = new double[,] { { 0, 0.59999999 }, { 0, 0.5999999 } };
             tela.Params["COE"] = new double[,] { { 10.0000001, 1000 }, { 15.000001, 15000 } };
             templates.Add(tela);


             Diagnosis apl = new Diagnosis();
             apl.Title = "Апластическая анемия ";
             apl.Params["RBC"] = new double[,] { { 0, 3.999999999 }, { 0, 3.49999999999 } };
             apl.Params["MCV"] = new double[,] { { 0, 69.99999999 }, { 0, 69.999999999 } };
             apl.Params["PLT"] = new double[,] { { 0, 179.9999999 }, { 0, 179.9999999 } };
             apl.Params["WBC"] = new double[,] { { 0, 3.99999999 }, { 0, 3.99999999 } };
             apl.Params["RET"] = new double[,] { { 2.500000001, 3.99999999 }, { 3.00000001, 4.999999999 } };
             apl.Params["HGB"] = new double[,] { { 0, 69.999999 }, { 0, 69.999999 } };
             apl.Params["MCH"] = new double[,] { { 0, 299.99999999 }, { 0, 299.99999999 } };
             apl.Params["Hct"] = new double[,] { { 0, 419999999999 }, { 0, 37999999999 } };
             apl.Params["CP"] = new double[,] { { 0, 0.59999999 }, { 0, 0.5999999 } };
             apl.Params["COE"] = new double[,] { { 10.0000001, 1000 }, { 15.000001, 15000 } };
             templates.Add(apl);
        }
    }
}
