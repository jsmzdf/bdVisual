﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Data;
namespace AppBD.Clases
{   
    class Contrato
    {
        public Conexion con = new Conexion();
        public DataSet TABLA;
        public OleDbDataAdapter ORDEN;
        DataTable dt = new DataTable();
        public string cod = "";
        public string objeto = "";
        public string codigo="";
        public string ano="";
        public string ubicacion="";
        public string carpetasNO = "";
        public string sedno = "";
        public string numeFol = "";
        public string plazodias = "";
        public string contraVal="";
        public string interventr = "";
        

        OleDbCommand ORDENU;
        public void consultarC(string consulta)
        {                        
            con.conectar();
            ORDEN = new OleDbDataAdapter("SELECT * FROM [CONTRATO] WHERE CONTRATO_NO =@CLABUSCAR", con.CANAL);
            ORDEN.SelectCommand.Parameters.Add(new OleDbParameter("@CLABUSCAR", OleDbType.VarChar));
            ORDEN.SelectCommand.Parameters["@CLABUSCAR"].Value = consulta;
            dt = new DataTable();
            TABLA = new DataSet();
            ORDEN.Fill(TABLA);
            dt = TABLA.Tables[0];
            
            foreach (DataRow row in dt.Rows)
            {
                cod = Convert.ToString(row["CONTRATO_NO"]);
                codigo = Convert.ToString(row["CONTRATO_SECOP"]);
                ano = Convert.ToString(row["AÑO"]);
                ubicacion = Convert.ToString(row["UBICACIÓN"]);
                carpetasNO = Convert.ToString(row["CARPETAS_NO"]);
                sedno = Convert.ToString(row["SED_NO"]);
                numeFol = Convert.ToString(row["NO_FOLIOS"]);
                objeto = Convert.ToString(row["OBJETO"]);  
                plazodias = Convert.ToString(row["PLAZO_DIAS"]);
                contraVal= Convert.ToString(row["CONTRATO_VALOR"]);
                interventr = Convert.ToString(row["INTERVENTOR"]);
                
            }
           


        }
        public void updateC(string ubicacion,string numerocCarpetas,string numeroContrato,string numeroFolios)
        {
            con.conectar();
            string q = "update  [CONTRATO] set [UBICACIÓN] = @UBI, CARPETAS_NO = @CNO NO_FOLIOS=@NF" +
                " where CONTRATO_NO= @contratono" ;
            ORDENU = new OleDbCommand(q, con.CANAL);
            ORDENU.Parameters.Add(new OleDbParameter("@UBI", OleDbType.VarWChar));
            ORDENU.Parameters["@UBI"].Value = ubicacion;
            ORDENU.Parameters.Add(new OleDbParameter("@CNO", OleDbType.VarChar));
            ORDENU.Parameters["@CNO"].Value =numerocCarpetas ;
            ORDENU.Parameters.Add(new OleDbParameter("@NF", OleDbType.VarChar));
            ORDENU.Parameters["@NF"].Value = numeroFolios;
            ORDENU.Parameters.Add(new OleDbParameter("@contratono", OleDbType.VarChar));
            ORDENU.Parameters["@contratono"].Value = numeroContrato;
            

            ORDENU.Connection.Open();
            ORDENU.ExecuteNonQuery();
            ORDENU.Connection.Close();
        }
        //string con_no,string con_secop,int ano, string ubicacion,string caprtas,
        //string sed_no,string nolfoli,string obj,int plazoDias,double contratovalor,string interventor
        public void addC(ExtraerExcel ex)
        {
            Console.WriteLine(ex.cod);
            string q = "INSERT INTO [CONTRATO](CONTRATO_NO,CONTRATO_SECOP,[AÑO],[UBICACIÓN],CARPETAS_NO,SED_NO,NO_FOLIOS,OBJETO,PLAZO_DIAS,CONTRATO_VALOR,INTERVENTOR) " +
                "values(@CONTRATO_NO,@CONTRATO_SECOP,@ANO,@UBICACION,@CARPETAS_NO,@SED_NO,@NoFOLIO,@OBJETO,@PLAZO_DIAS,@CONTRATO_VALOR,@INTERVENTOR)";
            try {
                ORDENU = new OleDbCommand(q, con.CANAL);
                ORDENU.Parameters.Add(new OleDbParameter("@CONTRATO_NO", OleDbType.VarChar));
                ORDENU.Parameters["@CONTRATO_NO"].Value = ex.cod;
                ORDENU.Parameters.Add(new OleDbParameter("@CONTRATO_SECOP", OleDbType.VarChar));
                ORDENU.Parameters["@CONTRATO_SECOP"].Value = ex.codigo;
                ORDENU.Parameters.Add(new OleDbParameter("@ANO", OleDbType.Numeric));
                ORDENU.Parameters["@ANO"].Value = ex.ano;
                ORDENU.Parameters.Add(new OleDbParameter("@UBICACION", OleDbType.VarChar));
                ORDENU.Parameters["@UBICACION"].Value = ex.ubicacion;
                ORDENU.Parameters.Add(new OleDbParameter("@CARPETAS_NO", OleDbType.VarChar));
                ORDENU.Parameters["@CARPETAS_NO"].Value = ex.carpetasNO;
                ORDENU.Parameters.Add(new OleDbParameter("@SED_NO", OleDbType.VarChar));
                ORDENU.Parameters["@SED_NO"].Value = ex.sedno;
                ORDENU.Parameters.Add(new OleDbParameter("@NoFOLIO", OleDbType.VarChar));
                ORDENU.Parameters["@NoFOLIO"].Value = ex.numeFol;
                ORDENU.Parameters.Add(new OleDbParameter("@OBJETO", OleDbType.VarChar));
                ORDENU.Parameters["@OBJETO"].Value = ex.objeto;
                ORDENU.Parameters.Add(new OleDbParameter("@PLAZO_DIAS", OleDbType.Numeric));
                ORDENU.Parameters["@PLAZO_DIAS"].Value = ex.plazodias;
                ORDENU.Parameters.Add(new OleDbParameter("@CONTRATO_VALOR", OleDbType.Double));
                ORDENU.Parameters["@CONTRATO_VALOR"].Value = ex.contraVal;
                ORDENU.Parameters.Add(new OleDbParameter("@INTERVENTOR", OleDbType.VarChar));
                ORDENU.Parameters["@INTERVENTOR"].Value = ex.interventr;
                ORDENU.Connection.Open();
                ORDENU.ExecuteNonQuery();
                ORDENU.Connection.Close();
            }
            catch (Exception) { 
            }
            
        }
    }
}
